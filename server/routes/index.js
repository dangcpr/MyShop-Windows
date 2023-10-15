const homeRouter = require("../routes/home.router.js");
const productRouter = require("../routes/product.router.js");

const route = (app) => {
  app.use("/product", productRouter);
  app.use("/", homeRouter);
};

module.exports = route;
