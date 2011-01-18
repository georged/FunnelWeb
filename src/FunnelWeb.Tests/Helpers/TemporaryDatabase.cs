﻿using System;
using System.Data.SqlClient;
using System.Diagnostics;
using FunnelWeb.DatabaseDeployer;
using FunnelWeb.DatabaseDeployer.Infrastructure;

namespace FunnelWeb.Tests.Helpers
{
    public class TemporaryDatabase : IDisposable
    {
        private readonly string connectionString;
        private readonly AdHocSqlRunner database;
        private readonly string databaseName;
        private readonly AdHocSqlRunner master;

        public TemporaryDatabase()
        {
            databaseName = "FW" + Guid.NewGuid();
            connectionString = string.Format("Server=(local)\\SQLEXPRESS;Database={0};Trusted_connection=true;Pooling=false", databaseName);
            database = new AdHocSqlRunner(connectionString);

            var builder = new SqlConnectionStringBuilder(connectionString);
            builder.InitialCatalog = "master";

            master = new AdHocSqlRunner(builder.ToString());
        }

        public string ConnectionString
        {
            get { return connectionString; }
        }

        public AdHocSqlRunner AdHoc
        {
            get { return database; }
        }

        public void CreateAndDeploy()
        {
            master.ExecuteNonQuery("create database [" + databaseName + "]");

            var app = new ApplicationDatabase();
            app.PerformUpgrade(connectionString, new TraceLog());
        }

        public void Dispose()
        {
            master.ExecuteNonQuery("drop database [" + databaseName + "]");
        }

        public class TraceLog : ILog
        {
            public void WriteInformation(string format, params object[] args)
            {
                Trace.TraceInformation(format, args);
            }

            public void WriteError(string format, params object[] args)
            {
                Trace.TraceError(format, args);
            }

            public void WriteWarning(string format, params object[] args)
            {
                Trace.TraceWarning(format, args);
            }

            public IDisposable Indent()
            {
                return new FooDisposable();
            }


            public class FooDisposable : IDisposable
            {
                public void Dispose()
                {
                }
            }
        }
    }
}