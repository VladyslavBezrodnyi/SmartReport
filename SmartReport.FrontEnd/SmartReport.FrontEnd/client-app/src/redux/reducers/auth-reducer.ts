import { Action } from './../../types/common-types';
import { AuthState } from './../../types/state-types';
import {
    LOGIN_SUCCESS,
    REGISTRATION_SUCCESS,
    LOGOUT_SUCCESS
  } from '../../constants/auth-constants';
import { AppUser } from '../../types/common-types';
import tokenService from '../../services/token-service';
import {RECIEVE_WEBSOCKET_MESSAGE} from '../../constants/socket-constants';

const initialState: AuthState = {
  isAuthenticated: false,
  work: null,
  user: null
};

const authReducer = (state: AuthState = initialState, action: Action): AuthState => {
    switch (action.type) {
    case LOGIN_SUCCESS:
      return {
        ...state,
        user: {
            ...action.payload.user
        } as AppUser,
        isAuthenticated: true,
      };
    case LOGOUT_SUCCESS:
      return {
        ...state,
        user: null,
        isAuthenticated: false,
      };
    case RECIEVE_WEBSOCKET_MESSAGE:
      return{
        ...state,
        work: action.payload.message
      }
    default:
      const access_token = localStorage.getItem('access_token');
      let isAuth = (access_token) ? (true) : (false);
      let user = (access_token) ? (tokenService(access_token)) : (null);
      return {
        ...state,
        user,
        isAuthenticated: isAuth
      };
  }
}

export default authReducer;