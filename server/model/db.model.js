const initOptions = {};
const pgp = require("pg-promise")(initOptions);
const dbStringConfig = require("../config/dbConfig.js");

// Lấy String connect để kết nối database
const db = pgp(dbStringConfig);

const getAllProduct = async () => {
  const rs = await db.any('SELECT * FROM "product"');
  return rs;
};

const getAllAccount = async () => {
  const rs = await db.any('SELECT * FROM "account"');
  return rs;
};

const getAllCustomer = async () => {
  const rs = await db.any(
    'select * from "customer" ORDER BY "customer_id" ASC'
  );
  return rs;
};

module.exports = {
  getAllProduct,
  getAllAccount,
  getAllCustomer,
};
