--DBCC CHECKDB('StudentDetails') WITH NO_INFOMSGS, ALL_ERRORMSGS;

USE StudentDetails;
EXEC sp_dbcmptlevel 'StudentDetails';  -- Set the compatibility level if needed
--EXEC sp_changedbowner 'db';  -- Change owner if needed
