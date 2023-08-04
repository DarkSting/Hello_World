import react from 'react';
import Wrapper from '../../../components/widgets/wrapper';
import { useState } from 'react';
import './stationstyle.css'
import TextField from '@mui/material/TextField';
import { Alert, Box, Card, Paper, Snackbar, Typography } from '@mui/material';
import Axios from '../../../api/axios';

let is_Sent = false;

export default function StationForm(){

    const[stationName,setStationName]=useState('');
    const[longitude,setLongitude]=useState('');
    const[latitude,setLatitude]=useState('');
    const[stationNumber,setStationNumber]=useState(0);
    const[stationClass,setStationClass]=useState('');
    const [opensnack, setOpenSnack] = useState(false);
    const [msg, setMsg] = useState("Action Failed");
    const [severity, setSeverity] = useState("error");

    
    function handleSnackClose() {
        setOpenSnack(false);
    }
      
      const handleSubmit =(array,e)=>{

        e.preventDefault();
        
        setTimeout(()=>{
            if(is_Sent===false){

            }
        },3000)
      

        let errorList = [];
        
        if(array[0]===""){

            errorList.push("please insert station name");

        }else{

        }

        if(array[1]===0){
            errorList.push("please insert station number");
        }else{
            
        }

        if(array[2]===""){
            errorList.push("please insert station class");
        }else{
            
        }

        if(array[3]===""){
            errorList.push("please insert latitude");

        }else{
            
        }

        if(array[4]===""){
            errorList.push("please insert station longitude");
        }else{
            
        }

        if(errorList.length===0){

            let data = {
                stationNumber:array[1],
                stationName:array[0],
                longitude:array[4],
                latitude:array[3],
                stationClass:array[2]
            }

            setMsg("submitting");
            setSeverity('warning');
            setOpenSnack(true);

            Axios.post('station/createstation',data).then(r=>{
                is_Sent = true;
                setMsg("submit succeed");
                setSeverity('success');
                setOpenSnack(true);

            }).catch(er=>{
                setMsg("submit failed");
                setSeverity('error');
                setOpenSnack(true);
            })
        }
        
        setMsg("submit failed");
        setSeverity('error');
        setOpenSnack(true);

      }
      
      const valueArray = [stationName,stationNumber,stationClass,latitude,longitude];

    return(
        <Wrapper>
             <section className="form-section">
        
        <form style={{display:'flex',flexDirection:'column',alignItems:'center'}}
          autoComplete="false"
          action="https://formspree.io/f/meqvlgqr"
          method="POST"
        >
        <Card elevation={2} sx={{display:'flex',flexDirection:'column',gap:2, width:'100%',padding:'50px',}}>
        <Typography variant='h4' sx={{marginBottom:'20px',alignContent:'center'}}>Add Station</Typography>
          <TextField id="outlined-basic" label="Station Name" onChange={(e)=>setStationName(e.target.value)} value={stationName} variant="outlined" />
          <TextField id="outlined-basic" label="Station Number" onChange={(e)=>setStationNumber(e.target.value)} value={stationNumber} variant="outlined" />
            {/* {form.password.length < 6 ? (
              <p className="warning-message">
                Password length should be more than 6
              </p>
            ) : (
              ''
            )} */}
          <TextField id="outlined-basic" label="Station Class" value={stationClass} onChange={(e)=>setStationClass(e.target.value)} variant="outlined" />
          <TextField id="outlined-basic" label="longituted" value={longitude} onChange={(e)=>setLongitude(e.target.value)} variant="outlined" />
          <TextField id="outlined-basic" label="latitude" value={latitude} onChange={(e)=>setLatitude(e.target.value)} variant="outlined" />
          <div
            className={`submit-button-wrapper ${false ? 'float' : ''}`}
          >
            <button
              tabIndex={-1}
              className={`submit-button ${
                true> 6 ? 'button-success' : ''
              }`}

              onClick={(e)=>handleSubmit(valueArray,e)}
            >
              Submit
            </button>
           
          </div>
          </Card>
        </form>  
      </section>
      <Snackbar open={opensnack} autoHideDuration={3000} onClose={handleSnackClose}>
        {/* Custom Snackbar content */}
        <Alert onClose={handleSnackClose} severity={severity} variant="filled">
          {msg}
        </Alert>
      </Snackbar>
        </Wrapper>
    )

}