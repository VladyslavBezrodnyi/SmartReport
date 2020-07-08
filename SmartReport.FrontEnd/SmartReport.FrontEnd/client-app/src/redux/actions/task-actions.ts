import {
    CREATE_TASK,
    GET_TASKS,
    GET_USER_TASKS
} from './../../constants/task-constants';
import { message } from 'antd';
import { TaskDTO, UserTaskDTO } from './../../types/DTO-types';
import axios from 'axios';
import { environment } from './../../environment/environment';

export const getTasks = () => (despatch: any) => {
    axios.get(environment.API_URL + '/api/Task/GetAll').then((response: any) => {
        despatch({
            type: GET_TASKS,
            payload: {
                tasksList: response.data
            }
        });
    })
};

export const createTask = (taskDTO: TaskDTO) => (despatch: any) => {
    axios.put(environment.API_URL + '/api/Task/create', {
        ...taskDTO
    }).then((response: any) => {
        message.success("Success!")
        despatch({
            type: CREATE_TASK,
            payload: {
                taskResponse: {
                    ...response.data
                }
            }
        });
    })
}

export const deleteTask = () => (despatch: any) => {

};

export const assignTask = (userTaskDTO: UserTaskDTO) => (despatch: any) => {
    axios.post(environment.API_URL + '/api/Task/CreateTaskForUser', {
        ...userTaskDTO
    }).then((response: any) => {
        message.success("Success!")
    }).catch((err: any) => {
        message.warning("Warning!");
        console.log(err);
    })
};

export const getUserTasks = () => (despatch: any) => {
    axios.get(environment.API_URL + '/api/Task/GetMissedTasks', {
        headers: { "Authorization": `Bearer ${localStorage.getItem('access_token')}` }
    }).then((response: any) => {
        despatch({
            type: GET_USER_TASKS,
            payload: {
                userTasksList: response.data
            }
        });
    }).catch((err: any) => {
        message.warning("Warning!");
        console.log(err);
    })
};