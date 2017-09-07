-- phpMyAdmin SQL Dump
-- version 4.5.1
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Sep 07, 2017 at 03:29 AM
-- Server version: 10.1.19-MariaDB
-- PHP Version: 7.0.13

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `amc`
--

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
-- Table structure for table `chart_of_accounts`
--

CREATE TABLE `chart_of_accounts` (
  `code` int(11) NOT NULL,
  `title` varchar(100) DEFAULT NULL,
  `type` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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
  `timestamp` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `interest_rate_log`
--

INSERT INTO `interest_rate_log` (`id`, `interest_rate`, `timestamp`) VALUES
(1, 0.03, '2017-09-06 18:43:19');

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
(1, 'DELA CRUZ', 'JUAN', 'SANTOS', '1981-11-21', 'MALE', 'RIVERDALE, DAVAO CITY', '09123456789', 'PHYSICIAN', 'SOUTHERN PHILIPPINES MEDICAL CENTER', 'CHIEF OF CLINICS', '500000.00', '123456789123', 'DOCTOR OF MEDICINE', 1, 'CATHOLIC', 1, 'JUANA DELA CRUZ', 0, 1, '2015-06-07', 123456, NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `savings`
--

CREATE TABLE `savings` (
  `savings_account_id` int(11) NOT NULL,
  `member_id` int(11) DEFAULT NULL,
  `opening_date` date DEFAULT NULL,
  `initial_balance` decimal(13,2) DEFAULT NULL,
  `outstanding_balance` decimal(13,2) DEFAULT NULL,
  `interest_rate` double DEFAULT NULL,
  `account_status` int(11) DEFAULT NULL,
  `termination_date` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `savings`
--

INSERT INTO `savings` (`savings_account_id`, `member_id`, `opening_date`, `initial_balance`, `outstanding_balance`, `interest_rate`, `account_status`, `termination_date`) VALUES
(1, 1, '2017-09-07', '100.00', '100.00', 0.03, 1, NULL);

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

-- --------------------------------------------------------

--
-- Table structure for table `savings_transaction_lin`
--

CREATE TABLE `savings_transaction_lin` (
  `savings_trans_line_id` int(11) NOT NULL,
  `savings_transaction_id` int(11) DEFAULT NULL,
  `account_code` int(11) DEFAULT NULL,
  `amount` decimal(13,2) DEFAULT NULL,
  `type` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `loans`
--
ALTER TABLE `loans`
  ADD PRIMARY KEY (`loan_account_id`),
  ADD KEY `loan_member_idx` (`member_id`);

--
-- Indexes for table `loan_transaction`
--
ALTER TABLE `loan_transaction`
  ADD PRIMARY KEY (`loan_transaction_id`),
  ADD KEY `loantrans_loan_idx` (`loan_account_id`),
  ADD KEY `loan_encoded_by_idx` (`encoded_by`);

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
-- Indexes for table `savings_transaction`
--
ALTER TABLE `savings_transaction`
  ADD PRIMARY KEY (`savings_transaction_id`),
  ADD KEY `savtrans_savings_idx` (`savings_account_id`),
  ADD KEY `savings_encodedby_idx` (`encoded_by`);

--
-- Indexes for table `savings_transaction_lin`
--
ALTER TABLE `savings_transaction_lin`
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
-- AUTO_INCREMENT for table `capitals`
--
ALTER TABLE `capitals`
  MODIFY `capital_account_id` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `capitals_transaction`
--
ALTER TABLE `capitals_transaction`
  MODIFY `capital_transaction_id` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `chart_of_accounts_log`
--
ALTER TABLE `chart_of_accounts_log`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;
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
-- AUTO_INCREMENT for table `loan_transaction`
--
ALTER TABLE `loan_transaction`
  MODIFY `loan_transaction_id` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `members`
--
ALTER TABLE `members`
  MODIFY `member_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT for table `savings`
--
ALTER TABLE `savings`
  MODIFY `savings_account_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT for table `savings_transaction`
--
ALTER TABLE `savings_transaction`
  MODIFY `savings_transaction_id` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `savings_transaction_lin`
--
ALTER TABLE `savings_transaction_lin`
  MODIFY `savings_trans_line_id` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT;
--
-- Constraints for dumped tables
--

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
-- Constraints for table `loans`
--
ALTER TABLE `loans`
  ADD CONSTRAINT `loan_member` FOREIGN KEY (`member_id`) REFERENCES `members` (`member_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `loan_transaction`
--
ALTER TABLE `loan_transaction`
  ADD CONSTRAINT `loan_encoded_by` FOREIGN KEY (`encoded_by`) REFERENCES `users` (`user_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `loantrans_loan` FOREIGN KEY (`loan_account_id`) REFERENCES `loans` (`loan_account_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `savings`
--
ALTER TABLE `savings`
  ADD CONSTRAINT `savings_member` FOREIGN KEY (`member_id`) REFERENCES `members` (`member_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `savings_transaction`
--
ALTER TABLE `savings_transaction`
  ADD CONSTRAINT `savings_encodedby` FOREIGN KEY (`encoded_by`) REFERENCES `users` (`user_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `savtrans_savings` FOREIGN KEY (`savings_account_id`) REFERENCES `savings` (`savings_account_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `savings_transaction_lin`
--
ALTER TABLE `savings_transaction_lin`
  ADD CONSTRAINT `fk_savings_particular` FOREIGN KEY (`account_code`) REFERENCES `chart_of_accounts` (`code`),
  ADD CONSTRAINT `fk_savings_transaction` FOREIGN KEY (`savings_transaction_id`) REFERENCES `savings_transaction` (`savings_transaction_id`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
