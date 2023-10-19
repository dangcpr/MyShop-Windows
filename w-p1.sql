-- Database: WP1

-- DROP DATABASE IF EXISTS "WP1";
ALTER USER postgres PASSWORD '123';

CREATE DATABASE "WP1"
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'Vietnamese_Vietnam.1258'
    LC_CTYPE = 'Vietnamese_Vietnam.1258'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;
	

CREATE TABLE IF NOT EXISTS "account" (
	"id" SERIAL,
	"username" VARCHAR(20) NOT NULL UNIQUE,
	"password" VARCHAR(256) NOT NULL,
	"fullname" VARCHAR(128),
	"role" VARCHAR(20) DEFAULT 'admin',
	"avatar" VARCHAR(512),
	"create_at" TIMESTAMP DEFAULT NOW(),
	"modify_at" TIMESTAMP DEFAULT NOW(),
	CONSTRAINT "PK_account" PRIMARY KEY("id")
);

CREATE TABLE IF NOT EXISTS "customer" (
	"customer_id" SERIAL,
	"name" VARCHAR(128) NOT NULL,
	"address" VARCHAR(512),
	"phone" VARCHAR NOT NULL,
	"create_at" TIMESTAMP DEFAULT NOW(),
	"modify_at" TIMESTAMP DEFAULT NOW(),
	CONSTRAINT "PK_customer" PRIMARY KEY("customer_id")
);

CREATE TABLE IF NOT EXISTS "order" (
	"order_id" SERIAL,
	"customer_id" int NOT NULL,
	"price" int DEFAULT 0,
	"deliver_address" VARCHAR(512) NOT NULL,
	"status" VARCHAR(20) DEFAULT 'created',
	"order_date" TIMESTAMP DEFAULT NOW(),
	"modify_at" TIMESTAMP DEFAULT NOW(),
	CONSTRAINT "PK_order" PRIMARY KEY("order_id")	
);

CREATE TABLE IF NOT EXISTS "product" (
	"product_id" SERIAL,
	"name" VARCHAR(512) NOT NULL,
	"inventory_number" int,
	"import_price" int,
	"price" int,
	"image" VARCHAR(512),
	"detail" VARCHAR(512),
	"manufacture" VARCHAR(30),
	"status" VARCHAR(20) DEFAULT 'activing',
	"create_at" TIMESTAMP DEFAULT NOW(),
	"modify_at" TIMESTAMP DEFAULT NOW(),
	CONSTRAINT "PK_product" PRIMARY KEY("product_id")
);

CREATE TABLE IF NOT EXISTS "detail_order" (
	"order_id" int,
	"product_id" int,
	"quantity" int default 1,
	"discount_id" int,
	"after_price" int,
	CONSTRAINT "PK_detail_order" PRIMARY KEY("order_id", "product_id")
);

CREATE TABLE IF NOT EXISTS "category" (
	"category_id" SERIAL,
	"name" VARCHAR(512),
	"create_at" TIMESTAMP DEFAULT NOW(),
	"modify_at" TIMESTAMP DEFAULT NOW(),
	CONSTRAINT "PK_category" PRIMARY KEY("category_id")
);

CREATE TABLE IF NOT EXISTS "category_product" (
	"product_id" int,
	"category_id" int,
	CONSTRAINT "PK_category_product" PRIMARY KEY("product_id", "category_id")
);

