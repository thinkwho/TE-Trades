USE master
GO

--	this is table redesign after meeting with myron and team 12092021
--	goal organize table data, find which tables are used in leaderboard 
--		ex: user_games_id will represent A player in AN instance of a game
--
--	commenting out all pkfks for easier drawing later / creating new relationships

--drop database if it exists
IF DB_ID('redesign_final_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE redesign_final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE redesign_final_capstone;
END

CREATE DATABASE redesign_final_capstone
GO

USE redesign_final_capstone
GO

--create tables
CREATE TABLE users (
	user_id			int				IDENTITY(1,1)	NOT NULL,
	username		varchar(50)						NOT NULL,
	password_hash	varchar(200)					NOT NULL,
	salt			varchar(200)					NOT NULL,
	user_role		varchar(50)						NOT NULL,
--	email			varchar(50)						NOT NULL could add email address for potential reset accoutn send invite in future

	CONSTRAINT PK_users PRIMARY KEY (user_id)
);

CREATE TABLE games (
	game_id			INT				IDENTITY(1,1)	not null,
	game_name		VARCHAR(100)					not null,
	game_organizer	VARCHAR(100)					not null,
	start_date		DATETIME						not null,
	end_date		DATETIME						not null,
	winner			VARCHAR(100)					,
	isCompleted		BIT								not null  	--could make a isCompleted join table? completion_status with an id and status 'finished' or 'in progress'

	CONSTRAINT PK_game PRIMARY KEY (game_id)
)

--renaming table to game_users
CREATE TABLE game_users (
	game_user_id	INT			IDENTITY(1,1)	not null, --added games_user_id for primary key to identify a user in a games: portfolio and trades
	user_id		    INT							not null, --changing this one key from player_id to user_id
	game_id			INT							not null

	CONSTRAINT pk_game_user_id			PRIMARY KEY (game_user_id),
	CONSTRAINT fk_game_users_user_id	FOREIGN KEY (user_id)			REFERENCES users(user_id),
	CONSTRAINT fk_game_users_game		FOREIGN KEY (game_id)			REFERENCES games(game_id) --commented out as not working correctly
	--CONSTRAINT fk_game_player_game			FOREIGN KEY (game_id)	REFERENCES games(game_id)
)


CREATE TABLE portfolios (
	portfolio_id	int			IDENTITY(1,1)	not null,
	game_user_id	int							not null,
	balance			decimal						not null,
	asset_balance	decimal						
	
	CONSTRAINT	pk_portfolio_id				PRIMARY KEY (portfolio_id),
	CONSTRAINT	fk_portfolios_game_user_id	FOREIGN KEY (game_user_id)	REFERENCES game_users(game_user_id)
)


CREATE TABLE active_stocks (
	portfolio_id		INT			not null,
	stock_symbol		VARCHAR(10) not null,
	stock_name			VARCHAR(50) not null,
	quantity			INT			not null

	CONSTRAINT pk_portfolio_id_stock_symbol		PRIMARY KEY (portfolio_id, stock_symbol),
	CONSTRAINT fk_portfolio_id					FOREIGN KEY (portfolio_id) REFERENCES portfolios(portfolio_id)
)


CREATE TABLE trades (
	trade_id			INT				IDENTITY(1,1)	not null,
	portfolio_id		INT								not null,
	stock_symbol		VARCHAR(10)						not null,
	stock_name			VARCHAR(50)						not null,
	stock_price			DECIMAL							not null,
	trade_type			INT								not null,
	quantity			INT								not null

	CONSTRAINT pk_trades_trade_id		PRIMARY KEY (trade_id),
	CONSTRAINT fk_trades_portfolio_id	FOREIGN KEY (portfolio_id)	REFERENCES portfolios(portfolio_id)
)

--populate default data **DO NOT DELETE FOR NOW... SEE BELOW... JUST MOVED TO LINES 111 and 112
--INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
--INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');
--

INSERT INTO users (username, password_hash, salt, user_role) VALUES ('UNICRON','Jg45HuwT7PZkfuKTz6IB90CtWY4=', 'LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('TRINITY','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('NEO', 'Jg45HuwT7PZkfuKTz6IB90CtWY4=', 'LHxP4Xh7bN0=', 'user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('LUKE','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('PARZIVAL', 'Jg45HuwT7PZkfuKTz6IB90CtWY4=', 'LHxP4Xh7bN0=', 'user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('OPTIMUS','Jg45HuwT7PZkfuKTz6IB90CtWY4=', 'LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('JORDAN BELFORT','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('MICHAEL BURRY', 'Jg45HuwT7PZkfuKTz6IB90CtWY4=', 'LHxP4Xh7bN0=', 'user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('WARREN BUFFET','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('GEORGE SOROS', 'Jg45HuwT7PZkfuKTz6IB90CtWY4=', 'LHxP4Xh7bN0=', 'user');


INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');


INSERT INTO games (game_name, game_organizer, start_date, end_date, isCompleted) VALUES ('Masters of the Universe', 'GEORGE SOROS', '2016-05-05 12:00:00', '2016-06-05 12:00:00', 0);
INSERT INTO games (game_name, game_organizer, start_date, end_date, winner, isCompleted) VALUES ('Fish and Chips', 'LUKE', '2015-05-05 12:00:00', '2016-06-05 12:00:00', 'PARZIVAL', 1);
INSERT INTO games (game_name, game_organizer, start_date, end_date, winner, isCompleted) VALUES ('Master and Commander', 'MICHAEL BURRY', '2019-05-05 12:00:00', '2021-06-05 12:00:00', 'UNICRON', 1);
INSERT INTO games (game_name, game_organizer, start_date, end_date, winner, isCompleted) VALUES ('FRED', 'NEO', '2016-05-05 12:00:00', '2016-06-05 12:00:00', 'LUKE', 1);
INSERT INTO games (game_name, game_organizer, start_date, end_date, winner, isCompleted) VALUES ('Space Crusaders', 'UNICRON', '2016-05-05 12:00:00', '2016-06-05 12:00:00', 'WARREN BUFFET', 1);
INSERT INTO games (game_name, game_organizer, start_date, end_date, winner, isCompleted) VALUES ('Frogger', 'MICHAEL BURRY', '2021-07-05 12:00:00', '2021-12-23 12:00:00', 'WARREN BUFFET', 1);



                    --CURRENT GAME--

INSERT INTO game_users (user_id, game_id) VALUES (10, 1);
INSERT INTO game_users (user_id, game_id) VALUES (9, 1);
INSERT INTO game_users (user_id, game_id) VALUES (8, 1);
INSERT INTO game_users (user_id, game_id) VALUES (7, 1);
INSERT INTO game_users (user_id, game_id) VALUES (6, 1);
INSERT INTO game_users (user_id, game_id) VALUES (5, 1);
INSERT INTO game_users (user_id, game_id) VALUES (4, 1);
INSERT INTO game_users (user_id, game_id) VALUES (3, 1);
INSERT INTO game_users (user_id, game_id) VALUES (2, 1);
INSERT INTO game_users (user_id, game_id) VALUES (1, 1);

					--PAST GAME--

INSERT INTO game_users (user_id, game_id) VALUES (10, 6);
INSERT INTO game_users (user_id, game_id) VALUES (9, 6);
INSERT INTO game_users (user_id, game_id) VALUES (8, 6);
INSERT INTO game_users (user_id, game_id) VALUES (7, 6);
INSERT INTO game_users (user_id, game_id) VALUES (6, 6);
INSERT INTO game_users (user_id, game_id) VALUES (5, 6);
INSERT INTO game_users (user_id, game_id) VALUES (4, 6);
INSERT INTO game_users (user_id, game_id) VALUES (3, 6);
INSERT INTO game_users (user_id, game_id) VALUES (2, 6);
INSERT INTO game_users (user_id, game_id) VALUES (1, 6);



				--BALANCES FOR CURRENT GAME--

INSERT INTO portfolios (game_user_id, balance) VALUES (1, 175000.00);
INSERT INTO portfolios (game_user_id, balance) VALUES (2, 105000.00);
INSERT INTO portfolios (game_user_id, balance) VALUES (3, 223343.00);
INSERT INTO portfolios (game_user_id, balance) VALUES (4, 354998.00);
INSERT INTO portfolios (game_user_id, balance) VALUES (5, 60978.00);
INSERT INTO portfolios (game_user_id, balance) VALUES (6, 203999.00);
INSERT INTO portfolios (game_user_id, balance) VALUES (7, 40000.00);
INSERT INTO portfolios (game_user_id, balance) VALUES (8, 120000.00);
INSERT INTO portfolios (game_user_id, balance) VALUES (9, 600000.00);
INSERT INTO portfolios (game_user_id, balance) VALUES (10, 710.00);


INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (1,'TSLA','Tesla inc', 4);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (1,'AAPL','Apple inc', 2);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (1,'NFLX','Netflix inc', 7);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (1,'GOOG','Alphabet inc', 2);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (1,'PYPL','PayPal Holdings inc', 4);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (1,'PPG','PPG Industries inc', 2);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (1,'AMZN','Amazon.com inc', 7);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (1,'TM','Toyota Motor Corp', 20);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (1,'COP','Conoco Phillips', 10);

INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (2,'TSLA','Tesla inc', 40);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (2,'AAPL','Apple inc', 2);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (2,'NFLX','Netflix inc', 1);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (2,'GOOG','Alphabet inc', 1);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (2,'PYPL','PayPal Holdings inc', 6);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (2,'PPG','PPG Industries inc', 2);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (2,'AMZN','Amazon.com inc', 7);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (2,'TM','Toyota Motor Corp', 2);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (2,'COP','Conoco Phillips', 11);

INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (3,'TSLA','Tesla inc', 15);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (3,'AAPL','Apple inc', 2);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (3,'NFLX','Netflix inc', 10);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (3,'GOOG','Alphabet inc', 2);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (3,'PYPL','PayPal Holdings inc', 7);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (3,'PPG','PPG Industries inc', 11);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (3,'AMZN','Amazon.com inc', 6);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (3,'TM','Toyota Motor Corp', 8);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (3,'COP','Conoco Phillips', 11);

INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (4,'TSLA','Tesla inc', 4);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (4,'AAPL','Apple inc', 1);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (4,'NFLX','Netflix inc', 1);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (4,'GOOG','Alphabet inc', 1);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (4,'PYPL','PayPal Holdings inc', 1);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (4,'PPG','PPG Industries inc', 1);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (4,'AMZN','Amazon.com inc', 1);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (4,'TM','Toyota Motor Corp', 1);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (4,'COP','Conoco Phillips', 110);

INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (5,'TSLA','Tesla inc', 70);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (5,'AAPL','Apple inc', 55);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (5,'NFLX','Netflix inc', 8);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (5,'GOOG','Alphabet inc', 8);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (5,'PYPL','PayPal Holdings inc', 6);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (5,'PPG','PPG Industries inc', 8);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (5,'AMZN','Amazon.com inc', 8);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (5,'TM','Toyota Motor Corp', 8);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (5,'COP','Conoco Phillips', 22);

INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (6,'TSLA','Tesla inc', 44);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (6,'AAPL','Apple inc', 1);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (6,'NFLX','Netflix inc', 1);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (6,'GOOG','Alphabet inc', 1);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (6,'PYPL','PayPal Holdings inc', 1);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (6,'PPG','PPG Industries inc', 1);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (6,'AMZN','Amazon.com inc', 66);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (6,'TM','Toyota Motor Corp', 2);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (6,'COP','Conoco Phillips', 1);

INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (7,'TSLA','Tesla inc', 44);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (7,'AAPL','Apple inc', 2);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (7,'NFLX','Netflix inc', 7);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (7,'GOOG','Alphabet inc', 2);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (7,'PYPL','PayPal Holdings inc', 4);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (7,'PPG','PPG Industries inc', 22);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (7,'AMZN','Amazon.com inc', 7);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (7,'TM','Toyota Motor Corp', 9);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (7,'COP','Conoco Phillips', 10);

INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (8,'TSLA','Tesla inc', 20);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (8,'AAPL','Apple inc', 20);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (8,'NFLX','Netflix inc', 20);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (8,'GOOG','Alphabet inc', 20);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (8,'PYPL','PayPal Holdings inc', 20);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (8,'PPG','PPG Industries inc', 20);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (8,'AMZN','Amazon.com inc', 20);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (8,'TM','Toyota Motor Corp', 20);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (8,'COP','Conoco Phillips', 20);

INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (9,'TSLA','Tesla inc', 14);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (9,'AAPL','Apple inc', 12);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (9,'NFLX','Netflix inc', 17);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (9,'GOOG','Alphabet inc', 12);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (9,'PYPL','PayPal Holdings inc', 14);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (9,'PPG','PPG Industries inc', 12);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (9,'AMZN','Amazon.com inc', 17);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (9,'TM','Toyota Motor Corp', 20);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (9,'COP','Conoco Phillips', 10);

INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (10,'TSLA','Tesla inc', 9);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (10,'AAPL','Apple inc', 2);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (10,'NFLX','Netflix inc', 100);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (10,'GOOG','Alphabet inc', 1);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (10,'PYPL','PayPal Holdings inc', 6);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (10,'PPG','PPG Industries inc', 2);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (10,'AMZN','Amazon.com inc', 7);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (10,'TM','Toyota Motor Corp', 2);
INSERT INTO active_stocks (portfolio_id, stock_symbol, stock_name, quantity) VALUES (10,'COP','Conoco Phillips', 11);


GO


SELECT * FROM portfolios;
SELECT * FROM game_users;
SELECT * FROM games;
SELECT * FROM users;
SELECT * FROM active_stocks;
SELECT * FROM active_stocks WHERE portfolio_id = 1;

SELECT * FROM active_stocks;

SELECT * FROM active_stocks WHERE portfolio_id = 1;

SELECT game_name, game_organizer, start_date, end_date
FROM games
WHERE game_id = 1

SELECT portfolio_id FROM portfolios P
JOIN game_users GU ON GU.game_user_id = P.game_user_id
JOIN users U ON U.user_id = GU.user_id 
JOIN games G ON G.game_id = GU.game_id
WHERE U.username = 'UNICRON'AND G.game_id = 1;

SELECT distinct G.game_id, G.game_name, G.end_date, G.game_organizer, G.start_date, G.winner, G.isCompleted FROM games G
JOIN game_users GU ON GU.game_id = G.game_id
JOIN users U ON GU.user_id = GU.user_id
WHERE U.username = 'Unicron' AND isCompleted = 0;


