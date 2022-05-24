USE master
GO

--drop database if it exists
IF DB_ID('going_infinite') IS NOT NULL
BEGIN
	ALTER DATABASE going_infinite SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE going_infinite;
END

CREATE DATABASE going_infinite
GO

USE going_infinite
GO

--create tables
CREATE TABLE players (
	player_id int IDENTITY(1,1) NOT NULL,
	username varchar(100) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	PRIMARY KEY(player_id)
);

CREATE TABLE decks (
	deck_id int IDENTITY(1,1) NOT NULL,
	player_id int NOT NULL,
	PRIMARY KEY(deck_id),
	FOREIGN KEY(player_id) REFERENCES players(player_id)
);

CREATE TABLE fnm (
	event_id int IDENTITY(1,1) NOT NULL,
	event_date date NOT NULL,
	lgs varchar (100) NOT NULL,
	PRIMARY KEY(event_id)
);

CREATE TABLE matches (
	match_id int IDENTITY(1,1) NOT NULL,
	player_id int NOT NULL,
	deck_id int NOT NULL,
	event_id int NOT NULL,
	opponent varchar(50) NOT NULL,
	outcome varchar(4) NOT NULL,
	PRIMARY KEY (match_id),
	FOREIGN KEY(player_id) REFERENCES players(player_id),
	FOREIGN KEY(deck_id) REFERENCES decks(deck_id),
	FOREIGN KEY(event_id) REFERENCES fnm(event_id)
);

CREATE TABLE games (
	game_id int IDENTITY(1,1) NOT NULL,
	match_id int NOT NULL,
	outcome varchar(4) NOT NULL,
	PRIMARY KEY(game_id),
	FOREIGN KEY(match_id) REFERENCES matches(match_id)
);

CREATE TABLE cards_in_deck (
	deck_id int NOT NULL,
	card_id varchar(40) NOT NULL,
	number_of_cards int NOT NULL,
	PRIMARY KEY(deck_id, card_id)
);

CREATE TABLE cards (
	card_id varchar(40) NOT NULL,
	name varchar(200) NOT NULL,
	rarity varchar(11) NOT NULL,
	mana_cost varchar(50) NOT NULL,
	cmc decimal(8,1) NOT NULL,
	type_line varchar(80) NOT NULL,
	oracle_text varchar(800) NOT NULL,
	colors varchar(10) NOT NULL,
	color_identity varchar(10) NOT NULL,
	keywords varchar(200) NOT NULL,
	set_code varchar(5) NOT NULL,
	set_name varchar(100) NOT NULL,
	image varchar(150) NOT NULL,
	art_crop varchar(150) NOT NULL,
	PRIMARY KEY(card_id)
);