const router = require("express").Router();
const { CreateTournementMatches, getTournement, assignTeam, updateMatchStatus } = require("../Controllers/TournementController");
const { createRoute,getRoute, getRoutes} = require("../Controllers/TrainRouteControllers");

router.post("/create-tournement", CreateTournementMatches);
router.get("/get-tournement", getTournement);
router.post("/assign-tournement-team", assignTeam);
router.put("/update-tournement-match", updateMatchStatus);
router.post("/getroute", getRoute);
router.get("/getroutes", getRoutes);

module.exports = router;