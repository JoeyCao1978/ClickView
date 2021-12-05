import React, { useEffect, Fragment, useContext } from 'react';
import { Container } from 'semantic-ui-react';
import NavBar from '../../features/nav/NavBar';
import PlaylistDashboard from '../../features/playlists/dashboard/PlaylistDashboard';
import LoadingComponent from './LoadingComponent';
import {observer} from 'mobx-react-lite';
import PlaylistStore from '../stores/playlistStore';

const App = () => {
  const playlistStore = useContext(PlaylistStore)

  useEffect(() => {
    playlistStore.loadPlaylists();
  }, [playlistStore]);

  if (playlistStore.loadingInitial) return <LoadingComponent content='Loading playlists' />

  return (
    <Fragment>
      <NavBar />
      <Container style={{ marginTop: '7em' }}>
        <PlaylistDashboard />
      </Container>
    </Fragment>
  );
};

export default observer(App);
