select * from HumanResources.Department;

select * from HumanResources.Department where GroupName like '%g';

select * from HumanResources.Department where Name like '%g';

ALTER TABLE HumanResources.Department ADD Department_Details int;

EXEC sp_columns Department;

EXEC sp_rename 'HumanResources.Department_details' , 'Department';

EXEC sp_rename 'HumanResources.Department.Department_details' , 'Department', 'Column';


SELECT * FROM AdventureWorks2022.INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE';

SELECT * FROM AdventureWorks2022.INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='HUMANResources';

SELECT * FROM HumanResources.Employee;

EXEC sp_columns Employee

EXEC sp_employee_details 'M', 'Production Technician - WC60';

BACKUP DATABASE AdventureWorks2022 TO DISK = 'D:\Backups_test\AdventureWorks2022.bak';

BACKUP DATABASE Test TO DISK = 'D:\Backups_test\Test.bak';

ALTER TABLE HumanResources.Department DROP COLUMN Department;

SELECT * FROM HumanResources.Employee;
EXEC sp_columns Employee;
SELECT * FROM HumanResources.Employee WHERE HireDate BETWEEN '2009-01-01' AND '2009-02-01';

SELECT COUNT(BusinessEntityID) AS [NUMBER OF EMPLOYEES], DATENAME(MONTH, HireDate) AS MONTH FROM HumanResources.Employee GROUP BY DATENAME(MONTH, HireDate), MONTH(HireDate) ORDER BY MONTH(HireDate);

SELECT * FROM HumanResources.Employee ORDER BY HireDate DESC;

SELECT * FROM HumanResources.EmployeeDepartmentHistory;

SELECT * FROM HumanResources.EmployeePayHistory;

SELECT * FROM HumanResources.JobCandidate;

SELECT * FROM Person.AddressType;

SELECT * FROM Person.Address AS A RIGHT JOIN Person.AddressType AS B ON A.AddressID = B.AddressTypeID;

SELECT TOP 5 * FROM Sales.Customer ORDER BY AccountNumber;

SELECT * FROM Sales.Customer ORDER BY AccountNumber;

SELECT TOP 50 PERCENT * FROM Sales.Customer ORDER BY AccountNumber;


SELECT REPLICATE('HELLO WORLD!', 5);

SELECT GETDATE();
SELECT SYSDATETIME();

SELECT * FROM Person.PersonPhone AS A JOIN Person.PhoneNumberType AS B ON A.PhoneNumberTypeID = B.PhoneNumberTypeID;

SELECT SYSTEM_USER;
