DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `computeCapitalAvgMonthlyBalance`(`yr` INT, `accountid` INT) RETURNS decimal(13,2)
    READS SQL DATA
BEGIN
RETURN (
    SELECT 
    COALESCE((SUM(
        CASE WHEN DAY(date) <= 7
    THEN COALESCE(transaction_type * total_amount * (13 - MONTH(date)),0)
    ELSE COALESCE(transaction_type * total_amount * (12 - MONTH(date)),0)
    END)
    + (COALESCE(amc.getCapitalBeginningBalance(yr,accountid),0) * 12))
             / 12,0)
    FROM amc.capitals_transaction WHERE YEAR(date) = yr AND capital_account_id = accountid GROUP BY capital_account_id
);
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `computeCapitalDifference`(`yr` INT, `accountid` INT) RETURNS decimal(13,2)
    READS SQL DATA
BEGIN
RETURN
(
    SELECT COALESCE(amc.computeCapitalOutstandingBalance(YEAR(date),capital_account_id) - 		   COALESCE( 		   amc.getCapitalBeginningBalance(YEAR(date),capital_account_id),0), 0)    
    FROM amc.capitals_transaction WHERE YEAR(date) = yr AND capital_account_id = accountid LIMIT 1
);
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `computeCapitalNetBookValue`(`yr` INT, `accountid` INT) RETURNS decimal(13,2)
    READS SQL DATA
BEGIN
RETURN (
    SELECT 
    COALESCE(
        amc.computeCapitalOutstandingBalance(YEAR(date),capital_account_id) / amc.computeTotalCapitalOutstandingBalance(YEAR(date)) * amc.computeTotalCapitalOutstandingBalance(YEAR(date))
        ,0)
    FROM amc.capitals_transaction WHERE YEAR(date) = yr AND capital_account_id = accountid GROUP BY capital_account_id
);
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `computeCapitalOutstandingBalance`(`yr` INT, `accountid` INT) RETURNS decimal(13,2)
    READS SQL DATA
BEGIN
RETURN (
    SELECT 
    COALESCE(SUM(total_amount * transaction_type),0)
    FROM amc.capitals_transaction WHERE YEAR(date) = yr AND capital_account_id = accountid GROUP BY capital_account_id
);
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `computeCapitalPercentAccomplished`(`yr` INT, `accountid` INT) RETURNS decimal(13,2)
    READS SQL DATA
BEGIN
RETURN
(
    SELECT CONCAT((COALESCE(amc.computeCapitalDifference(YEAR(date),capital_account_id) / (SELECT COALESCE(amount,0) FROM amc.capital_general_log WHERE fund_type = 3 AND YEAR(date) <= 2017 ORDER BY date DESC LIMIT 1), 0)) * 100, ' %')   
    FROM amc.capitals_transaction WHERE YEAR(date) = yr AND capital_account_id = accountid LIMIT 1
);
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `computeDividend`(`yr` INT, `accountid` INT) RETURNS decimal(13,2)
    READS SQL DATA
BEGIN
RETURN (
    SELECT 
    COALESCE(
        amc.computeCapitalAvgMonthlyBalance(YEAR(date),capital_account_id) / amc.computeTotalCapitalAvgBalance(YEAR(date)) * (amc.computeDividendMultiplier(YEAR(date)) * 0.65)
        ,0)
    FROM amc.capitals_transaction WHERE YEAR(date) = yr AND capital_account_id = accountid GROUP BY capital_account_id
);
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `computeDividendMultiplier`(`yr` INT) RETURNS decimal(13,2)
    READS SQL DATA
BEGIN
RETURN
(
    SELECT COALESCE(CONVERT(
        amount * (SELECT 1 - amount FROM `capital_general_log` WHERE fund_type = 1 AND YEAR(date) = yr ORDER BY date DESC LIMIT 1)
        , DECIMAL(13,2)),0) FROM `capital_general_log` WHERE fund_type = 0 AND YEAR(date) = yr ORDER BY date DESC LIMIT 1
    );
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `computeMonthAvgDailyBalance`(`mn` INT, `yr` INT, `accountid` INT) RETURNS decimal(13,2)
    READS SQL DATA
