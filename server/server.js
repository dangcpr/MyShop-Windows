const express = require("express");
const app = express();
const cors = require("cors");
const port = 5000;
const graphql_port = 4000;
const route = require("./routes/index.js");

// https://dev.to/nditah/how-to-build-a-graphql-api-with-node-prisma-and-postgres-ajg
// https://www.digitalocean.com/community/tutorials/how-to-set-up-a-graphql-api-server-in-node-js

const { ApolloServer } = require("apollo-server");
const { typeDefs } = require("./schema/schema.js");
const { resolvers } = require("./resolvers/resolvers.js");

const server = new ApolloServer({ resolvers, typeDefs });

app.use(
  cors({
    origin: "http://localhost:3000",
    methods: ["GET", "POST", "PUT", "DELETE"],
  })
);

app.use(express.json());
app.use(express.urlencoded({ extended: true }));

route(app);

app.listen(port, () => {
  console.log(`Server listening to port: http://localhost:${port}`);
});

server.listen(graphql_port, () =>
  console.log(
    `Graphql listening to port: http://localhost:${graphql_port}/graphql`
  )
);
