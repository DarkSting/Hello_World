const express = require("express");
const mongoose = require("mongoose");
const bcrypt = require("bcrypt");
const organizerController = require("./controllers/OrganizerController");
const playerController = require("./controllers/PlayerController");
const csgoTournamentController = require("./controllers/CSGOTournamentController");
const cors = require('cors');
const cookieParser = require('cookie-parser');
const dotenv = require('dotenv');
const { default: authenticateUser } = require("./Middleware/authentication");
const playerRoutes = require('./Routes/PlayerRoutes');

dotenv.config();

// Initialize Express
const app = express();

// Connect to MongoDB
mongoose
  .connect(
    "mongodb+srv://krazyabilash:bscfinalproject2023@etms.t4ewahs.mongodb.net/etms",
    {
      useNewUrlParser: true,
      useUnifiedTopology: true,
    }
  )
  .then(() => {
    console.log("Connected to MongoDB");
  })
  .catch((error) => {
    console.error("Error connecting to MongoDB: ", error);
  });

// Middleware
app.use(express.json());
app.use(cors({ credentials: true, origin: true }))
app.use(cookieParser());
app.use(playerRoutes);
/* Routes */

app.post("/api/organizers/register", organizerController.registerOrganizer);
app.post("/api/organizers/login", organizerController.loginOrganizer);
app.post(
  "/api/organizers/create-tournament/cs-go",
  csgoTournamentController.createCSGOTournament
);




// Start the server
const port = 3001;
app.listen(port, () => {
  console.log(`Server is running on port ${port}`);
});
