# ErrorUnit.Logger_log4net
For ErrorUnit to work with your log4net; add `ErrorUnitCentral._Logger = new ErrorUnitLogger();` where your application start code is.

Only read compatable with `log4net.Appender.AdoNetAppender` with an SQL insert statement for the CommandText (it can't find the table name for read otherwise).
For write compatability your "Message" column in your "Log" table should allow for any amount of characters i.e. nvarchar(max) for MS-SQL.

http://johngoldinc.com/Help/html/T_ErrorUnit_Interfaces_ILogger.htm