import React, { useState } from 'react';
import { TextField, Button, Grid,Card,CardContent } from '@mui/material';
import io from 'socket.io-client';
import Wrap from '../../components/widgets/wrapper';

const socket = io('http://localhost:8080'); // Replace with your server URL

const NavigationComponent = () => {
  const [lat, setlat] = useState('');
  const [lon, setlon] = useState('');
  const [trainNumber, settrainNumber] = useState('');
  const [errorMessages, setErrorMessages] = useState({
    lat: '',
    lon: '',
    trainNumber: '',
  });

  const validateFields = () => {
    const newErrorMessages = {
      lat: lat === '' ? 'Latitude is required' : '',
      lon: lon === '' ? 'Longitude is required' : '',
      trainNumber: trainNumber === '' ? 'Train Number is required' : '',
    };

    setErrorMessages(newErrorMessages);

    return Object.values(newErrorMessages).every((message) => message === '');
  };

  const handleSubmit = (e) => {

    e.preventDefault();
    
    if (validateFields()) {
      const data = {
        lat: lat,
        lon: lon,
        trainNumber: trainNumber,
      };
      socket.emit('update-train-position', data);
      
    }
   
  };

  return (
    <Wrap>
    <form>
    <Card>
      <CardContent>
          <TextField
            label="Latitude"
            value={lat}
            onChange={(e) => setlat(e.target.value)}
            error={errorMessages.lat !== ''}
            helperText={errorMessages.lat}
            fullWidth
            style={{ marginBottom: '16px' }}
          />
        
          <TextField
            label="Longitude"
            value={lon}
            onChange={(e) => setlon(e.target.value)}
            error={errorMessages.lon !== ''}
            helperText={errorMessages.lon}
            fullWidth
            style={{ marginBottom: '16px' }}
          />
        
          <TextField
            label="Train Number"
            value={trainNumber}
            onChange={(e) => settrainNumber(e.target.value)}
            error={errorMessages.trainNumber !== ''}
            helperText={errorMessages.trainNumber}
            fullWidth
            style={{ marginBottom: '16px' }}
          />
        
          <Button variant="contained" color="primary" onClick={handleSubmit}>
            Update Position
          </Button>
          </CardContent>
    </Card>
    </form>
    </Wrap>
  );
};

export default NavigationComponent;
