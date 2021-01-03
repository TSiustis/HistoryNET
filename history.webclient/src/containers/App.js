import './App.css';
import Layout from '../components/Layout/Layout';
import React, {Component} from 'react';
import Home from '../components/Home/Home';
import {BrowserRouter, Switch, Route} from 'react-router-dom';
import NotFound from '../components/ErrorPages/NotFound/NotFound';
import InternalError from '../components/ErrorPages/InternalError/InternalError';

import asyncComponent from '../hoc/AsyncComponent/AsyncComponent';
const AsyncEventList = asyncComponent(() => {
  return import('../components/EventList/EventList');
});
class App extends Component {
  render() {
    return (
      <BrowserRouter>
      <Layout>
      <Switch>
        <Route path = "/" exact component = {Home}/>
        <Route path="/event-list" component={AsyncEventList} />
        <Route path = "*" component = {NotFound} />
        <Route path="/500" component={InternalError} />
        </Switch>  
      </Layout>
      </BrowserRouter>
    );
  }
}

export default App;