BEGIN
RETURN 
(
    SELECT COALESCE( amc.computeMonthMultipliedSum(MONTH(date),YEAR(date),savings_account_id) / amc.getMonthDays(MONTH(date),YEAR(date)),0) AS amount FROM amc.savings_transaction WHERE MONTH(date) = mn AND YEAR(date) = yr AND savings_account_id = accountid GROUP BY savings_account_id
);    
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `computeMonthBalanceDifference`(`mn` INT, `yr` INT, `accountid` INT) RETURNS decimal(13,2)
    READS SQL DATA
BEGIN
RETURN
(
    SELECT COALESCE(amc.computeMonthOutstandingBalance(MONTH(date),YEAR(date),savings_account_id) - 		   amc.getMonthBeginningBalance(MONTH(date),YEAR(date),savings_account_id), 0)    
    FROM amc.savings_transaction WHERE MONTH(date) = mn AND YEAR(date) = yr AND savings_account_id = accountid GROUP BY savings_account_id
);
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `computeMonthEndBalance`(`mn` INT, `yr` INT, `accountid` INT) RETURNS decimal(13,2)
    READS SQL DATA
BEGIN
RETURN
(
    SELECT CASE
    WHEN mn % 3 = 0 
    THEN COALESCE(COALESCE(amc.computeMonthOutstandingBalance(mn,yr,accountid),0) + COALESCE(amc.computeQuarterInterest(mn,yr,accountid),0),0)
    ELSE COALESCE(amc.computeMonthOutstandingBalance(mn,yr,accountid),0)
    END 
);
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `computeMonthInterest`(`mn` INT, `yr` INT, `accountid` INT) RETURNS decimal(13,2)
    READS SQL DATA
BEGIN
RETURN
(
    SELECT COALESCE(COALESCE(amc.computeMonthMultipliedSum(mn,yr,accountid),0) * (COALESCE(amc.getMonthInterestRate(mn,yr),0) / 365),0)
);
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `computeMonthInterestExpense`(`mn` INT, `yr` INT, `accountid` INT) RETURNS decimal(13,2)
    READS SQL DATA
BEGIN
DECLARE x DECIMAL(13,2);
IF (SELECT COALESCE(amc.computeMonthAvgDailyBalance(mn,yr,accountid),0) FROM amc.savings_transaction LIMIT 1) > (SELECT COALESCE(amc.getMonthAvgDailyBalance(mn,yr),0) FROM amc.avg_daily_balance_log LIMIT 1)
	THEN SET x = (SELECT COALESCE( amc.computeMonthInterest(mn,yr,accountid),0) FROM amc.savings_transaction LIMIT 1);
ELSE SET x = 0;
END IF;
RETURN x;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `computeMonthMultipliedSum`(`mn` INT, `yr` INT, `accountid` INT) RETURNS decimal(13,2)
    READS SQL DATA
BEGIN
RETURN (SELECT COALESCE(SUM(((total_amount * (amc.getMonthDays(MONTH(date), YEAR(date)) - DAY(date))) * transaction_type)),0) + ((amc.getMonthDays(MONTH(date), YEAR(date)) * COALESCE(amc.getMonthBeginningBalance(mn, yr,accountid),0))) AS amount FROM amc.savings_transaction WHERE MONTH(date) = mn AND YEAR(date) = yr AND savings_account_id = accountid LIMIT 1);
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `computeMonthOutstandingBalance`(`mn` INT, `yr` INT, `accountid` INT) RETURNS decimal(13,2)
    READS SQL DATA
BEGIN
RETURN
(
    SELECT COALESCE(COALESCE(SUM(total_amount * transaction_type),0) + amc.getMonthBeginningBalance(mn,yr,accountid),0) AS outstanding_balance FROM savings_transaction WHERE MONTH(date) = mn AND YEAR(date) = yr AND savings_account_id = accountid LIMIT 1
);
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `computeQuarterInterest`(`mn` INT, `yr` INT, `accountid` INT) RETURNS decimal(13,2)
    READS SQL DATA
