import { UserDTO } from './../../types/DTO-types';
import { AccountState } from './../../types/state-types';
import { GET_USERS } from './../../constants/account-constants';
import { Action } from './../../types/common-types';
import { REGISTRATION_SUCCESS } from '../../constants/auth-constants';

const initialState = {
    usersList: Array<UserDTO>()
} as AccountState

const accountReducer = (state: AccountState = initialState, action: Action): AccountState => {
    switch (action.type) {
        case GET_USERS:
            return {
                ...state,
                usersList: action.payload.usersList
            }
        case REGISTRATION_SUCCESS:
            const response = action.payload?.userResponse;
            const users = state.usersList;
            if(response) {
                users.push(response as UserDTO)
            }
            return {
                ...state,
                usersList: users
            }
        default:
            return {
                ...state
            }
    }
}
export default accountReducer;