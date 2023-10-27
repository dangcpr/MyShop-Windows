var express = require("express");
var router = express.Router();
const CustomerController = require("../controller/customer.controller.js");

router.get("/", CustomerController.getAllCustomer);

module.exports = router;
