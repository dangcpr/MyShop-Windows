const { gql } = require("apollo-server");

const typeDefs = gql`
  type Customer {
    customer_id: Int!
    name: String!
    address: String!
    phone: String!
    create_at: String!
    modify_at: String!
  }

  type Query {
    customers: [Customer!]!
    customer(customer_id: ID!): Customer
  }
`;

module.exports = {
  typeDefs,
};
