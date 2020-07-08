import { createStore, applyMiddleware } from 'redux';
import thunk from 'redux-thunk';
import rootReducer from './reducers';
import { environment } from '../environment/environment';
import createSocketMiddleware from './middlewares/signalRMiddleware';
const initialState = {};

const store = createStore(
    rootReducer,
    initialState,
    applyMiddleware(thunk, thunk, createSocketMiddleware(`${environment.API_URL}/notification`))
);

export default store;