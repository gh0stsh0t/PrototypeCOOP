-- phpMyAdmin SQL Dump
-- version 4.7.4
-- https://www.phpmyadmin.net/
--
-- Host: localhost
-- Generation Time: Sep 20, 2017 at 08:49 AM
-- Server version: 10.1.19-MariaDB
-- PHP Version: 7.0.13

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `amc`
--

DELIMITER $$
--
-- Procedures
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `displayQuarterMonthTable` (IN `mn` INT, IN `yr` INT)  READS SQL DATA
BEGIN

SELECT s.savings_account_id AS 'Account Number', CONCAT(m.family_name, ', ', m.first_name, ' ', m.middle_name) AS Name, amc.getMonthBeginningBalance(mn,yr,st.savings_account_id) as 'Beginning Balance', amc.computeMonthOutstandingBalance(mn,yr,st.savings_account_id) as 'Outstanding Balance', amc.computeMonthInterest(mn,yr,st.savings_account_id) AS 'Computed Interest',
amc.computeMonthInterestExpense(mn,yr,st.savings_account_id) AS 'Interest Expense for the Month',
amc.computeQuarterInterest(mn,yr,st.savings_account_id) AS 'Interest Credited for the Quarter',
amc.computeMonthEndBalance(mn,yr,st.savings_account_id) AS 'Month End Balance'
FROM savings s LEFT JOIN savings_transaction st ON s.savings_account_id = st.savings_account_id INNER JOIN members m ON s.member_id = m.member_id WHERE m.status = 1 GROUP BY s.savings_account_id;
END$$

--
-- Functions
--
CREATE DEFINER=`root`@`localhost` FUNCTION `computeMonthAvgDailyBalance` (`mn` INT, `yr` INT, `accountid` INT) RETURNS DECIMAL(13,2) READS SQL DATA
BEGIN
RETURN 
(
    SELECT amc.computeMonthMultipliedSum(MONTH(date),YEAR(date),savings_account_id) / amc.getMonthDays(MONTH(date),YEAR(date)) AS amount FROM amc.savings_transaction WHERE MONTH(date) = mn AND YEAR(date) = yr AND savings_account_id = accountid GROUP BY savings_account_id
);    
END$$

CREATE DEFINER=`root`@`localhost` FUNCTION `computeMonthBalanceDifference` (`mn` INT, `yr` INT, `accountid` INT) RETURNS DECIMAL(13,2) READS SQL DATA
BEGIN
RETURN
(
    SELECT amc.computeMonthOutstandingBalance(MONTH(date),YEAR(date),savings_account_id) - 		   amc.getMonthBeginningBalance(MONTH(date),YEAR(date),savings_account_id)    
    FROM amc.savings_transaction WHERE MONTH(date) = mn AND YEAR(date) = yr AND savings_account_id = accountid GROUP BY savings_account_id
);
END$$

CREATE DEFINER=`root`@`localhost` FUNCTION `computeMonthEndBalance` (`mn` INT, `yr` INT, `accountid` INT) RETURNS DECIMAL(13,2) READS SQL DATA
BEGIN
RETURN
(
    SELECT CASE
    WHEN MONTH(date) % 3 = 0 
    THEN amc.computeMonthOutstandingBalance(MONTH(date),YEAR(date),savings_account_id) + amc.computeQuarterInterest(MONTH(date),YEAR(date),savings_account_id)
    ELSE amc.computeMonthOutstandingBalance(MONTH(date),YEAR(date),savings_account_id)
    END AS balance
    FROM amc.savings_transaction WHERE MONTH(date) = mn AND YEAR(date) = yr AND savings_account_id = accountid GROUP BY savings_account_id
);
END$$

CREATE DEFINER=`root`@`localhost` FUNCTION `computeMonthInterest` (`mn` INT, `yr` INT, `accountid` INT) RETURNS DECIMAL(13,2) READS SQL DATA
BEGIN
RETURN
(
    SELECT amc.computeMonthMultipliedSum(MONTH(date),YEAR(date),savings_account_id) * (amc.getMonthInterestRate(MONTH(date),YEAR(date)) / 365) as computed_interest FROM amc.savings_transaction WHERE MONTH(date) = mn AND YEAR(date) = yr AND savings_account_id = accountid GROUP BY savings_account_id
);
END$$

