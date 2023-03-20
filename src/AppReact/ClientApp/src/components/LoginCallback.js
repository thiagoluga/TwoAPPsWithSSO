import React, { Component } from 'react';

class LoginCallback extends Component {
  constructor(props) {
    super(props);
    this.state = { 
      token: ""
    };
  }

  componentDidMount() {
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);
    const tokenFromUrl = urlParams.get('token');
    this.setState({ token: tokenFromUrl })
    if (tokenFromUrl) {
      localStorage.setItem('token', tokenFromUrl);
    }
  }

  render() {
    return (
      <div>
        <h3>Token:</h3> <p>{this.state.token}</p>
        <br /><br /><br /><br />
        <h3>Now your token has been stored in the browser.</h3>
        <h3>You are able to fetch data that needs authentication</h3>
      </div>
    );
  }
}

export default LoginCallback;
