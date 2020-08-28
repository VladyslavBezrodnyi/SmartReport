import { combineReducers } from 'redux';
import authReducer from './auth-reducer';
import { intlReducer } from 'react-intl-redux'
import localesReducer from './locales-reducer';
import taskReducer from './task-reducer';
import accountReducer from './account-reducer'
import reportReducer from './report-reducer'

const rootReducer = combineReducers({
    auth: authReducer,
    intl: intlReducer,
    locales: localesReducer,
    task:taskReducer,
    acc: accountReducer,
    report: reportReducer,
});

export default rootReducer;