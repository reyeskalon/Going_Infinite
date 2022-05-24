SELECT * FROM decks WHERE player_id = 1;
SELECT * FROM fnm;
SELECT * FROM matches;
SELECT * FROM games;
SELECT * FROM fnm WHERE event_id = 2;
SELECT * FROM fnm WHERE event_id = 1;
SELECT * FROM matches;
SELECT * FROM decks;
SELECT * FROM cards_in_deck WHERE deck_id = 1;

SELECT * FROM cards_in_deck WHERE deck_id = 2;

SELECT * FROM cards WHERE card_id = '08f33c8a-8e93-4296-964b-da132a854b3b';

SELECT DISTINCT fnm.event_id, event_date, lgs FROM fnm
JOIN matches ON fnm.event_id = matches.event_id
WHERE player_id = 1;

SELECT game_id, games.match_id, games.outcome FROM games
JOIN matches ON games.match_id = matches.match_id
WHERE matches.match_id = 1;

SELECT game_id, games.match_id, games.outcome FROM games
JOIN matches ON games.match_id = matches.match_id
WHERE matches.player_id = 1;

SELECT * FROM matches;
SELECT DISTINCT deck_id FROM matches WHERE event_id = 2 AND player_id = 1;

SELECT game_id, games.match_id, games.outcome FROM games
JOIN matches ON games.match_id = matches.match_id
WHERE matches.event_id = 1;