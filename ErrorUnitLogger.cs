using ErrorUnit.Interfaces;
using log4net;
using log4net.Appender;
using log4net.Config;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ErrorUnit.Logger_log4net
{
    public class ErrorUnitLogger : ILogger
    {
        private static ILog log = LogManager.GetLogger(typeof(ErrorUnitLogger));

        public IEnumerable<string> GetErrorUnitJson(DateTime afterdate)
        {
            var ErrorUnitJson = new ConcurrentBag<string>();
            log4net.Util.LogLog.InternalDebugging = true; //todo remove
            System.Diagnostics.Trace.Listeners.Add(new System.Diagnostics.TextWriterTraceListener(@"f:\Temp\Logger_log4net.log", "myListener"));
            System.Diagnostics.Trace.AutoFlush = true;
            System.Diagnostics.Trace.TraceInformation("Test Logger_log4net message.");

            var config = log4net.Config.XmlConfigurator.Configure();
            log = LogManager.GetLogger(typeof(ErrorUnitLogger));

            // Parallel.ForEach(logs, log => {
            var log4net_Logger = log.Logger as log4net.Repository.Hierarchy.Logger;
            if (log4net_Logger != null)
            {
                Parallel.ForEach(log4net_Logger.Appenders.Cast<log4net.Appender.IAppender>(), appender =>
                {
                    if (appender is log4net.Appender.AdoNetAppender)
                    {
                        var adoNetAppender = appender as log4net.Appender.AdoNetAppender;
                        var type = typeof(log4net.Appender.AdoNetAppender);

                        var regexInsertStatment = new Regex(@"[Ii][Nn][Ss][Ee][Rr][Tt]\s+[Ii][Nn][Tt][Oo]\s+([^\(]+)\s+\(([^\)]+)\)\s+[Vv][Aa][Ll][Uu][Ee][Ss]\s+\(([^\)]+)\)")
                                         .Match(adoNetAppender.CommandText);

                        if (regexInsertStatment.Groups.Count == 3)
                        {
                            var tableName = regexInsertStatment.Groups[0].Value;
                            var columnNames = regexInsertStatment.Groups[1].Value
                                                                      .Split(',').Select(s => s.Trim())
                                                                      .ToList();
                            var parameterNames = regexInsertStatment.Groups[2].Value
                                                                         .Split(',').Select(s => s.Trim())
                                                                         .ToList();

                            var m_parameters = ((ArrayList)type.GetField("m_parameters", System.Reflection.BindingFlags.NonPublic)
                                                                    .GetValue(adoNetAppender))
                                                                    .Cast<AdoNetAppenderParameter>()
                                                                    .ToList();

                            var dateParameter = m_parameters.FirstOrDefault(p => p.DbType == DbType.Date
                                                                                   || p.DbType == DbType.DateTime
                                                                                   || p.DbType == DbType.DateTime2
                                                                                   || p.DbType == DbType.DateTimeOffset
                                                                                   || p.Layout is log4net.Layout.RawTimeStampLayout
                                                                                   || p.Layout is log4net.Layout.RawUtcTimeStampLayout
                                                                                   );

                            var messageParameter = m_parameters.FirstOrDefault(p => p.Layout is log4net.Layout.PatternLayout
                                                                                      && ((log4net.Layout.PatternLayout)p.Layout)?.ConversionPattern == "%message"
                                                                                      );
                            if (dateParameter != null && messageParameter != null)
                            {
                                var dateField = columnNames[parameterNames.IndexOf(dateParameter.ParameterName)];
                                var messageField = columnNames[parameterNames.IndexOf(messageParameter.ParameterName)];

                                    //open the connection object for us by sending an empty list of events to log
                                    type.GetMethod("SendBuffer", System.Reflection.BindingFlags.NonPublic, null, new Type[] { typeof(log4net.Core.LoggingEvent[]) }, null)
                                                                 .Invoke(adoNetAppender, new object[] { new log4net.Core.LoggingEvent[] { } });
                                var connection = (IDbConnection)type.GetProperty("Connection", System.Reflection.BindingFlags.NonPublic)
                                                                       .GetValue(adoNetAppender, null);

                                using (IDbCommand dbCmd = connection.CreateCommand())
                                {
                                    dbCmd.CommandText = $"SELECT {messageField} FROM {tableName} WHERE {dateField} > {dateParameter.ParameterName} AND {messageField} LIKE '{{%}}'";
                                    dbCmd.CommandType = CommandType.Text;
                                    dbCmd.Prepare();
                                    dbCmd.Parameters.Clear();
                                    dateParameter.Prepare(dbCmd);
                                    ((IDbDataParameter)dbCmd.Parameters[dateParameter.ParameterName]).Value = afterdate;
                                    using (var reader = dbCmd.ExecuteReader())
                                        while (reader.Read())
                                            ErrorUnitJson.Add(reader.GetString(0));
                                }

                            }
                        }
                    }
                });
            }
            // });
            return ErrorUnitJson;
        }

        public void Log(Exception ex)
        {
            //Parallel.ForEach(logs, log => {
            log.Error(ex.Message, ex);
            //});
        }

        public string Log(string testableErrorJson, Exception exception)
        {
            //Parallel.ForEach(logs, log => {
            log.Error(testableErrorJson, exception);
            //});
            return testableErrorJson;
        }
    }
}
