// importing the modules
const express = require("express");
const cors = require("cors");
const mongoose = require("mongoose");
const cookieParser = require("cookie-parser");
require("dotenv").config();
const { Server, Socket } = require('socket.io');

// app config
const app = express();
const  http = require('http');
const httpServer = http.createServer(app)
const io = new Server(httpServer,{
  cors:{origin:"*"}
});

//middlewares
app.use(
  cors({
    origin: "*",
    methods: ["GET", "POST", "DELETE","PUT"],
    credentials: true,
  })
);
app.use(express.json());
app.use(cookieParser());

// routes
app.use("/train", require("../Routes/TrainRoutes"));
app.use("/user", require("../Routes/UserRoutes"));
app.use("/trip", require("../Routes/TripRoutes"));
app.use("/station", require("../Routes/StationRoutes"));
app.use("/route", require("../Routes/TrainPathRoutes"));


// mongodb
mongoose
  .connect("mongodb+srv://akashtest:12345@test.9rz1ljg.mongodb.net/railway-test", {
    useCreateIndex: true,
    useUnifiedTopology: true,
    useNewUrlParser: true,
  })
  .then(() => console.log(`Database Connection Established at ${27017}`))
  .catch((err) => console.log(err));

//port
const PORT = process.env.PORT|| 5000;

//listen
httpServer.listen(PORT, () => console.log(`server started at ${process.env.PORT}`));

module.exports = {app};


