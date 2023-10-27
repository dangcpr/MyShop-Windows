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

const customerList = [
  {
    customer_id: 1,
    name: "Nguyễn Văn A",
    address: "200 Bà Hạt, Phường 3, Quận 5, TPHCM",
    phone: "0965432875",
    create_at: "2023-10-20T03:56:13.925Z",
    modify_at: "2023-10-20T03:56:13.925Z",
  },
  {
    customer_id: 2,
    name: "Nguyễn Văn B",
    address: "187 Nguyễn Văn Bá, Phường Linh Trung, TP Thủ Đức, TPHCM",
    phone: "0967436213",
    create_at: "2023-10-20T03:56:13.925Z",
    modify_at: "2023-10-20T03:56:13.925Z",
  },
  {
    customer_id: 3,
    name: "Nguyễn Văn C",
    address: "115 Hai Bà Trưng, Phường 4, Quận 1, TPHCM",
    phone: "0367894566",
    create_at: "2023-10-20T03:56:13.925Z",
    modify_at: "2023-10-20T03:56:13.925Z",
  },
];

module.exports = {
  getAllProduct,
  getAllAccount,
  getAllCustomer,
  customerList,
};
