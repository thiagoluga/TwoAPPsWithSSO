import React, { useState } from 'react';
import { Form, Button, Alert } from 'react-bootstrap';

const Login = () => {
  const [email, setemail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');

  const handleLogin = async (e) => {
    e.preventDefault();
    setError('');

    if (!email || !password) {
      setError('Please enter both email and password.');
      return;
    }

    const response = await fetch('http://localhost:4001/api/accounts/login', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({ email, password })
    });

    if (response.status >= 400) {
      setError('Invalid email or password. Please try again.');
      return;
    }

    const data = await response.json();

    if (response.ok) {
      localStorage.setItem('token', data.accessToken);
      let url = `https://localhost:9002/login-callback?token=${data.accessToken}`;
      window.location.href = url;
    } else {
      setError(data.message);
    }
  };

  return (
    <div className="container mt-5">
      <div className="row justify-content-center">
        <div className="col-md-6">
          <Form onSubmit={handleLogin}>
            <Form.Group controlId="formBasicemail">
              <Form.Label>Email:</Form.Label>
              <Form.Control type="text" value={email} onChange={(e) => setemail(e.target.value)} placeholder="Enter email" />
            </Form.Group>
            <Form.Group controlId="formBasicPassword">
              <Form.Label>Password:</Form.Label>
              <Form.Control type="password" value={password} onChange={(e) => setPassword(e.target.value)} placeholder="Password" />
            </Form.Group>
            {error && <Alert variant="danger">{error}</Alert>}
            <Button variant="primary" type="submit" className="mt-3">Login</Button>
          </Form>
        </div>
      </div>
    </div>
  );
};

export default Login;
