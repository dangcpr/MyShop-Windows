const express = require("express");
const app = express();
const cors = require("cors");
const port = 5000;
const route = require("./routes/index.js");

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
  console.log(`Listening to port: http://localhost:${port}`);
});
