const db = require("../model/db.model.js");

class ProductController {
  getAllProducts = async (req, res, next) => {
    try {
      const productList = await db.getAllProduct();

      res.status(200).json({
        message: "success",
        data: productList,
      });
    } catch (error) {
      next(error);
    }
  };
}

module.exports = new ProductController();
