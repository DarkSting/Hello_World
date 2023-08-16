import React ,{useState,useEffect}from 'react';

import { AppBar,
     Box,
     Tab,
    Tabs, 
    Alert,
    TextField, 
    FormControl, 
    InputLabel, 
    Select, 
    MenuItem, 
    Button, 
    Typography, 
    Card,
    CardContent,
    CardActions,
    Snackbar,
    Stack} 
from '@mui/material';
import Wrap from '../../components/widgets/wrapper';
import Axios from '../../api/axios';
import { Height } from '@mui/icons-material';

function TrainForm() {
  const [tabIndex, setTabIndex] = useState(0);
  const handleTabChange = (event, newValue) => {
    setTabIndex(newValue);
  };

  return (
 
        <Wrap>
      <AppBar position="static" sx={{backgroundColor:'#F5F5F5',borderRadius:'5px'}}>
        <Tabs value={tabIndex} onChange={handleTabChange} >
          <Tab label="Add Trains"/>
          <Tab label="Available Trains" />
        </Tabs>
      </AppBar>
      <TabPanel value={tabIndex} index={0}>
        <AddTrainsForm />
      </TabPanel>
      <TabPanel value={tabIndex} index={1}>
        <AvailableTrains />
      </TabPanel>
      </Wrap>
  );
}




function AddTrainsForm() {

    const [trainNumber, setTrainNumber] = useState('');
    const [trainName, setTrainName] = useState('');
    const [trainBoxCount, setTrainBoxCount] = useState('');
    const [formErrors, setFormErrors] = useState({
      trainNumber: false,
      trainName: false,
      trainBoxCount: false,
    });
    const [snackbarOpen, setSnackbarOpen] = useState(false);
    const [snackbarMessage, setSnackbarMessage] = useState('');
    const [snackbarSeverity, setSnackbarSeverity] = useState('success');

  const handleSubmit = (event) => {
    event.preventDefault();
    const newFormErrors = {};

    if (trainNumber === '') newFormErrors.trainNumber = true;
    if (trainName === '') newFormErrors.trainName = true;
    if (trainBoxCount === '') newFormErrors.trainBoxCount = true;

    if (Object.keys(newFormErrors).length > 0) {

      setFormErrors(newFormErrors);
      setSnackbarSeverity('error');
      setSnackbarMessage('Form submission failed. Please fill in all required fields.');

    } else {

      setFormErrors({});
      Axios.post('/train/addtrain',{
        name:trainName,
        trainNumber:trainNumber,
        boxcount:trainBoxCount
    }).then(v=>{
        setSnackbarSeverity('success');
        setSnackbarMessage('Train added successfully.');
    }).catch(r=>{
        setSnackbarSeverity('error');
        setSnackbarMessage('submition error');
    })
    }

    setSnackbarOpen(true);
  };

  const handleSnackbarClose = () => {
    setSnackbarOpen(false);
  };

  return (
    <Card>
        <CardContent >
    <form onSubmit={handleSubmit}>
      <Typography variant="h5">Add Trains</Typography>
      <TextField
        label="Train Number"
        value={trainNumber}
        onChange={(e) => setTrainNumber(e.target.value)}
        fullWidth
        margin="normal"
        error={formErrors.trainNumber}
        helperText={formErrors.trainNumber && 'Train number is required'}
      />
      <TextField
        label="Train Name"
        value={trainName}
        onChange={(e) => setTrainName(e.target.value)}
        fullWidth
        margin="normal"
        error={formErrors.trainName}
        helperText={formErrors.trainName && 'Train name is required'}
      />
      <FormControl fullWidth margin="normal">
        <InputLabel>Train Box Count</InputLabel>
        <Select
          value={trainBoxCount}
          onChange={(e) => setTrainBoxCount(e.target.value)}
          error={formErrors.trainBoxCount}
          helperText={formErrors.trainBoxCount && 'Train box count is required'}
        >
          <MenuItem value={1}>1</MenuItem>
          <MenuItem value={2}>2</MenuItem>
          <MenuItem value={3}>3</MenuItem>
          <MenuItem value={4}>4</MenuItem>
          <MenuItem value={5}>5</MenuItem>
          <MenuItem value={6}>6</MenuItem>
          <MenuItem value={7}>7</MenuItem>
          <MenuItem value={8}>8</MenuItem>
          <MenuItem value={9}>9</MenuItem>
          <MenuItem value={10}>10</MenuItem>
          <MenuItem value={11}>11</MenuItem>
          <MenuItem value={12}>12</MenuItem>
          <MenuItem value={13}>13</MenuItem>
          <MenuItem value={14}>14</MenuItem>
          <MenuItem value={15}>15</MenuItem>
        </Select>
      </FormControl>
      <Button type="submit" variant="contained" color="primary" onClick={handleSubmit}>
        Add Train
      </Button>
    </form>
    <Snackbar open={snackbarOpen} autoHideDuration={6000} onClose={handleSnackbarClose}>
        <Alert onClose={handleSnackbarClose} severity={snackbarSeverity}>
          {snackbarMessage}
        </Alert>
      </Snackbar>
    </CardContent>
    </Card>
  );
}

function TabPanel({ children, value, index }) {
  return (
    <div hidden={value !== index}>
      {value === index && (
        <Box p={3}>
          {children}
        </Box>
      )}
    </div>
  );
}



function AvailableTrains() {
  const [trains, setTrains] = useState([]);

  useEffect(() => {
    // Simulate fetching data from an API
    Axios.post('train/gettrains')
      .then(response => {
        setTrains(response.data.trains);
        console.log(response.data.trains    );})
      .catch(error => {
        console.error('Error fetching available trains:', error);
      });
  }, []);

  return (
    <div>
      
      <Card>
        <CardContent sx={{overflowX:'scroll'}}>
        <Typography variant="h5">Available Trains</Typography>
      <Stack direction="row" >
      {trains.map(train => (
        <TrainCard key={train.id} train={train} />
      ))}
      </Stack>
      </CardContent>
      </Card>
    </div>
  );
}

function TrainCard({ train }) {
  return (
    <Card sx={{ width: '600px', margin: '16px', }}>
      <CardContent>
        <Typography variant="h6" gutterBottom>
          Train Number: {train.trainNumber}
        </Typography>
        <Typography variant="body1">
          Train Name: {train.name}
        </Typography>
        <Typography variant="body1">
          Train Box Count: {train.trainBoxes.length}
        </Typography>
      </CardContent>
      <CardActions>
        <Button variant="contained" size="small">See Boxes</Button>
      </CardActions>
    </Card>
  );
}



export default TrainForm;
