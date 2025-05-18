-- MySQL dump 10.13  Distrib 8.0.42, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: warehouse
-- ------------------------------------------------------
-- Server version	8.0.42

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `boxes`
--

DROP TABLE IF EXISTS `boxes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `boxes` (
  `id` int NOT NULL AUTO_INCREMENT,
  `width` decimal(10,2) NOT NULL,
  `height` decimal(10,2) NOT NULL,
  `depth` decimal(10,2) NOT NULL,
  `weight` decimal(10,2) NOT NULL,
  `production_date` date DEFAULT NULL,
  `expiration_date` date DEFAULT NULL,
  `pallet_id` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `pallet_id` (`pallet_id`),
  CONSTRAINT `boxes_ibfk_1` FOREIGN KEY (`pallet_id`) REFERENCES `pallets` (`id`) ON DELETE CASCADE,
  CONSTRAINT `boxes_chk_1` CHECK ((((`production_date` is not null) and (`expiration_date` is null)) or ((`production_date` is null) and (`expiration_date` is not null))))
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `boxes`
--

LOCK TABLES `boxes` WRITE;
/*!40000 ALTER TABLE `boxes` DISABLE KEYS */;
INSERT INTO `boxes` VALUES (1,60.00,30.00,40.00,10.00,NULL,'2025-08-01',1),(2,55.00,25.00,35.00,8.00,NULL,'2025-07-15',1),(3,50.00,20.00,30.00,6.50,'2025-03-01',NULL,2),(4,60.00,25.00,30.00,7.00,'2025-02-20',NULL,2),(5,65.00,28.00,35.00,9.20,'2025-03-05',NULL,2),(6,40.00,18.00,28.00,5.00,NULL,'2025-09-30',3),(7,42.00,19.00,30.00,5.50,NULL,'2025-09-28',3),(8,55.00,22.00,33.00,7.80,'2025-01-10',NULL,4),(9,50.00,20.00,30.00,6.00,NULL,'2025-12-01',5),(10,52.00,22.00,32.00,6.50,NULL,'2025-11-20',5);
/*!40000 ALTER TABLE `boxes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pallets`
--

DROP TABLE IF EXISTS `pallets`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `pallets` (
  `id` int NOT NULL AUTO_INCREMENT,
  `width` decimal(10,2) NOT NULL,
  `height` decimal(10,2) NOT NULL,
  `depth` decimal(10,2) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pallets`
--

LOCK TABLES `pallets` WRITE;
/*!40000 ALTER TABLE `pallets` DISABLE KEYS */;
INSERT INTO `pallets` VALUES (1,120.00,150.00,100.00),(2,130.00,160.00,110.00),(3,110.00,140.00,90.00),(4,125.00,155.00,105.00),(5,115.00,145.00,95.00);
/*!40000 ALTER TABLE `pallets` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'warehouse'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-05-18 13:57:51
