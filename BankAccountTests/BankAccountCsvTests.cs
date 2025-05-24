using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAccountApp;
using System;
using System.IO;
using CsvHelper;
using System.Globalization;
using System.Collections.Generic;

namespace BankAccountTests
{
    [TestClass]
    public class BankAccountCsvTests
    {
        public static IEnumerable<object[]> GetTestData()
        {
            string filePath = "BankAccountTestData.csv";

            // Create a custom configuration that ignores case in headers
            var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower()
            };

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvHelper.CsvReader(reader, config))
            {
                var records = csv.GetRecords<TestData>();
                foreach (var record in records)
                {
                    yield return new object[]
                    {
                record.CustomerName,
                record.InitialBalance,
                record.DebitAmount,
                record.ExpectedBalance
                    };
                }
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method)]
        public void Debit_CsvData_UpdatesBalance(string customerName, decimal initialBalance, decimal debitAmount, string expectedBalance)
        {
            // Arrange
            var account = new BankAccount(customerName, initialBalance);

            // Act & Assert
            if (expectedBalance == "Insufficient funds")
            {
                Assert.ThrowsException<InvalidOperationException>(() => account.Debit(debitAmount));
            }
            else
            {
                account.Debit(debitAmount);
                Assert.AreEqual(decimal.Parse(expectedBalance), account.Balance);
            }
        }

        public class TestData
        {
            public string CustomerName { get; set; }
            public decimal InitialBalance { get; set; }
            public decimal DebitAmount { get; set; }
            public string ExpectedBalance { get; set; }
        }
    }
}