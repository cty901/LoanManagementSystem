-- MySQL dump 10.13  Distrib 5.6.17, for Win64 (x86_64)
--
-- Host: localhost    Database: loandb
-- ------------------------------------------------------
-- Server version	5.5.38

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `branch`
--

DROP TABLE IF EXISTS `branch`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `branch` (
  `ID` varchar(32) NOT NULL,
  `BRANCH_ID` varchar(10) DEFAULT NULL,
  `ADDRESS` varchar(100) DEFAULT NULL,
  `PHONE` varchar(15) DEFAULT NULL,
  `EMAIL` varchar(45) DEFAULT NULL,
  `STATUS` tinyint(1) DEFAULT '1',
  `INSERT_USER_ID` varchar(32) DEFAULT NULL,
  `UPDATE_USER_ID` varchar(32) DEFAULT NULL,
  `INSERT_DATETIME` datetime DEFAULT NULL,
  `UPDATE_DATETIME` datetime DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `branch`
--

LOCK TABLES `branch` WRITE;
/*!40000 ALTER TABLE `branch` DISABLE KEYS */;
/*!40000 ALTER TABLE `branch` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `customer`
--

DROP TABLE IF EXISTS `customer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `customer` (
  `ID` varchar(32) NOT NULL,
  `CUSTOMER_ID` varchar(45) NOT NULL,
  `ID_TYPE` set('nic','dl','pp') NOT NULL,
  `ID_NUM` varchar(45) NOT NULL,
  `FIRST_NAME` varchar(45) NOT NULL,
  `LAST_NAME` varchar(45) NOT NULL,
  `DOB` datetime NOT NULL,
  `GENDER` set('male','female') NOT NULL,
  `NATIONALITY` varchar(45) DEFAULT NULL,
  `CIVIL_STATUS` varchar(45) NOT NULL,
  `ADDRESS` varchar(100) DEFAULT NULL,
  `EMAIL` varchar(70) DEFAULT NULL,
  `PHONE_HP1` varchar(15) DEFAULT NULL,
  `PHONE_HP2` varchar(15) DEFAULT NULL,
  `PHONE_RECIDENCE` varchar(15) DEFAULT NULL,
  `REMARK` varchar(100) DEFAULT NULL,
  `ISACTIVE` tinyint(1) DEFAULT '0',
  `STATUS` tinyint(1) DEFAULT '1',
  `INSERT_USER_ID` varchar(32) DEFAULT NULL,
  `UPDATE_USER_ID` varchar(32) DEFAULT NULL,
  `INSERT_DATETIME` datetime DEFAULT NULL,
  `UPDATE_DATETIME` datetime DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `customer`
--

LOCK TABLES `customer` WRITE;
/*!40000 ALTER TABLE `customer` DISABLE KEYS */;
/*!40000 ALTER TABLE `customer` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `employee`
--

DROP TABLE IF EXISTS `employee`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `employee` (
  `ID` varchar(32) NOT NULL,
  `EMP_ID` varchar(45) NOT NULL,
  `ID_TYPE` set('nic','dl','pp') NOT NULL,
  `ID_NUM` varchar(45) NOT NULL,
  `FIRST_NAME` varchar(45) NOT NULL,
  `LAST_NAME` varchar(45) NOT NULL,
  `DOB` datetime NOT NULL,
  `GENDER` set('male','female') NOT NULL,
  `NATIONALITY` varchar(45) DEFAULT NULL,
  `RELIGION` varchar(45) DEFAULT NULL,
  `CIVIL_STATUS` varchar(45) NOT NULL,
  `ADDRESS` varchar(100) DEFAULT NULL,
  `EMAIL` varchar(70) DEFAULT NULL,
  `PHONE_HP1` varchar(15) DEFAULT NULL,
  `PHONE_HP2` varchar(15) DEFAULT NULL,
  `PHONE_RECIDENCE` varchar(15) DEFAULT NULL,
  `ACCOUNT_TYPE` set('admin','staff','other') NOT NULL,
  `USERNAME` varchar(45) NOT NULL,
  `PASSWORD` varchar(45) NOT NULL,
  `PROFPIC` longblob,
  `REMARK` varchar(100) DEFAULT NULL,
  `ISRESIGN` tinyint(1) DEFAULT '0',
  `STATUS` tinyint(1) DEFAULT '1',
  `INSERT_USER_ID` varchar(32) DEFAULT NULL,
  `UPDATE_USER_ID` varchar(32) DEFAULT NULL,
  `INSERT_DATETIME` datetime DEFAULT NULL,
  `UPDATE_DATETIME` datetime DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `employee`
--

LOCK TABLES `employee` WRITE;
/*!40000 ALTER TABLE `employee` DISABLE KEYS */;
INSERT INTO `employee` VALUES ('1','1','nic','862064372v','sampath','sampath','1986-07-24 00:00:00','male','sinhala','buddhism','single','udadumbara','sbpallekumbura@gmail.com','0712732233','0717410955','0815725692','admin','sampath','sampath',NULL,NULL,0,1,NULL,NULL,NULL,NULL);
/*!40000 ALTER TABLE `employee` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `employee_cash`
--

DROP TABLE IF EXISTS `employee_cash`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `employee_cash` (
  `ID` varchar(32) NOT NULL,
  `BORROW_DATE_TIME` datetime DEFAULT NULL,
  `BORROW_AMOUNT` decimal(12,2) DEFAULT NULL,
  `BORROW_REMARK` varchar(100) DEFAULT NULL,
  `RETURN_DATE_TIME` datetime DEFAULT NULL,
  `RETURN_AMOUNT` decimal(12,2) DEFAULT NULL,
  `RETURN_REMARK` varchar(100) DEFAULT NULL,
  `STATUS` tinyint(1) DEFAULT '1',
  `INSERT_USER_ID` varchar(32) DEFAULT NULL,
  `UPDATE_USER_ID` varchar(32) DEFAULT NULL,
  `INSERT_DATETIME` datetime DEFAULT NULL,
  `UPDATE_DATETIME` datetime DEFAULT NULL,
  `FK_EMPLOYEE_ID` varchar(32) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `fk_employee_cash_employee_idx` (`FK_EMPLOYEE_ID`),
  CONSTRAINT `fk_employee_cash_employee` FOREIGN KEY (`FK_EMPLOYEE_ID`) REFERENCES `employee` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `employee_cash`
--

LOCK TABLES `employee_cash` WRITE;
/*!40000 ALTER TABLE `employee_cash` DISABLE KEYS */;
/*!40000 ALTER TABLE `employee_cash` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `loan`
--

DROP TABLE IF EXISTS `loan`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `loan` (
  `ID` varchar(32) NOT NULL,
  `LOAN_ID` varchar(32) DEFAULT NULL,
  `START_DATE` date DEFAULT NULL,
  `END_DATE` varchar(45) DEFAULT NULL,
  `AMOUNT` decimal(12,2) DEFAULT NULL,
  `INSTALLMENT` decimal(12,2) DEFAULT NULL,
  `LOAN_STATUS` tinyint(1) DEFAULT NULL,
  `REMARK` varchar(100) DEFAULT NULL,
  `STATUS` tinyint(1) DEFAULT '1',
  `INSERT_USER_ID` varchar(32) DEFAULT NULL,
  `UPDATE_USER_ID` varchar(32) DEFAULT NULL,
  `INSERT_DATETIME` datetime DEFAULT NULL,
  `UPDATE_DATETIME` datetime DEFAULT NULL,
  `FK_EMPLOYEE_ID` varchar(32) NOT NULL,
  `FK_CUSTOMER_ID` varchar(32) NOT NULL,
  `FK_LOAN_TYPE_ID` varchar(32) NOT NULL,
  `FK_BRANCH_ID` varchar(32) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `fk_loan_employee1_idx` (`FK_EMPLOYEE_ID`),
  KEY `fk_loan_customer1_idx` (`FK_CUSTOMER_ID`),
  KEY `fk_loan_loan_type1_idx` (`FK_LOAN_TYPE_ID`),
  KEY `fk_loan_branch1_idx` (`FK_BRANCH_ID`),
  CONSTRAINT `fk_loan_employee1` FOREIGN KEY (`FK_EMPLOYEE_ID`) REFERENCES `employee` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_loan_customer1` FOREIGN KEY (`FK_CUSTOMER_ID`) REFERENCES `customer` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_loan_loan_type1` FOREIGN KEY (`FK_LOAN_TYPE_ID`) REFERENCES `loan_type` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_loan_branch1` FOREIGN KEY (`FK_BRANCH_ID`) REFERENCES `branch` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `loan`
--

LOCK TABLES `loan` WRITE;
/*!40000 ALTER TABLE `loan` DISABLE KEYS */;
/*!40000 ALTER TABLE `loan` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `loan_type`
--

DROP TABLE IF EXISTS `loan_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `loan_type` (
  `ID` varchar(32) NOT NULL,
  `LOAN_TYPE_ID` varchar(10) DEFAULT NULL,
  `AMOUNT` decimal(12,2) DEFAULT NULL,
  `INSTALLMENT` decimal(12,2) DEFAULT NULL,
  `REMARK` varchar(100) DEFAULT NULL,
  `STATUS` tinyint(1) DEFAULT '1',
  `INSERT_USER_ID` varchar(32) DEFAULT NULL,
  `UPDATE_USER_ID` varchar(32) DEFAULT NULL,
  `INSERT_DATETIME` datetime DEFAULT NULL,
  `UPDATE_DATETIME` datetime DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `loan_type`
--

LOCK TABLES `loan_type` WRITE;
/*!40000 ALTER TABLE `loan_type` DISABLE KEYS */;
/*!40000 ALTER TABLE `loan_type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `payment`
--

DROP TABLE IF EXISTS `payment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `payment` (
  `ID` varchar(32) NOT NULL,
  `PAYMENT_ID` varchar(32) DEFAULT NULL,
  `DATE_TIME` datetime DEFAULT NULL,
  `AMOUNT` decimal(12,2) DEFAULT NULL,
  `REMARK` varchar(100) DEFAULT NULL,
  `STATUS` tinyint(1) DEFAULT '1',
  `INSERT_USER_ID` varchar(32) DEFAULT NULL,
  `UPDATE_USER_ID` varchar(32) DEFAULT NULL,
  `INSERT_DATETIME` datetime DEFAULT NULL,
  `UPDATE_DATETIME` datetime DEFAULT NULL,
  `LOAN_ID` varchar(32) NOT NULL,
  PRIMARY KEY (`ID`,`LOAN_ID`),
  KEY `fk_payment_loan1_idx` (`LOAN_ID`),
  CONSTRAINT `fk_payment_loan1` FOREIGN KEY (`LOAN_ID`) REFERENCES `loan` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `payment`
--

LOCK TABLES `payment` WRITE;
/*!40000 ALTER TABLE `payment` DISABLE KEYS */;
/*!40000 ALTER TABLE `payment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sms`
--

DROP TABLE IF EXISTS `sms`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sms` (
  `ID` varchar(32) NOT NULL,
  `SEND_DATE_TIME` datetime DEFAULT NULL,
  `CONTENT` varchar(45) DEFAULT NULL,
  `STATUS` tinyint(1) DEFAULT '1',
  `INSERT_USER_ID` varchar(32) DEFAULT NULL,
  `UPDATE_USER_ID` varchar(32) DEFAULT NULL,
  `INSERT_DATETIME` datetime DEFAULT NULL,
  `UPDATE_DATETIME` datetime DEFAULT NULL,
  `FK_EMPLOYEE_ID` varchar(32) NOT NULL,
  `FK_CUSTOMER_ID` varchar(32) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `fk_SMS_employee1_idx` (`FK_EMPLOYEE_ID`),
  KEY `fk_SMS_customer1_idx` (`FK_CUSTOMER_ID`),
  CONSTRAINT `fk_SMS_employee1` FOREIGN KEY (`FK_EMPLOYEE_ID`) REFERENCES `employee` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_SMS_customer1` FOREIGN KEY (`FK_CUSTOMER_ID`) REFERENCES `customer` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sms`
--

LOCK TABLES `sms` WRITE;
/*!40000 ALTER TABLE `sms` DISABLE KEYS */;
/*!40000 ALTER TABLE `sms` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2015-08-13 14:37:15
