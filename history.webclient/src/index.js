import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './containers/App';
import repositoryReducer from './store/reducers/repositoryReducer';
import errorHandlerReducer from './store/reducers/errorHandlerReducer';
import { Provider } from 'react-redux';
import { createStore, applyMiddleware, combineReducers } from 'redux';
import thunk from 'redux-thunk';
import 'bootstrap/dist/css/bootstrap.css';

const rootReducers = combineReducers({
  repository: repositoryReducer,
  errorHandler: errorHandlerReducer
})
const store = createStore(rootReducers, applyMiddleware(thunk));
ReactDOM.render(<Provider store={store}><App /></Provider>, document.getElementById('root'));
// registerServiceWorker();