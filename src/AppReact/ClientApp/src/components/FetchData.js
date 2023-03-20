import React, { Component } from 'react';

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
    this.state = { 
      noAuthResult: "",
      withAuthResult: ""
    };
  }

  componentDidMount() {

  //   fetch("weatherforecast")
  //   .then(response => response.json())
  //   .then(data => {
  //   console.log("data: "+data);
  // })


    // this.populateWeatherData();
    this.fetchNoAuth();
    this.fetchWithAuth();
    // this.fetchData();
  }

  static renderForecastsTable(forecasts) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Date</th>
            <th>Temp. (C)</th>
            <th>Temp. (F)</th>
            <th>Summary</th>
          </tr>
        </thead>
        <tbody>
          {forecasts.map(forecast =>
            <tr key={forecast.date}>
              <td>{forecast.date}</td>
              <td>{forecast.temperatureC}</td>
              <td>{forecast.temperatureF}</td>
              <td>{forecast.summary}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let {noAuthResult, withAuthResult} = this.state;


  return (
    <div>
      <h3 id="tabelLabel" >Result from API without auth:</h3>
      {noAuthResult}
      <br />
      <br />
      <br />
      <br />
      <h3 id="tabelLabel" >Result from API WITH auth:</h3>
      {withAuthResult}
    </div>
  );
  }

  async fetchNoAuth(){
    await fetch('authtest/no-auth', {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      },
    }).then(async res => {
      this.setState({noAuthResult: await res.text() });
    });
  };

  async fetchWithAuth(){
    const token = localStorage.getItem('token');

    // const response = await fetch('weatherforecast', {
    await fetch('authtest/with-auth', {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      },
    }).then(async res => {
      console.log(res)
      if(res.status == 401){
        this.setState({ withAuthResult: res.statusText });
        return;
      }

      this.setState({ withAuthResult: await res.text() });
    });

    // if (response.status >= 400) {
    //   console.log(response);
    //   return;
    // }

    // console.log(response.json())

    // if (response.ok) {

    // // const data = await response.json();

    // //   localStorage.setItem('token', data.accessToken);
    // //   let url = `https://localhost:9002/login-callback?token=${data.accessToken}`;
    // //   window.location.href = url;
    // // } else {
    // //   alert(data.message);
    // }
  };

  async fetchData(url){
    this.setState({ isLoading: true })
    fetch(url)
    .then(res => {
      if(res.status >= 400) {
          throw new Error("Server responds with error! Try login and try again!");
      }
      return res.json();
    })
    .then(users => {
      this.setState({
          users,
          isLoading: false
      })
    },
    err => {
      alert(err)
      this.setState({
          err,
          isLoading: false
      })
    })
  }
}
