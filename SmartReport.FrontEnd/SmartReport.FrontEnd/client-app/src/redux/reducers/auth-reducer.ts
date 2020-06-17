import { Action } from './../../types/common-types';
import { AuthState } from './../../types/state-types';
import {
    LOGIN_SUCCESS,
    REGISTRATION_SUCCESS,
    LOGOUT_SUCCESS
  } from '../../constants/auth-constants';
import { AppUser } from '../../types/common-types';

const initialState: AuthState = {
  isAuthenticated: false,
  user: {
    access_token: localStorage.getItem('access_token')
  } as AppUser
};

const authReducer = (state: AuthState = initialState, action: Action): AuthState => {
    switch (action.type) {
    case LOGIN_SUCCESS:
      const token = action.payload.user.access_token;
      if (token) {
        localStorage.setItem('access_token', token as string);
      }
      return {
        ...state,
        user: {
            ...action.payload.user
        } as AppUser,
        isAuthenticated: true,
      };
    case REGISTRATION_SUCCESS:
      return {
        ...state,
      };
    case LOGOUT_SUCCESS:
      localStorage.removeItem('access_token');
      return {
        ...state,
        user: null,
        isAuthenticated: false,
      };
    default:
      const access_token = localStorage.getItem('access_token');
      const isAuth = (access_token) ? (true) : (false);
      let user = null;
      if(isAuth){
        user = {access_token} as AppUser
      }
      return {
        ...state,
        user,
        isAuthenticated: isAuth
      };
  }
}

export default authReducer;