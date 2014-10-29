go
USE master
IF EXISTS(select * from sys.databases where name='contactsDB')
DROP DATABASE contactsDB

CREATE DATABASE contactsDB

go
use contactsDB
CREATE TABLE personnes(
id int PRIMARY KEY IDENTITY(1,1) NOT NULL ,
password VARCHAR(MAX) null,
nom VARCHAR(MAX) not null,
compagnie VARCHAR(MAX) null,
celulaire VARCHAR(12) null,
telephone VARCHAR(12) null,
siteWeb VARCHAR(MAX) null,
courriel VARCHAR(MAX) null,
aniversaire DATETIME null,
lastVisit DATETIME null,
adresse VARCHAR(MAX) null,
ville VARCHAR(MAX) null,
province VARCHAR(MAX) null,
pays VARCHAR(MAX) null,
urlPhoto VARCHAR(MAX) null,
isUser bit not null Default 0,
isVisible bit not null Default 0
)
go
use contactsDB
CREATE TABLE contacts(
  idUser int NOT NULL,
  idContact int NOT NULL,
  isFavorite bit NOT NULL DEFAULT 0,
  PRIMARY KEY (idUser, idContact),
  CONSTRAINT fk_idUser FOREIGN KEY (idUser) REFERENCES personnes(id),
  CONSTRAINT fk_idContact FOREIGN KEY (idContact) REFERENCES personnes(id)
)

go
use contactsDB
insert into personnes (password, nom, compagnie, celulaire, telephone, siteWeb, courriel, aniversaire, lastVisit, adresse, ville, province, pays, urlPhoto, isUser, isVisible)
	values('12345','Raphael Franco','mobilemidia','15145788147','17160563','www.mobile-midia.com','candido@mobile-midia.com', '19830730', '20140901', '135, Rue sherbrooke est', 'montreal', 'quebec', 'canada', 'images\photoProfile.png', 1, 1)
insert into personnes (password, nom, compagnie, celulaire, telephone, siteWeb, courriel, aniversaire, lastVisit, adresse, ville, province, pays, urlPhoto, isUser, isVisible)
	values('12345','Taher','mobilemidia','15145788147','17160563','www.mobile-midia.com','Taher@gmail.com','19830730', '20140901', '135, Rue sherbrooke est', 'montreal', 'quebec', 'canada', 'images\photoProfile.png', 1, 1)
insert into personnes (password, nom, compagnie, celulaire, telephone, siteWeb, courriel, aniversaire, lastVisit, adresse, ville, province, pays, urlPhoto, isUser, isVisible)
	values( '12345','Jf','mobilemidia','15145788147','17160563','www.mobile-midia.com','Jf@gmail.com', '19830730', '20140901', '135, Rue sherbrooke est', 'montreal', 'quebec', 'canada', 'images\photoProfile.png', 1, 1)
insert into personnes (password, nom, compagnie, celulaire, telephone, siteWeb, courriel, aniversaire, lastVisit, adresse, ville, province, pays, urlPhoto, isUser, isVisible)
	values( '12345','Mamoudou','mobilemidia','15145788147','17160563','www.mobile-midia.com','Mamoudou@gmail.com', '19830730', '20140901', '135, Rue sherbrooke est', 'montreal', 'quebec', 'canada', 'images\photoProfile.png', 1, 1)
insert into personnes (password, nom, compagnie, celulaire, telephone, siteWeb, courriel, aniversaire, lastVisit, adresse, ville, province, pays, urlPhoto, isUser, isVisible)
	values( '12345','Xue','mobilemidia','15145788147','17160563','www.mobile-midia.com','Xue@gmail.com', '19830730', '20140901', '135, Rue sherbrooke est', 'montreal', 'quebec', 'canada', 'images\photoProfile.png', 1, 1)
insert into personnes (password, nom, compagnie, celulaire, telephone, siteWeb, courriel, aniversaire, lastVisit, adresse, ville, province, pays, urlPhoto, isUser, isVisible)
	values( '12345','Marc-André','mobilemidia','15145788147','17160563','www.mobile-midia.com','Marc@gmail.com', '19830730', '20140901', '135, Rue sherbrooke est', 'montreal', 'quebec', 'canada', 'images\photoProfile.png', 1, 1)
