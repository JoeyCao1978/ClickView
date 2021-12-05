import React, { useContext } from 'react';
import { Grid } from 'semantic-ui-react';
import Playlist from './Playlist';
import { observer } from 'mobx-react-lite';
import PlaylistStore from '../../../app/stores/playlistStore';

const PlaylistDashboard: React.FC = () => {
  const playlistStore = useContext(PlaylistStore);
  const {editMode, selectedPlaylist} = playlistStore;
  return (
    <Grid>
      <Grid.Column width={10}>
        {/* <Video /> */}
      </Grid.Column>
      <Grid.Column width={6}>
        <Playlist/>
      </Grid.Column>
    </Grid>
  );
};

export default observer(PlaylistDashboard);
