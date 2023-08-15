import React, { useEffect, useState } from 'react';
import {
  Card,
  CardContent,
  TextField,
  Button,
  Typography,
  Chip,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Snackbar,
  Alert
} from '@mui/material';
import {Add} from '@mui/icons-material' ;
import Wrap from '../../components/widgets/wrapper';
import Axios from '../../api/axios';
import { DemoContainer } from '@mui/x-date-pickers/internals/demo';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { DateTimePicker } from '@mui/x-date-pickers/DateTimePicker';

const TripForm = () => {


  useEffect(()=>{

    const headers = {'Content-Type':'application/json'};
    Axios.post('train/gettrains',headers).then(r=>{
      console.log(r.data.trains);
      setroute(r.data.trains);
    }).catch(er=>{
      console.log(er);
    })
    Axios.get('route/getroutes',headers).then(r=>{
      console.log(r.data.data);
      setroute(r.data.data);
    }).catch(er=>{
      console.log(er);
    })

  },[])

  const [departure, setDeparture] = useState(null);
  const [arrival, setArrival] = useState(null);
  const [route, setroute] = useState([]);
  const [bookingExpire, setBookingExpire] = useState('');
  const [trainNumber, setTrainNumber] = useState('');
  const [openDialog, setOpenDialog] = useState(false);
  const [selectedroute, setSelectedroute] = useState([]);
  const [openSnackbar, setOpenSnackbar] = useState(false);
  const [snackbarMessage, setSnackbarMessage] = useState('');
  const [snackbarSeverity, setSnackbarSeverity] = useState('success');
  const [openrouteDialog, setOpenrouteDialog] = useState(false);

  const handleAddroute = () => {
    setOpenDialog(true);
  };

  const handlerouteelect = (route) => {
    if (!selectedroute.includes(route)) {
      setSelectedroute([...selectedroute, route]);
    }
    handleCloserouteDialog();
  };

  const handleDialogClose = () => {
    setOpenDialog(false);
  };

  
  const validateForm = () => {
    return  departure !== null &&
    arrival !== null &&
      selectedroute.length > 0 &&
      bookingExpire !== '' &&
      trainNumber !== '';
  };

  const handleSubmit = () => {
    if (validateForm()) {
      // Handle form submission
      Axios.post('/trip/createtrip',{}).then(value=>{
        setOpenSnackbar(true);
        setSnackbarSeverity('success');
        setSnackbarMessage('Form submitted successfully!');
      })
      .catch(e=>{
        setOpenSnackbar(true);
        setSnackbarSeverity('error');
        setSnackbarMessage('submitting error.');
      })
     
    } else {
      setOpenSnackbar(true);
      setSnackbarSeverity('error');
      setSnackbarMessage('Please fill out all fields before submitting.');
    }
  };


  
  const handleOpenrouteDialog = () => {
    setOpenrouteDialog(true);
  };

  const handleCloserouteDialog = () => {
    setOpenrouteDialog(false);
  };



  const handleSnackbarClose = () => {
    setOpenSnackbar(false);
  };

  return (
    <Wrap>
    <Card>
      <CardContent>
     
      <LocalizationProvider dateAdapter={AdapterDayjs}>
      <DemoContainer components={['DateTimePicker', 'DateTimePicker']}>
        <DateTimePicker
          label="Arrival"
          defaultValue={arrival}
          onChange={(newValue) => setArrival(newValue)}
        />
        <DateTimePicker
          label="Departure"
          value={departure}
          onChange={(newValue) => setDeparture(newValue)}
        />
      </DemoContainer>
      </LocalizationProvider>
      <div style={{marginBottom:'30px'}}></div>
        <div style={{ display: 'flex', alignItems: 'center', marginBottom: '10px' }}>
          <Typography variant="body1" style={{ marginRight: '10px' }}>
            route:
          </Typography>
          {selectedroute.map((route, index) => (
            <Chip key={index} label={route.routeNumber} style={{ marginRight: '8px', fontSize: '14px', backgroundColor:'#153462',color:'white',padding:'10px' }} />
          ))}
          <Button
            variant="outlined"
            color="primary"
            size="small"
            startIcon={<Add />}
            onClick={handleOpenrouteDialog}
          >
            Add route
          </Button>
        
        </div>
        <div style={{marginTop:'30px'}}></div>

        <LocalizationProvider dateAdapter={AdapterDayjs}>
        <DemoContainer components={['DateTimePicker', 'DateTimePicker']}>
        <DateTimePicker
          label="Booking Expire"
          value={bookingExpire}
          onChange={(newValue) => setBookingExpire(newValue)}
        />
        </DemoContainer>
        </LocalizationProvider>
        <TextField
          label="Train Number"
          value={trainNumber}
          onChange={(e) => setTrainNumber(e.target.value)}
          fullWidth
          margin="normal"
        />
        <Button variant="contained" color="primary" onClick={handleSubmit}>
          Submit
        </Button>
      </CardContent>
      {/* route Selection Dialog */}
      <Dialog open={openrouteDialog} onClose={handleCloserouteDialog}>
        <DialogTitle>Select a route</DialogTitle>
        <DialogContent>
          {route.map((route, index) => (
            <Button
              key={index}
              variant="outlined"
              onClick={() => handlerouteelect(route)}
              fullWidth
              style={{ marginBottom: '10px' }}
            >
              {route.routeNumber}
            </Button>
          ))}
        </DialogContent>
        <DialogActions>
          <Button onClick={handleCloserouteDialog} color="primary">
            Cancel
          </Button>
        </DialogActions>
      </Dialog>
      <Snackbar open={openSnackbar} autoHideDuration={4000} onClose={handleSnackbarClose}>
        <Alert onClose={handleSnackbarClose} severity={snackbarSeverity}>
          {snackbarMessage}
        </Alert>
      </Snackbar>
    </Card>
    </Wrap>
  );
};

export default TripForm;
