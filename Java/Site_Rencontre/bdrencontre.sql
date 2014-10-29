-- phpMyAdmin SQL Dump
-- version 4.1.14
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Jul 22, 2014 at 01:25 AM
-- Server version: 5.6.17
-- PHP Version: 5.5.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `bdrencontre`
--
CREATE DATABASE IF NOT EXISTS `bdrencontre` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `bdrencontre`;

-- --------------------------------------------------------

--
-- Table structure for table `categorie`
--

DROP TABLE IF EXISTS `categorie`;
CREATE TABLE IF NOT EXISTS `categorie` (
  `categorieId` bigint(20) NOT NULL AUTO_INCREMENT,
  `description` varchar(40) DEFAULT NULL,
  PRIMARY KEY (`categorieId`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=6 ;

--
-- Dumping data for table `categorie`
--

INSERT INTO `categorie` (`categorieId`, `description`) VALUES
(1, 'homme '),
(2, 'femme '),
(3, 'couple'),
(4, 'bisexuel'),
(5, 'transexuel');

-- --------------------------------------------------------

--
-- Table structure for table `city`
--

DROP TABLE IF EXISTS `city`;
CREATE TABLE IF NOT EXISTS `city` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(20) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=9 ;

--
-- Dumping data for table `city`
--

INSERT INTO `city` (`id`, `nom`) VALUES
(1, 'Montréal'),
(2, 'Québec'),
(3, 'Vancouver'),
(4, 'Ottawa'),
(5, 'Toronto'),
(6, 'Laval'),
(7, 'Sherbrooke'),
(8, 'Longueil');

-- --------------------------------------------------------

--
-- Table structure for table `clinsdoeil`
--

DROP TABLE IF EXISTS `clinsdoeil`;
CREATE TABLE IF NOT EXISTS `clinsdoeil` (
  `clindoeilid` bigint(20) NOT NULL AUTO_INCREMENT,
  `fromId` bigint(20) NOT NULL,
  `toId` bigint(20) NOT NULL,
  `dateEnvoi` datetime DEFAULT NULL,
  PRIMARY KEY (`clindoeilid`),
  KEY `fromId` (`fromId`),
  KEY `toId` (`toId`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=35 ;

--
-- Dumping data for table `clinsdoeil`
--

INSERT INTO `clinsdoeil` (`clindoeilid`, `fromId`, `toId`, `dateEnvoi`) VALUES
(1, 1, 2, '2014-06-11 03:11:14'),
(2, 2, 5, '2014-07-01 04:18:31'),
(3, 3, 5, '2014-07-08 04:16:17'),
(4, 5, 6, '2014-07-05 09:20:06'),
(5, 1, 6, '2014-07-02 00:00:00'),
(6, 1, 6, '2011-02-17 16:22:25'),
(7, 4, 1, '2014-07-04 12:09:35'),
(8, 6, 3, '2014-06-04 05:20:00'),
(9, 6, 4, '2014-07-01 03:07:15'),
(10, 4, 5, '2014-07-10 05:13:15'),
(12, 9, 10, '2014-07-10 06:00:00'),
(13, 11, 12, '2014-07-05 00:19:12'),
(14, 8, 2, '2014-07-11 04:00:13'),
(15, 10, 7, '2014-07-09 03:00:13'),
(16, 12, 9, '2014-07-03 00:18:00'),
(17, 9, 11, '2014-07-02 00:13:00'),
(18, 8, 11, '2014-07-17 19:48:17'),
(19, 8, 9, '2014-07-17 19:49:30'),
(20, 8, 9, '2014-07-17 19:49:31'),
(25, 8, 7, '2014-07-18 15:35:26'),
(27, 8, 2, '2014-07-18 18:55:01'),
(28, 8, 1, '2014-07-19 18:33:46'),
(29, 8, 2, '2014-07-19 18:34:15'),
(30, 8, 5, '2014-07-19 18:36:41'),
(31, 8, 5, '2014-07-19 18:37:05'),
(32, 1, 8, '2014-07-20 00:58:52'),
(33, 10, 4, '2014-07-20 17:31:48'),
(34, 10, 4, '2014-07-20 17:31:50');

-- --------------------------------------------------------

--
-- Table structure for table `eye_color`
--

DROP TABLE IF EXISTS `eye_color`;
CREATE TABLE IF NOT EXISTS `eye_color` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `description` varchar(20) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=4 ;

--
-- Dumping data for table `eye_color`
--

INSERT INTO `eye_color` (`id`, `description`) VALUES
(1, 'bleu'),
(2, 'vert'),
(3, 'brun');

-- --------------------------------------------------------

--
-- Table structure for table `favoris`
--

DROP TABLE IF EXISTS `favoris`;
CREATE TABLE IF NOT EXISTS `favoris` (
  `favorisID` bigint(20) NOT NULL AUTO_INCREMENT,
  `dateAjout` datetime DEFAULT NULL,
  `membreFavorisant` bigint(20) NOT NULL COMMENT 'le propriétaire de ce favori',
  `membreFavorise` bigint(20) NOT NULL COMMENT 'le membre ayant été ajouté comme favori',
  PRIMARY KEY (`favorisID`),
  KEY `membreFavorisant` (`membreFavorisant`),
  KEY `membreFavorise` (`membreFavorise`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=34 ;

--
-- Dumping data for table `favoris`
--

INSERT INTO `favoris` (`favorisID`, `dateAjout`, `membreFavorisant`, `membreFavorise`) VALUES
(1, '2014-07-01 04:08:08', 1, 2),
(2, '2014-07-03 04:09:08', 2, 3),
(3, '2014-07-04 02:11:07', 4, 5),
(4, '2014-07-09 01:07:13', 6, 1),
(5, '2014-07-07 10:15:16', 3, 4),
(6, '2014-07-09 06:11:00', 5, 6),
(7, '2014-07-02 17:40:30', 1, 3),
(8, '2014-07-03 03:07:36', 2, 6),
(9, '2014-07-10 10:11:18', 4, 1),
(10, '2014-07-04 00:00:00', 3, 2),
(11, '2014-07-02 04:11:23', 7, 8),
(12, '2014-07-05 04:16:11', 9, 10),
(13, '2014-07-10 06:16:16', 11, 12),
(15, '2014-07-03 06:00:12', 12, 11),
(25, '2014-07-17 00:00:00', 8, 11),
(27, '2014-07-17 00:00:00', 8, 2),
(30, '2014-07-18 19:24:51', 8, 1),
(31, '2014-07-19 18:37:22', 8, 5),
(33, '2014-07-20 17:03:03', 10, 4);

-- --------------------------------------------------------

--
-- Table structure for table `hair_color`
--

DROP TABLE IF EXISTS `hair_color`;
CREATE TABLE IF NOT EXISTS `hair_color` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `description` varchar(20) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=8 ;

--
-- Dumping data for table `hair_color`
--

INSERT INTO `hair_color` (`id`, `description`) VALUES
(1, 'noir'),
(2, 'rouge'),
(3, 'blond'),
(4, 'blanc'),
(5, 'chauve'),
(6, 'brun'),
(7, 'girs');

-- --------------------------------------------------------

--
-- Table structure for table `height_range`
--

DROP TABLE IF EXISTS `height_range`;
CREATE TABLE IF NOT EXISTS `height_range` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `start_range` int(11) NOT NULL,
  `end_range` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=5 ;

--
-- Dumping data for table `height_range`
--

INSERT INTO `height_range` (`id`, `start_range`, `end_range`) VALUES
(1, 130, 150),
(2, 151, 170),
(3, 171, 190),
(4, 191, 250);

-- --------------------------------------------------------

--
-- Table structure for table `hobby`
--

DROP TABLE IF EXISTS `hobby`;
CREATE TABLE IF NOT EXISTS `hobby` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `description` varchar(20) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=11 ;

--
-- Dumping data for table `hobby`
--

INSERT INTO `hobby` (`id`, `description`) VALUES
(1, 'Musique'),
(2, 'Films'),
(3, 'Théâtre'),
(4, 'Art'),
(5, 'Ordinateur'),
(6, 'Sport'),
(7, 'Photographie'),
(8, 'Animaux'),
(9, 'Danse'),
(10, 'Livres');

-- --------------------------------------------------------

--
-- Table structure for table `membre`
--

DROP TABLE IF EXISTS `membre`;
CREATE TABLE IF NOT EXISTS `membre` (
  `membreId` bigint(20) NOT NULL AUTO_INCREMENT,
  `nom` varchar(40) NOT NULL,
  `prenom` varchar(40) NOT NULL,
  `age` int(3) NOT NULL,
  `description` varchar(3000) DEFAULT NULL,
  `categorieId` bigint(20) NOT NULL,
  `isOnline` tinyint(1) DEFAULT '0',
  `niveauMembreId` bigint(20) NOT NULL,
  `lastVisit` datetime DEFAULT NULL,
  `email` varchar(50) NOT NULL,
  `password` varchar(15) NOT NULL,
  `pseudo` varchar(15) NOT NULL,
  `sexe` tinyint(1) DEFAULT NULL,
  `notification` bit(1) DEFAULT NULL,
  `hair_color_id` int(11) NOT NULL,
  `skin_color_id` int(11) NOT NULL,
  `eye_color_id` int(11) NOT NULL,
  `height` int(11) NOT NULL,
  `weight_range_id` int(11) NOT NULL,
  `city_id` int(11) NOT NULL,
  `smoke` tinyint(1) NOT NULL,
  `status_id` int(11) NOT NULL,
  `informed_message_received` tinyint(4) NOT NULL,
  `informed_added_by_others` tinyint(4) NOT NULL,
  `informed_removed_by_others` tinyint(4) NOT NULL,
  PRIMARY KEY (`membreId`,`pseudo`),
  KEY `fk_membre_categorie` (`categorieId`),
  KEY `fk_membre_niveauxMembres` (`niveauMembreId`),
  KEY `hair_color_id` (`hair_color_id`),
  KEY `skin_color_id` (`skin_color_id`),
  KEY `eye_color_id` (`eye_color_id`),
  KEY `weight_range_id` (`weight_range_id`),
  KEY `city_id` (`city_id`),
  KEY `status_id` (`status_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=35 ;

--
-- Dumping data for table `membre`
--

INSERT INTO `membre` (`membreId`, `nom`, `prenom`, `age`, `description`, `categorieId`, `isOnline`, `niveauMembreId`, `lastVisit`, `email`, `password`, `pseudo`, `sexe`, `notification`, `hair_color_id`, `skin_color_id`, `eye_color_id`, `height`, `weight_range_id`, `city_id`, `smoke`, `status_id`, `informed_message_received`, `informed_added_by_others`, `informed_removed_by_others`) VALUES
(1, 'LeBob', 'bob', 30, 'une desc de bob', 1, 0, 3, '2011-02-17 16:20:16', 'bob@gmail.com', '12345', 'beaubob', NULL, NULL, 1, 1, 1, 170, 1, 2, 0, 1, 0, 0, 0),
(2, 'drolet', 'dominic', 40, 'une desc de dom', 1, 0, 3, NULL, 'dominic@dominic.com', '12345', 'Dom', NULL, NULL, 5, 2, 2, 168, 1, 2, 1, 2, 0, 0, 0),
(3, 'proulx', 'daniel', 20, 'une desc de dan', 1, 0, 2, NULL, 'daniel@daniel.com', '12345', 'Dany', NULL, NULL, 3, 3, 3, 180, 3, 3, 0, 3, 0, 0, 0),
(4, 'marchildon', 'rock', 30, 'une desc de rock', 1, 0, 2, NULL, 'rock@rock.com', '12345', 'Rocky', NULL, NULL, 7, 3, 3, 179, 1, 1, 1, 4, 0, 0, 0),
(5, 'Magnagna', 'albert', 30, 'une desc de albert', 1, 0, 2, NULL, 'albert@albert.com', '12345', 'Albert', NULL, NULL, 2, 1, 1, 167, 2, 4, 0, 1, 0, 0, 0),
(6, 'Hajj', 'Toufic', 30, 'une desc de toufic', 1, 0, 3, NULL, 'toufic@toufic.com', '12345', 'Hajj', NULL, NULL, 6, 4, 3, 180, 3, 5, 1, 2, 0, 0, 0),
(7, 'Tremblay', 'Lea', 25, 'ajouter un description de Lea', 2, 0, 2, NULL, 'LeaTremblay@hotmail.com', '12345', 'Lea', NULL, NULL, 3, 1, 2, 170, 2, 8, 0, 1, 0, 0, 0),
(8, 'Lavoie', 'Florence', 31, 'ajouter un description de Florence', 4, 1, 2, NULL, 'FlorenceLavoie@msn.com', '12345', 'flor', NULL, NULL, 2, 4, 3, 171, 3, 7, 1, 3, 0, 0, 0),
(9, 'Pelletier', 'Alice', 28, 'ajouter un description de Alice', 2, 0, 1, NULL, 'AlicePeltier@gmail.com', '12345', 'Alice', NULL, NULL, 6, 3, 3, 160, 1, 6, 0, 4, 0, 0, 0),
(10, 'Bergeron', 'Emma', 21, 'ajouter un description de Emma', 2, 0, 1, NULL, 'EmmaBergeron@yahoo.com', '12345', 'Emma', NULL, NULL, 1, 2, 3, 180, 2, 7, 1, 2, 0, 0, 0),
(11, 'Paquette', 'Olivia', 35, 'ajouter un description de Olivia', 2, 0, 2, NULL, 'candidopala@gmail.com', '12345', 'Oli', NULL, NULL, 6, 2, 2, 159, 2, 2, 1, 2, 1, 1, 1),
(12, 'Girard', 'Adèle', 27, 'ajouter un description de Adele', 4, 0, 3, NULL, 'AdeleGirard@gmail.com', '12345', 'Adele', NULL, NULL, 1, 1, 3, 155, 2, 1, 0, 2, 0, 0, 0),
(22, 'Lagace', 'Alan', 30, NULL, 1, 0, 3, NULL, 'Alan@hotmail.com', '12345', 'Alan', NULL, NULL, 1, 1, 1, 170, 1, 1, 0, 1, 1, 1, 1),
(23, 'Lafrance', 'Alex', 30, NULL, 1, 0, 3, NULL, 'AlexLafrance@msn.com', '12345', 'Alex', NULL, NULL, 1, 1, 1, 170, 1, 1, 0, 1, 1, 1, 1),
(24, 'Billy', 'Gagne', 30, NULL, 1, 0, 3, NULL, 'Billy@htmail.com', '12345', 'Bill', NULL, NULL, 1, 1, 1, 170, 1, 1, 0, 1, 1, 1, 1),
(25, 'Page', 'Cody', 30, NULL, 1, 0, 3, NULL, 'Cody@htmail.com', '12345', 'Cody', NULL, NULL, 1, 1, 1, 170, 1, 1, 0, 1, 1, 1, 1),
(26, 'Trudeau', 'Danny', 30, NULL, 1, 0, 3, NULL, 'Danny@htmail.com', '12345', 'Dann', NULL, NULL, 1, 1, 1, 170, 1, 1, 0, 1, 1, 1, 1),
(27, 'Cantin', 'Edward', 30, NULL, 1, 0, 3, NULL, 'edward@htmail.com', '12345', 'Eddy', NULL, NULL, 1, 1, 1, 170, 1, 1, 0, 1, 1, 1, 1),
(28, 'St Luis', 'Felix', 30, NULL, 1, 0, 3, NULL, 'FelixStluis@htmail.com', '12345', 'Fe', NULL, NULL, 1, 1, 1, 170, 1, 1, 0, 1, 1, 1, 1),
(29, 'Bureau', 'Gabriel', 30, 'Un desc de gabriel', 1, 0, 3, NULL, 'Gabriel@htmail.com', '12345', 'Gabo', NULL, NULL, 1, 1, 1, 170, 1, 1, 0, 1, 1, 1, 1),
(30, 'Madore', 'Jacob', 30, 'un desc de gabriel', 1, 0, 3, NULL, 'Gabriel@htmail.com', '12345', 'Jacob', NULL, NULL, 1, 1, 1, 170, 1, 1, 0, 1, 1, 1, 1),
(31, 'Blais', 'Jeremy', 30, 'un desc de jeremy', 1, 0, 3, NULL, 'Jeremy@htmail.com', '12345', 'Jeremy', NULL, NULL, 1, 1, 1, 170, 1, 1, 0, 1, 1, 1, 1);

-- --------------------------------------------------------

--
-- Table structure for table `messages`
--

DROP TABLE IF EXISTS `messages`;
CREATE TABLE IF NOT EXISTS `messages` (
  `messageid` bigint(20) NOT NULL AUTO_INCREMENT,
  `msgFrom` bigint(20) NOT NULL,
  `msgTo` bigint(20) NOT NULL,
  `replyToMsgId` bigint(20) DEFAULT NULL COMMENT 'le msg auquel ce msg est une réponse',
  `sendDate` datetime NOT NULL,
  `contenu` varchar(5000) DEFAULT NULL,
  `dejaLu` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`messageid`),
  KEY `msgFrom` (`msgFrom`),
  KEY `msgTo` (`msgTo`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=37 ;

--
-- Dumping data for table `messages`
--

INSERT INTO `messages` (`messageid`, `msgFrom`, `msgTo`, `replyToMsgId`, `sendDate`, `contenu`, `dejaLu`) VALUES
(1, 3, 1, 3, '2012-07-09 00:00:00', 'un message envoye', 1),
(2, 4, 1, 3, '2015-05-09 00:00:00', 'un second messages envoye', 1),
(3, 3, 1, 3, '2017-07-09 00:00:00', 'un 3eme messages envoye', 1),
(4, 2, 1, 3, '2020-08-09 00:00:00', 'un 4eme messages envoye', 1),
(13, 11, 8, NULL, '2014-07-17 00:00:00', 'salut, test affichage messages', 1),
(14, 3, 8, NULL, '2014-07-09 00:00:00', 'hello, do u speak english or french?', 1),
(15, 10, 8, NULL, '2014-07-14 00:00:00', 'kjhsghgsfshgfhgfs ', 1),
(22, 8, 11, 0, '2014-07-17 21:46:53', '', 1),
(23, 8, 11, 0, '2014-07-17 21:55:11', 'alo', 1),
(24, 8, 3, 0, '2014-07-17 21:56:13', 'test', 0),
(25, 8, 11, 0, '2014-07-18 14:39:01', 'aloaloaloalo', 1),
(26, 8, 11, 0, '2014-07-18 14:39:18', 'aloaloaloalo', 1),
(27, 1, 3, 0, '2014-07-20 00:59:02', 'ALO DANI', 0),
(28, 1, 2, 0, '2014-07-20 00:59:22', 'DOMDOMDOM', 0),
(29, 10, 4, 0, '2014-07-20 17:31:40', '', 0),
(30, 10, 4, 0, '2014-07-20 17:31:43', '', 0),
(31, 10, 4, 0, '2014-07-20 17:31:43', '', 0),
(32, 10, 4, 0, '2014-07-20 17:31:46', '', 0),
(33, 10, 4, 0, '2014-07-20 17:31:52', '', 0),
(34, 10, 4, 0, '2014-07-20 17:31:57', '', 0),
(35, 10, 4, 0, '2014-07-20 17:34:02', '', 0),
(36, 8, 11, 0, '2014-07-20 19:08:16', 'alo, test', 1);

-- --------------------------------------------------------

--
-- Table structure for table `niveaumembres`
--

DROP TABLE IF EXISTS `niveaumembres`;
CREATE TABLE IF NOT EXISTS `niveaumembres` (
  `niveauMembreId` bigint(20) NOT NULL AUTO_INCREMENT,
  `description` varchar(100) DEFAULT NULL,
  `nbPhotosMax` int(11) NOT NULL,
  `nbFavorisMax` int(11) NOT NULL,
  `droitEnvoyerMsg` tinyint(1) NOT NULL,
  PRIMARY KEY (`niveauMembreId`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

--
-- Dumping data for table `niveaumembres`
--

INSERT INTO `niveaumembres` (`niveauMembreId`, `description`, `nbPhotosMax`, `nbFavorisMax`, `droitEnvoyerMsg`) VALUES
(1, 'visiteur', 1, 5, 0),
(2, 'membre régulier', 5, 25, 1),
(3, 'membre privilège', 15, 100, 1);

-- --------------------------------------------------------

--
-- Table structure for table `photos`
--

DROP TABLE IF EXISTS `photos`;
CREATE TABLE IF NOT EXISTS `photos` (
  `photoid` bigint(11) NOT NULL AUTO_INCREMENT,
  `membreId` bigint(20) NOT NULL,
  `chemin` varchar(255) NOT NULL,
  `isprofil` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`photoid`),
  KEY `fk_photos_membre` (`membreId`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=86 ;

--
-- Dumping data for table `photos`
--

INSERT INTO `photos` (`photoid`, `membreId`, `chemin`, `isprofil`) VALUES
(1, 1, 'photos/pic1.jpg', 1),
(7, 2, 'photos/pic2.jpg', 1),
(8, 2, 'photos/pic16.jpg', 0),
(9, 2, 'photos/pic17.jpg', 0),
(10, 2, 'photos/pic18.jpg', 0),
(11, 3, 'photos/pic3.jpg', 1),
(12, 4, 'photos/pic4.jpg', 1),
(13, 5, 'photos/pic5.jpg', 1),
(14, 6, 'photos/pic6.jpg', 1),
(15, 7, 'photos/pic7.jpg', 1),
(17, 9, 'photos/pic9.jpg', 1),
(18, 10, 'photos/pic10.jpg', 1),
(19, 11, 'photos/pic11.jpg', 1),
(20, 12, 'photos/pic12.jpg', 1),
(54, 22, 'photos/pic22.jpg', 1),
(55, 23, 'photos/pic23.jpg', 1),
(56, 24, 'photos/pic24.jpg', 1),
(57, 25, 'photos/pic25.jpg', 1),
(58, 26, 'photos/pic26.jpg', 1),
(59, 27, 'photos/pic27.jpg', 1),
(60, 28, 'photos/pic28.jpg', 1),
(61, 29, 'photos/pic29.jpg', 1),
(62, 30, 'photos/pic30.jpg', 1),
(63, 31, 'photos/pic31.jpg', 1),
(64, 8, 'photos/pic8.jpg', 1);

-- --------------------------------------------------------

--
-- Table structure for table `skin_color`
--

DROP TABLE IF EXISTS `skin_color`;
CREATE TABLE IF NOT EXISTS `skin_color` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `description` varchar(20) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=5 ;

--
-- Dumping data for table `skin_color`
--

INSERT INTO `skin_color` (`id`, `description`) VALUES
(1, 'blanc'),
(2, 'noir'),
(3, 'asian'),
(4, 'Latino');

-- --------------------------------------------------------

--
-- Table structure for table `status`
--

DROP TABLE IF EXISTS `status`;
CREATE TABLE IF NOT EXISTS `status` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `description` varchar(20) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=5 ;

--
-- Dumping data for table `status`
--

INSERT INTO `status` (`id`, `description`) VALUES
(1, 'celibataire'),
(2, 'marié(e)'),
(3, 'divorcé'),
(4, 'veuf(ve)');

-- --------------------------------------------------------

--
-- Table structure for table `user_hobby_link`
--

DROP TABLE IF EXISTS `user_hobby_link`;
CREATE TABLE IF NOT EXISTS `user_hobby_link` (
  `id` bigint(11) NOT NULL AUTO_INCREMENT,
  `user_id` bigint(11) NOT NULL,
  `hobby_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `hobby_id` (`hobby_id`),
  KEY `user_id` (`user_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=25 ;

--
-- Dumping data for table `user_hobby_link`
--

INSERT INTO `user_hobby_link` (`id`, `user_id`, `hobby_id`) VALUES
(1, 1, 1),
(2, 1, 5),
(3, 2, 2),
(4, 2, 10),
(5, 3, 3),
(6, 3, 6),
(7, 4, 4),
(8, 4, 7),
(9, 5, 8),
(10, 5, 9),
(11, 6, 2),
(12, 6, 10),
(13, 7, 5),
(14, 7, 3),
(15, 8, 1),
(16, 8, 2),
(17, 9, 4),
(18, 9, 10),
(19, 10, 6),
(20, 10, 9),
(21, 11, 7),
(22, 11, 8),
(23, 12, 2),
(24, 12, 6);

-- --------------------------------------------------------

--
-- Table structure for table `weight_range`
--

DROP TABLE IF EXISTS `weight_range`;
CREATE TABLE IF NOT EXISTS `weight_range` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `description` varchar(50) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=4 ;

--
-- Dumping data for table `weight_range`
--

INSERT INTO `weight_range` (`id`, `description`) VALUES
(1, 'mince'),
(2, 'normal'),
(3, 'musculé');

--
-- Constraints for dumped tables
--

--
-- Constraints for table `clinsdoeil`
--
ALTER TABLE `clinsdoeil`
  ADD CONSTRAINT `clinsdoeil_ibfk_1` FOREIGN KEY (`fromId`) REFERENCES `membre` (`membreId`),
  ADD CONSTRAINT `clinsdoeil_ibfk_2` FOREIGN KEY (`toId`) REFERENCES `membre` (`membreId`);

--
-- Constraints for table `favoris`
--
ALTER TABLE `favoris`
  ADD CONSTRAINT `favoris_ibfk_1` FOREIGN KEY (`membreFavorisant`) REFERENCES `membre` (`membreId`),
  ADD CONSTRAINT `favoris_ibfk_2` FOREIGN KEY (`membreFavorise`) REFERENCES `membre` (`membreId`);

--
-- Constraints for table `membre`
--
ALTER TABLE `membre`
  ADD CONSTRAINT `fk_membre_categorie` FOREIGN KEY (`categorieId`) REFERENCES `categorie` (`categorieId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_membre_niveauxMembres` FOREIGN KEY (`niveauMembreId`) REFERENCES `niveaumembres` (`niveauMembreId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `membre_ibfk_1` FOREIGN KEY (`hair_color_id`) REFERENCES `hair_color` (`id`),
  ADD CONSTRAINT `membre_ibfk_2` FOREIGN KEY (`skin_color_id`) REFERENCES `skin_color` (`id`),
  ADD CONSTRAINT `membre_ibfk_3` FOREIGN KEY (`eye_color_id`) REFERENCES `eye_color` (`id`),
  ADD CONSTRAINT `membre_ibfk_4` FOREIGN KEY (`weight_range_id`) REFERENCES `weight_range` (`id`),
  ADD CONSTRAINT `membre_ibfk_5` FOREIGN KEY (`city_id`) REFERENCES `city` (`id`),
  ADD CONSTRAINT `membre_ibfk_6` FOREIGN KEY (`status_id`) REFERENCES `status` (`id`);

--
-- Constraints for table `messages`
--
ALTER TABLE `messages`
  ADD CONSTRAINT `messages_ibfk_1` FOREIGN KEY (`msgFrom`) REFERENCES `membre` (`membreId`),
  ADD CONSTRAINT `messages_ibfk_2` FOREIGN KEY (`msgTo`) REFERENCES `membre` (`membreId`);

--
-- Constraints for table `photos`
--
ALTER TABLE `photos`
  ADD CONSTRAINT `fk_photos_membre` FOREIGN KEY (`membreId`) REFERENCES `membre` (`membreId`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `user_hobby_link`
--
ALTER TABLE `user_hobby_link`
  ADD CONSTRAINT `user_hobby_link_ibfk_1` FOREIGN KEY (`hobby_id`) REFERENCES `hobby` (`id`),
  ADD CONSTRAINT `user_hobby_link_ibfk_2` FOREIGN KEY (`user_id`) REFERENCES `membre` (`membreId`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