CREATE TABLE IF NOT EXISTS "discount" (
	"discount_id" SERIAL,
	"product_id" int,
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
REFERENCES "customer" ("customer_id") ON DELETE CASCADE;

ALTER TABLE "detail_order" 
ADD CONSTRAINT "FK_detail_order_order" 
FOREIGN KEY ("order_id") 
REFERENCES "order" ("order_id") ON DELETE CASCADE;

ALTER TABLE "detail_order" 
ADD CONSTRAINT "FK_detail_order_product" 
FOREIGN KEY ("product_id") 
REFERENCES "product" ("product_id") ON DELETE CASCADE;

ALTER TABLE "category_product" 
ADD CONSTRAINT "FK_category_product_product" 
FOREIGN KEY ("product_id") 
REFERENCES "product" ("product_id") ON DELETE CASCADE;


ALTER TABLE "category_product" 
ADD CONSTRAINT "FK_category_product_category" 
FOREIGN KEY ("category_id") 
REFERENCES "category" ("category_id") ON DELETE CASCADE;

ALTER TABLE "discount" 
ADD CONSTRAINT "FK_discount_product" 
FOREIGN KEY ("product_id") 
REFERENCES "product" ("product_id") ON DELETE CASCADE;

ALTER TABLE "detail_order" 
ADD CONSTRAINT "FK_detail_order_discount" 
FOREIGN KEY ("discount_id") 
REFERENCES "discount" ("discount_id") ON DELETE CASCADE;

INSERT INTO "account" ("username", "password", "fullname")
VALUES 
	('admin1', '123456', 'Nguyễn Văn A'),
	('admin2', '109238', 'Nguyễn Văn B'),
	('minhtrifit', '123', 'Minh Trí');
	
INSERT INTO "category" ("name")
VALUES 
	('Laptop'),
	('Điện thoại'),
	('Tablet');
	
SELECT * FROM "category"
	
INSERT INTO "product" ("name", "inventory_number", "import_price", "price", "image", "detail", "manufacture")
VALUES 
	('iPhone 12 128GB', 3, 11000000, 14990000, '1.jpg', 'Apple đã trình diện đến người dùng mẫu điện thoại iPhone 12 128GB với sự tuyên bố về một kỷ nguyên mới của iPhone 5G, nâng cấp về màn hình và hiệu năng hứa hẹn đây sẽ là smartphone cao cấp đáng để mọi người đầu tư sở hữu.', 'Apple'),
	('Laptop HP Gaming VICTUS 15', 4, 13000000, 19900000, '2.jpg', 'Laptop HP VICTUS 15 fa0155TX i5 12450H (81P00PA) hứa hẹn mang đến trải nghiệm làm việc và giải trí tuyệt vời nhờ bộ vi xử lý Intel thế hệ 12 mạnh mẽ, card đồ họa NVIDIA RTX 30-series và màn hình 144 Hz siêu mượt mà.', 'HP'),
	('iPad 9 WiFi 64GB', 3, 5000000, 7490000, '3.jpg', 'Sau thành công của iPad 8, Apple cho đã cho ra mắt máy tính bảng iPad 9 WiFi 64GB - phiên bản tiếp theo của dòng iPad 10.2 inch, về cơ bản nó kế thừa những điểm mạnh từ các phiên bản trước đó và được cải tiến thêm hiệu suất, trải nghiệm người dùng nhằm giúp nhu cầu sử dụng giải trí và làm việc tiện lợi, linh hoạt hơn.', 'Apple'),
	('Samsung Galaxy Tab A7 Lite', 2, 2900000, 3590000, '4.jpg', 'Máy tính bảng Samsung Galaxy Tab A7 Lite một phiên bản rút gọn của dòng máy tính bảng "ăn khách" Galaxy Tab A7 thuộc thương hiệu Samsung, đáp ứng nhu cầu giải trí của khách hàng thuộc phân khúc bình dân với màn hình lớn nhưng vẫn gọn nhẹ hợp túi tiền.', 'Samsung'),
	('Nokia G22', 4, 3000000, 3590000, '5.jpg', 'Nokia G22 là mẫu điện thoại giá rẻ được ra mắt chính thức vào tháng 03/2023 tại thị trường Việt Nam. Máy nổi bật với màn hình lớn, camera có độ phân giải 50 MP cùng một viên pin trâu cho thời gian sử dụng vô cùng ấn tượng.', 'Nokia'),
	('Vivo Y17s 4GB', 48, 3000000, 3790000, '6.jpg', 'Vào tháng 09/2023, nhà vivo cho ra mắt mẫu điện thoại vivo Y17s 4GB tại thị trường Việt Nam với mức giá bán đầy ấn tượng, máy được hãng định hình là dòng điện thoại phân khúc thấp với điểm nổi bật là pin lớn, cấu hình mạnh trong tầm giá cùng với việc trang bị camera có độ phân giải tới 50 MP.', 'Vivo');
SELECT * FROM "product";

INSERT INTO "customer" ("name", "address", "phone")
VALUES 
	('Nguyễn Văn A', '200 Bà Hạt, Phường 3, Quận 5, TPHCM', '0965432875'),
	('Nguyễn Văn B', '187 Nguyễn Văn Bá, Phường Linh Trung, TP Thủ Đức, TPHCM', '0967436213'),
	('Nguyễn Văn C', '115 Hai Bà Trưng, Phường 4, Quận 1, TPHCM', '0367894566');
SELECT * FROM "customer";

INSERT INTO "order" ("customer_id", "price", "deliver_address", "order_date") 
VALUES 
	(1, 34890000, '187 Nguyễn Văn Bá, Phường Linh Trung, TP Thủ Đức, TPHCM', '2023-10-08'),
	(3, 7380000, '115 Hai Bà Trưng, Phường 4, Quận 1, TPHCM', '2023-09-21'),
	(2, 7380000, '115 Hai Bà Trưng, Phường 4, Quận 1, TPHCM', '2023-10-07'),
	(1, 7380000, '115 Hai Bà Trưng, Phường 4, Quận 1, TPHCM', '2023-10-06'),
	(3, 7380000, '115 Hai Bà Trưng, Phường 4, Quận 1, TPHCM', '2023-10-13'),
	(2, 7380000, '115 Hai Bà Trưng, Phường 4, Quận 1, TPHCM', '2023-10-10');
	
INSERT INTO "detail_order" ("order_id", "product_id", "quantity", "after_price")
VALUES 
	(1, 1, 1, 14990000),
	(1, 2, 1, 14990000),
	(2, 5, 1, 3590000),
	(2, 6, 1, 3790000),
	(3, 4, 1, 3590000),
	(3, 5, 1, 3790000),	
	(4, 4, 1, 3590000),
	(4, 6, 1, 3790000),
	(5, 4, 1, 3590000),
	(5, 6, 1, 3790000),
	(6, 4, 1, 3590000),
	(6, 6, 1, 3790000);

SELECT * FROM "order" WHERE EXTRACT('MONTH' FROM "order_date") = EXTRACT('MONTH' FROM NOW());
SELECT * FROM "order" WHERE EXTRACT('MONTH' FROM "order_date") = EXTRACT('MONTH' FROM NOW()) - 1;
SELECT * FROM "order" WHERE EXTRACT('WEEK' FROM "order_date") = EXTRACT('WEEK' FROM NOW());
SELECT * FROM "order" WHERE EXTRACT('WEEK' FROM "order_date") = EXTRACT('WEEK' FROM NOW()) - 1;

INSERT INTO "category_product" 
VALUES
	(1, 2),
	(2, 1),
	(3, 3),
	(4, 3),
	(5, 2),
	(6, 2);
--DELETE FROM "category_product"

SELECT * FROM "detail_order" ;

SELECT SUM("inventory_number") FROM "product";

SELECT c."category_id", c."name", SUM(p."inventory_number") as "in_num_cat" FROM "category_product" ct
JOIN "product" p ON ct.product_id = p.product_id
JOIN "category" c ON ct."category_id" = c."category_id"
GROUP BY c."category_id";
