import { LoginDTO } from './../../types/account-types';
import { environment } from './../../environment/environment';
import {
  LOGIN_SUCCESS,
  LOGOUT_SUCCESS,
} from '../../constants/auth-constants';
import axios from 'axios';
import { message } from 'antd';
import jwt_decoded from 'jwt-decode'
import { AppUser } from '../../types/common-types';

export const login = (loginDTO: LoginDTO) => (despatch: any) => {
  axios.post(environment.API_URL + '/api/account/login', {
    ...loginDTO
  })
    .then((response: any) => {
      console.log(response);
      debugger
      const tokenPayload: { [id: string]: string; } = jwt_decoded<{ [id: string]: string; }>(response.data.accessToken);
      console.log(tokenPayload);
      despatch({
        type: LOGIN_SUCCESS,
        payload: {
          user: {
            id: tokenPayload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"],
            roles: tokenPayload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"],
            userName: tokenPayload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
            access_token: response.data.accessToken
          } as AppUser
        }
      });
    })
    .catch((err: any) => {
      console.log(err);
      message.error('Error!');
    });
};

export const logout = () => (despatch: any): void => {
  despatch({
    type: LOGOUT_SUCCESS,
    payload: {}
  });
};

export const registration = () => (despatch: any): void => {
  despatch({
    type: LOGOUT_SUCCESS,
    payload: {}
  });
};