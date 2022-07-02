CREATE DATABASE InstagramDB
DROP DATABASE InstagramDB

USE InstagramDB
GO

CREATE TABLE  Account
(
accountId         INT IDENTITY PRIMARY KEY,
Fname             VARCHAR(50)   NOT NULL,
Lname             VARCHAR(50)   NOT NULL,
Email             VARCHAR(50)   NOT NULL,
Password          VARCHAR(50)   NOT NULL,
profile_pic       VARCHAR(2000) NOT NULL,
);


CREATE TABLE Post
(
postId            INT IDENTITY PRIMARY KEY NOT NULL,
post_pic          VARCHAR(250)             NOT NULL,
content           VARCHAR (100)            NOT NULL,
fk_Acount_id      INT FOREIGN KEY REFERENCES Account (accountId)
);


CREATE TABLE Friend_Request (
requestId         INT IDENTITY PRIMARY KEY NOT NULL,
reciever          INT FOREIGN KEY REFERENCES Account (accountId),
sender            INT FOREIGN KEY REFERENCES Account (accountId)
);


CREATE TABLE ApprovedFriend (
friendId          INT IDENTITY PRIMARY KEY NOT NULL,
mainuser          INT FOREIGN KEY REFERENCES Account (accountId),
frienduser        INT FOREIGN KEY REFERENCES Account (accountId)
);


CREATE TABLE Comment (
commentId         INT IDENTITY PRIMARY KEY NOT NULL,
comment           NVARCHAR (250) NULL,
postId            INT FOREIGN KEY REFERENCES Post ([postId]),
accountId         INT FOREIGN KEY REFERENCES Account (accountId)
);


CREATE TABLE Likes (
likeId            INT IDENTITY PRIMARY KEY NOT NULL,
taha              INT NULL,
accountId         INT FOREIGN KEY REFERENCES Account (accountId),
postId            INT FOREIGN KEY REFERENCES Post (postId)
);


CREATE TABLE DisLikes (
dislikeId         INT IDENTITY PRIMARY KEY NOT NULL,
aers              INT NULL,
accountId         INT FOREIGN KEY REFERENCES Account (accountId),
postId            INT FOREIGN KEY REFERENCES Post (postId)
);