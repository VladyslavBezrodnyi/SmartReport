import { TaskDTO } from './../../types/DTO-types';
import { TaskState } from './../../types/state-types';
import { CREATE_TASK, GET_TASKS, GET_USER_TASKS } from './../../constants/task-constants';
import { Action } from './../../types/common-types';

const initialState = {
    tasksList: Array<TaskDTO>(),
    userTasksList: Array<TaskDTO>()
} as TaskState

const taskReducer = (state: any = initialState, action: Action) => {
    switch (action.type) {
        case CREATE_TASK:
            const response = action.payload?.taskResponse;
            const tasks = state.tasksList;
            if(response) {
                tasks.push(response as TaskDTO)
            }
            return {
                ...state,
                tasksList: tasks
            }
        case GET_TASKS:
            return {
                ...state,
                tasksList: action.payload.tasksList
            }
        case GET_USER_TASKS:
            return {
                ...state,
                userTasksList: action.payload.userTasksList
            }
        default:
            return {
                ...state
            }
    }
}
export default taskReducer;