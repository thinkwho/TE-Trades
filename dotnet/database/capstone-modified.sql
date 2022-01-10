USE master
GO

--drop database if it exists
IF DB_ID('final_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE final_capstone;
END

CREATE DATABASE final_capstone
GO

USE final_capstone
GO

--create tables
CREATE TABLE users (
	user_id			int				IDENTITY(1,1)	NOT NULL,
	username		varchar(50)						NOT NULL,
	password_hash	varchar(200)					NOT NULL,
	salt			varchar(200)					NOT NULL,
	user_role		varchar(50)						NOT NULL

	CONSTRAINT PK_user PRIMARY KEY (user_id)
);

CREATE TABLE games (
	game_id			INT				IDENTITY(1,1)	not null,
	game_name		VARCHAR(100)					not null,
	game_organizer	VARCHAR(100)					not null,
	start_date		DATETIME						not null,
	end_date		DATETIME						not null,
	winner			VARCHAR(100)					,
	isCompleted		BIT								not null
	
	CONSTRAINT PK_game PRIMARY KEY (game_id)
)

CREATE TABLE game_player (
	player_id			INT		not null,	
	game_id				INT		not null,
	holdings_id			INT		

	CONSTRAINT pk_game_player				PRIMARY KEY (player_id, game_id),
	CONSTRAINT fk_game_player_user			FOREIGN KEY (player_id) REFERENCES users(user_id),
	--CONSTRAINT fk_game_player_holdings		FOREIGN KEY (holdings_id) REFERENCES holdings(holdings_id),
	CONSTRAINT fk_game_player_game			FOREIGN KEY (game_id)	REFERENCES games(game_id),
)

CREATE TABLE holdings (
	holdings_id			INT				IDENTITY(1,1)	not null,
	cash_balance		INT								not null,
	stock_balance		INT								not null
	
	CONSTRAINT pk_holdings				PRIMARY KEY (holdings_id)
)

CREATE TABLE stock (
	stock_symbol	VARCHAR(10) not null,
	stock_name		VARCHAR(50) not null,
	amount			INT not null

	CONSTRAINT pk_stock	   PRIMARY KEY (stock_symbol)
)

CREATE TABLE holdings_stock (
	holdings_id			INT	   			not null,
	stock_symbol		VARCHAR(10)		not null

	CONSTRAINT pk_holdings_stock			PRIMARY KEY (holdings_id, stock_symbol),
	CONSTRAINT fk_holdings_stock_stock		FOREIGN KEY (stock_symbol)					REFERENCES stock(stock_symbol),
	CONSTRAINT fk_holdings_stock_holdings	FOREIGN KEY (holdings_id)					REFERENCES holdings(holdings_id)
)

CREATE TABLE portfolios (
	player_id	int			not null,
	game_id		int			not null,
	balance		decimal		not null
	
	CONSTRAINT pk_portfolio			PRIMARY KEY (player_id, game_id),
	CONSTRAINT fk_portfolio_player	FOREIGN KEY (player_id) REFERENCES users(user_id),
	CONSTRAINT fk_portfolio_game	FOREIGN KEY (game_id)	REFERENCES games(game_id)
)

CREATE TABLE trades (
	stock_symbol	VARCHAR(10)		not null,
	stock_price		decimal			not null,
	trade_type		int				not null,
	quantity		decimal			not null,
	total_trade_amount decimal		not null

	--add constraints

)
--////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

--populate default data
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');

INSERT INTO games (game_name, game_organizer, start_date, end_date, winner, isCompleted) VALUES ('Call Of Duty', 'user', 2000/03/10, 2000/03/20, 'admin', 0);
INSERT INTO games (game_name, game_organizer, start_date, end_date, isCompleted) VALUES ('Crazy Taxi', 'admin', 12/31/1995, 04/10/1996, 0);
INSERT INTO games (game_name, game_organizer, start_date, end_date, isCompleted) VALUES ('Halo', 'user',  05/03/2005, 05/13/2006, 1);
INSERT INTO games (game_name, game_organizer, start_date, end_date, winner, isCompleted) VALUES ('Gears Of War', 'admin', 09/13/2008, 09/13/2009, 'user', 1);

INSERT INTO portfolios (player_id, game_id, balance) VALUES (1,1,100000);
INSERT INTO portfolios (player_id, game_id, balance) VALUES (2,1,0);
INSERT INTO portfolios (player_id, game_id, balance) VALUES (1,2,0);

INSERT INTO trades (stock_symbol, stock_price, trade_type, quantity, total_trade_amount) VALUES ('APPL', 120.13, 0, 100, 45012.12);
INSERT INTO trades (stock_symbol, stock_price, trade_type, quantity, total_trade_amount) VALUES ('GOOG', 23.43, 0, 113.11, 45012.12);
INSERT INTO trades (stock_symbol, stock_price, trade_type, quantity, total_trade_amount) VALUES ('NTFX', 1020.13, 1, 95.331, 45012.12);
INSERT INTO trades (stock_symbol, stock_price, trade_type, quantity, total_trade_amount) VALUES ('LOLO', 29.11, 1, 100.09, 45012.12);

GO

