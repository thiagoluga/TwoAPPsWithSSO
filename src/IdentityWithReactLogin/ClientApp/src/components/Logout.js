import React, { useState } from 'react';
import { Form, Button, Alert } from 'react-bootstrap';

const Logout = () => {
  const [error, setError] = useState('');

  const handleLogout = async (e) => {
    e.preventDefault();
    setError('');

    localStorage.removeItem('token');
    window.location.reload();
  };

  return (
    <div className="container mt-5">
      <div className="row justify-content-center">
        <div className="col-md-6">
          <Form onSubmit={handleLogout}>
            <Button variant="primary" type="submit" className="mt-3">Logout</Button>
          </Form>
        </div>
      </div>
    </div>
  );
};

export default Logout;
