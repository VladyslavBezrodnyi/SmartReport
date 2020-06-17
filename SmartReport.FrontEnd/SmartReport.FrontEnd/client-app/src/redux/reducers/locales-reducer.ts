import { Action } from './../../types/common-types';
import { UPDATE_LOCALES } from './../../constants/intl-constants';

const initialState = {...require('../../locales.json')}

const localesReducer = (state: any = initialState, action: Action) => {
    switch (action.type) {
        case UPDATE_LOCALES:
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
export default localesReducer;