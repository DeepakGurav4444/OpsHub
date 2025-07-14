-- MySQL dump 10.13  Distrib 8.0.34, for Win64 (x86_64)
--
-- Host: localhost    Database: ops_hub_new
-- ------------------------------------------------------
-- Server version	8.0.34

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
-- Table structure for table `client_wf_steps`
--

DROP TABLE IF EXISTS `client_wf_steps`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `client_wf_steps` (
  `idclient_wf_step` int NOT NULL AUTO_INCREMENT,
  `client_id` int NOT NULL,
  `wf_step_id` int NOT NULL,
  `user_id` int NOT NULL,
  `step_status` varchar(20) NOT NULL DEFAULT 'Pending',
  `completed_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`idclient_wf_step`),
  KEY `fk_cl_wf_step_cl_id_idx` (`client_id`),
  KEY `fk_cl_wf_step_wf_step_id_idx` (`wf_step_id`),
  KEY `fk_cl_wf_step_user_id_idx` (`user_id`),
  CONSTRAINT `fk_cl_wf_step_cl_id` FOREIGN KEY (`client_id`) REFERENCES `user_client` (`iduser_client`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_cl_wf_step_user_id` FOREIGN KEY (`user_id`) REFERENCES `ten_users` (`idten_users`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_cl_wf_step_wf_step_id` FOREIGN KEY (`wf_step_id`) REFERENCES `wf_steps` (`idwf_steps`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `client_wf_steps`
--

LOCK TABLES `client_wf_steps` WRITE;
/*!40000 ALTER TABLE `client_wf_steps` DISABLE KEYS */;
INSERT INTO `client_wf_steps` VALUES (5,4,1,22,'Pending','2025-07-12 21:00:08'),(6,4,2,23,'Approved','2025-07-12 21:20:16');
/*!40000 ALTER TABLE `client_wf_steps` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `mod_modules`
--

DROP TABLE IF EXISTS `mod_modules`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `mod_modules` (
  `idmod_modules` int NOT NULL AUTO_INCREMENT,
  `module_name` varchar(45) NOT NULL,
  `description` varchar(200) NOT NULL,
  `icon_url` varchar(1000) NOT NULL,
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `is_active` int DEFAULT '1',
  PRIMARY KEY (`idmod_modules`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `mod_modules`
--

LOCK TABLES `mod_modules` WRITE;
/*!40000 ALTER TABLE `mod_modules` DISABLE KEYS */;
INSERT INTO `mod_modules` VALUES (1,'Visitor Management','Manage visitor entries/check-ins','/icons/visitor.svg','2025-07-04 16:53:18',1),(2,'Truck Management','Track truck entry and logistics','/icons/truck.svg','2025-07-04 16:53:18',1),(3,'Vehicle Management','Gate pass & vehicle tracking','/icons/vehicle.svg','2025-07-04 16:53:18',1),(4,'Employee Management','Register and manage employees','/icons/employee.svg','2025-07-04 16:53:18',1),(5,'Contractor Management','Manage contract workers access','/icons/contractor.svg','2025-07-04 16:53:18',1);
/*!40000 ALTER TABLE `mod_modules` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `refresh_tokens`
--

DROP TABLE IF EXISTS `refresh_tokens`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `refresh_tokens` (
  `idRefresh_Token` int NOT NULL AUTO_INCREMENT,
  `user_id` int NOT NULL,
  `user_type` varchar(20) NOT NULL,
  `token` varchar(255) NOT NULL,
  `created_at` datetime DEFAULT CURRENT_TIMESTAMP,
  `expires_at` datetime NOT NULL,
  `device_id` varchar(255) NOT NULL,
  `is_used` tinyint(1) DEFAULT '0',
  `is_revoked` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`idRefresh_Token`),
  UNIQUE KEY `idx_uniq_token` (`token`),
  KEY `idx_id_type_used_revoked` (`user_id`,`user_type`,`is_used`,`is_revoked`),
  KEY `idx_expire_at` (`expires_at`)
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `refresh_tokens`
--

LOCK TABLES `refresh_tokens` WRITE;
/*!40000 ALTER TABLE `refresh_tokens` DISABLE KEYS */;
INSERT INTO `refresh_tokens` VALUES (18,21,'TenantUser','HoszaLIDzjj3rVsK7hqTtFL3ostVU6P8YKKOjLozSqCN6bc0o7cpN4AKHlFzRTDUSd7+J1By5t5+4WQjECwaKQ==','2025-07-12 20:11:23','2025-08-12 20:11:23','567890',0,0),(19,21,'TenantUser','M9Hb3LQkFpK5lzUrOXbAutuEFw7Q1WEwfUEG4XM1j/CI9IcU9QCgmhi4NUA7DDzh876Dz25WiUuFcJz1NUg5sg==','2025-07-12 20:21:28','2025-08-12 20:21:28','1234567890',0,0),(20,22,'TenantUser','E9dGoXZNKDGGSVMaeaJDlN7U05nr65cyH9girFT8/Ej0Rq3EedTfZYOEdW8UV64IAfmLXAr/79baJ21U9uCpsQ==','2025-07-12 20:25:08','2025-08-12 20:25:08','1234567890',1,0),(21,22,'TenantUser','QfGAA0lU6Vqsuo3X3r08hl4WkXOLJDORUydOpOicFEVgFooTHpW1FxSAmWcMKT5wCE7vTl3NgjUVoKc5EOe0rg==','2025-07-12 20:38:43','2025-08-12 20:38:43','0987654321',0,0),(22,22,'TenantUser','mSUyQh5T+OQIWJHaI0k8c23VzQVoWbHVPz4emeLZRen0M/IYJhm+Wi/uwxJUZCN98YSNqo2ZvfauPs0+AMiBbg==','2025-07-12 20:50:05','2025-08-12 20:50:05','1234567890',0,0),(23,22,'TenantUser','pgwlEIWMCHHr00DY+bTWTyrpGdzXax5NMLmUdZoa3Ual3nePQl2Ngigae7eq/eGpkl/mQGJOta8CQVMpSkjKRQ==','2025-07-12 20:59:33','2025-08-12 20:59:33','1234567',0,0),(24,21,'TenantUser','g/jIsU0DDFyL2W21x5EWjB9xSIB4b2jbsbxYH/KzTjmQzJYsiPBl3YP2dKGv5JEfswCWqQE43Q6bEsStHnO2RA==','2025-07-12 21:04:13','2025-08-12 21:04:14','1234567',0,0),(25,23,'TenantUser','txL0+Q2izRKntFxe1KSuZDEnE4p7xWughq0jKqjAoQQ2raW8Tmptz0kP2pXAOi/Ah9mNILZ8dcrT8lqmVF5XDw==','2025-07-12 21:06:15','2025-08-12 21:06:15','1234567',0,0),(26,23,'TenantUser','Lgr3TjJMayndSyaO2evLc59KqtLk+QM916LrO9IFy0owPRl8ASQ5xAndl91JdDVG2X3ZZOxOLxEHqqIgjL65ig==','2025-07-12 21:19:13','2025-08-12 21:19:13','12345',0,0),(27,21,'TenantUser','E2n3wBHxvoLn8EgLjrhm+5BFSYzo+h9PV68H85gDznYyGcG7/j/swSpvZ+zfjbvFJtRdBckWRsPtIiU+1QYWYQ==','2025-07-12 21:22:18','2025-08-12 21:22:18','12345',0,0),(28,24,'TenantUser','Y48UZ6GIkwXJSEBT0VG70CPYYDuEarCHVTvi0YFAA/0FBmoTCuj8C/YsQE8GSEiCfuvl7FpRsfCY1JcfSFZRPg==','2025-07-12 21:26:26','2025-08-12 21:26:27','12345',1,0),(29,24,'TenantUser','R5Pmtk6l4C0fDuvCZmZ+Ju1yacOGVMvcc8FP7SHtUd5Wx0SSdpLohIxDPiy5TrRPcEUPy7a0kbUPNLVjafNoDA==','2025-07-12 21:33:13','2025-08-12 21:33:13','12345',0,0);
/*!40000 ALTER TABLE `refresh_tokens` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `role_roles`
--

DROP TABLE IF EXISTS `role_roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `role_roles` (
  `idrole_roles` int NOT NULL AUTO_INCREMENT,
  `role_name` varchar(100) NOT NULL,
  `description` varchar(255) DEFAULT NULL,
  `is_for_admin` tinyint DEFAULT '0',
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `is_active` tinyint DEFAULT '1',
  PRIMARY KEY (`idrole_roles`),
  UNIQUE KEY `role_name` (`role_name`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role_roles`
--

LOCK TABLES `role_roles` WRITE;
/*!40000 ALTER TABLE `role_roles` DISABLE KEYS */;
INSERT INTO `role_roles` VALUES (1,'Visitor Admin Officer','Manages visitor operations',1,'2025-07-11 18:44:06',1),(2,'Front Desk Executive','Handles front desk visitor entries',0,'2025-07-11 18:44:06',1),(3,'Security Officer','Performs visitor/security checks',0,'2025-07-11 18:44:06',1),(4,'Visitor Approving Manager','Final visitor approval authority',0,'2025-07-11 18:44:06',1),(5,'Transport Supervisor','Supervises truck logistics',1,'2025-07-11 18:44:06',1),(6,'Gate Entry Clerk','Records truck gate entries',0,'2025-07-11 18:44:06',1),(7,'Logistics Checker','Checks truck goods/documents',0,'2025-07-11 18:44:06',1),(8,'Transport In-Charge','Final truck clearance',0,'2025-07-11 18:44:06',1),(9,'Vehicle Gate Admin','Manages vehicle gate operations',1,'2025-07-11 18:44:06',1),(10,'Security Gate Operator','Handles security clearance at gate',0,'2025-07-11 18:44:06',1),(11,'Vehicle Clearance Officer','Performs vehicle document checks',0,'2025-07-11 18:44:06',1),(12,'Fleet Supervisor','Supervises overall vehicle movement',0,'2025-07-11 18:44:06',1),(13,'HR Admin','Manages employee data & approval flow',1,'2025-07-11 18:44:06',1),(14,'HR Executive','Initial employee onboarding step',0,'2025-07-11 18:44:06',1),(15,'HR Approver','Approves employee information',0,'2025-07-11 18:44:06',1),(16,'Head of HR','Final authority for employee workflows',0,'2025-07-11 18:44:06',1),(17,'Contractor Control Lead','Supervises contractor data/visits',1,'2025-07-11 18:44:06',1),(18,'Contract Coordinator','Coordinates contractor visits',0,'2025-07-11 18:44:06',1),(19,'Safety Inspector','Validates contractor safety compliance',0,'2025-07-11 18:44:06',1),(20,'Compliance Manager','Final compliance authority',0,'2025-07-11 18:44:06',1);
/*!40000 ALTER TABLE `role_roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sub_mod_map`
--

DROP TABLE IF EXISTS `sub_mod_map`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sub_mod_map` (
  `idsub_mod_map` int NOT NULL AUTO_INCREMENT,
  `sub_plan_id` int NOT NULL,
  `module_id` int NOT NULL,
  `is_active` int DEFAULT '1',
  PRIMARY KEY (`idsub_mod_map`),
  KEY `fk_map_sub_plan_id_idx` (`sub_plan_id`),
  KEY `fk_map_module_id_idx` (`module_id`),
  KEY `idx_sub_mod_map` (`sub_plan_id`,`module_id`),
  CONSTRAINT `fk_map_module_id` FOREIGN KEY (`module_id`) REFERENCES `mod_modules` (`idmod_modules`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_map_sub_plan_id` FOREIGN KEY (`sub_plan_id`) REFERENCES `sub_plans` (`idsub_plans`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sub_mod_map`
--

LOCK TABLES `sub_mod_map` WRITE;
/*!40000 ALTER TABLE `sub_mod_map` DISABLE KEYS */;
INSERT INTO `sub_mod_map` VALUES (1,1,1,1),(2,2,2,1),(3,3,3,1),(4,4,5,1),(5,5,4,1),(6,6,2,1),(7,6,3,1),(8,7,1,1),(9,7,5,1),(10,8,4,1),(11,8,5,1),(12,9,1,1),(13,9,3,1),(14,9,4,1),(15,10,1,1),(16,10,2,1),(17,10,3,1),(18,10,4,1),(19,10,5,1),(20,11,2,1),(21,11,3,1),(22,11,4,1),(23,11,5,1);
/*!40000 ALTER TABLE `sub_mod_map` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sub_plans`
--

DROP TABLE IF EXISTS `sub_plans`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sub_plans` (
  `idsub_plans` int NOT NULL AUTO_INCREMENT,
  `plan_name` varchar(45) NOT NULL,
  `duration_in_month` int NOT NULL,
  `price` decimal(14,2) NOT NULL,
  `max_users` int NOT NULL,
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `is_active` int DEFAULT '1',
  PRIMARY KEY (`idsub_plans`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sub_plans`
--

LOCK TABLES `sub_plans` WRITE;
/*!40000 ALTER TABLE `sub_plans` DISABLE KEYS */;
INSERT INTO `sub_plans` VALUES (1,'Visitor Solo',1,499.00,10,'2025-07-05 06:08:36',1),(2,'Truck Solo',1,699.00,10,'2025-07-05 06:08:36',1),(3,'Vehicle Solo',1,599.00,10,'2025-07-05 06:08:36',1),(4,'Contractor Solo',1,649.00,10,'2025-07-05 06:08:36',1),(5,'Access Control Solo',1,549.00,10,'2025-07-05 06:08:36',1),(6,'Logistics Combo',3,1499.00,25,'2025-07-05 06:08:36',1),(7,'Site Entry Combo',3,1299.00,25,'2025-07-05 06:08:36',1),(8,'Workforce Combo',3,1399.00,25,'2025-07-05 06:08:36',1),(9,'Essentials Bundle',6,2999.00,100,'2025-07-05 06:08:36',1),(10,'Enterprise All-In-One',12,6999.00,500,'2025-07-05 06:08:36',1),(11,'Unified FieldOps ',9,5999.00,250,'2025-07-10 16:44:50',1);
/*!40000 ALTER TABLE `sub_plans` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sys_admins`
--

DROP TABLE IF EXISTS `sys_admins`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sys_admins` (
  `idsys_admins` int NOT NULL AUTO_INCREMENT,
  `user_name` varchar(45) NOT NULL,
  `email_id` varchar(45) NOT NULL,
  `user_pass` varchar(64) NOT NULL,
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `is_active` int DEFAULT '1',
  PRIMARY KEY (`idsys_admins`),
  KEY `idx_email_pass` (`email_id`,`user_pass`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sys_admins`
--

LOCK TABLES `sys_admins` WRITE;
/*!40000 ALTER TABLE `sys_admins` DISABLE KEYS */;
INSERT INTO `sys_admins` VALUES (1,'Deepak','admin@gmail.com','password_hash','2025-07-06 19:09:22',1);
/*!40000 ALTER TABLE `sys_admins` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ten_modules`
--

DROP TABLE IF EXISTS `ten_modules`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ten_modules` (
  `idten_modules` int NOT NULL AUTO_INCREMENT,
  `tenant_id` int NOT NULL,
  `modules_id` int NOT NULL,
  `purchase_date` date NOT NULL,
  `expires_on` timestamp NOT NULL,
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `is_active` int DEFAULT '1',
  PRIMARY KEY (`idten_modules`),
  KEY `fk_ten_mod_modules_id_idx` (`modules_id`),
  KEY `fk_ten_mod_tenant_id_idx` (`tenant_id`),
  KEY `idx_tenid_moduleid_active` (`tenant_id`,`modules_id`,`is_active`),
  CONSTRAINT `fk_ten_mod_modules_id` FOREIGN KEY (`modules_id`) REFERENCES `mod_modules` (`idmod_modules`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_ten_mod_tenant_id` FOREIGN KEY (`tenant_id`) REFERENCES `ten_tenants` (`idten_tenants`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ten_modules`
--

LOCK TABLES `ten_modules` WRITE;
/*!40000 ALTER TABLE `ten_modules` DISABLE KEYS */;
INSERT INTO `ten_modules` VALUES (7,3,1,'2025-07-12','2025-08-12 19:36:36','2025-07-12 19:36:36',1);
/*!40000 ALTER TABLE `ten_modules` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ten_sub_map`
--

DROP TABLE IF EXISTS `ten_sub_map`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ten_sub_map` (
  `idten_sub_map` int NOT NULL AUTO_INCREMENT,
  `tenant_id` int NOT NULL,
  `sub_plan_id` int NOT NULL,
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `is_active` int DEFAULT '1',
  PRIMARY KEY (`idten_sub_map`),
  KEY `fk_ten_sub_map_tenant_id_idx` (`tenant_id`),
  KEY `fk_ten_sub_map_subplan_id_idx` (`sub_plan_id`),
  KEY `idx_tenid_subid_active` (`tenant_id`,`sub_plan_id`,`is_active`),
  CONSTRAINT `fk_ten_sub_map_subplan_id` FOREIGN KEY (`sub_plan_id`) REFERENCES `sub_plans` (`idsub_plans`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_ten_sub_map_tenant_id` FOREIGN KEY (`tenant_id`) REFERENCES `ten_tenants` (`idten_tenants`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ten_sub_map`
--

LOCK TABLES `ten_sub_map` WRITE;
/*!40000 ALTER TABLE `ten_sub_map` DISABLE KEYS */;
INSERT INTO `ten_sub_map` VALUES (5,3,1,'2025-07-12 19:36:34',1);
/*!40000 ALTER TABLE `ten_sub_map` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ten_tenants`
--

DROP TABLE IF EXISTS `ten_tenants`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ten_tenants` (
  `idten_tenants` int NOT NULL AUTO_INCREMENT,
  `tenant_name` varchar(45) NOT NULL,
  `email_Id` varchar(45) NOT NULL,
  `domain` varchar(45) NOT NULL,
  `logo_path` varchar(1000) NOT NULL,
  `ten_pass` varchar(45) NOT NULL,
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `is_active` int DEFAULT '1',
  PRIMARY KEY (`idten_tenants`),
  KEY `idx_email_pass` (`email_Id`,`ten_pass`),
  FULLTEXT KEY `idx_ser_name_email_domain` (`tenant_name`,`email_Id`,`domain`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ten_tenants`
--

LOCK TABLES `ten_tenants` WRITE;
/*!40000 ALTER TABLE `ten_tenants` DISABLE KEYS */;
INSERT INTO `ten_tenants` VALUES (3,'Adani','adani@gmail.com','Power','logos/adani.png','adani_hash','2025-07-12 17:16:29',1);
/*!40000 ALTER TABLE `ten_tenants` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ten_users`
--

DROP TABLE IF EXISTS `ten_users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ten_users` (
  `idten_users` int NOT NULL AUTO_INCREMENT,
  `principal_id` int DEFAULT NULL,
  `tenant_id` int NOT NULL,
  `modules_id` int NOT NULL,
  `ten_user_name` varchar(45) NOT NULL,
  `email_id` varchar(45) NOT NULL,
  `ten_user_pass` varchar(64) NOT NULL,
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `is_active` int DEFAULT '1',
  `is_admin` tinyint DEFAULT '0',
  PRIMARY KEY (`idten_users`),
  KEY `fk_ten_user_mod_id_idx` (`modules_id`),
  KEY `fk_ten_user_principal_id_idx` (`principal_id`),
  KEY `idx_email_pass` (`email_id`,`ten_user_pass`),
  KEY `idx_principle` (`principal_id`),
  FULLTEXT KEY `idx_ser_name_email` (`ten_user_name`,`email_id`),
  CONSTRAINT `fk_ten_user_mod_id` FOREIGN KEY (`modules_id`) REFERENCES `mod_modules` (`idmod_modules`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_ten_user_principal_id` FOREIGN KEY (`principal_id`) REFERENCES `ten_users` (`idten_users`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ten_users`
--

LOCK TABLES `ten_users` WRITE;
/*!40000 ALTER TABLE `ten_users` DISABLE KEYS */;
INSERT INTO `ten_users` VALUES (21,NULL,3,1,'Visitor admin','admin.visitor@gmail.com','vsitor_hash','2025-07-12 19:59:53',1,1),(22,21,3,1,'desk Executive','desk.visitor@gmail.com','desh_hash','2025-07-12 20:22:08',1,0),(23,21,3,1,'Security Officer','security.visitor@gmail.com','security_hash','2025-07-12 21:05:24',1,0),(24,21,3,1,'Visitor Approver','approver.visitor@gmail.com','approver_hash','2025-07-12 21:25:38',1,0);
/*!40000 ALTER TABLE `ten_users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_client`
--

DROP TABLE IF EXISTS `user_client`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user_client` (
  `iduser_client` int NOT NULL AUTO_INCREMENT,
  `client_name` varchar(45) NOT NULL,
  `user_id` int NOT NULL,
  `module_id` int NOT NULL,
  `firebase_token` varchar(1000) DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `is_active` int DEFAULT '1',
  PRIMARY KEY (`iduser_client`),
  KEY `fk_user_client_user_id_idx` (`user_id`),
  KEY `fk_user_client_module_id_idx` (`module_id`),
  FULLTEXT KEY `idx_client_name_ser` (`client_name`),
  CONSTRAINT `fk_user_client_module_id` FOREIGN KEY (`module_id`) REFERENCES `mod_modules` (`idmod_modules`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_user_client_user_id` FOREIGN KEY (`user_id`) REFERENCES `ten_users` (`idten_users`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_client`
--

LOCK TABLES `user_client` WRITE;
/*!40000 ALTER TABLE `user_client` DISABLE KEYS */;
INSERT INTO `user_client` VALUES (3,'Deepak Gurav',22,1,NULL,'2025-07-12 20:52:57',1),(4,'Deepak Gurav',22,1,NULL,'2025-07-12 21:00:03',1);
/*!40000 ALTER TABLE `user_client` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_permissions`
--

DROP TABLE IF EXISTS `user_permissions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user_permissions` (
  `iduser_permissions` int NOT NULL AUTO_INCREMENT,
  `permission_name` varchar(45) NOT NULL,
  `description` varchar(45) NOT NULL,
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `is_active` int DEFAULT '1',
  PRIMARY KEY (`iduser_permissions`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_permissions`
--

LOCK TABLES `user_permissions` WRITE;
/*!40000 ALTER TABLE `user_permissions` DISABLE KEYS */;
INSERT INTO `user_permissions` VALUES (1,'view_all_tabs','Can view all module tabs','2025-07-05 16:11:11',1),(2,'create_entry','Can create entry records','2025-07-05 16:11:11',1),(3,'approve_entry','Can approve entries','2025-07-05 16:11:11',1),(4,'final_approve','Final approval rights','2025-07-05 16:11:11',1),(5,'create_user','Can create users','2025-07-06 13:17:53',1),(6,'create_admin','Can create module respective admin','2025-07-07 09:29:07',1),(7,'create_tenant','Can create tenant','2025-07-07 11:09:22',1),(8,'subscribe_plan','Can subscribe plan','2025-07-07 11:11:17',1);
/*!40000 ALTER TABLE `user_permissions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_role_map`
--

DROP TABLE IF EXISTS `user_role_map`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user_role_map` (
  `iduser_role_map` int NOT NULL AUTO_INCREMENT,
  `ten_user_id` int NOT NULL,
  `role_id` int NOT NULL,
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `is_active` int DEFAULT '1',
  PRIMARY KEY (`iduser_role_map`),
  KEY `fk_user_role_map_user_id_idx` (`ten_user_id`),
  KEY `fk_user_role_map_role_id_idx` (`role_id`),
  KEY `idx_ten_user_role_id` (`ten_user_id`,`role_id`),
  CONSTRAINT `fk_user_role_map_role_id` FOREIGN KEY (`role_id`) REFERENCES `user_roles` (`iduser_roles`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_user_role_map_user_id` FOREIGN KEY (`ten_user_id`) REFERENCES `ten_users` (`idten_users`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_role_map`
--

LOCK TABLES `user_role_map` WRITE;
/*!40000 ALTER TABLE `user_role_map` DISABLE KEYS */;
INSERT INTO `user_role_map` VALUES (21,21,1,'2025-07-12 19:59:55',1),(22,22,2,'2025-07-12 20:22:08',1),(23,23,3,'2025-07-12 21:05:24',1),(24,24,4,'2025-07-12 21:25:38',1);
/*!40000 ALTER TABLE `user_role_map` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_role_permissions`
--

DROP TABLE IF EXISTS `user_role_permissions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user_role_permissions` (
  `iduser_role_permissions` int NOT NULL AUTO_INCREMENT,
  `role_id` int NOT NULL,
  `permission_id` int NOT NULL,
  `module_id` int NOT NULL,
  `is_active` int DEFAULT '1',
  PRIMARY KEY (`iduser_role_permissions`),
  KEY `fk_us_rol_per_role_id_idx` (`role_id`),
  KEY `fk_us_rol_per_pem_id_idx` (`permission_id`),
  KEY `fk_us_rol_per_module_id_idx` (`module_id`),
  KEY `idx_role_per_mod_id` (`role_id`,`permission_id`,`module_id`),
  CONSTRAINT `fk_us_rol_per_module_id` FOREIGN KEY (`module_id`) REFERENCES `mod_modules` (`idmod_modules`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_us_rol_per_pem_id` FOREIGN KEY (`permission_id`) REFERENCES `user_permissions` (`iduser_permissions`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_us_rol_per_role_id` FOREIGN KEY (`role_id`) REFERENCES `user_roles` (`iduser_roles`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=116 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_role_permissions`
--

LOCK TABLES `user_role_permissions` WRITE;
/*!40000 ALTER TABLE `user_role_permissions` DISABLE KEYS */;
INSERT INTO `user_role_permissions` VALUES (111,1,1,2,1),(112,1,5,2,1),(113,2,2,2,1),(114,3,3,2,1),(115,4,4,2,1);
/*!40000 ALTER TABLE `user_role_permissions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_roles`
--

DROP TABLE IF EXISTS `user_roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user_roles` (
  `iduser_roles` int NOT NULL AUTO_INCREMENT,
  `tenant_id` int NOT NULL,
  `module_id` int NOT NULL,
  `role_name` varchar(45) NOT NULL,
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `is_for_admin` tinyint NOT NULL,
  `is_active` int DEFAULT '1',
  PRIMARY KEY (`iduser_roles`),
  KEY `fk_us_role_module_id_idx` (`module_id`),
  KEY `fk_us_role_ten_id_idx` (`tenant_id`),
  CONSTRAINT `fk_us_role_module_id` FOREIGN KEY (`module_id`) REFERENCES `mod_modules` (`idmod_modules`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_us_tenant_id` FOREIGN KEY (`tenant_id`) REFERENCES `ten_tenants` (`idten_tenants`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_roles`
--

LOCK TABLES `user_roles` WRITE;
/*!40000 ALTER TABLE `user_roles` DISABLE KEYS */;
INSERT INTO `user_roles` VALUES (1,3,1,'Visitor Admin Officer','2025-07-05 18:57:25',1,1),(2,3,1,'Front Desk Executive','2025-07-05 18:57:25',0,1),(3,3,1,'Security Officer','2025-07-05 18:57:25',0,1),(4,3,1,'Visitor Approving Manager','2025-07-05 18:57:25',0,1);
/*!40000 ALTER TABLE `user_roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `wf`
--

DROP TABLE IF EXISTS `wf`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `wf` (
  `idwf` int NOT NULL AUTO_INCREMENT,
  `module_id` int NOT NULL,
  `tenant_id` int NOT NULL,
  `wf_name` varchar(60) NOT NULL,
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `is_active` int DEFAULT '1',
  PRIMARY KEY (`idwf`),
  KEY `fk_wf_ten_id_idx` (`tenant_id`),
  KEY `fk_wf_module_id_idx` (`module_id`),
  KEY `idx_mod_ten_id` (`module_id`,`tenant_id`),
  CONSTRAINT `fk_wf_module_id` FOREIGN KEY (`module_id`) REFERENCES `mod_modules` (`idmod_modules`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_wf_ten_id` FOREIGN KEY (`tenant_id`) REFERENCES `ten_tenants` (`idten_tenants`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `wf`
--

LOCK TABLES `wf` WRITE;
/*!40000 ALTER TABLE `wf` DISABLE KEYS */;
INSERT INTO `wf` VALUES (1,1,3,'visitor Management','2025-07-05 16:12:45',1);
/*!40000 ALTER TABLE `wf` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `wf_steps`
--

DROP TABLE IF EXISTS `wf_steps`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `wf_steps` (
  `idwf_steps` int NOT NULL AUTO_INCREMENT,
  `work_flow_id` int NOT NULL,
  `step_name` varchar(45) NOT NULL,
  `step_order` int NOT NULL,
  `role_id` int NOT NULL,
  `permission_id` int NOT NULL,
  `is_final_step` tinyint NOT NULL,
  PRIMARY KEY (`idwf_steps`),
  KEY `fk_wf_step_wf_id_idx` (`work_flow_id`),
  KEY `fk_wf_steps_rol_id_idx` (`role_id`),
  KEY `k_wf_steps_per_id_idx` (`permission_id`),
  KEY `idx_steps_wf_role_per_id` (`work_flow_id`,`role_id`,`permission_id`),
  CONSTRAINT `fk_wf_step_wf_id` FOREIGN KEY (`work_flow_id`) REFERENCES `wf` (`idwf`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_wf_steps_rol_id` FOREIGN KEY (`role_id`) REFERENCES `user_roles` (`iduser_roles`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `k_wf_steps_per_id` FOREIGN KEY (`permission_id`) REFERENCES `user_permissions` (`iduser_permissions`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `wf_steps`
--

LOCK TABLES `wf_steps` WRITE;
/*!40000 ALTER TABLE `wf_steps` DISABLE KEYS */;
INSERT INTO `wf_steps` VALUES (1,1,'Front Desk Registration',1,2,2,0),(2,1,'Initial Security Check',2,3,3,0),(3,1,'Final Visitor Approval',3,4,4,1);
/*!40000 ALTER TABLE `wf_steps` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'ops_hub_new'
--

--
-- Dumping routines for database 'ops_hub_new'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-07-14  9:11:40
