const router = require("express").Router();
const { CreateTournementMatches } = require("../Controllers/TournementController");
const { createRoute,getRoute, getRoutes} = require("../Controllers/TrainRouteControllers");

router.post("/create-tournement", CreateTournementMatches);
router.post("/getroute", getRoute);
router.get("/getroutes", getRoutes);

module.exports = router;