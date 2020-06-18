import { CREATE_TASK } from './../../constants/task-constants';
import { Action } from './../../types/common-types';

const initialState = {}

const taskReducer = (state: any = initialState, action: Action) => {
    switch (action.type) {
        case CREATE_TASK:
            return {
                ...state,
                ...action.payload,
            }
        default:
            return {
                ...state
            }
    }
}
export default taskReducer;