BEGIN
RETURN
(
    SELECT COALESCE(amc.computeMonthInterestExpense(MONTH(date)-2,YEAR(date),savings_account_id),0) + 		   COALESCE(amc.computeMonthInterestExpense(MONTH(date)-1,YEAR(date),savings_account_id),0) +
    COALESCE(amc.computeMonthInterestExpense(MONTH(date),YEAR(date),savings_account_id),0)    
    FROM amc.savings_transaction WHERE MONTH(date) = mn AND YEAR(date) = yr AND savings_account_id = accountid GROUP BY savings_account_id
);
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `computeTotalCapitalAvgBalance`(`yr` INT) RETURNS decimal(13,2)
    READS SQL DATA
BEGIN
RETURN(
    SELECT COALESCE(SUM(DISTINCT amc.computeCapitalAvgMonthlyBalance(yr,capital_account_id)),0) FROM amc.capitals_transaction WHERE YEAR(date) = yr LIMIT 1
    );
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `computeTotalCapitalOutstandingBalance`(`yr` INT) RETURNS decimal(13,2)
    READS SQL DATA
BEGIN
RETURN(
    SELECT COALESCE(SUM(DISTINCT amc.computeCapitalOutstandingBalance(yr,capital_account_id)),0) FROM amc.capitals_transaction WHERE YEAR(date) = yr LIMIT 1
    );
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `computeYearInterest`(`yr` INT, `accountid` INT) RETURNS decimal(13,2)
    READS SQL DATA
BEGIN
RETURN
(
    SELECT 
    COALESCE(amc.computeMonthInterest(1,yr,accountid),0) +
    COALESCE(amc.computeMonthInterest(2,yr,accountid),0) +
    COALESCE(amc.computeMonthInterest(3,yr,accountid),0) +
    COALESCE(amc.computeMonthInterest(4,yr,accountid),0) +
    COALESCE(amc.computeMonthInterest(5,yr,accountid),0) +
    COALESCE(amc.computeMonthInterest(6,yr,accountid),0) +
    COALESCE(amc.computeMonthInterest(7,yr,accountid),0) +
    COALESCE(amc.computeMonthInterest(8,yr,accountid),0) +
    COALESCE(amc.computeMonthInterest(9,yr,accountid),0) +
    COALESCE(amc.computeMonthInterest(10,yr,accountid),0) +
    COALESCE(amc.computeMonthInterest(11,yr,accountid),0) +
    COALESCE(amc.computeMonthInterest(12,yr,accountid),0)
    AS year_interest 
    FROM amc.savings_transaction WHERE YEAR(date) = yr AND savings_account_id = accountid LIMIT 1
);
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `computeYearInterestExpense`(`yr` INT, `accountid` INT) RETURNS decimal(13,2)
    READS SQL DATA
BEGIN
RETURN
(
    SELECT 
    COALESCE(amc.computeMonthInterestExpense(1,yr,accountid),0) +
    COALESCE(amc.computeMonthInterestExpense(2,yr,accountid),0) +
    COALESCE(amc.computeMonthInterestExpense(3,yr,accountid),0) +
    COALESCE(amc.computeMonthInterestExpense(4,yr,accountid),0) +
    COALESCE(amc.computeMonthInterestExpense(5,yr,accountid),0) +
    COALESCE(amc.computeMonthInterestExpense(6,yr,accountid),0) +
    COALESCE(amc.computeMonthInterestExpense(7,yr,accountid),0) +
    COALESCE(amc.computeMonthInterestExpense(8,yr,accountid),0) +
    COALESCE(amc.computeMonthInterestExpense(9,yr,accountid),0) +
    COALESCE(amc.computeMonthInterestExpense(10,yr,accountid),0) +
    COALESCE(amc.computeMonthInterestExpense(11,yr,accountid),0) +
    COALESCE(amc.computeMonthInterestExpense(12,yr,accountid),0)
    AS year_interest 
    FROM amc.savings_transaction WHERE YEAR(date) = yr AND savings_account_id = accountid LIMIT 1
);
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `computeYearOutstandingBalance`(`yr` INT, `accountid` INT) RETURNS decimal(13,2)
    READS SQL DATA
