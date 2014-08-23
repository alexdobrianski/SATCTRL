use MissionLog;

DROP TABLE mission_session;

CREATE TABLE IF NOT EXISTS mission_session
(
	session_no 		VARCHAR(10) 	NOT NULL,
	packet_type		VARCHAR(1)		NOT NULL,
	packet_no		VARCHAR(5)		NOT NULL,
	d_time			VARCHAR(21)		NOT NULL,
	g_station		VARCHAR(1)		NOT NULL,
	gs_time			VARCHAR(21)		NULL,
	package			VARCHAR(512)	NULL
);