using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAccountApp;
using System;

namespace BankAccountTests
{
    [TestClass]
    public class BankAccountTests
    {
        [DataTestMethod]
        [DataRow("John Doe", 1000.0, 200.0, 800.0)] // Add decimal points to make explicit decimals
        [DataRow("Jane Smith", 500.0, 100.0, 400.0)]
        [DataRow("Alice Johnson", 300.0, 50.0, 250.0)]
        public void Debit_ValidAmount_UpdatesBalance(string customerName, double initialBalance, double debitAmount, double expectedBalance)
        {
            // Arrange
            var account = new BankAccount(customerName, (decimal)initialBalance);

            // Act
            account.Debit((decimal)debitAmount);

            // Assert
            Assert.AreEqual((decimal)expectedBalance, account.Balance);
        }

        [DataTestMethod]
        [DataRow("Bob Brown", 100, 150)]
        public void Debit_InsufficientFunds_ThrowsException(string customerName, double initialBalance, double debitAmount)
        {
            // Arrange
            var account = new BankAccount(customerName, (decimal)initialBalance);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => account.Debit((decimal)debitAmount));
        }

        [DataTestMethod]
        [DataRow("John Doe", 1000.0, 200.0, 1200.0)] // Add decimal points to make explicit decimals
        [DataRow("Jane Smith", 500.0, 100.0, 600.0)]
        [DataRow("Alice Johnson", 300.0, 50.0, 350.0)]
        public void Credit_ValidAmount_UpdatesBalance(string customerName, double initialBalance, double creditAmount, double expectedBalance)
        {
            // Arrange
            var account = new BankAccount(customerName, (decimal)initialBalance);

            // Act
            account.Credit((decimal)creditAmount);

            // Assert
            Assert.AreEqual((decimal)expectedBalance, account.Balance);
        }

        [DataTestMethod]
        [DataRow("John Doe", 1000.0, 200.0, 800.0)] // Add decimal points to make explicit decimals
        [DataRow("Jane Smith", 500.0, 100.0, 400.0)]
        [DataRow("Alice Johnson", 300.0, 50.0, 250.0)]
        public void Withdraw_ValidAmount_UpdatesBalance(string customerName, double initialBalance, double withdrawAmount, double expectedBalance)
        {
            // Arrange
            var account = new BankAccount(customerName, (decimal)initialBalance);

            // Act
            account.Withdraw((decimal)withdrawAmount);

            // Assert
            Assert.AreEqual((decimal)expectedBalance, account.Balance);
        }
    }
}