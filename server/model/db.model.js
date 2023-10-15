const initOptions = {};
const pgp = require("pg-promise")(initOptions);
const dbStringConfig = require("../config/dbConfig.js");

// Lấy String connect để kết nối database
const db = pgp(dbStringConfig);

async function getAllTasks() {
  const rs = await db.any('SELECT * FROM "tasks"');
  return rs;
}

async function createNewTask(task) {
  const rs = await db.one(
    'INSERT INTO "tasks"(id, name, status) VALUES($1, $2, $3) RETURNING *',
    [task.id, task.name, task.status]
  );
}

async function deleteTask(task) {
  const rs = await db.one('DELETE FROM "tasks" WHERE id = $1 RETURNING *', [
    task.id,
    task.name,
    task.status,
  ]);
}

async function editTask(task) {
  const rs = await db.one(
    'UPDATE "tasks" SET name = $2 WHERE id = $1 RETURNING *',
    [task.id, task.name, task.status]
  );
}

async function completeTask(task) {
  const rs = await db.one(
    'UPDATE "tasks" SET status = $3 WHERE id = $1 RETURNING *',
    [task.id, task.name, task.status]
  );
}

const getAllProduct = async () => {
  const rs = await db.any('SELECT * FROM "product"');
  return rs;
};

module.exports = {
  getAllTasks,
  createNewTask,
  deleteTask,
  editTask,
  completeTask,
  getAllProduct,
};
