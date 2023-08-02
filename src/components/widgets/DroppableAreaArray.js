import React, { useState } from 'react';
import { Box,Typography } from '@mui/material';

const DroppableAreaArray = ({width,props,filename,height,notifyUpdate, header,color,storedData,Snack}) => {
  
  const handleDragOver = (e) => {
    e.preventDefault();
  };

  const handleDrop = (e) => {
    e.preventDefault();
    try {

      if(e.dataTransfer.getData('null')===null){
        return;
      }
      
      const droppedData = JSON.parse(e.dataTransfer.getData(filename));

      if(storedData.find(value=>value._id===droppedData._id)){
        return;
      }
      
      notifyUpdate([...storedData,droppedData]);
      Snack.setMsg("Action Inserted");
      Snack.setSeverity("success");
      Snack.openSnack();
      console.log(droppedData.stationName);
    } catch (error) {
      Snack.setMsg("Action Failed");
      Snack.setSeverity("error");
      Snack.openSnack();
      
      // Handle the error, e.g., show a message to the user
    }
    
  };



  return (
    <Box
      onDragOver={handleDragOver}
      onDrop={handleDrop}
      sx={{display:'flex', justifyContent:'center',alignItems:'center',
       border:'dashed 2px black',height:height,width:width, padding:'5px'}}
    >
        <Typography variant='subtitle1' sx={{marginBottom:'5px',color:color}}> {header}</Typography>

    </Box>
  );
};

export default DroppableAreaArray;
