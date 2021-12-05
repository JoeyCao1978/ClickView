import React, { useContext } from 'react';
import { Item, Button, Label, Segment } from 'semantic-ui-react';
import { observer } from 'mobx-react-lite';
import PlaylistStore from '../../../app/stores/playlistStore';

const Playlist: React.FC = () => {
  const playlistStore = useContext(PlaylistStore);
  const {playlistsByDate, deletePlaylist, submitting, target} = playlistStore;
  return (
    <Segment clearing>
      <Item.Group divided>
        {playlistsByDate.map(playlist => (
          <Item key={playlist.id}>
            <Item.Content>
              <Item.Header as='a'>{playlist.name}</Item.Header>
              <Item.Meta>{playlist.dateCreated}</Item.Meta>
              <Item.Description>
                <div>{playlist.description}</div>

                  {playlist.videoIds.split(',').map((video: any) => (
                    <div>{video} </div>
                  ))}

                
              </Item.Description>
              <Item.Extra>
                <Button
                  name={playlist.name}
                  loading={target === playlist.name && submitting}
                  onClick={(e) => deletePlaylist(e, playlist.name)}
                  floated='right'
                  content='Delete'
                  color='red'
                />
              </Item.Extra>
            </Item.Content>
          </Item>
        ))}
      </Item.Group>
    </Segment>
  );
};

export default observer(Playlist);
