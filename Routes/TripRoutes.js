const {getTrip,createTrip}= require("../Controllers/TripController");

const router = require("express").Router();



router.post('/createtrip',createTrip);
router.get('/gettrip',getTrip)


module.exports = router;