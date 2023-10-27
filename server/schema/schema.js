const { gql } = require("apollo-server");
const { GraphQLDateTime } = require("graphql-type-datetime");

const typeDefs = gql`
  scalar DateTime

  type Customer {
    customer_id: Int!
    name: String!
    address: String!
    phone: String!
    create_at: DateTime!
    modify_at: DateTime!
  }

  type Query {
    customers: [Customer!]!
    customer(customer_id: ID!): Customer
  }
`;

module.exports = {
  typeDefs,
};
