-- Database: WP1

-- DROP DATABASE IF EXISTS "WP1";

CREATE DATABASE "WP1"
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'Vietnamese_Vietnam.1258'
    LC_CTYPE = 'Vietnamese_Vietnam.1258'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;
	
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE IF NOT EXISTS "account" (
	"id" uuid DEFAULT uuid_generate_v4 (),
	"username" VARCHAR(20) NOT NULL,
	"password" VARCHAR(256) NOT NULL,
	"fullname" VARCHAR(128),
	"role" VARCHAR(20) DEFAULT 'admin',
	"avatar" VARCHAR(512),
	"create_at" TIMESTAMP DEFAULT NOW(),
	"modify_at" TIMESTAMP DEFAULT NOW(),
	CONSTRAINT "PK_account" PRIMARY KEY("id")
);

CREATE TABLE IF NOT EXISTS "customer" (
	"customer_id" uuid DEFAULT uuid_generate_v4(),
	"name" VARCHAR(128) NOT NULL,
	"address" VARCHAR(512),
	"phone" VARCHAR NOT NULL,
	"create_at" TIMESTAMP DEFAULT NOW(),
	"modify_at" TIMESTAMP DEFAULT NOW(),
	CONSTRAINT "PK_customer" PRIMARY KEY("customer_id")
);

CREATE TABLE IF NOT EXISTS "order" (
	"order_id" uuid DEFAULT uuid_generate_v4(),
	"customer_id" uuid NOT NULL,
	"price" int DEFAULT 0,
	"deliver_address" VARCHAR(512) NOT NULL,
	"status" VARCHAR(20),
	"order_date" TIMESTAMP DEFAULT NOW(),
	"modify_at" TIMESTAMP DEFAULT NOW(),
	CONSTRAINT "PK_order" PRIMARY KEY("order_id")	
);

CREATE TABLE IF NOT EXISTS "product" (
	"product_id" uuid DEFAULT uuid_generate_v4(),
	"inventory_number" int,
	"import_price" int,
	"price" int,
	"image" VARCHAR(512),
	"detail" VARCHAR(512),
	"manufacture" VARCHAR(30),
	"status" VARCHAR(20),
	"create_at" TIMESTAMP DEFAULT NOW(),
	"modify_at" TIMESTAMP DEFAULT NOW(),
	CONSTRAINT "PK_product" PRIMARY KEY("product_id")
);

CREATE TABLE IF NOT EXISTS "detail_order" (
	"order_id" uuid,
	"product_id" uuid,
	"quantity" int default 1,
	"discount_id" uuid,
	"after_price" int,
	CONSTRAINT "PK_detail_order" PRIMARY KEY("order_id", "product_id")
);

CREATE TABLE IF NOT EXISTS "category" (
	"category_id" uuid DEFAULT uuid_generate_v4(),
	"name" VARCHAR(512),
	"create_at" TIMESTAMP DEFAULT NOW(),
	"modify_at" TIMESTAMP DEFAULT NOW(),
	CONSTRAINT "PK_category" PRIMARY KEY("category_id")
);

CREATE TABLE IF NOT EXISTS "category_product" (
	"product_id" uuid,
	"category_id" uuid,
	CONSTRAINT "PK_category_product" PRIMARY KEY("product_id", "category_id")
);

CREATE TABLE IF NOT EXISTS "discount" (
	"discount_id" uuid DEFAULT uuid_generate_v4(),
	"product_id" uuid,
	"percent" int,
	"maximum" int,
	"started" TIMESTAMP,
	"ended" TIMESTAMP,
	"create_at" TIMESTAMP DEFAULT NOW(),
	"modify_at" TIMESTAMP DEFAULT NOW(),
	CONSTRAINT "PK_discount" PRIMARY KEY("discount_id")
);
	

ALTER TABLE "order" 
ADD CONSTRAINT "FK_order_customer" 
FOREIGN KEY ("customer_id") 
REFERENCES "customer" ("customer_id");

ALTER TABLE "detail_order" 
ADD CONSTRAINT "FK_detail_order_order" 
FOREIGN KEY ("order_id") 
REFERENCES "order" ("order_id");

ALTER TABLE "detail_order" 
ADD CONSTRAINT "FK_detail_order_product" 
FOREIGN KEY ("product_id") 
REFERENCES "product" ("product_id");

ALTER TABLE "category_product" 
ADD CONSTRAINT "FK_category_product_product" 
FOREIGN KEY ("product_id") 
REFERENCES "product" ("product_id");


ALTER TABLE "category_product" 
ADD CONSTRAINT "FK_category_product_category" 
FOREIGN KEY ("category_id") 
REFERENCES "category" ("category_id");

ALTER TABLE "discount" 
ADD CONSTRAINT "FK_discount_product" 
FOREIGN KEY ("product_id") 
REFERENCES "product" ("product_id");

ALTER TABLE "detail_order" 
ADD CONSTRAINT "FK_detail_order_discount" 
FOREIGN KEY ("discount_id") 
REFERENCES "discount" ("discount_id");

INSERT INTO "account" ("username", "password", "fullname")
VALUES 
	('admin1', '123456', 'Nguyễn Văn A'),
	('admin2', '109238', 'Nguyễn Văn B');