CREATE DEFINER=`root`@`localhost` FUNCTION `computeMonthInterestExpense` (`mn` INT, `yr` INT, `accountid` INT) RETURNS DECIMAL(13,2) READS SQL DATA
BEGIN
DECLARE x DECIMAL(13,2);
IF (SELECT amc.computeMonthAvgDailyBalance(mn,yr,accountid) FROM amc.savings_transaction GROUP BY savings_account_id) > (SELECT amc.getMonthAvgDailyBalance(mn,yr) FROM amc.avg_daily_balance_log GROUP BY id)
	THEN SET x = (SELECT amc.computeMonthInterest(mn,yr,accountid) FROM amc.savings_transaction GROUP BY savings_account_id);
ELSE SET x = 0;
END IF;
RETURN x;
END$$

CREATE DEFINER=`root`@`localhost` FUNCTION `computeMonthMultipliedSum` (`mn` INT, `yr` INT, `accountid` INT) RETURNS DECIMAL(13,2) READS SQL DATA
BEGIN
RETURN (SELECT SUM(((total_amount * (amc.getMonthDays(MONTH(date), YEAR(date)) - DAY(date))) * transaction_type)) AS amount FROM amc.savings_transaction WHERE MONTH(date) = mn AND YEAR(date) = yr AND savings_account_id = accountid);
END$$

CREATE DEFINER=`root`@`localhost` FUNCTION `computeMonthOutstandingBalance` (`mn` INT, `yr` INT, `accountid` INT) RETURNS DECIMAL(13,2) READS SQL DATA
BEGIN
RETURN
(
    SELECT SUM(total_amount * transaction_type) + amc.getMonthBeginningBalance(mn,yr,accountid) AS outstanding_balance FROM savings_transaction WHERE MONTH(date) = mn AND YEAR(date) = yr AND savings_account_id = accountid
);
END$$

CREATE DEFINER=`root`@`localhost` FUNCTION `computeQuarterInterest` (`mn` INT, `yr` INT, `accountid` INT) RETURNS DECIMAL(13,2) READS SQL DATA
BEGIN
RETURN
(
    SELECT amc.computeMonthInterestExpense(MONTH(date)-2,YEAR(date),savings_account_id) + 		   amc.computeMonthInterestExpense(MONTH(date)-1,YEAR(date),savings_account_id) +
    amc.computeMonthInterestExpense(MONTH(date)-2,YEAR(date),savings_account_id)    
    FROM amc.savings_transaction WHERE MONTH(date) = mn AND YEAR(date) = yr AND savings_account_id = accountid GROUP BY savings_account_id
);
END$$

CREATE DEFINER=`root`@`localhost` FUNCTION `getMonthAvgDailyBalance` (`mn` INT, `yr` INT) RETURNS DECIMAL(13,2) READS SQL DATA
BEGIN
RETURN
(
    SELECT amount FROM amc.avg_daily_balance_log WHERE MONTH(date) <= mn AND YEAR(date) <= yr ORDER BY date DESC LIMIT 1
);
END$$

CREATE DEFINER=`root`@`localhost` FUNCTION `getMonthBeginningBalance` (`mn` INT, `yr` INT, `accountid` INT) RETURNS DECIMAL(13,2) READS SQL DATA
BEGIN
RETURN
(
SELECT amount FROM amc.savings_balance_log WHERE savings_account_id = accountid AND MONTH(date) = mn AND YEAR(date) = yr ORDER BY date DESC LIMIT 1
);
END$$

CREATE DEFINER=`root`@`localhost` FUNCTION `getMonthDays` (`month` INT, `year` INT) RETURNS INT(11) BEGIN
DECLARE x int;
IF month = 4 OR month = 6 OR month = 9 OR month = 11
	THEN SET x = 30;
ELSEIF month = 2 AND year % 4 = 0
	THEN SET x = 29;
ELSEIF month = 2
 	THEN SET x = 28;
ELSE SET x = 31;
END IF;
RETURN x;
END$$

