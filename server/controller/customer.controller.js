const db = require("../model/db.model.js");

class CustomerController {
  getAllCustomer = async (req, res, next) => {
    try {
      const accountList = await db.getAllCustomer();

      res.status(200).json({
        accountList,
      });
    } catch (error) {
      next(error);
    }
  };
}

module.exports = new CustomerController();
