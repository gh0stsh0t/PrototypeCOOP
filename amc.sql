-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema amc
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `amc` ;

-- -----------------------------------------------------
-- Schema amc
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `amc` ;
USE `amc` ;

-- -----------------------------------------------------
-- Table `amc`.`members`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `amc`.`members` (
  `member_id` INT NOT NULL AUTO_INCREMENT,
  `family_name` VARCHAR(45) NULL,
  `first_name` VARCHAR(45) NULL,
  `middle_name` VARCHAR(45) NULL,
  `birthdate` DATETIME NULL,
  `gender` VARCHAR(6) NULL,
  `address` VARCHAR(75) NULL,
  `contact_no` VARCHAR(20) NULL,
  `occupation` VARCHAR(45) NULL,
  `company_name` VARCHAR(65) NULL,
  `position` VARCHAR(45) NULL,
  `annual_income` DECIMAL(13,2) NULL,
  `tin` INT NULL,
  `educ_attainment` VARCHAR(45) NULL,
  `civil_status` INT NULL,
  `religion` VARCHAR(20) NULL,
  `no_of_dependents` INT NULL,
  `beneficiary_name` VARCHAR(45) NULL,
  `type` INT NULL,
  `status` INT NULL,
  `acceptance_date` DATETIME NULL,
  `acceptance_no` INT NULL,
  `termination_date` DATETIME NULL,
  `termination_no` INT NULL,
  PRIMARY KEY (`member_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `amc`.`loans`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `amc`.`loans` (
  `loan_account_id` INT NOT NULL AUTO_INCREMENT,
  `member_id` INT NULL,
  `loan_type` INT NULL,
  `request_type` INT NULL,
  `date_granted` DATETIME NULL,
  `approval_no` INT NULL,
  `term` INT NULL,
  `orig_amount` DECIMAL(13,2) NULL,
  `interest_rate` DOUBLE NULL,
  `purpose` VARCHAR(65) NULL,
  `loan_status` INT NULL,
  `outstanding_balance` DECIMAL(13,2) NULL,
  `date_terminated` DATETIME NULL,
  PRIMARY KEY (`loan_account_id`),
  INDEX `loan_member_idx` (`member_id` ASC),
  CONSTRAINT `loan_member`
    FOREIGN KEY (`member_id`)
    REFERENCES `amc`.`members` (`member_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `amc`.`comakers`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `amc`.`comakers` (
  `comaker_id` INT NOT NULL AUTO_INCREMENT,
  `loan_id` INT NULL,
  `name` VARCHAR(75) NULL,
  `address` VARCHAR(75) NULL,
  `company_name` VARCHAR(65) NULL,
  `position` VARCHAR(45) NULL,
  PRIMARY KEY (`comaker_id`),
  INDEX `comaker_loan_idx` (`loan_id` ASC),
  CONSTRAINT `comaker_loan`
    FOREIGN KEY (`loan_id`)
    REFERENCES `amc`.`loans` (`loan_account_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `amc`.`savings`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `amc`.`savings` (
  `savings_account_id` INT NOT NULL AUTO_INCREMENT,
  `member_id` INT NULL,
  `opening_date` DATETIME NULL,
  `outstanding_balance` DECIMAL(13,2) NULL,
  `account_status` INT NULL,
  `termination_date` DATETIME NULL,
  PRIMARY KEY (`savings_account_id`),
  INDEX `savings_member_idx` (`member_id` ASC),
  CONSTRAINT `savings_member`
    FOREIGN KEY (`member_id`)
    REFERENCES `amc`.`members` (`member_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `amc`.`capitals`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `amc`.`capitals` (
  `capital_account_id` INT NOT NULL AUTO_INCREMENT,
  `member_id` INT NULL,
  `opening_date` DATETIME NULL,
  `ics_no` INT NULL,
  `ics_amount` DECIMAL(13,2) NULL,
  `ipuc_amount` DECIMAL(13,2) NULL,
  `outstanding_balance` DECIMAL(13,2) NULL,
  `account_status` INT NULL,
  `withdrawal_date` DATETIME NULL,
  PRIMARY KEY (`capital_account_id`),
  INDEX `capitals_members_idx` (`member_id` ASC),
  CONSTRAINT `capitals_members`
    FOREIGN KEY (`member_id`)
    REFERENCES `amc`.`members` (`member_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `amc`.`dividend_general`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `amc`.`dividend_general` (
  `dividend_id` INT NOT NULL AUTO_INCREMENT,
  `year` INT NULL,
  `total_paid_up` DECIMAL(13,2) NULL,
  `net_surplus` DECIMAL(13,2) NULL,
  `reserve_fund` DECIMAL(13,2) NULL,
  `date_set` DATETIME NULL,
  PRIMARY KEY (`dividend_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `amc`.`individual_dividends`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `amc`.`individual_dividends` (
  `ind_dividend_id` INT NOT NULL AUTO_INCREMENT,
  `capital_account_id` INT NULL,
  `dividend_id` INT NULL,
  `current_balance` DECIMAL(13,2) NULL,
  `dividend_amount` DECIMAL(13,2) NULL,
  `date_computed` DATETIME NULL,
  `date_released` DATETIME NULL,
  PRIMARY KEY (`ind_dividend_id`),
  INDEX `dividend_capitalaccount_idx` (`capital_account_id` ASC),
  INDEX `dividend_general_idx` (`dividend_id` ASC),
  CONSTRAINT `dividend_capitalaccount`
    FOREIGN KEY (`capital_account_id`)
    REFERENCES `amc`.`capitals` (`capital_account_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `dividend_general`
    FOREIGN KEY (`dividend_id`)
    REFERENCES `amc`.`dividend_general` (`dividend_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `amc`.`users`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `amc`.`users` (
  `user_id` INT NOT NULL AUTO_INCREMENT,
  `last_name` VARCHAR(45) NULL,
  `first_name` VARCHAR(45) NULL,
  `middle_name` VARCHAR(45) NULL,
  `username` VARCHAR(45) NULL,
  `password` VARCHAR(45) NULL,
  `user_type` INT NULL,
  `user_status` INT NULL,
  PRIMARY KEY (`user_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `amc`.`loan_transaction`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `amc`.`loan_transaction` (
  `loan_transaction_id` INT NOT NULL AUTO_INCREMENT,
  `loan_account_id` INT NULL,
  `transaction_type` INT NULL,
  `date` DATETIME NULL,
  `total_amount` DECIMAL(13,2) NULL,
  `principal` DECIMAL(13,2) NULL,
  `interest` DECIMAL(13,2) NULL,
  `penalty` DECIMAL(13,2) NULL,
  `encoded_by` INT NULL,
  PRIMARY KEY (`loan_transaction_id`),
  INDEX `loantrans_loan_idx` (`loan_account_id` ASC),
  INDEX `loan_encoded_by_idx` (`encoded_by` ASC),
  CONSTRAINT `loantrans_loan`
    FOREIGN KEY (`loan_account_id`)
    REFERENCES `amc`.`loans` (`loan_account_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `loan_encoded_by`
    FOREIGN KEY (`encoded_by`)
    REFERENCES `amc`.`users` (`user_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `amc`.`savings_transaction`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `amc`.`savings_transaction` (
  `savings_transaction_id` INT NOT NULL AUTO_INCREMENT,
  `savings_account_id` INT NULL,
  `transaction_type` INT NULL,
  `date` DATETIME NULL,
  `total_amount` DECIMAL(13,2) NULL,
  `interest_rate` DOUBLE NULL,
  `encoded_by` INT NULL,
  PRIMARY KEY (`savings_transaction_id`),
  INDEX `savtrans_savings_idx` (`savings_account_id` ASC),
  INDEX `savings_encodedby_idx` (`encoded_by` ASC),
  CONSTRAINT `savtrans_savings`
    FOREIGN KEY (`savings_account_id`)
    REFERENCES `amc`.`savings` (`savings_account_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `savings_encodedby`
    FOREIGN KEY (`encoded_by`)
    REFERENCES `amc`.`users` (`user_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `amc`.`capitals_transaction`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `amc`.`capitals_transaction` (
  `capital_transaction_id` INT NOT NULL AUTO_INCREMENT,
  `capital_account_id` INT NULL,
  `total_amount` DECIMAL(13,2) NULL,
  `encoded_by` INT NULL,
  PRIMARY KEY (`capital_transaction_id`),
  INDEX `captrans_capitals_idx` (`capital_account_id` ASC),
  INDEX `capital_encodedby_idx` (`encoded_by` ASC),
  CONSTRAINT `captrans_capitals`
    FOREIGN KEY (`capital_account_id`)
    REFERENCES `amc`.`capitals` (`capital_account_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `capital_encodedby`
    FOREIGN KEY (`encoded_by`)
    REFERENCES `amc`.`users` (`user_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `amc`.`chart_of_accounts`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `amc`.`chart_of_accounts` (
  `code` INT NOT NULL,
  `account_title` VARCHAR(75) NULL,
  PRIMARY KEY (`code`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `amc`.`capitals_transaction_line`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `amc`.`capitals_transaction_line` (
  `cap_trans_line_id` INT NOT NULL AUTO_INCREMENT,
  `capital_transaction_id` INT NULL,
  `particular_id` INT NULL,
  `type` INT NULL,
  `amount` DECIMAL(13,2) NULL,
  PRIMARY KEY (`cap_trans_line_id`),
  INDEX `captransline_captrans_idx` (`capital_transaction_id` ASC),
  INDEX `captrans_particular_idx` (`particular_id` ASC),
  CONSTRAINT `captransline_captrans`
    FOREIGN KEY (`capital_transaction_id`)
    REFERENCES `amc`.`capitals_transaction` (`capital_transaction_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `captrans_particular`
    FOREIGN KEY (`particular_id`)
    REFERENCES `amc`.`chart_of_accounts` (`code`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `amc`.`savings_transaction_line`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `amc`.`savings_transaction_line` (
  `sav_trans_line_id` INT NOT NULL AUTO_INCREMENT,
  `savings_transaction_id` INT NULL,
  `particular_id` INT NULL,
  `type` INT NULL,
  `amount` DECIMAL(13,2) NULL,
  PRIMARY KEY (`sav_trans_line_id`),
  INDEX `savtransline_savtrans_idx` (`savings_transaction_id` ASC),
  INDEX `savtrans_particular_idx` (`particular_id` ASC),
  CONSTRAINT `savtransline_savtrans`
    FOREIGN KEY (`savings_transaction_id`)
    REFERENCES `amc`.`savings_transaction` (`savings_transaction_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `savtrans_particular`
    FOREIGN KEY (`particular_id`)
    REFERENCES `amc`.`chart_of_accounts` (`code`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `amc`.`loan_transaction_line`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `amc`.`loan_transaction_line` (
  `loan_trans_line_id` INT NOT NULL,
  `loan_transaction_id` INT NULL,
  `particular_id` INT NULL,
  `type` INT NULL,
  `amount` DECIMAL(13,2) NULL,
  PRIMARY KEY (`loan_trans_line_id`),
  INDEX `loantransline_loantrans_idx` (`loan_transaction_id` ASC),
  INDEX `loantrans_particular_idx` (`particular_id` ASC),
  CONSTRAINT `loantransline_loantrans`
    FOREIGN KEY (`loan_transaction_id`)
    REFERENCES `amc`.`loan_transaction` (`loan_transaction_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `loantrans_particular`
    FOREIGN KEY (`particular_id`)
    REFERENCES `amc`.`chart_of_accounts` (`code`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