BEGIN
RETURN
(
    SELECT COALESCE(amc.computeMonthEndBalance((SELECT max(MONTH(date)) FROM amc.savings_transaction WHERE YEAR(date) = yr AND savings_account_id = accountid LIMIT 1) ,yr,accountid),0)
    AS outstanding_balance 
    FROM amc.savings_transaction WHERE YEAR(date) = yr AND savings_account_id = accountid LIMIT 1
);
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `computeYearQuarterInterest`(`yr` INT, `accountid` INT) RETURNS decimal(13,2)
    READS SQL DATA
BEGIN
RETURN
(
    SELECT 
    COALESCE(amc.computeQuarterInterest(3,yr,accountid),0) +
    COALESCE(amc.computeQuarterInterest(6,yr,accountid),0) +
    COALESCE(amc.computeQuarterInterest(9,yr,accountid),0) +
    COALESCE(amc.computeQuarterInterest(12,yr,accountid),0)
    FROM amc.savings_transaction WHERE YEAR(date) = yr AND savings_account_id = accountid LIMIT 1
);
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `displayCapitalsTable`(IN `yr` INT, IN `accountstatus` INT, IN `likephrase` VARCHAR(75))
    READS SQL DATA
BEGIN

SELECT m.member_id, CAST(c.capital_account_id AS CHAR(5)) AS 'Acc. No.', CONCAT(m.family_name, ', ', m.first_name, ' ', m.middle_name) AS 'Member Name', COALESCE(amc.getCapitalBeginningBalance(yr,ct.capital_account_id),0) as 'Beginning Balance for the Year',
COALESCE(amc.computeCapitalOutstandingBalance(yr,ct.capital_account_id),0) as 'Outstanding Balance',
COALESCE(amc.computeCapitalAvgMonthlyBalance(yr,ct.capital_account_id),0) as 'Average Monthly Balance',
COALESCE(amc.computeDividend(yr,ct.capital_account_id),0) as 'Interest on Share Capital or Dividend',
COALESCE(amc.computeCapitalDifference(yr,ct.capital_account_id),0) as 'Increase / Decrease in SC for the Year',
(SELECT amount FROM amc.capital_general_log WHERE fund_type = 3 AND YEAR(date) <= yr ORDER BY date DESC LIMIT 1)  as 'Targeted Increase for the Year',
COALESCE(amc.computeCapitalPercentAccomplished(yr,ct.capital_account_id),0) as '% Accomplished',
COALESCE(amc.computeCapitalNetBookValue(yr,ct.capital_account_id),0) as 'Net Book Value of Share Capital'
FROM capitals c LEFT JOIN capitals_transaction ct ON c.capital_account_id = ct.capital_account_id INNER JOIN members m ON c.member_id = m.member_id WHERE m.status = 1 AND c.account_status = accountstatus  AND (c.capital_account_id LIKE likephrase OR m.family_name LIKE likephrase OR m.first_name LIKE likephrase OR m.middle_name LIKE likephrase ) GROUP BY c.capital_account_id;

END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `displayMonthTable`(IN `mn` INT, IN `yr` INT, IN `accountstatus` INT, IN `likephrase` VARCHAR(75))
    READS SQL DATA
BEGIN

SELECT m.member_id, s.savings_account_id AS 'Acc. No.', CONCAT(m.family_name, ', ', m.first_name, ' ', m.middle_name) AS 'Member Name', COALESCE(amc.getMonthBeginningBalance(mn,yr,st.savings_account_id),0) as 'Beginning Balance', COALESCE(amc.computeMonthOutstandingBalance(mn,yr,st.savings_account_id),0) as 'Outstanding Balance', COALESCE(amc.computeMonthInterest(mn,yr,st.savings_account_id),0) AS 'Computed Interest',
COALESCE(amc.computeMonthInterestExpense(mn,yr,st.savings_account_id),0) AS 'Interest Expense for the Month',
COALESCE(amc.computeMonthEndBalance(mn,yr,st.savings_account_id),0) AS 'Month End Balance',
COALESCE(amc.computeMonthAvgDailyBalance(mn,yr,st.savings_account_id),0) AS 'Average Daily Balance',
COALESCE(amc.computeMonthBalanceDifference(mn,yr,st.savings_account_id),0) AS 'Increase (Decrease) for the Month'
FROM savings s LEFT JOIN savings_transaction st ON s.savings_account_id = st.savings_account_id INNER JOIN members m ON s.member_id = m.member_id WHERE m.status = 1 AND s.account_status = accountstatus AND MONTH(s.opening_date) <= mn AND YEAR(s.opening_date) <= yr AND (s.savings_account_id LIKE likephrase OR m.family_name LIKE likephrase OR m.first_name LIKE likephrase OR m.middle_name LIKE likephrase ) GROUP BY s.savings_account_id;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `displayQuarterMonthTable`(IN `mn` INT, IN `yr` INT, IN `accountstatus` INT, IN `likephrase` VARCHAR(75))
    READS SQL DATA
