import { GET_USERS } from './../../constants/account-constants';
import { environment } from './../../environment/environment';
import axios from 'axios';
import { message } from 'antd';

export const getUsers = () => (despatch: any) => {
    axios.get(environment.API_URL + '/api/Account/users')
        .then((response: any) => {
            console.log(response.data)
            despatch({
                type: GET_USERS,
                payload: {
                    usersList: response.data
                }
            });
        })
        .catch((err: any) => {
            console.log(err);
            message.error('Error!');
        });
};

export const deleteUser = () => (despatch: any) => {

};