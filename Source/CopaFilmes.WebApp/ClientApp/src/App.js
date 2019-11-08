import React, { Component } from 'react';
import { Route } from 'react-router';
import { Home } from './components/Home';
import { Result } from './components/Result';

export default class App extends Component {
  displayName = App.name

  render() {
    return (
      <div>
        <Route exact path='/' component={Home} />
        <Route path='/Result' component={Result} />
      </div>
    );
  }
}
