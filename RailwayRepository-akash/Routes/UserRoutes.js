const router = require("express").Router();
const {
  createUser,
  getUser,
  getAllUsers,
  deleteUser,
  returnCurrentUser,
  loginUser,
} = require("../Controllers/UserControllers");
const { isAdmin, isAuthenticated } = require("../Middlewares/authentication");
const { creatTable, insertTimeSlot, updateTimeSlot, deleteTimeSlot } = require("../Controllers/TimeTableController");

router.post("/adduser", createUser);
router.post("/getuser", getUser);
router.get("/getallusers", getAllUsers);
router.delete("/deleteuser", deleteUser);
router.post("/loginuser", loginUser);
router.post("/create-table", creatTable);
router.post("/insert-timeslot", insertTimeSlot);
router.post("/update-timeslot", updateTimeSlot);
router.post("/delete-timeslot", deleteTimeSlot);
//hello there im akash

module.exports = router;