insert into personnes (password, nom, compagnie, celulaire, telephone, siteWeb, courriel, aniversaire, lastVisit, adresse, ville, province, pays, urlPhoto, isUser, isVisible)
	values( '12345','Marco','mobilemidia','15145788147','17160563','www.mobile-midia.com','Marcos@gmail.com', '19830730', '20140901', '135, Rue sherbrooke est', 'montreal', 'quebec', 'canada', 'images\photoProfile.png', 1, 1)
	insert into personnes (password, nom, compagnie, celulaire, telephone, siteWeb, courriel, aniversaire, lastVisit, adresse, ville, province, pays, urlPhoto, isUser, isVisible)
	values( '12345','Stephanne','mobilemidia','15145788147','17160563','www.mobile-midia.com','Stephanne@gmail.com', '19830730', '20140901', '135, Rue sherbrooke est', 'montreal', 'quebec', 'canada', 'images\photoProfile.png', 1, 1)
	insert into personnes (password, nom, compagnie, celulaire, telephone, siteWeb, courriel, aniversaire, lastVisit, adresse, ville, province, pays, urlPhoto, isUser, isVisible)
	values( '12345','kevin','mobilemidia','15145788147','17160563','www.mobile-midia.com','kevin@gmail.com', '19830730', '20140901', '135, Rue sherbrooke est', 'montreal', 'quebec', 'canada', 'images\photoProfile.png', 1, 1)
	insert into personnes (password, nom, compagnie, celulaire, telephone, siteWeb, courriel, aniversaire, lastVisit, adresse, ville, province, pays, urlPhoto, isUser, isVisible)
	values( '12345','Marie','mobilemidia','15145788147','17160563','www.mobile-midia.com','Marie@gmail.com', '19830730', '20140901', '135, Rue sherbrooke est', 'montreal', 'quebec', 'canada', 'images\photoProfile.png', 1, 1)
	insert into personnes (password, nom, compagnie, celulaire, telephone, siteWeb, courriel, aniversaire, lastVisit, adresse, ville, province, pays, urlPhoto, isUser, isVisible)
	values( '12345','Marion','mobilemidia','15145788147','17160563','www.mobile-midia.com','Marion@gmail.com', '19830730', '20140901', '135, Rue sherbrooke est', 'montreal', 'quebec', 'canada', 'images\photoProfile.png', 1, 1)
	insert into personnes (password, nom, compagnie, celulaire, telephone, siteWeb, courriel, aniversaire, lastVisit, adresse, ville, province, pays, urlPhoto, isUser, isVisible)
	values( '12345','Alex','mobilemidia','15145788147','17160563','www.mobile-midia.com','Alex@gmail.com', '19830730', '20140901', '135, Rue sherbrooke est', 'montreal', 'quebec', 'canada', 'images\photoProfile.png', 1, 1)
	insert into personnes (password, nom, compagnie, celulaire, telephone, siteWeb, courriel, aniversaire, lastVisit, adresse, ville, province, pays, urlPhoto, isUser, isVisible)
	values( '12345','David','mobilemidia','15145788147','17160563','www.mobile-midia.com','David@gmail.com', '19830730', '20140901', '135, Rue sherbrooke est', 'montreal', 'quebec', 'canada', 'images\photoProfile.png', 1, 1)
	insert into personnes (password, nom, compagnie, celulaire, telephone, siteWeb, courriel, aniversaire, lastVisit, adresse, ville, province, pays, urlPhoto, isUser, isVisible)
	values( '12345','Raf','mobilemidia','15145788147','17160563','www.mobile-midia.com','Raf@gmail.com', '19830730', '20140901', '135, Rue sherbrooke est', 'montreal', 'quebec', 'canada', 'images\photoProfile.png', 1, 1)
	go
	use contactsDB

insert into contacts (idUser, idContact, isFavorite) values(1,2,0)
insert into contacts (idUser, idContact, isFavorite) values(1,3,0)
insert into contacts (idUser, idContact, isFavorite) values(1,5,1)
insert into contacts (idUser, idContact, isFavorite) values(1,6,1)
insert into contacts (idUser, idContact, isFavorite) values(1,7,1)
insert into contacts (idUser, idContact, isFavorite) values(1,8,1)
insert into contacts (idUser, idContact, isFavorite) values(1,9,1)
insert into contacts (idUser, idContact, isFavorite) values(1,10,0)
insert into contacts (idUser, idContact, isFavorite) values(1,11,1)
insert into contacts (idUser, idContact, isFavorite) values(1,12,0)
insert into contacts (idUser, idContact, isFavorite) values(1,13,1)
insert into contacts (idUser, idContact, isFavorite) values(1,14,0)
go
	use contactsDB

insert into contacts (idUser, idContact, isFavorite) values(2,3,0)
insert into contacts (idUser, idContact, isFavorite) values(2,4,0)
insert into contacts (idUser, idContact, isFavorite) values(2,5,1)
insert into contacts (idUser, idContact, isFavorite) values(2,6,1)
go
	use contactsDB
insert into contacts (idUser, idContact, isFavorite) values(3,2,1)
insert into contacts (idUser, idContact, isFavorite) values(3,4,0)
insert into contacts (idUser, idContact, isFavorite) values(3,5,1)
insert into contacts (idUser, idContact, isFavorite) values(3,6,1)
insert into contacts (idUser, idContact, isFavorite) values(3,1,1)
go
	use contactsDB
select * from personnes
select * from contacts
select*from contacts where idUser='1'
select * from personnes as p inner join contacts as c on p.id = c.idUser where c.idUser='1'