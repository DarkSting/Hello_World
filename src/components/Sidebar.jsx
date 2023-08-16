import React, { useState } from "react"

import {
  Box,
  List,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
  Switch,
} from "@mui/material"

import {
  Person,
  Home,
  Route,
  NavigateNext,
  Pages,
  Groups,
  AccountBox,
  Storefront,
  Settings,
  ModeNight,
  LightMode,
  Train,
  TripOrigin,
  Bookmark,
  Navigation,
} from "@mui/icons-material"
import { red } from "@mui/material/colors"
import { CustomListLinkButton } from "../components/widgets/LinkButton"
import styled from "@emotion/styled";


const ListItemBtn = styled(CustomListLinkButton)(({theme})=>({


}));

const ListButtonProp =()=>{

  return {
   
    '&.Mui-selected':{backgroundColor:"#FD8D14",color:'white'},
   '&.Mui-selected:hover':{backgroundColor:"#FD8D14"},
   '&:hover':{
     backgroundColor:"#F3AA60",
     color:'white',
     
   },
   borderRadius:'0px 10px 10px 0px'
  }
 
 
 }

const Sidebar = ({ toogleThemeMode, themeMode }) => {

  const [currentState,setCurrentState] = useState('Home')
  let links = [
    { icon: <Train />, title: "Homepage" },
    { icon: <TripOrigin />, title: "Pages" },
    { icon: <Bookmark />, title: "Groups" },
    { icon: <Route />, title: "Groups" },
    { icon: <Navigation />, title: "Groups" },
  ]

  
const state = ['Trains','Set Trip','Stations', 'Routes', 'Navigate Train'];
const routes = ['trains','trips','stations','routes' ,'navigation']

  return (
    <Box
      flex={1}

      flexDirection="row"
      // bgcolor={red[200]} // debuging stuff
      sx={{ display: { xs: "none", sm: "block" }, marginTop:'150px'}}
    >
      <Box position="fixed" sx={{width:'200px'}}>
        <List>
          {links.map(({ title, icon }, index) => (
            <ListItem key={index} disablePadding >
              <ListItemBtn to={routes[index]} component='a' selected={currentState===state[index]} onClick={()=>{setCurrentState(state[index])}}  sx={ListButtonProp}>
                <ListItemIcon>{icon}</ListItemIcon>
                <ListItemText primary={state[index]} />
              </ListItemBtn >
            </ListItem>
          ))}

          
        
        </List>
      </Box>
    </Box>
  )
}

export default Sidebar
