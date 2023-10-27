const homeRouter = require("../routes/home.router.js");
const accountRouter = require("./account.router.js");
const customerRouter = require("./customer.router.js");

const route = (app) => {
  app.use("/account", accountRouter);
  app.use("/customer", customerRouter);
  app.use("/", homeRouter);
};

module.exports = route;
