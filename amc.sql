-- phpMyAdmin SQL Dump
-- version 4.5.1
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Aug 24, 2017 at 03:42 AM
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
  `opening_date` datetime DEFAULT NULL,
  `ics_no` int(11) DEFAULT NULL,
  `ics_amount` decimal(13,2) DEFAULT NULL,
  `ipuc_amount` decimal(13,2) DEFAULT NULL,
  `outstanding_balance` decimal(13,2) DEFAULT NULL,
  `account_status` int(11) DEFAULT NULL,
  `withdrawal_date` datetime DEFAULT NULL
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
-- Table structure for table `capitals_transaction_line`
--

CREATE TABLE `capitals_transaction_line` (
  `cap_trans_line_id` int(11) NOT NULL,
  `capital_transaction_id` int(11) DEFAULT NULL,
  `particular_id` int(11) DEFAULT NULL,
  `type` int(11) DEFAULT NULL,
  `amount` decimal(13,2) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `chart_of_accounts`
--

CREATE TABLE `chart_of_accounts` (
  `code` int(11) NOT NULL,
  `account_title` varchar(75) DEFAULT NULL
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
-- Table structure for table `general`
--

CREATE TABLE `general` (
  `id` varchar(7) NOT NULL,
  `interest_rate` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `general`
--

INSERT INTO `general` (`id`, `interest_rate`) VALUES
('control', 0.03);

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
-- Table structure for table `loans`
--

CREATE TABLE `loans` (
  `loan_account_id` int(11) NOT NULL,
  `member_id` int(11) DEFAULT NULL,
  `loan_type` int(11) DEFAULT NULL,
  `request_type` int(11) DEFAULT NULL,
  `date_granted` datetime DEFAULT NULL,
  `approval_no` int(11) DEFAULT NULL,
  `term` int(11) DEFAULT NULL,
  `orig_amount` decimal(13,2) DEFAULT NULL,
  `interest_rate` double DEFAULT NULL,
  `purpose` varchar(65) DEFAULT NULL,
  `loan_status` int(11) DEFAULT NULL,
  `outstanding_balance` decimal(13,2) DEFAULT NULL,
  `date_terminated` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `loan_transaction`
--

CREATE TABLE `loan_transaction` (
  `loan_transaction_id` int(11) NOT NULL,
  `loan_account_id` int(11) DEFAULT NULL,
  `transaction_type` int(11) DEFAULT NULL,
  `date` datetime DEFAULT NULL,
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
  `loan_transaction_id` int(11) DEFAULT NULL,
  `particular_id` int(11) DEFAULT NULL,
  `type` int(11) DEFAULT NULL,
  `amount` decimal(13,2) DEFAULT NULL
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
(1, 'DELA CRUZ', 'JUAN', 'SANTOS', '1981-11-21', 'MALE', 'RIVERDALE, DAVAO CITY', '09123456789', 'PHYSICIAN', 'SOUTHERN PHILIPPINES MEDICAL CENTER', 'CHIEF OF CLINICS', '500000.00', '12345', 'DOCTOR OF MEDICINE', 1, 'CATHOLIC', 1, 'JUANA DELA CRUZ', 0, 1, '2015-06-07', 123456, NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `savings`
--

CREATE TABLE `savings` (
  `savings_account_id` int(11) NOT NULL,
  `member_id` int(11) DEFAULT NULL,
  `opening_date` date DEFAULT NULL,
  `outstanding_balance` decimal(13,2) DEFAULT NULL,
  `account_status` int(11) DEFAULT NULL,
  `termination_date` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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
-- Table structure for table `savings_transaction_line`
--

CREATE TABLE `savings_transaction_line` (
  `sav_trans_line_id` int(11) NOT NULL,
  `savings_transaction_id` int(11) DEFAULT NULL,
  `particular_id` int(11) DEFAULT NULL,
  `type` int(11) DEFAULT NULL,
  `amount` decimal(13,2) DEFAULT NULL
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
-- Indexes for table `capitals_transaction_line`
--
ALTER TABLE `capitals_transaction_line`
  ADD PRIMARY KEY (`cap_trans_line_id`),
  ADD KEY `captransline_captrans_idx` (`capital_transaction_id`),
  ADD KEY `captrans_particular_idx` (`particular_id`);

--
-- Indexes for table `chart_of_accounts`
--
ALTER TABLE `chart_of_accounts`
  ADD PRIMARY KEY (`code`);

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
-- Indexes for table `loan_transaction_line`
--
ALTER TABLE `loan_transaction_line`
  ADD PRIMARY KEY (`loan_trans_line_id`),
  ADD KEY `loantransline_loantrans_idx` (`loan_transaction_id`),
  ADD KEY `loantrans_particular_idx` (`particular_id`);

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
-- Indexes for table `savings_transaction_line`
--
ALTER TABLE `savings_transaction_line`
  ADD PRIMARY KEY (`sav_trans_line_id`),
  ADD KEY `savtransline_savtrans_idx` (`savings_transaction_id`),
  ADD KEY `savtrans_particular_idx` (`particular_id`);

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
-- AUTO_INCREMENT for table `capitals_transaction_line`
--
ALTER TABLE `capitals_transaction_line`
  MODIFY `cap_trans_line_id` int(11) NOT NULL AUTO_INCREMENT;
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
  MODIFY `savings_account_id` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `savings_transaction`
--
ALTER TABLE `savings_transaction`
  MODIFY `savings_transaction_id` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `savings_transaction_line`
--
ALTER TABLE `savings_transaction_line`
  MODIFY `sav_trans_line_id` int(11) NOT NULL AUTO_INCREMENT;
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
-- Constraints for table `capitals_transaction_line`
--
ALTER TABLE `capitals_transaction_line`
  ADD CONSTRAINT `captrans_particular` FOREIGN KEY (`particular_id`) REFERENCES `chart_of_accounts` (`code`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `captransline_captrans` FOREIGN KEY (`capital_transaction_id`) REFERENCES `capitals_transaction` (`capital_transaction_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

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
-- Constraints for table `loan_transaction_line`
--
ALTER TABLE `loan_transaction_line`
  ADD CONSTRAINT `loantrans_particular` FOREIGN KEY (`particular_id`) REFERENCES `chart_of_accounts` (`code`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `loantransline_loantrans` FOREIGN KEY (`loan_transaction_id`) REFERENCES `loan_transaction` (`loan_transaction_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

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
-- Constraints for table `savings_transaction_line`
--
ALTER TABLE `savings_transaction_line`
  ADD CONSTRAINT `savtrans_particular` FOREIGN KEY (`particular_id`) REFERENCES `chart_of_accounts` (`code`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `savtransline_savtrans` FOREIGN KEY (`savings_transaction_id`) REFERENCES `savings_transaction` (`savings_transaction_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
