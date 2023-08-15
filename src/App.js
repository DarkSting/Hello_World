import React, { useState } from "react";
import { createTheme, Stack, Box, ThemeProvider } from "@mui/material"
import Dashboard from "./views/adminpage/AdminDashboard"
import { Route,RouterProvider,Routes, createBrowserRouter, createRoutesFromElements } from 'react-router-dom';
import Feed from "./components/Feed";
import TripPage from "./views/TripPage/TripPage";
import Station from "./views/StationPage/StationPage"
import NavigationComponent from "./views/Navigation/Navigation";
import TripForm from "./views/TripPage/TripPage";
import RouteForm from "./views/RoutePage/RoutePage";


const router = createBrowserRouter(

  createRoutesFromElements(

    <Route>
         
         <Route path="/" element={ <Feed />} />
          <Route path="trains" element={<TripPage />} />
          <Route path="trips" element={<TripForm />} />
          <Route path="stations" element={<Station/>} />
          <Route path="navigation" element={<NavigationComponent/>} />
          <Route path="routes" element={<RouteForm />} />
     
    
    </Route>
  

        
  )

)


const App = () => {
  const [mode, setMode] = useState("light")
  const toogleThemeMode = () => setMode(mode === "light" ? "dark" : "light")

  const theme = createTheme({
    palette: {
      mode,
    },
  })

  return (
    <ThemeProvider theme={theme}>

      <Box bgcolor={"background.default"} color={"text.primary"}>
        <Dashboard />
      </Box>
      <RouterProvider router={router}/>
    </ThemeProvider>
  )
}

export default App
