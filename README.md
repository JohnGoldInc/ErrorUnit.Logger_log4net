# ErrorUnit.Logger_log4net
For ErrorUnit to work with your log4net; add ErrorUnitCentral._Logger = new ErrorUnitLogger(); where your application start code is.

Only read compatable with `log4net.Appender.AdoNetAppender` with an SQL insert statement for the CommandText (it can't find the table name for read otherwise).
For write compatability your "Message" column in your "Log" table should allow for any amount of characters i.e. nvarchar(max) for MS-SQL.

http://johngoldinc.com/Help/html/T_ErrorUnit_Interfaces_ILogger.htm

Partially complete, can log errors but can not read them, GetErrorUnitJson does not work yet, as no Appenders are loaded.

Here is the Log file, not seeing an issue:
`devenv.exe Information: 0 : Test Logger_log4net message.
log4net: configuring repository [log4net-default-repository] using .config file section
log4net: Application config file is [F:\Documents\Visual Studio 2015\Projects\WebApplication1\WebApplication1.Tests\obj\Release]
log4net: Configuring Repository [log4net-default-repository]
log4net: Configuration update mode [Merge].
log4net: Logger [root] Level string is [ALL].
log4net: Logger [root] level set to [name="ALL",value=-2147483648].
log4net: Loading Appender [DebugAppender] type: [log4net.Appender.DebugAppender]
log4net: Converter [message] Option [] Format [min=-1,max=2147483647,leftAlign=False]
log4net: Converter [newline] Option [] Format [min=-1,max=2147483647,leftAlign=False]
log4net: Setting Property [ConversionPattern] to String value [%date [%thread] %-5level %logger [%property{NDC}] - %message%newline]
log4net: Converter [date] Option [] Format [min=-1,max=2147483647,leftAlign=False]
log4net: Converter [literal] Option [ [] Format [min=-1,max=2147483647,leftAlign=False]
log4net: Converter [thread] Option [] Format [min=-1,max=2147483647,leftAlign=False]
log4net: Converter [literal] Option [] ] Format [min=-1,max=2147483647,leftAlign=False]
log4net: Converter [level] Option [] Format [min=5,max=2147483647,leftAlign=True]
log4net: Converter [literal] Option [ ] Format [min=-1,max=2147483647,leftAlign=False]
log4net: Converter [logger] Option [] Format [min=-1,max=2147483647,leftAlign=False]
log4net: Converter [literal] Option [ [] Format [min=-1,max=2147483647,leftAlign=False]
log4net: Converter [property] Option [NDC] Format [min=-1,max=2147483647,leftAlign=False]
log4net: Converter [literal] Option [] - ] Format [min=-1,max=2147483647,leftAlign=False]
log4net: Converter [message] Option [] Format [min=-1,max=2147483647,leftAlign=False]
log4net: Converter [newline] Option [] Format [min=-1,max=2147483647,leftAlign=False]
log4net: Setting Property [Layout] to object [log4net.Layout.PatternLayout]
log4net: Created Appender [DebugAppender]
log4net: Adding appender named [DebugAppender] to logger [root].
log4net: Loading Appender [AdoNetAppender] type: [log4net.Appender.AdoNetAppender]
log4net: Setting Property [BufferSize] to Int32 value [1]
log4net: Setting Property [ConnectionType] to String value [System.Data.SqlClient.SqlConnection, System.Data, Version=1.0.3300.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]
log4net: Setting Property [ConnectionString] to String value [data source=localhost;initial catalog=Northwind;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework]
log4net: Setting Property [CommandText] to String value [INSERT INTO log4net ([Date],[Thread],[Level],[Logger],[Message],[Exception]) VALUES (@log_date, @thread, @log_level, @logger, @message, @exception)]
log4net: Setting Property [ParameterName] to String value [@log_date]
log4net: Setting Property [DbType] to DbType value [DateTime]
log4net: Setting Property [Layout] to object [log4net.Layout.RawTimeStampLayout]
log4net: Setting Collection Property [AddParameter] to object [log4net.Appender.AdoNetAppenderParameter]
log4net: Setting Property [ParameterName] to String value [@thread]
log4net: Setting Property [DbType] to DbType value [String]
log4net: Setting Property [Size] to Int32 value [255]
log4net: Converter [message] Option [] Format [min=-1,max=2147483647,leftAlign=False]
log4net: Converter [newline] Option [] Format [min=-1,max=2147483647,leftAlign=False]
log4net: Setting Property [ConversionPattern] to String value [%thread]
log4net: Converter [thread] Option [] Format [min=-1,max=2147483647,leftAlign=False]
log4net: Setting Property [Layout] to object [log4net.Layout.Layout2RawLayoutAdapter]
log4net: Setting Collection Property [AddParameter] to object [log4net.Appender.AdoNetAppenderParameter]
log4net: Setting Property [ParameterName] to String value [@log_level]
log4net: Setting Property [DbType] to DbType value [String]
log4net: Setting Property [Size] to Int32 value [50]
log4net: Converter [message] Option [] Format [min=-1,max=2147483647,leftAlign=False]
log4net: Converter [newline] Option [] Format [min=-1,max=2147483647,leftAlign=False]
log4net: Setting Property [ConversionPattern] to String value [%level]
log4net: Converter [level] Option [] Format [min=-1,max=2147483647,leftAlign=False]
log4net: Setting Property [Layout] to object [log4net.Layout.Layout2RawLayoutAdapter]
log4net: Setting Collection Property [AddParameter] to object [log4net.Appender.AdoNetAppenderParameter]
log4net: Setting Property [ParameterName] to String value [@logger]
log4net: Setting Property [DbType] to DbType value [String]
log4net: Setting Property [Size] to Int32 value [255]
log4net: Converter [message] Option [] Format [min=-1,max=2147483647,leftAlign=False]
log4net: Converter [newline] Option [] Format [min=-1,max=2147483647,leftAlign=False]
log4net: Setting Property [ConversionPattern] to String value [%logger]
log4net: Converter [logger] Option [] Format [min=-1,max=2147483647,leftAlign=False]
log4net: Setting Property [Layout] to object [log4net.Layout.Layout2RawLayoutAdapter]
log4net: Setting Collection Property [AddParameter] to object [log4net.Appender.AdoNetAppenderParameter]
log4net: Setting Property [ParameterName] to String value [@message]
log4net: Setting Property [DbType] to DbType value [String]
log4net: Setting Property [Size] to Int32 value [4000]
log4net: Converter [message] Option [] Format [min=-1,max=2147483647,leftAlign=False]
log4net: Converter [newline] Option [] Format [min=-1,max=2147483647,leftAlign=False]
log4net: Setting Property [ConversionPattern] to String value [%message]
log4net: Converter [message] Option [] Format [min=-1,max=2147483647,leftAlign=False]
log4net: Setting Property [Layout] to object [log4net.Layout.Layout2RawLayoutAdapter]
log4net: Setting Collection Property [AddParameter] to object [log4net.Appender.AdoNetAppenderParameter]
log4net: Setting Property [ParameterName] to String value [@exception]
log4net: Setting Property [DbType] to DbType value [String]
log4net: Setting Property [Size] to Int32 value [2000]
log4net: Setting Property [Layout] to object [log4net.Layout.Layout2RawLayoutAdapter]
log4net: Setting Collection Property [AddParameter] to object [log4net.Appender.AdoNetAppenderParameter]
log4net: Created Appender [AdoNetAppender]
log4net: Adding appender named [AdoNetAppender] to logger [root].
log4net: Hierarchy Threshold []`