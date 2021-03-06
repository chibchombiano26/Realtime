﻿using HefesoftRealTime.Azure.MapUsersSample;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HefesoftRealTime.Azure
{
    public class Table_Storage
    {
        public Table_Storage()
        {
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=hefesoft;AccountKey=dodn17DT7hBi3lXrWlvXihLS9J7xuItHLIpWLBZn2QEMdBHm02Lqxr055rNCpP5z3FhfcjjX3MhPy1Npk3VF3Q==";
            storageAccount = CloudStorageAccount.Parse(connectionString);

            
        }

        public ConnectionResult send(string who, string message, string name, dynamic Clients)
        {
            var result = new ConnectionResult();
            var table = GetConnectionTable();

            var query = new TableQuery<ConnectionEntity>()
                .Where(TableQuery.GenerateFilterCondition(
                "PartitionKey",
                QueryComparisons.Equal,
                who));

            var queryResult = table.ExecuteQuery(query).ToList();
            if (queryResult.Count == 0)
            {
                result.tieneElementos = false;
                //Clients.Caller.showErrorMessage("The user is no longer connected.");
            }
            else
            {
                result.tieneElementos = true;
                result.Listado = queryResult.ToList();

                //foreach (var entity in queryResult)
                //{
                //    Clients.Client(entity.RowKey).addChatMessage(name + ": " + message);
                //}
            }

            return result;
        }

        public void insert(dynamic name, dynamic Context)
        {
            var table = GetConnectionTable();
            table.CreateIfNotExists();

            var entity = new ConnectionEntity(
                name.ToLower(),
                Context.ConnectionId);
            var insertOperation = TableOperation.InsertOrReplace(entity);
            table.Execute(insertOperation);
        }

        public void delete(dynamic name, dynamic Context)
        {
            var table = GetConnectionTable();

            var deleteOperation = TableOperation.Delete(
                new ConnectionEntity(name, Context.ConnectionId) { ETag = "*" });
            table.Execute(deleteOperation);
        }


        private CloudTable GetConnectionTable()
        {
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=hefesoft;AccountKey=dodn17DT7hBi3lXrWlvXihLS9J7xuItHLIpWLBZn2QEMdBHm02Lqxr055rNCpP5z3FhfcjjX3MhPy1Npk3VF3Q==";
            storageAccount = CloudStorageAccount.Parse(connectionString);
            var tableClient = storageAccount.CreateCloudTableClient();
            return tableClient.GetTableReference("connection");
        }

        public CloudStorageAccount storageAccount { get; set; }
    }
}
