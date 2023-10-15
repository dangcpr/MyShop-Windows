const homeRouter = require("../routes/home.router.js");
const accountRouter = require("./account.router.js");

const route = (app) => {
  app.use("/account", accountRouter);
  app.use("/", homeRouter);
};

module.exports = route;
