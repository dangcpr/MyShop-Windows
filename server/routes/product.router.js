var express = require("express");
var router = express.Router();
const ProductController = require("../controller/product.controller.js");

router.get("/", ProductController.getAllProducts);

module.exports = router;
