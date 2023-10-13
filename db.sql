CREATE TABLE accounts (
   user_id serial PRIMARY KEY,
	username VARCHAR ( 50 ) UNIQUE NOT NULL,
	password VARCHAR ( 50 ) NOT NULL,
	email VARCHAR ( 255 ) UNIQUE NOT NULL,
	created_on TIMESTAMP NOT NULL
);

INSERT INTO public.accounts(
	user_id, username, password, email, created_on)
	VALUES (1, 'minhtrifit', '123', 'minhtri.fit@gmail.com', '10/13/2023');