CREATE DEFINER=`root`@`localhost` FUNCTION `getMonthInterestRate` (`mn` INT, `yr` INT) RETURNS DECIMAL(13,2) READS SQL DATA
BEGIN
RETURN
(
    SELECT interest_rate FROM amc.interest_rate_log WHERE MONTH(date) <= mn AND YEAR(date) <= yr ORDER BY date DESC LIMIT 1
);
END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `avg_daily_balance_log`
--

CREATE TABLE `avg_daily_balance_log` (
  `id` int(11) NOT NULL,
  `date` date NOT NULL,
  `amount` decimal(13,2) NOT NULL,
  `updated_by` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `avg_daily_balance_log`
--

INSERT INTO `avg_daily_balance_log` (`id`, `date`, `amount`, `updated_by`) VALUES
(1, '2017-09-14', '500.00', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `capitals`
--

CREATE TABLE `capitals` (
  `capital_account_id` int(11) NOT NULL,
  `member_id` int(11) DEFAULT NULL,
  `opening_date` date DEFAULT NULL,
  `ics_no` int(11) DEFAULT NULL,
  `ics_amount` decimal(13,2) DEFAULT NULL,
  `ipuc_amount` decimal(13,2) DEFAULT NULL,
  `outstanding_balance` decimal(13,2) DEFAULT NULL,
  `account_status` int(11) DEFAULT NULL,
  `withdrawal_date` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `capitals`
--

INSERT INTO `capitals` (`capital_account_id`, `member_id`, `opening_date`, `ics_no`, `ics_amount`, `ipuc_amount`, `outstanding_balance`, `account_status`, `withdrawal_date`) VALUES
(1, 1, '2017-09-12', NULL, NULL, NULL, NULL, 1, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `capitals_transaction`
--

CREATE TABLE `capitals_transaction` (
  `capital_transaction_id` int(11) NOT NULL,
  `capital_account_id` int(11) DEFAULT NULL,
  `total_amount` decimal(13,2) DEFAULT NULL,
  `encoded_by` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `capitals_transaction_line`
--

CREATE TABLE `capitals_transaction_line` (
  `capital_trans_line_id` int(11) NOT NULL,
  `capital_transaction_id` int(11) DEFAULT NULL,
  `account_code` int(11) DEFAULT NULL,
  `amount` decimal(13,2) DEFAULT NULL,
  `type` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `chart_of_accounts`
--

CREATE TABLE `chart_of_accounts` (
  `code` int(11) NOT NULL,
  `title` varchar(100) DEFAULT NULL,
  `type` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `chart_of_accounts`
--

INSERT INTO `chart_of_accounts` (`code`, `title`, `type`) VALUES
(101, 'Cash', 0),
(102, 'Notes Payable', 2);

-- --------------------------------------------------------

--
-- Table structure for table `chart_of_accounts_log`
--

CREATE TABLE `chart_of_accounts_log` (
  `id` int(11) NOT NULL,
  `code` int(11) NOT NULL,
  `timestamp` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `total_amount` decimal(13,2) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `chart_of_accounts_log`
--

INSERT INTO `chart_of_accounts_log` (`id`, `code`, `timestamp`, `total_amount`) VALUES
(1, 101, '2017-09-07 03:34:20', '10000.00'),
(2, 102, '2017-09-07 03:34:20', '30000.00'),
(3, 101, '2017-09-12 06:12:59', '10020.00'),
(4, 102, '2017-09-12 06:12:59', '30020.00'),
(5, 101, '2017-09-12 06:42:47', '9995.00'),
(6, 102, '2017-09-12 06:42:47', '29995.00'),
(7, 102, '2017-09-12 06:46:02', '30025.00'),
(8, 101, '2017-09-12 06:46:02', '10025.00'),
(9, 101, '2017-09-19 03:06:28', '10045.00'),
(10, 102, '2017-09-19 03:06:28', '30045.00');

-- --------------------------------------------------------

--
-- Table structure for table `comakers`
--

CREATE TABLE `comakers` (
  `comaker_id` int(11) NOT NULL,
  `loan_id` int(11) DEFAULT NULL,
  `name` varchar(75) DEFAULT NULL,
  `address` varchar(75) DEFAULT NULL,
  `company_name` varchar(65) DEFAULT NULL,
  `position` varchar(45) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `dividend_general`
--

CREATE TABLE `dividend_general` (
  `dividend_id` int(11) NOT NULL,
  `year` int(11) DEFAULT NULL,
  `total_paid_up` decimal(13,2) DEFAULT NULL,
  `net_surplus` decimal(13,2) DEFAULT NULL,
  `reserve_fund` decimal(13,2) DEFAULT NULL,
  `date_set` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `individual_dividends`
--

CREATE TABLE `individual_dividends` (
  `ind_dividend_id` int(11) NOT NULL,
  `capital_account_id` int(11) DEFAULT NULL,
  `dividend_id` int(11) DEFAULT NULL,
  `current_balance` decimal(13,2) DEFAULT NULL,
  `dividend_amount` decimal(13,2) DEFAULT NULL,
  `date_computed` datetime DEFAULT NULL,
  `date_released` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `interest_rate_log`
--

CREATE TABLE `interest_rate_log` (
  `id` int(11) NOT NULL,
  `interest_rate` double NOT NULL,
  `date` date NOT NULL,
  `updated_by` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `interest_rate_log`
--

INSERT INTO `interest_rate_log` (`id`, `interest_rate`, `date`, `updated_by`) VALUES
(1, 0.03, '2017-08-07', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `loans`
--

CREATE TABLE `loans` (
  `loan_account_id` int(11) NOT NULL,
  `member_id` int(11) DEFAULT NULL,
  `loan_type` int(11) DEFAULT NULL,
  `request_type` int(11) DEFAULT NULL,
  `date_granted` date DEFAULT NULL,
  `approval_no` int(11) DEFAULT NULL,
  `term` int(11) DEFAULT NULL,
  `orig_amount` decimal(13,2) DEFAULT NULL,
  `interest_rate` double DEFAULT NULL,
  `purpose` varchar(65) DEFAULT NULL,
  `loan_status` int(11) DEFAULT NULL,
  `outstanding_balance` decimal(13,2) DEFAULT NULL,
  `date_terminated` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `loan_balance_log`
--

CREATE TABLE `loan_balance_log` (
  `id` int(11) NOT NULL,
  `loan_account_id` int(11) NOT NULL,
  `date` date NOT NULL,
  `amount` decimal(13,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `loan_transaction`
--

CREATE TABLE `loan_transaction` (
  `loan_transaction_id` int(11) NOT NULL,
  `loan_account_id` int(11) DEFAULT NULL,
  `transaction_type` int(11) DEFAULT NULL,
  `date` date DEFAULT NULL,
  `total_amount` decimal(13,2) DEFAULT NULL,
  `principal` decimal(13,2) DEFAULT NULL,
  `interest` decimal(13,2) DEFAULT NULL,
  `penalty` decimal(13,2) DEFAULT NULL,
  `encoded_by` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `loan_transaction_line`
--

CREATE TABLE `loan_transaction_line` (
  `loan_trans_line_id` int(11) NOT NULL,
  `loan_transaction_id` int(11) NOT NULL,
  `account_code` int(11) NOT NULL,
  `amount` decimal(13,2) NOT NULL,
  `type` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `members`
--

CREATE TABLE `members` (
  `member_id` int(11) NOT NULL,
  `family_name` varchar(45) DEFAULT NULL,
  `first_name` varchar(45) DEFAULT NULL,
  `middle_name` varchar(45) DEFAULT NULL,
  `birthdate` date DEFAULT NULL,
  `gender` varchar(6) DEFAULT NULL,
  `address` varchar(75) DEFAULT NULL,
  `contact_no` varchar(20) DEFAULT NULL,
  `occupation` varchar(45) DEFAULT NULL,
  `company_name` varchar(65) DEFAULT NULL,
  `position` varchar(45) DEFAULT NULL,
  `annual_income` decimal(13,2) DEFAULT NULL,
  `tin` varchar(12) DEFAULT NULL,
  `educ_attainment` varchar(45) DEFAULT NULL,
  `civil_status` int(11) DEFAULT NULL,
  `religion` varchar(20) DEFAULT NULL,
  `no_of_dependents` int(11) DEFAULT NULL,
  `beneficiary_name` varchar(45) DEFAULT NULL,
  `type` int(11) DEFAULT NULL,
  `status` int(11) DEFAULT NULL,
  `acceptance_date` date DEFAULT NULL,
  `acceptance_no` int(11) DEFAULT NULL,
  `termination_date` date DEFAULT NULL,
  `termination_no` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `members`
--

INSERT INTO `members` (`member_id`, `family_name`, `first_name`, `middle_name`, `birthdate`, `gender`, `address`, `contact_no`, `occupation`, `company_name`, `position`, `annual_income`, `tin`, `educ_attainment`, `civil_status`, `religion`, `no_of_dependents`, `beneficiary_name`, `type`, `status`, `acceptance_date`, `acceptance_no`, `termination_date`, `termination_no`) VALUES
(1, 'DELA CRUZ', 'JUAN', 'SANTOS', '1981-11-21', 'MALE', 'RIVERDALE, DAVAO CITY', '09123456789', 'PHYSICIAN', 'SOUTHERN PHILIPPINES MEDICAL CENTER', 'CHIEF OF CLINICS', '500000.00', '123456789123', 'DOCTOR OF MEDICINE', 1, 'CATHOLIC', 1, 'JUANA DELA CRUZ', 0, 1, '2015-06-07', 123456, NULL, NULL),
(2, 'RIZAL', 'JOSE', 'MERCADO', '1989-06-19', 'MALE', 'DAVAO CITY', '09123456788', 'WRITER', 'LA SOLIDARIDAD', 'EDITOR', '100000.00', '123456789123', 'HIGH SCHOOL GRADUATE', 0, 'CATHOLIC', 0, 'NONE', 0, 1, '2017-09-01', 123, NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `savings`
--

CREATE TABLE `savings` (
  `savings_account_id` int(11) NOT NULL,
  `member_id` int(11) DEFAULT NULL,
  `opening_date` date DEFAULT NULL,
  `initial_balance` decimal(13,2) DEFAULT NULL,
  `account_status` int(11) DEFAULT NULL,
  `termination_date` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `savings`
--

INSERT INTO `savings` (`savings_account_id`, `member_id`, `opening_date`, `initial_balance`, `account_status`, `termination_date`) VALUES
(1, 1, '2017-09-07', '100.00', 1, NULL),
(2, 2, '2017-09-19', NULL, 1, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `savings_balance_log`
--

CREATE TABLE `savings_balance_log` (
  `id` int(11) NOT NULL,
  `savings_account_id` int(11) NOT NULL,
  `date` date NOT NULL,
  `amount` decimal(13,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `savings_balance_log`
--

INSERT INTO `savings_balance_log` (`id`, `savings_account_id`, `date`, `amount`) VALUES
(1, 1, '2017-09-01', '100.00'),
(2, 1, '2017-08-01', '200.00');

-- --------------------------------------------------------

--
-- Table structure for table `savings_transaction`
--

CREATE TABLE `savings_transaction` (
  `savings_transaction_id` int(11) NOT NULL,
  `savings_account_id` int(11) DEFAULT NULL,
  `transaction_type` int(11) DEFAULT NULL,
  `date` date DEFAULT NULL,
  `time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `total_amount` decimal(13,2) DEFAULT NULL,
  `interest_rate` double DEFAULT NULL,
  `encoded_by` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `savings_transaction`
--

INSERT INTO `savings_transaction` (`savings_transaction_id`, `savings_account_id`, `transaction_type`, `date`, `time`, `total_amount`, `interest_rate`, `encoded_by`) VALUES
(4, 1, -1, '2017-09-12', '2017-09-14 02:08:31', '10.00', NULL, NULL),
(18, 1, 1, '2017-09-12', '2017-09-12 06:11:05', '20.00', NULL, NULL),
(19, 1, 1, '2017-09-12', '2017-09-12 06:12:59', '20.00', NULL, NULL),
(20, 1, -1, '2017-09-12', '2017-09-14 02:08:39', '25.00', NULL, NULL),
(21, 1, 1, '2017-09-12', '2017-09-12 06:46:02', '30.00', NULL, NULL),
(22, 1, 1, '2017-08-08', '2017-09-14 01:50:13', '35.00', NULL, NULL),
(23, 1, -1, '2017-08-30', '2017-09-14 02:08:47', '14.00', NULL, NULL),
(24, 1, 1, '2017-09-19', '2017-09-19 03:06:27', '20.00', NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `savings_transaction_line`
--

CREATE TABLE `savings_transaction_line` (
  `savings_trans_line_id` int(11) NOT NULL,
  `savings_transaction_id` int(11) DEFAULT NULL,
  `account_code` int(11) DEFAULT NULL,
  `amount` decimal(13,2) DEFAULT NULL,
  `type` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `savings_transaction_line`
--

INSERT INTO `savings_transaction_line` (`savings_trans_line_id`, `savings_transaction_id`, `account_code`, `amount`, `type`) VALUES
(5, 19, 101, '20.00', 0),
(6, 19, 102, '20.00', 1),
(7, 20, 101, '25.00', 1),
(8, 20, 102, '25.00', 0),
(9, 21, 102, '30.00', 1),
(10, 21, 101, '30.00', 0),
(11, 24, 101, '20.00', 0),
(12, 24, 102, '20.00', 1);

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `user_id` int(11) NOT NULL,
  `last_name` varchar(45) DEFAULT NULL,
  `first_name` varchar(45) DEFAULT NULL,
  `middle_name` varchar(45) DEFAULT NULL,
  `username` varchar(45) DEFAULT NULL,
  `password` varchar(45) DEFAULT NULL,
  `user_type` int(11) DEFAULT NULL,
  `user_status` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `avg_daily_balance_log`
--
ALTER TABLE `avg_daily_balance_log`
  ADD PRIMARY KEY (`id`),
  ADD KEY `updated_by` (`updated_by`);

--
-- Indexes for table `capitals`
--
ALTER TABLE `capitals`
  ADD PRIMARY KEY (`capital_account_id`),
  ADD KEY `capitals_members_idx` (`member_id`);

--
-- Indexes for table `capitals_transaction`
--
ALTER TABLE `capitals_transaction`
  ADD PRIMARY KEY (`capital_transaction_id`),
  ADD KEY `captrans_capitals_idx` (`capital_account_id`),
  ADD KEY `capital_encodedby_idx` (`encoded_by`);

--
-- Indexes for table `capitals_transaction_line`
--
ALTER TABLE `capitals_transaction_line`
  ADD KEY `capital_transaction_id` (`capital_transaction_id`),
  ADD KEY `account_code` (`account_code`);

--
-- Indexes for table `chart_of_accounts`
--
ALTER TABLE `chart_of_accounts`
  ADD PRIMARY KEY (`code`);

--
-- Indexes for table `chart_of_accounts_log`
--
ALTER TABLE `chart_of_accounts_log`
  ADD PRIMARY KEY (`id`),
  ADD KEY `code` (`code`);

--
-- Indexes for table `comakers`
--
ALTER TABLE `comakers`
  ADD PRIMARY KEY (`comaker_id`),
  ADD KEY `comaker_loan_idx` (`loan_id`);

--
-- Indexes for table `dividend_general`
--
ALTER TABLE `dividend_general`
  ADD PRIMARY KEY (`dividend_id`);

--
-- Indexes for table `individual_dividends`
--
ALTER TABLE `individual_dividends`
  ADD PRIMARY KEY (`ind_dividend_id`),
  ADD KEY `dividend_capitalaccount_idx` (`capital_account_id`),
  ADD KEY `dividend_general_idx` (`dividend_id`);

--
-- Indexes for table `interest_rate_log`
--
ALTER TABLE `interest_rate_log`
  ADD PRIMARY KEY (`id`),
  ADD KEY `updated_by` (`updated_by`);

--
-- Indexes for table `loans`
--
ALTER TABLE `loans`
  ADD PRIMARY KEY (`loan_account_id`),
  ADD KEY `loan_member_idx` (`member_id`);

--
-- Indexes for table `loan_balance_log`
--
ALTER TABLE `loan_balance_log`
  ADD PRIMARY KEY (`id`),
  ADD KEY `loan_account_id` (`loan_account_id`);

--
-- Indexes for table `loan_transaction`
--
ALTER TABLE `loan_transaction`
  ADD PRIMARY KEY (`loan_transaction_id`),
  ADD KEY `loantrans_loan_idx` (`loan_account_id`),
  ADD KEY `loan_encoded_by_idx` (`encoded_by`);

--
-- Indexes for table `loan_transaction_line`
--
ALTER TABLE `loan_transaction_line`
  ADD PRIMARY KEY (`loan_trans_line_id`),
  ADD KEY `loan_transaction_id` (`loan_transaction_id`),
  ADD KEY `account_code` (`account_code`);

--
-- Indexes for table `members`
--
ALTER TABLE `members`
  ADD PRIMARY KEY (`member_id`);

--
-- Indexes for table `savings`
--
ALTER TABLE `savings`
  ADD PRIMARY KEY (`savings_account_id`),
  ADD KEY `savings_member_idx` (`member_id`);

--
-- Indexes for table `savings_balance_log`
--
ALTER TABLE `savings_balance_log`
  ADD PRIMARY KEY (`id`),
  ADD KEY `savings_account_id` (`savings_account_id`);

--
-- Indexes for table `savings_transaction`
--
ALTER TABLE `savings_transaction`
  ADD PRIMARY KEY (`savings_transaction_id`),
  ADD KEY `savtrans_savings_idx` (`savings_account_id`),
  ADD KEY `savings_encodedby_idx` (`encoded_by`);

--
-- Indexes for table `savings_transaction_line`
--
ALTER TABLE `savings_transaction_line`
  ADD PRIMARY KEY (`savings_trans_line_id`),
  ADD KEY `savings_transaction_id` (`savings_transaction_id`),
  ADD KEY `account_code` (`account_code`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`user_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `avg_daily_balance_log`
--
ALTER TABLE `avg_daily_balance_log`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `capitals`
--
ALTER TABLE `capitals`
  MODIFY `capital_account_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `capitals_transaction`
--
ALTER TABLE `capitals_transaction`
  MODIFY `capital_transaction_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `chart_of_accounts_log`
--
ALTER TABLE `chart_of_accounts_log`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `comakers`
--
ALTER TABLE `comakers`
  MODIFY `comaker_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `dividend_general`
--
ALTER TABLE `dividend_general`
  MODIFY `dividend_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `individual_dividends`
--
ALTER TABLE `individual_dividends`
  MODIFY `ind_dividend_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `interest_rate_log`
--
ALTER TABLE `interest_rate_log`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `loans`
--
ALTER TABLE `loans`
  MODIFY `loan_account_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `loan_balance_log`
--
ALTER TABLE `loan_balance_log`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `loan_transaction`
--
ALTER TABLE `loan_transaction`
  MODIFY `loan_transaction_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `loan_transaction_line`
--
ALTER TABLE `loan_transaction_line`
  MODIFY `loan_trans_line_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `members`
--
ALTER TABLE `members`
  MODIFY `member_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `savings`
--
ALTER TABLE `savings`
  MODIFY `savings_account_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `savings_balance_log`
--
ALTER TABLE `savings_balance_log`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `savings_transaction`
--
ALTER TABLE `savings_transaction`
  MODIFY `savings_transaction_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;

--
-- AUTO_INCREMENT for table `savings_transaction_line`
--
ALTER TABLE `savings_transaction_line`
  MODIFY `savings_trans_line_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `avg_daily_balance_log`
--
ALTER TABLE `avg_daily_balance_log`
  ADD CONSTRAINT `fk_avgdailybalance_updated_by` FOREIGN KEY (`updated_by`) REFERENCES `users` (`user_id`);

--
-- Constraints for table `capitals`
--
ALTER TABLE `capitals`
  ADD CONSTRAINT `capitals_members` FOREIGN KEY (`member_id`) REFERENCES `members` (`member_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `capitals_transaction`
--
ALTER TABLE `capitals_transaction`
  ADD CONSTRAINT `capital_encodedby` FOREIGN KEY (`encoded_by`) REFERENCES `users` (`user_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `captrans_capitals` FOREIGN KEY (`capital_account_id`) REFERENCES `capitals` (`capital_account_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `capitals_transaction_line`
--
ALTER TABLE `capitals_transaction_line`
  ADD CONSTRAINT `fk_capital_particular` FOREIGN KEY (`account_code`) REFERENCES `chart_of_accounts` (`code`),
  ADD CONSTRAINT `fk_capital_transaction` FOREIGN KEY (`capital_transaction_id`) REFERENCES `capitals_transaction` (`capital_transaction_id`);

--
-- Constraints for table `chart_of_accounts_log`
--
ALTER TABLE `chart_of_accounts_log`
  ADD CONSTRAINT `fk_account_code_log` FOREIGN KEY (`code`) REFERENCES `chart_of_accounts` (`code`);

--
-- Constraints for table `comakers`
--
ALTER TABLE `comakers`
  ADD CONSTRAINT `comaker_loan` FOREIGN KEY (`loan_id`) REFERENCES `loans` (`loan_account_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `individual_dividends`
--
ALTER TABLE `individual_dividends`
  ADD CONSTRAINT `dividend_capitalaccount` FOREIGN KEY (`capital_account_id`) REFERENCES `capitals` (`capital_account_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `dividend_general` FOREIGN KEY (`dividend_id`) REFERENCES `dividend_general` (`dividend_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `interest_rate_log`
--
ALTER TABLE `interest_rate_log`
  ADD CONSTRAINT `fk_interest_updated_by` FOREIGN KEY (`updated_by`) REFERENCES `users` (`user_id`);

--
-- Constraints for table `loans`
--
ALTER TABLE `loans`
  ADD CONSTRAINT `loan_member` FOREIGN KEY (`member_id`) REFERENCES `members` (`member_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `loan_balance_log`
--
ALTER TABLE `loan_balance_log`
  ADD CONSTRAINT `fk_loan_balancelog_account` FOREIGN KEY (`loan_account_id`) REFERENCES `loans` (`loan_account_id`);

--
-- Constraints for table `loan_transaction`
--
ALTER TABLE `loan_transaction`
  ADD CONSTRAINT `loan_encoded_by` FOREIGN KEY (`encoded_by`) REFERENCES `users` (`user_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `loantrans_loan` FOREIGN KEY (`loan_account_id`) REFERENCES `loans` (`loan_account_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `loan_transaction_line`
--
ALTER TABLE `loan_transaction_line`
  ADD CONSTRAINT `fk_loan_particular` FOREIGN KEY (`account_code`) REFERENCES `chart_of_accounts` (`code`),
  ADD CONSTRAINT `fk_loan_transaction` FOREIGN KEY (`loan_transaction_id`) REFERENCES `loan_transaction` (`loan_transaction_id`);

--
-- Constraints for table `savings`
--
ALTER TABLE `savings`
  ADD CONSTRAINT `savings_member` FOREIGN KEY (`member_id`) REFERENCES `members` (`member_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `savings_balance_log`
--
ALTER TABLE `savings_balance_log`
  ADD CONSTRAINT `fk_sav_balancelog_account` FOREIGN KEY (`savings_account_id`) REFERENCES `savings` (`savings_account_id`);

--
-- Constraints for table `savings_transaction`
--
ALTER TABLE `savings_transaction`
  ADD CONSTRAINT `savings_encodedby` FOREIGN KEY (`encoded_by`) REFERENCES `users` (`user_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `savtrans_savings` FOREIGN KEY (`savings_account_id`) REFERENCES `savings` (`savings_account_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `savings_transaction_line`
--
ALTER TABLE `savings_transaction_line`
  ADD CONSTRAINT `fk_savings_particular` FOREIGN KEY (`account_code`) REFERENCES `chart_of_accounts` (`code`),
  ADD CONSTRAINT `fk_savings_transaction` FOREIGN KEY (`savings_transaction_id`) REFERENCES `savings_transaction` (`savings_transaction_id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
