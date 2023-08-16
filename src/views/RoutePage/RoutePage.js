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


const RouteForm = () => {


  useEffect(()=>{

    const headers = {'Content-Type':'application/json'};
  
    Axios.get('station/getstations',headers).then(r=>{
      console.log(r.data.foundStation);
      setStations(r.data.foundStation);
    }).catch(er=>{
      console.log(er);
    })

  },[])

  const[routeNumber,setRouteNumber] = useState('')
  const[startPoint,setStartPoint] = useState('')
  const[endPoint,setEndPoint] = useState('')
  const [stations, setStations] = useState([]);
  const [openDialog, setOpenDialog] = useState(false);
  const [selectedStations, setSelectedStations] = useState([]);
  const [openSnackbar, setOpenSnackbar] = useState(false);
  const [snackbarMessage, setSnackbarMessage] = useState('');
  const [snackbarSeverity, setSnackbarSeverity] = useState('success');
  const [openStationDialog, setOpenStationDialog] = useState(false);

  const handleAddStation = () => {
    setOpenDialog(true);
  };

  const handleStationSelect = (station) => {
    if (!selectedStations.includes(station)) {
      setSelectedStations([...selectedStations, station]);
    }
    handleCloseStationDialog();
  };

  const handleDialogClose = () => {
    setOpenDialog(false);
  };

  
  const validateForm = () => {
    return  routeNumber !== '' &&
    endPoint !== '' &&
      selectedStations.length > 0 &&
      startPoint !== ''
     
  };

  const handleSubmit = () => {
    if (validateForm()) {
      // Handle form submission
      Axios.post('/route/createroute',{
        routeNumber:routeNumber
        ,starting:startPoint,
        destination:endPoint,
        Stops:selectedStations}).then(value=>{
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


  
  const handleOpenStationDialog = () => {
    setOpenStationDialog(true);
  };

  const handleCloseStationDialog = () => {
    setOpenStationDialog(false);
  };



  const handleSnackbarClose = () => {
    setOpenSnackbar(false);
  };

  return (
    <Wrap>
    <Card>
      <CardContent>
      <Typography variant="h5" sx={{marginBottom:'20px'}}>Add Route</Typography>
      <div style={{marginBottom:'30px'}}></div>
        <div style={{ display: 'flex', alignItems: 'center', marginBottom: '10px' }}>
          <Typography variant="body1" style={{ marginRight: '10px' }}>
            Stations:
          </Typography>
          {selectedStations.map((station, index) => (
            <Chip key={index} label={station.stationName} style={{ marginRight: '8px', fontSize: '14px', backgroundColor:'#153462',color:'white',padding:'10px' }} />
          ))}
          <Button
            variant="outlined"
            color="primary"
            size="small"
            startIcon={<Add />}
            onClick={handleOpenStationDialog}
          >
            Add Station
          </Button>
        
        </div>
        <div style={{marginTop:'30px'}}></div>
        <TextField
          label="Route Number"
          value={routeNumber}
          onChange={(e) => setRouteNumber(e.target.value)}
          fullWidth
          margin="normal"
        />
         <TextField
          label="Start Point"
          value={startPoint}
          onChange={(e) => setStartPoint(e.target.value)}
          fullWidth
          margin="normal"
        />
         <TextField
          label="End Point"
          value={endPoint}
          onChange={(e) => setEndPoint(e.target.value)}
          fullWidth
          margin="normal"
        />
        <Button variant="contained" color="primary" onClick={handleSubmit}>
          Submit
        </Button>
      </CardContent>
      {/* Station Selection Dialog */}
      <Dialog open={openStationDialog} onClose={handleCloseStationDialog}>
        <DialogTitle>Select a Station</DialogTitle>
        <DialogContent>
          {stations.map((station, index) => (
            <Button
              key={index}
              variant="outlined"
              onClick={() => handleStationSelect(station)}
              fullWidth
              style={{ marginBottom: '10px' }}
            >
              {station.stationName}
            </Button>
          ))}
        </DialogContent>
        <DialogActions>
          <Button onClick={handleCloseStationDialog} color="primary">
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

export default RouteForm;