BEGIN

SELECT m.member_id, s.savings_account_id AS 'Acc. No.', CONCAT(m.family_name, ', ', m.first_name, ' ', m.middle_name) AS 'Member Name', COALESCE(amc.getMonthBeginningBalance(mn,yr,st.savings_account_id),0) as 'Beginning Balance', COALESCE(amc.computeMonthOutstandingBalance(mn,yr,st.savings_account_id),0) as 'Outstanding Balance', COALESCE(amc.computeMonthInterest(mn,yr,st.savings_account_id),0) AS 'Computed Interest',
COALESCE(amc.computeMonthInterestExpense(mn,yr,st.savings_account_id),0) AS 'Interest Expense for the Month',
COALESCE(amc.computeQuarterInterest(mn,yr,st.savings_account_id),0) AS 'Interest Credited for the Quarter',
COALESCE(amc.computeMonthEndBalance(mn,yr,st.savings_account_id),0) AS 'Month End Balance',
COALESCE(amc.computeMonthAvgDailyBalance(mn,yr,st.savings_account_id),0) AS 'Average Daily Balance',
COALESCE(amc.computeMonthBalanceDifference(mn,yr,st.savings_account_id),0) AS 'Increase (Decrease) for the Month'
FROM savings s LEFT JOIN savings_transaction st ON s.savings_account_id = st.savings_account_id INNER JOIN members m ON s.member_id = m.member_id WHERE m.status = 1 AND s.account_status = accountstatus AND MONTH(s.opening_date) <= mn AND YEAR(s.opening_date) <= yr AND (s.savings_account_id LIKE likephrase OR m.family_name LIKE likephrase OR m.first_name LIKE likephrase OR m.middle_name LIKE likephrase ) GROUP BY s.savings_account_id;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `displayYearTable`(IN `yr` INT, IN `accountstatus` INT, IN `likephrase` VARCHAR(75))
    READS SQL DATA
BEGIN

SELECT m.member_id, s.savings_account_id AS 'Acc. No.', CONCAT(m.family_name, ', ', m.first_name, ' ', m.middle_name) AS 'Member Name', COALESCE(amc.getMonthBeginningBalance(1,yr,st.savings_account_id),0) as 'Beginning Balance for the Year',
COALESCE(amc.computeYearOutstandingBalance(yr,st.savings_account_id),0) as 'Outstanding Balance, End of Year',
COALESCE(amc.computeYearOutstandingBalance(yr,st.savings_account_id),0) - COALESCE(amc.getMonthBeginningBalance(1,yr,st.savings_account_id),0) as 'Increase (Decrease)',
COALESCE(amc.computeYearInterest(yr,st.savings_account_id),0) as 'Total Computed Interest for the Year',
COALESCE(amc.computeYearInterestExpense(yr,st.savings_account_id),0) as 'Total Interest Credit for the Year',
COALESCE(amc.computeYearQuarterInterest(yr,st.savings_account_id),0) as 'Interest Expense for the Year (Based on Quarterly Credit)'
FROM savings s LEFT JOIN savings_transaction st ON s.savings_account_id = st.savings_account_id INNER JOIN members m ON s.member_id = m.member_id WHERE m.status = 1 AND s.account_status = accountstatus AND YEAR(s.opening_date) <= yr AND (s.savings_account_id LIKE likephrase OR m.family_name LIKE likephrase OR m.first_name LIKE likephrase OR m.middle_name LIKE likephrase ) GROUP BY s.savings_account_id;

