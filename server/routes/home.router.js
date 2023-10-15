var express = require("express");
var router = express.Router();
const HomeController = require("../controller/home.controller.js");

router.get("/", HomeController.getConnectServer);

// NOTE: Request from client to server must be: object type

module.exports = router;
