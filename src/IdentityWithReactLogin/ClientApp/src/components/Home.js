import React, { Component } from 'react';
import Login from './Login';
import Logout from './Logout';

export class Home extends Component {
  static displayName = Home.name;

  render() {
    if(!localStorage.getItem('token')){
      return (
        <Login></Login>
      );
    }else{
      return (
        <Logout></Logout>
      );
    }
  }
}
