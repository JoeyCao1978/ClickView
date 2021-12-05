import { observable, action, computed, configure, runInAction } from 'mobx';
import { createContext, SyntheticEvent } from 'react';
import { IPlaylist } from '../models/playlist';
import agent from '../api/agent';

configure({enforceActions: 'always'});

class PlaylistStore {
  @observable playlistRegistry = new Map();
  @observable playlists: IPlaylist[] = [];
  @observable selectedPlaylist: IPlaylist | undefined;
  @observable loadingInitial = false;
  @observable editMode = false;
  @observable submitting = false;
  @observable target = '';

  @computed get playlistsByDate() {
    return Array.from(this.playlistRegistry.values()).sort(
      (a, b) => Date.parse(a.dateCreated) - Date.parse(b.dateCreated)
    );
  }

  @action loadPlaylists = async () => {
    this.loadingInitial = true;
    try {
      const playlists = await agent.Playlists.list();
      runInAction('loading playlists', () => {
        playlists.forEach(playlist => {
          playlist.dateCreated = playlist.dateCreated.split('.')[0];
          this.playlistRegistry.set(playlist.id, playlist);
        });
        this.loadingInitial = false;
      })

    } catch (error) {
      runInAction('load playlists error', () => {
        this.loadingInitial = false;
      })
    }
  };

  @action createPlaylist = async (playlist: IPlaylist) => {
    this.submitting = true;
    try {
      await agent.Playlists.create(playlist);
      runInAction('create playlist', () => {
        this.playlistRegistry.set(playlist.id, playlist);
        this.editMode = false;
        this.submitting = false;
      })
    } catch (error) {
      runInAction('create playlist error', () => {
        this.submitting = false;
      })
      console.log(error);
    }
  };

  @action deletePlaylist = async (event: SyntheticEvent<HTMLButtonElement>, id: string) => {
    this.submitting = true;
    this.target = event.currentTarget.name;
    try {
      await agent.Playlists.delete(id);
      runInAction('deleting playlist', () => {
        this.playlistRegistry.delete(id);
        this.submitting = false;
        this.target = '';
      })
    } catch (error) {
      runInAction('delete playlist error', () => {
        this.submitting = false;
        this.target = '';
      })
      console.log(error);
    }
  }

  @action openCreateForm = () => {
    this.editMode = true;
    this.selectedPlaylist = undefined;
  };

  @action openEditForm = (id: string) => {
    this.selectedPlaylist = this.playlistRegistry.get(id);
    this.editMode = true;
  }

  @action cancelSelectedPlaylist = () => {
    this.selectedPlaylist = undefined;
  }

  @action cancelFormOpen = () => {
    this.editMode = false;
  }

  @action selectPlaylist = (id: string) => {
    this.selectedPlaylist = this.playlistRegistry.get(id);
    this.editMode = false;
  };
}

export default createContext(new PlaylistStore());
