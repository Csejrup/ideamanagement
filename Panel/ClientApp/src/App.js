import React, { Component } from 'react';
import { Route, Switch, useLocation } from 'react-router-dom';
import { Layout } from './components/Layout';
import axios from 'axios';

import AddIdea from './components/AddIdea';
import NoMatch from './components/NoMatch';
import { Test } from './components/Test';
import Login from './components/Login';

import './App.scss';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Switch>
          <Route exact path='/' component={AddIdea} />
          <Route path="/test" component={Test}></Route>
          <Route path="/login" component={Login} />
          <Route path="*">
            <NoMatch></NoMatch>
          </Route>
        </Switch>
      </Layout>
    );
  }
}
