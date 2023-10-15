const db = require("../model/db.model.js");

class AccountController {
  getAllProducts = async (req, res, next) => {
    try {
      const accountList = await db.getAllAccount();

      res.status(200).json({
        accountList,
      });
    } catch (error) {
      next(error);
    }
  };
}

module.exports = new AccountController();