END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `getCapitalBeginningBalance`(`yr` INT, `accountid` INT) RETURNS decimal(13,2)
    READS SQL DATA
BEGIN
RETURN
(
    SELECT COALESCE(amount,0) FROM amc.capitals_balance_log WHERE capital_account_id = accountid AND YEAR(date) = yr ORDER BY date ASC LIMIT 1
);
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `getMonthAvgDailyBalance`(`mn` INT, `yr` INT) RETURNS decimal(13,2)
    READS SQL DATA
BEGIN
RETURN
(
    SELECT COALESCE(amount,0) FROM amc.avg_daily_balance_log WHERE MONTH(date) <= mn AND YEAR(date) <= yr ORDER BY date DESC LIMIT 1
);
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `getMonthBeginningBalance`(`mn` INT, `yr` INT, `accountid` INT) RETURNS decimal(13,2)
    READS SQL DATA
BEGIN
RETURN
(
    SELECT COALESCE(amount,0) FROM amc.savings_balance_log WHERE savings_account_id = accountid AND MONTH(date) = mn AND YEAR(date) = yr ORDER BY date DESC LIMIT 1
);
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `getMonthDays`(`month` INT, `year` INT) RETURNS int(11)
BEGIN
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
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `getMonthInterestRate`(`mn` INT, `yr` INT) RETURNS decimal(13,2)
    READS SQL DATA
BEGIN
RETURN
(
    SELECT interest_rate FROM amc.interest_rate_log WHERE MONTH(date) <= mn AND YEAR(date) <= yr ORDER BY date DESC LIMIT 1
);
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `loansM`()
    READS SQL DATA
BEGIN
	SELECT members.member_id, concat_ws(',', family_name, first_name) as name FROM members inner JOIN loans on members.member_id=loans.member_id where date_terminated IS NULL AND loan_status=1;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `monthview`(IN `loanid` INT(11), IN `mo` INT(2))
    READS SQL DATA
BEGIN
	SELECT SUM(total_amount) AS Total, SUM(principal) AS Principal, SUM(interest) AS Interest, SUM(penalty) AS Penalty FROM loan_transaction WHERE MONTH(date) = mo AND loan_account_id = loanid;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `obtainHeaderValues`(IN `yr` INT)
    READS SQL DATA
BEGIN

SELECT PUC1 AS 'PUC1', PUC2 AS 'PUC2', NET AS 'NET', RF1 AS 'RF1' , CAST((NET * RF1) AS DECIMAL(13,2)) AS 'RF2', CAST(PUC1 - PUC2 AS DECIMAL(13,2)) AS 'DIFF1', CAST(NET - (NET * (RF1 / 100)) AS DECIMAL(13,2)) AS 'DIFF2'  
FROM (SELECT amc.computeTotalCapitalOutstandingBalance(yr) AS 'PUC1', (SELECT amount FROM amc.capital_general_log WHERE fund_type = 2 AND YEAR(date) <= yr ORDER BY date DESC LIMIT 1) AS 'PUC2', (SELECT amount FROM amc.capital_general_log WHERE fund_type = 0 AND YEAR(date) <= yr ORDER BY date DESC LIMIT 1) AS 'NET', (SELECT amount * 100 FROM amc.capital_general_log WHERE fund_type = 1 AND YEAR(date) <= yr ORDER BY date DESC LIMIT 1) AS 'RF1') tb;

END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `updateSavingsBalanceLog`()
    MODIFIES SQL DATA
BEGIN
INSERT INTO amc.savings_balance_log (savings_account_id, amount, date)
SELECT st.savings_account_id, COALESCE(amc.computeMonthEndBalance(MONTH(CURDATE()) - 1,YEAR(CURDATE()),st.savings_account_id),0) AS 'Month End Balance', CURDATE() 
FROM savings s LEFT JOIN savings_transaction st ON s.savings_account_id = st.savings_account_id INNER JOIN members m ON s.member_id = m.member_id 
WHERE m.status = 1 AND s.account_status = 1 GROUP BY s.savings_account_id;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `viewagingsched`()
    READS SQL DATA
