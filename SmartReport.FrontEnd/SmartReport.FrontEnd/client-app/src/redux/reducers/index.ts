import { combineReducers } from 'redux';
import authReducer from './auth-reducer';
import { intlReducer } from 'react-intl-redux'
import localesReducer from './locales-reducer';

const rootReducer = combineReducers({
    auth: authReducer,
    intl: intlReducer,
    locales: localesReducer,
});

export default rootReducer;