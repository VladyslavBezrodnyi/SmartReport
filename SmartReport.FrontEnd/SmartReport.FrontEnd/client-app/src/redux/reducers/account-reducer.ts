import { UserDTO } from './../../types/DTO-types';
import { AccountState } from './../../types/state-types';
import { GET_USERS } from './../../constants/account-constants';
import { Action } from './../../types/common-types';
import { REGISTRATION_SUCCESS } from '../../constants/auth-constants';
import { RECIEVE_WEBSOCKET_ADMIN_MESSAGE } from '../../constants/socket-constants';

const initialState = {
    usersList: Array<UserDTO>()
} as AccountState

const accountReducer = (state: AccountState = initialState, action: Action): AccountState => {
    switch (action.type) {
        case GET_USERS:
            console.log(action.payload.usersList);
            return {
                ...state,
                usersList: action.payload.usersList
            }
        case REGISTRATION_SUCCESS:
            const response = action.payload?.userResponse;
            const users = state.usersList;
            if (response) {
                users.push(response as UserDTO)
            }
            return {
                ...state,
                usersList: users
            }
        case RECIEVE_WEBSOCKET_ADMIN_MESSAGE:
            let message = action.payload.message;
            let user = state.usersList.find(u => u.id == message.userId);
            if(user){
                user.isWork = message.isWork;
            }
            return {
                ...state,
                usersList: state.usersList
            }
        default:
            return {
                ...state
            }
    }
}
export default accountReducer;