BEGIN
SELECT 
    Age,
    `Date Granted to Cut-off date`,
    CASE
        WHEN `Date Granted to Cut-off date` < 1 THEN 'Current'
        ELSE 'Past Due'
    END AS Status,
    CASE
        WHEN `Date Granted to Cut-off date` <= 1 THEN balance
        ELSE 0
    END AS 'Current',
    CASE
        WHEN `Date Granted to Cut-off date` BETWEEN 1 AND 30 THEN balance
        ELSE 0
    END AS '1-30 Days',
    CASE
        WHEN `Date Granted to Cut-off date` BETWEEN 31 AND 60 THEN balance
        ELSE 0
    END AS '31-60 Days',
    CASE
        WHEN `Date Granted to Cut-off date` BETWEEN 61 AND 90 THEN balance
        ELSE 0
    END AS '61-90 Days',
    CASE
        WHEN `Date Granted to Cut-off date` BETWEEN 91 AND 120 THEN balance
        ELSE 0
    END AS '91-120 Days',
    CASE
        WHEN `Date Granted to Cut-off date` BETWEEN 121 AND 180 THEN balance
        ELSE 0
    END AS '121-180 Days',
    CASE
        WHEN `Date Granted to Cut-off date` BETWEEN 181 AND 365 THEN balance
        ELSE 0
    END AS '181-365 Days',
    CASE
        WHEN `Date Granted to Cut-off date` BETWEEN 366 AND 730 THEN balance
        ELSE 0
    END AS '366 to 2 years (730 days)',
    CASE
        WHEN `Date Granted to Cut-off date` >= 731 THEN balance
        ELSE 0
    END AS 'More than 2 years (or 730 days)',
    CASE
        WHEN `Date Granted to Cut-off date` > 1 THEN balance
        ELSE 0
    END AS 'Total Past Due'
FROM
    (SELECT 
        Age,
            term,
            Age - term AS 'Date Granted to Cut-off date',
            balance
    FROM
        (SELECT 
        DATEDIFF(DATE_ADD('2016-11-30', INTERVAL EXTRACT(YEAR FROM CURDATE()) - 2016 YEAR), date_granted) AS Age,
            term,
            outstanding_balance AS balance
    FROM
        loans
    GROUP BY member_id) AS x) AS y;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `viewloanrequests`()
    READS SQL DATA
BEGIN
	SELECT loan_account_id, member_id, concat_ws(', ', family_name, first_name) as Name, cast(loan_type AS char(25)) as loan_type, cast(request_type AS char(25)) as request_type, term, orig_amount, interest_rate FROM loans NATURAL JOIN members where loan_status = 0;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `viewloansched`()
    READS SQL DATA
BEGIN
	SELECT loan_account_id, member_id, term, DATE_ADD(date_granted, INTERVAL term DAY) as due_date, orig_amount, outstanding_balance FROM loans NATURAL JOIN members;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `viewloanschedname`()
    READS SQL DATA
BEGIN
	SELECT loan_account_id, member_id, concat_ws(', ', family_name, first_name) as Name, date_granted FROM loans NATURAL JOIN members;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `viewloanscomplete`()
BEGIN
	SELECT  loan_account_id, member_id, concat_ws(', ', family_name, first_name) as Name, date_granted, term, DATE_ADD(date_granted, INTERVAL term DAY) as due_date, orig_amount, outstanding_balance FROM loans NATURAL JOIN members WHERE loan_status = 1;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `viewloanstotal`()
    READS SQL DATA
BEGIN
SELECT member_id,
		CASE WHEN SUM(interest) IS NULL THEN 0 
			ELSE SUM(interest)
            END AS 'Total Interest', 
		CASE WHEN SUM(principal) IS NULL THEN 0
			 ELSE SUM(principal)
             END AS 'Total Principal', 
		CASE WHEN SUM(penalty) IS NULL THEN 0
			 ELSE SUM(interest)
             END AS 'Total Penalty', 
		CASE WHEN SUM(outstanding_balance) IS NULL THEN 0
			 ELSE SUM(outstanding_balance)
             END AS balance
		FROM loan_transaction RIGHT JOIN loans on loans.loan_account_id=loan_transaction.loan_account_id
		GROUP BY member_id;
END$$
DELIMITER ;
