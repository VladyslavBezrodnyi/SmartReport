import "antd/dist/antd.min.css";
import { Action } from '../../types/common-types';
import openNotification from '../../services/SnackbarService';
import createWebsocket from '../../services/createWebsocket';
import { HubConnection } from '@microsoft/signalr';
import { LOGIN_SUCCESS, LOGOUT_SUCCESS } from "../../constants/auth-constants";
import { message } from "antd";
import { VisitDateDTO } from "../../types/DTO-types";
import { RECIEVE_WEBSOCKET_MESSAGE } from '../../constants/socket-constants'
import axios from "axios";
import { environment } from "../../environment/environment";

const startSignalRConnection = async (connection: HubConnection, store: any) => connection.start()
  .then(() => {
    console.info('SignalR Connected');

    connection.on('ServerNotify', (message: VisitDateDTO) => {
      // openNotification((message.isWork) ? ("Hi!") : ("Goodbye!"));
      store.dispatch({
        type: RECIEVE_WEBSOCKET_MESSAGE,
        payload: {
          message
        }
      });
    });
    axios.get(environment.API_URL + '/api/Account/workCheck', {
      headers: { "Authorization": `Bearer ${localStorage.getItem('access_token')}` }
    })
      .then((response: any) => {
        console.log(response);
      })
      .catch((err: any) => {
        console.log(err);
      });
  })
  .catch((err: any) => {
    console.error('SignalR Connection Error: ', err);
    message.error(err.toString());
    localStorage.removeItem('access_token');
    store.dispatch({
      type: LOGOUT_SUCCESS,
      payload: {}
    });
  });

const createSocketMiddleware = (url: string) => {
  let socket: HubConnection;

  return (store: any) => {
    const access_token = localStorage.getItem('access_token');
    if (access_token) {

      socket = createWebsocket(url, access_token)
      startSignalRConnection(socket, store);
    }

    return (next: any) => (action: Action) => {
      switch (action.type) {
        case LOGIN_SUCCESS: {
          if (action.payload.user) {
            const access_token = action.payload.user.access_token
            if (access_token) {
              localStorage.setItem('access_token', access_token as string);
              socket = createWebsocket(url, access_token);
              startSignalRConnection(socket, store);
            }
          }
          break;
        }
        case LOGOUT_SUCCESS:
          socket.stop();
      }
      return next(action);
    }
  }
}
export default createSocketMiddleware;