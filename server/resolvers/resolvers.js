const db = require("../model/db.model");

// const { customerList } = require("../model/db.model.js");

const resolvers = {
  Customer: {
    customer_id: (parent, args, context, info) => parent.customer_id,
    name: (parent) => parent.name,
    address: (parent) => parent.address,
    phone: (parent) => parent.phone,
    create_at: (parent) => parent.create_at,
    modify_at: (parent) => parent.modify_at,
  },

  Query: {
    customers: async (parent, args) => {
      const customerList = await db.getAllCustomer();
      return customerList;
    },
  },
};

module.exports = {
  resolvers,
};
