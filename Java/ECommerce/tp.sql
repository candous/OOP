-- phpMyAdmin SQL Dump
-- version 4.1.14
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: 10-Jun-2014 às 19:40
-- Versão do servidor: 5.6.17
-- PHP Version: 5.5.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `tp`
--
CREATE DATABASE IF NOT EXISTS `tp` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `tp`;

-- --------------------------------------------------------

--
-- Estrutura da tabela `categorie`
--

DROP TABLE IF EXISTS `categorie`;
CREATE TABLE IF NOT EXISTS `categorie` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(50) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `nom` (`nom`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

--
-- Extraindo dados da tabela `categorie`
--

INSERT INTO `categorie` (`id`, `nom`) VALUES
(2, 'custom'),
(1, 'retro'),
(3, 'sport');

-- --------------------------------------------------------

--
-- Estrutura da tabela `commande`
--

DROP TABLE IF EXISTS `commande`;
CREATE TABLE IF NOT EXISTS `commande` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `date_commande` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `id_client` varchar(200) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `id_client` (`id_client`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estrutura da tabela `produit`
--

DROP TABLE IF EXISTS `produit`;
CREATE TABLE IF NOT EXISTS `produit` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(50) NOT NULL,
  `description` varchar(500) NOT NULL,
  `prixCout` decimal(10,2) DEFAULT NULL,
  `prixVente` decimal(10,2) NOT NULL,
  `quantite` int(11) NOT NULL,
  `url` varchar(200) NOT NULL,
  `id_categorie` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `id_categorie` (`id_categorie`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=46 ;

--
-- Extraindo dados da tabela `produit`
--

INSERT INTO `produit` (`id`, `nom`, `description`, `prixCout`, `prixVente`, `quantite`, `url`, `id_categorie`) VALUES
(1, 'Full face 2', 'Révélé par une campagne de pub complètement fantasque qui retrace l’histoire d’une tête sans corps répondant au charmant prénom de « Quique », Diesel frappe fort pour le lancement de son premier casque et se distingue une fois de plus par son anticonformisme. Après le « Cavalier sans tête », Diesel invente « Quique the head », le scootériste sans corps ! ', '25.00', '99.90', 50, 'imgs/casques/retro/01.jpg', 1),
(2, 'The retro bell ', '"Petit casque jet, petit prix, mais des caractéristiques techniques qui ne sont pas en berne. Simple et design, le ""X60 Vision Plus"" se révélera être le compagnon de route idéal pour tous les scootéristes cherchant l’originalité à moindre coût. "', '32.00', '123.30', 50, 'imgs/casques/retro/02.jpg', 1),
(3, 'Roof boxer ', '"Casque retro, le ""Pearl"" s''accordera parfaitement avec une paire de lunettes moto ou d''un masque moto et Stormer prévoit à l''arrière de son casque une attache avec pressions permettant d’immobiliser votre masque ou vos lunettes moto."', '12.00', '102.50', 50, 'imgs/casques/retro/03.jpg', 1),
(4, 'Reevu MSX1', 'Attention la taille XXL taille relativement petit.En matière d’extravagance, le Nexx X60 n’a rien à envier à personne. Avec son impressionnante  gamme tendance, le X60 se décline en d’innombrables...', '25.90', '49.90', 50, 'imgs/casques/retro/04.jpg', 1),
(5, 'Random Inspiration', '"Les plus fantaisistes se tourneront surement vers sa déclinaison ""Vision Dragonfly"", recouverte de toile de jean brodée de fleurs et libellule; les nostalgiques du rétro se tourneront vers le ""Vision Vintage""; les passionnés d''arts martiaux préfèreront le ""Samourai""; les riders chevronnés opteront pour le nouveau modèle 2010 ""Eagle"""', '45.00', '87.45', 50, 'imgs/casques/retro/05.jpg', 1),
(6, 'FootBall Helmet', '"Destiné à une utilisation aussi bien urbaine qu''extra-urbaine, le casque jet ""OP07"" bénéficie d''une forme enveloppante, et sa calotte en résine thermoplastique concilie résistance et volume contenu. "', '39.90', '115.20', 50, 'imgs/casques/retro/06.jpg', 1),
(7, 'Motorcycle Clothing', 'Doté d''un double écran (incolore et solaire) sous capot de protection, qui lui donne ce look hors du commun, le jet Aircraft peut également s''équiper - en option - d''un masque qui vous protègera de l''air, du froid et autres insectes et vous offrir ainsi un style encore plus décalé.', '42.00', '103.32', 50, 'imgs/casques/retro/07.jpg', 1),
(8, 'Biltwell Cringo', '"Fondée en 1956 à Westminster en Californie puis transférée en Italie en 1987, la marque Premier nous propose des casques jet nommés ""Vintage"", hommage au style et au design qui ont marqués ses débuts"', '46.79', '125.00', 50, 'imgs/casques/retro/08.jpg', 1),
(9, 'Predator Motorcycle', '"Qu''il soit uni noir ou paré de ses déco clairement inspirées des USA et du Japon, le casque jet GPA ""Aircraft"" a un design particulièrement singulier et donc accrocheur. Une esthétique qui n''est pas sans rappeler celle des casques des pilotes de chasse."', '46.90', '98.78', 50, 'imgs/casques/retro/09.jpg', 1),
(10, 'ACQUIRE', 'Révélé par une campagne de pub complètement fantasque qui retrace l’histoire d’une tête sans corps répondant au charmant prénom de « Quique », Diesel frappe fort pour le lancement de son premier casque et se distingue une fois de plus par son anticonformisme. Après le « Cavalier sans tête », Diesel invente « Quique the head », le scootériste sans corps ! ', '25.00', '89.90', 50, 'imgs/casques/retro/10.jpg', 1),
(11, 'Elegant Pearl', '"Petit casque jet, petit prix, mais des caractéristiques techniques qui ne sont pas en berne. Simple et design, le ""X60 Vision Plus"" se révélera être le compagnon de route idéal pour tous les scootéristes cherchant l’originalité à moindre coût. "', '32.00', '99.90', 50, 'imgs/casques/retro/11.jpg', 1),
(12, 'New Bullit', '"Casque retro, le ""Pearl"" s''accordera parfaitement avec une paire de lunettes moto ou d''un masque moto et Stormer prévoit à l''arrière de son casque une attache avec pressions permettant d’immobiliser votre masque ou vos lunettes moto."', '12.00', '123.30', 50, 'imgs/casques/retro/12.jpg', 1),
(13, 'Plagiocephaly', 'Attention la taille XXL taille relativement petit.En matière d’extravagance, le Nexx X60 n’a rien à envier à personne. Avec son impressionnante  gamme tendance, le X60 se décline en d’innombrables...', '25.90', '102.50', 50, 'imgs/casques/retro/13.jpg', 1),
(14, 'Batman Motorcycle', '"Les plus fantaisistes se tourneront surement vers sa déclinaison ""Vision Dragonfly"", recouverte de toile de jean brodée de fleurs et libellule; les nostalgiques du rétro se tourneront vers le ""Vision Vintage""; les passionnés d''arts martiaux préfèreront le ""Samourai""; les riders chevronnés opteront pour le nouveau modèle 2010 ""Eagle"""', '45.00', '49.90', 50, 'imgs/casques/retro/14.jpg', 1),
(15, 'Spirited Helmet', '"Destiné à une utilisation aussi bien urbaine qu''extra-urbaine, le casque jet ""OP07"" bénéficie d''une forme enveloppante, et sa calotte en résine thermoplastique concilie résistance et volume contenu. "', '39.90', '87.45', 50, 'imgs/casques/retro/15.jpg', 1),
(16, 'Dirtbike Love', 'Doté d''un double écran (incolore et solaire) sous capot de protection, qui lui donne ce look hors du commun, le jet Aircraft peut également s''équiper - en option - d''un masque qui vous protègera de l''air, du froid et autres insectes et vous offrir ainsi un style encore plus décalé.', '42.00', '115.20', 50, 'imgs/casques/retro/16.jpg', 1),
(17, 'RocaMode', '"Fondée en 1956 à Westminster en Californie puis transférée en Italie en 1987, la marque Premier nous propose des casques jet nommés ""Vintage"", hommage au style et au design qui ont marqués ses débuts"', '46.79', '103.32', 50, 'imgs/casques/retro/17.jpg', 1),
(18, 'Full Face', '"Qu''il soit uni noir ou paré de ses déco clairement inspirées des USA et du Japon, le casque jet GPA ""Aircraft"" a un design particulièrement singulier et donc accrocheur. Une esthétique qui n''est pas sans rappeler celle des casques des pilotes de chasse."', '46.90', '125.00', 50, 'imgs/casques/retro/18.jpg', 1),
(19, 'Harley Davidson', 'Révélé par une campagne de pub complètement fantasque qui retrace l’histoire d’une tête sans corps répondant au charmant prénom de « Quique », Diesel frappe fort pour le lancement de son premier casque et se distingue une fois de plus par son anticonformisme. Après le « Cavalier sans tête », Diesel invente « Quique the head », le scootériste sans corps ! ', '25.00', '98.78', 50, 'imgs/casques/custom/01.jpg', 2),
(20, 'bell RS-1', '"Petit casque jet, petit prix, mais des caractéristiques techniques qui ne sont pas en berne. Simple et design, le ""X60 Vision Plus"" se révélera être le compagnon de route idéal pour tous les scootéristes cherchant l’originalité à moindre coût. "', '32.00', '89.90', 50, 'imgs/casques/custom/02.jpg', 2),
(21, 'Moto Leather', '"Casque retro, le ""Pearl"" s''accordera parfaitement avec une paire de lunettes moto ou d''un masque moto et Stormer prévoit à l''arrière de son casque une attache avec pressions permettant d’immobiliser votre masque ou vos lunettes moto."', '12.00', '99.90', 50, 'imgs/casques/custom/03.jpg', 2),
(22, 'Easy Rider', 'Attention la taille XXL taille relativement petit.En matière d’extravagance, le Nexx X60 n’a rien à envier à personne. Avec son impressionnante  gamme tendance, le X60 se décline en d’innombrables...', '25.90', '123.30', 50, 'imgs/casques/custom/04.jpg', 2),
(23, 'NFl baby', '"Les plus fantaisistes se tourneront surement vers sa déclinaison ""Vision Dragonfly"", recouverte de toile de jean brodée de fleurs et libellule; les nostalgiques du rétro se tourneront vers le ""Vision Vintage""; les passionnés d''arts martiaux préfèreront le ""Samourai""; les riders chevronnés opteront pour le nouveau modèle 2010 ""Eagle"""', '45.00', '102.50', 50, 'imgs/casques/custom/05.jpg', 2),
(24, 'Women''s Arrow', '"Destiné à une utilisation aussi bien urbaine qu''extra-urbaine, le casque jet ""OP07"" bénéficie d''une forme enveloppante, et sa calotte en résine thermoplastique concilie résistance et volume contenu. "', '39.90', '49.90', 50, 'imgs/casques/custom/06.jpg', 2),
(25, 'Luxury Sci-Fi', 'Doté d''un double écran (incolore et solaire) sous capot de protection, qui lui donne ce look hors du commun, le jet Aircraft peut également s''équiper - en option - d''un masque qui vous protègera de l''air, du froid et autres insectes et vous offrir ainsi un style encore plus décalé.', '42.00', '87.45', 50, 'imgs/casques/custom/07.jpg', 2),
(26, 'Roller Derby', '"Fondée en 1956 à Westminster en Californie puis transférée en Italie en 1987, la marque Premier nous propose des casques jet nommés ""Vintage"", hommage au style et au design qui ont marqués ses débuts"', '46.79', '115.20', 50, 'imgs/casques/custom/08.jpg', 2),
(27, 'Women''s Airframe', '"Qu''il soit uni noir ou paré de ses déco clairement inspirées des USA et du Japon, le casque jet GPA ""Aircraft"" a un design particulièrement singulier et donc accrocheur. Une esthétique qui n''est pas sans rappeler celle des casques des pilotes de chasse."', '46.90', '103.32', 50, 'imgs/casques/custom/09.jpg', 2),
(28, 'Madison Helmet', 'Révélé par une campagne de pub complètement fantasque qui retrace l’histoire d’une tête sans corps répondant au charmant prénom de « Quique », Diesel frappe fort pour le lancement de son premier casque et se distingue une fois de plus par son anticonformisme. Après le « Cavalier sans tête », Diesel invente « Quique the head », le scootériste sans corps ! ', '25.00', '125.00', 50, 'imgs/casques/custom/10.jpg', 2),
(29, 'Airbrushed', '"Petit casque jet, petit prix, mais des caractéristiques techniques qui ne sont pas en berne. Simple et design, le ""X60 Vision Plus"" se révélera être le compagnon de route idéal pour tous les scootéristes cherchant l’originalité à moindre coût. "', '32.00', '98.78', 50, 'imgs/casques/custom/11.jpg', 2),
(30, 'Castle Helmet', '"Casque retro, le ""Pearl"" s''accordera parfaitement avec une paire de lunettes moto ou d''un masque moto et Stormer prévoit à l''arrière de son casque une attache avec pressions permettant d’immobiliser votre masque ou vos lunettes moto."', '12.00', '89.90', 50, 'imgs/casques/custom/12.jpg', 2),
(31, 'Vintage Motocross', 'Attention la taille XXL taille relativement petit.En matière d’extravagance, le Nexx X60 n’a rien à envier à personne. Avec son impressionnante  gamme tendance, le X60 se décline en d’innombrables...', '25.90', '99.90', 50, 'imgs/casques/custom/13.jpg', 2),
(32, 'HJC CS-R2', '"Les plus fantaisistes se tourneront surement vers sa déclinaison ""Vision Dragonfly"", recouverte de toile de jean brodée de fleurs et libellule; les nostalgiques du rétro se tourneront vers le ""Vision Vintage""; les passionnés d''arts martiaux préfèreront le ""Samourai""; les riders chevronnés opteront pour le nouveau modèle 2010 ""Eagle"""', '45.00', '123.30', 50, 'imgs/casques/custom/14.jpg', 2),
(33, 'BMW AirFlow 2', '"Destiné à une utilisation aussi bien urbaine qu''extra-urbaine, le casque jet ""OP07"" bénéficie d''une forme enveloppante, et sa calotte en résine thermoplastique concilie résistance et volume contenu. "', '39.90', '102.50', 50, 'imgs/casques/custom/15.jpg', 2),
(34, 'Gmax Motorcyele', 'Doté d''un double écran (incolore et solaire) sous capot de protection, qui lui donne ce look hors du commun, le jet Aircraft peut également s''équiper - en option - d''un masque qui vous protègera de l''air, du froid et autres insectes et vous offrir ainsi un style encore plus décalé.', '42.00', '49.90', 50, 'imgs/casques/sportive/01.jpg', 3),
(35, 'Skull Motorcycle', '"Fondée en 1956 à Westminster en Californie puis transférée en Italie en 1987, la marque Premier nous propose des casques jet nommés ""Vintage"", hommage au style et au design qui ont marqués ses débuts"', '46.79', '87.45', 50, 'imgs/casques/sportive/02.jpg', 3),
(36, 'Kansas City', '"Qu''il soit uni noir ou paré de ses déco clairement inspirées des USA et du Japon, le casque jet GPA ""Aircraft"" a un design particulièrement singulier et donc accrocheur. Une esthétique qui n''est pas sans rappeler celle des casques des pilotes de chasse."', '46.90', '115.20', 50, 'imgs/casques/sportive/03.jpg', 3),
(37, 'Icon Airframe', 'Révélé par une campagne de pub complètement fantasque qui retrace l’histoire d’une tête sans corps répondant au charmant prénom de « Quique », Diesel frappe fort pour le lancement de son premier casque et se distingue une fois de plus par son anticonformisme. Après le « Cavalier sans tête », Diesel invente « Quique the head », le scootériste sans corps ! ', '25.00', '103.32', 50, 'imgs/casques/sportive/04.jpg', 3),
(38, 'Steampunk Army', '"Petit casque jet, petit prix, mais des caractéristiques techniques qui ne sont pas en berne. Simple et design, le ""X60 Vision Plus"" se révélera être le compagnon de route idéal pour tous les scootéristes cherchant l’originalité à moindre coût. "', '32.00', '125.00', 50, 'imgs/casques/sportive/05.jpg', 3),
(39, 'Crochet Pittsburgh', '"Casque retro, le ""Pearl"" s''accordera parfaitement avec une paire de lunettes moto ou d''un masque moto et Stormer prévoit à l''arrière de son casque une attache avec pressions permettant d’immobiliser votre masque ou vos lunettes moto."', '12.00', '98.78', 50, 'imgs/casques/sportive/06.jpg', 3),
(40, 'Tucano Urbano', 'Attention la taille XXL taille relativement petit.En matière d’extravagance, le Nexx X60 n’a rien à envier à personne. Avec son impressionnante  gamme tendance, le X60 se décline en d’innombrables...', '25.90', '89.90', 50, 'imgs/casques/sportive/07.jpg', 3),
(41, 'Glow in the dark', '"Les plus fantaisistes se tourneront surement vers sa déclinaison ""Vision Dragonfly"", recouverte de toile de jean brodée de fleurs et libellule; les nostalgiques du rétro se tourneront vers le ""Vision Vintage""; les passionnés d''arts martiaux préfèreront le ""Samourai""; les riders chevronnés opteront pour le nouveau modèle 2010 ""Eagle"""', '45.00', '99.90', 50, 'imgs/casques/sportive/08.jpg', 3),
(42, 'Bell Custom', '"Destiné à une utilisation aussi bien urbaine qu''extra-urbaine, le casque jet ""OP07"" bénéficie d''une forme enveloppante, et sa calotte en résine thermoplastique concilie résistance et volume contenu. "', '39.90', '123.30', 50, 'imgs/casques/sportive/09.jpg', 3),
(43, 'SparX', 'Doté d''un double écran (incolore et solaire) sous capot de protection, qui lui donne ce look hors du commun, le jet Aircraft peut également s''équiper - en option - d''un masque qui vous protègera de l''air, du froid et autres insectes et vous offrir ainsi un style encore plus décalé.', '42.00', '102.50', 50, 'imgs/casques/sportive/10.jpg', 3),
(44, 'Stormtrooper', '"Fondée en 1956 à Westminster en Californie puis transférée en Italie en 1987, la marque Premier nous propose des casques jet nommés ""Vintage"", hommage au style et au design qui ont marqués ses débuts"', '46.79', '49.90', 50, 'imgs/casques/sportive/11.jpg', 3),
(45, 'Speed ', '"Qu''il soit uni noir ou paré de ses déco clairement inspirées des USA et du Japon, le casque jet GPA ""Aircraft"" a un design particulièrement singulier et donc accrocheur. Une esthétique qui n''est pas sans rappeler celle des casques des pilotes de chasse."', '46.90', '87.45', 50, 'imgs/casques/sportive/12.jpg', 3);

-- --------------------------------------------------------

--
-- Estrutura da tabela `produitcommande`
--

DROP TABLE IF EXISTS `produitcommande`;
CREATE TABLE IF NOT EXISTS `produitcommande` (
  `id_commande` int(11) NOT NULL,
  `id_produit` int(11) NOT NULL,
  `quantite` int(11) NOT NULL,
  PRIMARY KEY (`id_commande`,`id_produit`),
  CONSTRAINT FOREIGN KEY (`id_commande`) REFERENCES `commande` (`id`),
  CONSTRAINT FOREIGN KEY (`id_produit`) REFERENCES `produit` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estrutura da tabela `role`
--

DROP TABLE IF EXISTS `role`;
CREATE TABLE IF NOT EXISTS `role` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `login` varchar(100) NOT NULL,
  `role` varchar(100) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `login_2` (`login`),
  KEY `login` (`login`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estrutura da tabela `user`
--

DROP TABLE IF EXISTS `user`;
CREATE TABLE IF NOT EXISTS `user` (
  `email` varchar(200) NOT NULL,
  `nom` varchar(100) NOT NULL,
  `adresse` varchar(200) NOT NULL,
  `telephone` varchar(15) DEFAULT NULL,
  `password` varchar(200) NOT NULL,
  PRIMARY KEY (`email`),
  UNIQUE KEY `email` (`email`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Constraints for dumped tables
--

--
-- Limitadores para a tabela `commande`
--
ALTER TABLE `commande`
  ADD CONSTRAINT `commande_ibfk_1` FOREIGN KEY (`id_client`) REFERENCES `user` (`email`);

--
-- Limitadores para a tabela `produit`
--
ALTER TABLE `produit`
  ADD CONSTRAINT `produit_ibfk_1` FOREIGN KEY (`id_categorie`) REFERENCES `categorie` (`id`);

--
-- Limitadores para a tabela `role`
--
ALTER TABLE `role`
  ADD CONSTRAINT `role_ibfk_1` FOREIGN KEY (`login`) REFERENCES `user` (`email`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
