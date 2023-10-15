const db = require("../model/db.model.js");

class HomeController {
  getConnectServer = async (req, res, next) => {
    try {
      res.status(200).json({
        message: "Server run successfully",
      });
    } catch (error) {
      next(error);
    }
  };
}

module.exports = new HomeController();
