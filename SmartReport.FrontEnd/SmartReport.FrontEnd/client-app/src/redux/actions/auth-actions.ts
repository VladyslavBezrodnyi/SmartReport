import { REGISTRATION_SUCCESS } from './../../constants/auth-constants';
import { LoginDTO, RegistrationDTO } from '../../types/DTO-types';
import { environment } from './../../environment/environment';
import {
  LOGIN_SUCCESS,
  LOGOUT_SUCCESS,
} from '../../constants/auth-constants';
import axios from 'axios';
import { message } from 'antd';
import { AppUser } from '../../types/common-types';
import tokenService from '../../services/token-service';

export const login = (loginDTO: LoginDTO) => (despatch: any) => {
  axios.post(environment.API_URL + '/api/Account/login', {
    ...loginDTO
  })
    .then((response: any) => {
      if (response.data.accessToken) {
        localStorage.setItem('access_token', response.data.accessToken as string);
      }
      despatch({
        type: LOGIN_SUCCESS,
        payload: {
          user: tokenService(response.data.accessToken)
        }
      });
    })
    .catch((err: any) => {
      console.log(err);
      message.error('Error!');
    });
};

export const logout = () => (despatch: any): void => {
  localStorage.removeItem('access_token');
  despatch({
    type: LOGOUT_SUCCESS,
    payload: {}
  });
};

export const register = (registerDTO: RegistrationDTO) => (despatch: any): void => {
  axios.post(environment.API_URL + '/api/Account/register', {
    ...registerDTO
  }).then((response: any) => {
    message.success("Success!")
    despatch({
      type: REGISTRATION_SUCCESS,
      payload: {}
    });
  }).then((err: any) => {
    message.warning("Warning!");
    console.log(err);
  })
};