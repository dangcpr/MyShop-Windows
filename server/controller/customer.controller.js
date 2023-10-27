const db = require("../model/db.model.js");

class CustomerController {
  getAllCustomer = async (req, res, next) => {
    try {
      const customerList = await db.getAllCustomer();

      res.status(200).json({
        customerList,
      });
    } catch (error) {
      next(error);
    }
  };
}

module.exports = new CustomerController();
