import './App.css';
import Layout from '../components/Layout/Layout';
import React, {Component} from 'react';
import Home from '../components/Home/Home';
import {BrowserRouter, Switch, Route} from 'react-router-dom';
import NotFound from '../components/ErrorPages/NotFound/NotFound';
import InternalError from '../components/ErrorPages/InternalError/InternalError';

import asyncComponent from '../hoc/AsyncComponent/AsyncComponent';
const AsyncEventList = asyncComponent(() => {
  return import('../components/DataComponents/EventList');
});
const AsyncDeathList = asyncComponent(() => {
  return import('../components/DataComponents/DeathList');
});

const AsyncBirthList = asyncComponent(() => {
  return import('../components/DataComponents/BirthList');
});
class App extends Component {
  render() {
    return (
      <BrowserRouter>
      <Layout>
      <Switch>
        <Route path = "/" exact component = {Home}/>
        <Route path="/birth-list" component={AsyncBirthList} />
        <Route path="/death-list" component={AsyncDeathList} />
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
