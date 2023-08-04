import React, { useEffect, useState } from 'react';
import DraggableItem from '../../../components/widgets/Draggable';
import DroppableArea from '../../../components/widgets/DroppableArea';
import Wrap from '../../../components/widgets/wrapper';
import Paper from '@mui/material/Paper';
import Axios from '../../../api/axios';
import { Typography,Box,Popover, Modal, Snackbar} from '@mui/material';
import styled from '@emotion/styled';
import Button from '@mui/material/Button';
import MuiAlert from '@mui/material/Alert';
import { Close, CloseFullscreen, Minimize, RemoveCircle } from '@mui/icons-material';
import Draggable from 'react-draggable';
import DroppableAreaArray from '../../../components/widgets/DroppableAreaArray';
import Wrapper from '../../../components/widgets/wrapper';

const CardBox = styled(Box)({
  marginBottom:'10px', 
  display:'flex', 
  flexDirection:'row', 
  alignItems:'center', 
  gap:10
})


const TripPage = () => {


  const[trainResults,setTrainResults] = useState([]);
  const[stationResults,setStationResults] = useState([]);
  const[trainDroppedData,setTrainDroppedData] = useState(null);
  const[stationDroppedData,setStationDroppedData] = useState([]);
  const [open, setOpen] = useState(false);
  const [opensnack, setOpenSnack] = useState(false);
  const [msg, setMsg] = useState("Action Failed");
  const [severity, setSeverity] = useState("error");


  function handleSnackClose() {
    setOpenSnack(false);
  }

  function openSnack(){
    setOpenSnack(true);
  }

   //snack event handling from the child 
   const snack ={}
   snack['setMsg'] = setMsg;
   snack['setSeverity'] =setSeverity;
   snack['openSnack'] = openSnack;

  const handleOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  
  useEffect(()=>{

    const headers = {'Content-Type':'application/json'};
    Axios.post('train/gettrains',headers).then(r=>{
      console.log(r.data.trains);
      setTrainResults(r.data.trains);
    }).catch(er=>{
      console.log(er);
    })
    Axios.get('station/getstations',headers).then(r=>{
      console.log(r.data.foundStation);
      setStationResults(r.data.foundStation);
    }).catch(er=>{
      console.log(er);
    })

  },[])



  //render train cards
  function populateTrainData (){
   
    let array =[];
    return(
    <Box>
    <Typography variant='h5' sx={{marginBottom:'10px',backgroundColor:'#55555511',borderRadius:'3px',padding:'20px'}}> Trains</Typography>
    <Box sx={{display:'flex',flexDirection:'row',gap:1}}>
    {trainResults.map((item,index)=>{

    return <DraggableItem key={index} width='200px' color='#4682A9' header={item.name} fileName="train" data={item} displayBody={renderDraggableItemBody(item??null)}/>

    })}
    </Box>
    </Box>
    
    )

}
 
   //render card body
   const renderDraggableItemBody = (data) => {
     return Object.keys(data).map((key, index) => {
       const value = data[key];
 
       // Check if the value is an array
       if (Array.isArray(value) || key === '__v' ) {
         return null; // Skip rendering Typography for this property
       }
   
       return (
           
            <Typography key={index} variant="body1">
                   {`${key}: ${value}`}
           </Typography>
           );
           
       } ) 
       
   };


   //render station card
function populateStationData (){
   
    let array =[];

    return(
      <Box sx={{marginTop:'50px'}}>
      <Typography variant='h5' sx={{marginBottom:'10px',backgroundColor:'#55555511',borderRadius:'3px',padding:'20px'}}> Stations</Typography>
      <Box sx={{display:'flex',flexDirection:'row',gap:1}}>
      {stationResults.map((item,index)=>{
  
      return <DraggableItem key={index} width='200px' color='#4682A9' header={item?.stationName} fileName="station" data={item} displayBody={renderDraggableItemBody(item??null)}/>
  
      })}
      </Box>
      </Box>
      
      )



}
 
 {/*clears the selected items */}
  function selectedItem(array,id){

    const updatedArray = array.filter((value) => value._id!== id);
    setStationDroppedData(updatedArray)
  }


 {/*place holder for the floating form*/}
  const PlaceHolder = (droppedData,header,color,type)=>{
    return (droppedData ? (
        <Paper elevation={4} sx={{width:'100%',height:'100%',display:"flex",justifyContent:'center',padding:'10px',flexDirection:'row',justifyContent:'space-between'}}>
            <Typography variant='subtitle1' sx={{marginBottom:'5px',fontWeight:'bold',color:color}}>{header}</Typography>
        
            
        </Paper>
      ) : (
        <p>Drag a {type} card here...</p>
      ));
  }

  const PlaceHolder2 = (droppedData,header,color,type)=>{
    return (droppedData ? (
        <Paper elevation={4} sx={{width:'100%',height:'100%',display:"flex",justifyContent:'center',padding:'10px',flexDirection:'row',justifyContent:'space-between'}}>
            <Typography variant='subtitle1' sx={{marginBottom:'5px',fontWeight:'bold',color:color}}>{header}</Typography>
            <Button onClick={()=>selectedItem(stationDroppedData,droppedData._id)} sx={{color:'red'}}>

              <RemoveCircle />
            </Button>
            
        </Paper>
      ) : (
        <p>Drag a {type} card here...</p>
      ));
  }

  
   {/*create array*/}
  function createArray(results){

    console.log(results);
    return results.map((item,index)=>{
      return PlaceHolder2(item,item.stationName+" station","green")
    })

    
  }
 
  function clearArray(){

    setStationDroppedData([]);
  }

  function handleSubmit(){



  }


  return (
    <>



    {/*draggable form*/}
    <div>
      <Button
        variant="contained"
        color="primary"
        onClick={handleOpen}
        style={{ position: 'fixed', bottom: '20px', right: '20px',backgroundColor:'#E9B384' }}
      >
        Open Form
      </Button>

      {open && (
        <Draggable>
          <Box
            sx={{
              position: 'absolute',
              bottom: 100,
              right:100,
              width: '300px',
              backgroundColor: 'white',
              boxShadow: 4,
              zIndex: 9999,
              padding: '1rem',
            }}
          >
            {/* Close Button */}
            <Button onClick={()=>{
              handleClose();
              clearArray();
              handleClose()
            }} style={{ float: 'right', color:'red', '&:hover':{
              backgroundColor:'red'
            }}}>
              <Close />
            </Button>
            <Box sx={{display:'flex',flexDirection:'column',alignItems:'cneter',justifyContent:'space-between',gap:1}}>
            {/* form fields  */}

            {/*drop areas*/}
            <DroppableArea filename="train" notifyUpdate={setTrainDroppedData} header={"Train : "+trainDroppedData?.name} color="green" setDropableData={PlaceHolder} type="train" Snack={snack}/>
            {createArray(stationDroppedData)}
            <DroppableAreaArray storedData={stationDroppedData} filename="station" notifyUpdate={setStationDroppedData} header="drag a station here" color="black"  type="train" Snack={snack}/>
            <Button variant='outlined' onClick={()=>{
              handleSubmit();
               clearArray();
            }
             } color="primary">
            Confirm
          </Button>
          </Box>
            
          </Box>
        </Draggable>
      )}
    </div>
   
   {/*content*/}
    <Wrapper sx={{overflowX:'scroll',width:'80%'}}>
      <CardBox >
        {trainResults.length>0?populateTrainData():<Typography>No trains available</Typography>}
      </CardBox>
      <CardBox >
        {trainResults.length>0?populateStationData():<Typography>No stations available</Typography>}
      </CardBox>
    </Wrapper>

    {/*action state notifier*/}
    <Snackbar open={opensnack} autoHideDuration={3000} onClose={handleSnackClose}>
        {/* Custom Snackbar content */}
        <MuiAlert onClose={handleSnackClose} severity={severity} variant="filled">
          {msg}
        </MuiAlert>
      </Snackbar>

    </>
  );
};

export default TripPage;