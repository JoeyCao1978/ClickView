import React, { useContext } from 'react';
import { Menu, Container, Button } from 'semantic-ui-react';
import PlaylistStore from '../../app/stores/playlistStore';
import { observer } from 'mobx-react-lite';

const NavBar: React.FC = () => {
  const playlistStore = useContext(PlaylistStore);
  return (
    <Menu fixed='top' inverted>
      <Container>
        <Menu.Item header>
            <img src="/assets/logo.png" alt="logo" style={{marginRight: 10}}/>
            Playlists
        </Menu.Item>
        <Menu.Item>
            <Button onClick={playlistStore.openCreateForm} positive content='Create Playlist' />
        </Menu.Item>
      </Container>
    </Menu>
  );
};

export default observer(NavBar);
