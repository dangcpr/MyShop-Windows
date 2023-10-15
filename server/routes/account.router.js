var express = require("express");
var router = express.Router();
const AccountController = require("../controller/account.controller.js");

router.get("/", AccountController.getAllProducts);

module.exports = router;
