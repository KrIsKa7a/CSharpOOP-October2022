namespace Chainblock.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Contracts;
    using Models;

    using NUnit.Framework;

    [TestFixture]
    public class ChainblockTests
    {
        private IChainblock chainblock;
        private ITransaction testTransaction;

        [SetUp]
        public void SetUp()
        {
            this.chainblock = new Chainblock();
            this.testTransaction = new Transaction(1, TransactionStatus.Successfull, "Pesho", "Gosho", 1000);
        }

        [Test]
        public void ConstructorShouldInitializeTransactionsCollection()
        {
            Type chainblockType = this.chainblock.GetType();
            FieldInfo transactionsField = chainblockType
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .FirstOrDefault(fi => fi.Name == "transactions");

            object value = transactionsField.GetValue(this.chainblock);

            Assert.IsNotNull(value);
        }

        [Test]
        public void AddShouldAppendTheTransactionToDataCollection()
        {
            this.chainblock.Add(this.testTransaction);

            bool transactionAdded = this.chainblock.Contains(this.testTransaction);
            Assert.IsTrue(transactionAdded);
        }

        [Test]
        public void AddShouldIncreaseCount()
        {
            this.chainblock.Add(this.testTransaction);

            int expectedCount = 1;
            int actualCount = this.chainblock.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void AddShouldThrowExceptionWhenAddingSameTransaction()
        {
            this.chainblock.Add(this.testTransaction);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.chainblock.Add(this.testTransaction);
            }, "You cannot add already existing transaction!");
        }

        [Test]
        public void AddShouldThrowExceptionWhenAddingExistingId()
        {
            this.chainblock.Add(this.testTransaction);

            ITransaction copyTransaction = new Transaction(this.testTransaction.Id, TransactionStatus.Failed, "Gosho", "Pesho", 100);
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.chainblock.Add(copyTransaction);
            }, "You cannot add already existing transaction!");
        }

        [Test]
        public void ContainsByTransactionShouldReturnTrueWhenExists()
        {
            this.chainblock.Add(this.testTransaction);

            bool transactionExists = this.chainblock.Contains(this.testTransaction);

            Assert.IsTrue(transactionExists);
        }

        [Test]
        public void ContainsByTransactionShouldReturnFalseWhenNotExists()
        {
            bool transactionExists = this.chainblock.Contains(this.testTransaction);

            Assert.IsFalse(transactionExists);
        }

        [Test]
        public void ContainsByIdShouldReturnTrueWhenExists()
        {
            this.chainblock.Add(this.testTransaction);

            bool transactionExists = this.chainblock.Contains(this.testTransaction.Id);

            Assert.IsTrue(transactionExists);
        }

        [Test]
        public void ContainsByIdShouldReturnFalseWhenNotExists()
        {
            bool transactionExists = this.chainblock.Contains(this.testTransaction.Id);

            Assert.IsFalse(transactionExists);
        }

        [Test]
        public void CountShouldReturnZeroWhenNoTransactionsAreAdded()
        {
            int expectedCount = 0;
            int actualCount = this.chainblock.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void ChangeTransactionStatusShouldThrowExceptionWithNonExistingId()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                this.chainblock.ChangeTransactionStatus(1, TransactionStatus.Failed);
            }, "You cannot change the status of non-existing transaction!");
        }

        [Test]
        public void ChangeTransactionStatusShouldChangeStatusIfItIsDifferent()
        {
            this.chainblock.Add(this.testTransaction);

            TransactionStatus expectedStatus = TransactionStatus.Unauthorized;
            this.chainblock.ChangeTransactionStatus(this.testTransaction.Id, expectedStatus);

            ITransaction changedTransaction = this.chainblock.GetById(this.testTransaction.Id);
            TransactionStatus actualStatus = changedTransaction.Status;

            Assert.AreEqual(expectedStatus, actualStatus);
        }

        [Test]
        public void ChangeTransactionStatusShouldRemainStatusIfItIsTheSame()
        {
            this.chainblock.Add(this.testTransaction);

            TransactionStatus expectedStatus = this.testTransaction.Status;
            this.chainblock.ChangeTransactionStatus(this.testTransaction.Id, expectedStatus);

            ITransaction changedTransaction = this.chainblock.GetById(this.testTransaction.Id);
            TransactionStatus actualStatus = changedTransaction.Status;

            Assert.AreEqual(expectedStatus, actualStatus);
        }

        [Test]
        public void GetByIdShouldThrowExceptionWithNonExistingId()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.chainblock.GetById(10);
            }, "Transaction with provided id does not exist!");
        }

        [Test]
        public void GetByIdShouldReturnCorrectTransactionWhenExists()
        {
            this.chainblock.Add(this.testTransaction);

            ITransaction actualTransaction = this.chainblock.GetById(this.testTransaction.Id);

            Assert.AreEqual(this.testTransaction, actualTransaction);
        }

        [Test]
        public void RemoveTransactionByIdShouldThrowExceptionWithNonExistingId()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.chainblock.RemoveTransactionById(10);
            }, "You cannot remove non-existing transaction!");
        }

        [Test]
        public void RemoveTransactionByIdShouldRemoveTransactionFromDataCollectionWhenExists()
        {
            ITransaction transaction = new Transaction(2, TransactionStatus.Failed, "Aaa", "Bbb", 50);
            this.chainblock.Add(this.testTransaction);
            this.chainblock.Add(transaction);

            this.chainblock.RemoveTransactionById(transaction.Id);

            bool transactionExists = this.chainblock.Contains(transaction);
            Assert.IsFalse(transactionExists);
        }

        [Test]
        public void RemoveTransactionByIdShouldDecreaseCountWhenExists()
        {
            ITransaction transaction = new Transaction(2, TransactionStatus.Failed, "Aaa", "Bbb", 50);
            this.chainblock.Add(this.testTransaction);
            this.chainblock.Add(transaction);

            this.chainblock.RemoveTransactionById(transaction.Id);

            int expectedCount = 1;
            int actualCount = this.chainblock.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void GetByTransactionStatusShouldThrowExceptionWhenThereAreNoTransactionsAtAll()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.chainblock.GetByTransactionStatus(TransactionStatus.Failed);
            }, "There are no transactions with the provided status!");
        }

        [Test]
        public void GetByTransactionStatusShouldThrowExceptionWhenThereAreNoTransactionsWithGivenStatus()
        {
            this.chainblock.Add(this.testTransaction);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.chainblock.GetByTransactionStatus(TransactionStatus.Aborted);
            }, "There are no transactions with the provided status!");
        }

        [Test]
        public void GetByTransactionStatusShouldReturnSingleTransactionWhenThereAreOneTransaction()
        {
            //Arrange
            ICollection<ITransaction> transactionsToAppend = new HashSet<ITransaction>()
            {
                this.testTransaction,
                new Transaction(2, TransactionStatus.Failed, "Aaa", "Bbb", 100)
            };
            foreach (ITransaction tx in transactionsToAppend)
            {
                this.chainblock.Add(tx);
            }

            //Act
            TransactionStatus wantedStatus = TransactionStatus.Successfull;
            ICollection<ITransaction> expectedTransactions = transactionsToAppend
                .Where(tx => tx.Status == wantedStatus)
                .ToArray();
            ICollection<ITransaction> actualTransactions = this.chainblock
                .GetByTransactionStatus(wantedStatus)
                .ToArray();

            //Assert
            CollectionAssert.AreEqual(expectedTransactions, actualTransactions);
        }

        [Test]
        public void GetByTransactionStatusShouldReturnManyTransactionsOrderedCorrecly()
        {
            //Arrange
            ICollection<ITransaction> transactionsToAppend = new HashSet<ITransaction>()
            {
                this.testTransaction,
                new Transaction(2, TransactionStatus.Failed, "Aaa", "Bbb", 100),
                new Transaction(3, TransactionStatus.Successfull, "Ccc", "Ddd", 500),
                new Transaction(4, TransactionStatus.Successfull, "Eee", "Ggg", 700)
            };
            foreach (ITransaction tx in transactionsToAppend)
            {
                this.chainblock.Add(tx);
            }

            //Act
            TransactionStatus wantedStatus = TransactionStatus.Successfull;
            ICollection<ITransaction> expectedTransactions = transactionsToAppend
                .Where(tx => tx.Status == wantedStatus)
                .OrderByDescending(tx => tx.Amount)
                .ToArray();
            ICollection<ITransaction> actualTransactions = this.chainblock
                .GetByTransactionStatus(wantedStatus)
                .ToArray();

            //Assert
            CollectionAssert.AreEqual(expectedTransactions, actualTransactions);
        }

        [Test]
        public void GetAllSendersWithTransactionStatusShouldThrowExceptionWhenThereAreNoTransactionsAtAll()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.chainblock.GetAllSendersWithTransactionStatus(TransactionStatus.Failed);
            }, "There are no transactions with the provided status!");
        }

        [Test]
        public void GetAllSendersWithTransactionStatusShouldThrowExceptionWhenThereAreNoTransactionsWithGivenStatus()
        {
            this.chainblock.Add(this.testTransaction);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.chainblock.GetAllSendersWithTransactionStatus(TransactionStatus.Aborted);
            }, "There are no transactions with the provided status!");
        }

        [Test]
        public void GetAllSendersWithTransactionStatusShouldReturnOneNameWhenThereIsOneTransactionWithGivenStatus()
        {
            this.chainblock.Add(this.testTransaction);

            TransactionStatus wantedStatus = this.testTransaction.Status;
            ICollection<string> expectedOutput = new string[] { this.testTransaction.From };

            ICollection<string> actualOutput = this.chainblock
                .GetAllSendersWithTransactionStatus(wantedStatus)
                .ToArray();

            CollectionAssert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void GetAllSendersWithTransactionStatusShouldReturnManyNamesOrderedWhenThereAreMoreTransactionsWithGivenStatus()
        {
            ICollection<ITransaction> transactionsToAppend = new HashSet<ITransaction>()
            {
                this.testTransaction,
                new Transaction(2, TransactionStatus.Failed, "Aaa", "Bbb", 100),
                new Transaction(3, TransactionStatus.Successfull, "Ccc", "Ddd", 500),
                new Transaction(4, TransactionStatus.Successfull, "Eee", "Ggg", 700)
            };
            foreach (ITransaction tx in transactionsToAppend)
            {
                this.chainblock.Add(tx);
            }

            //Act
            TransactionStatus wantedStatus = TransactionStatus.Successfull;
            ICollection<string> expectedOutput = transactionsToAppend
                .Where(tx => tx.Status == wantedStatus)
                .OrderBy(tx => tx.Amount)
                .Select(tx => tx.From)
                .ToArray();
            ICollection<string> actualOutput = this.chainblock
                .GetAllSendersWithTransactionStatus(wantedStatus)
                .ToArray();

            CollectionAssert.AreEqual(expectedOutput, actualOutput);
        }

        //Test whether we receive duplicated names
        [Test]
        public void GetAllSendersWithTransactionStatusShouldReturnManyNamesDuplicatedOrderedWhenThereAreMoreTransactionsWithGivenStatus()
        {
            ICollection<ITransaction> transactionsToAppend = new HashSet<ITransaction>()
            {
                this.testTransaction,
                new Transaction(2, TransactionStatus.Failed, "Aaa", "Bbb", 100),
                new Transaction(3, TransactionStatus.Successfull, "Pesho", "Ddd", 500),
                new Transaction(4, TransactionStatus.Successfull, "Eee", "Ggg", 700)
            };
            foreach (ITransaction tx in transactionsToAppend)
            {
                this.chainblock.Add(tx);
            }

            //Act
            TransactionStatus wantedStatus = TransactionStatus.Successfull;
            ICollection<string> expectedOutput = transactionsToAppend
                .Where(tx => tx.Status == wantedStatus)
                .OrderBy(tx => tx.Amount)
                .Select(tx => tx.From)
                .ToArray();
            ICollection<string> actualOutput = this.chainblock
                .GetAllSendersWithTransactionStatus(wantedStatus)
                .ToArray();

            CollectionAssert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void GetAllReceiversWithTransactionStatusShouldThrowExceptionWhenThereAreNoTransactionsAtAll()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.chainblock.GetAllReceiversWithTransactionStatus(TransactionStatus.Failed);
            }, "There are no transactions with the provided status!");
        }

        [Test]
        public void GetAllReceiversWithTransactionStatusShouldThrowExceptionWhenThereAreNoTransactionsWithGivenStatus()
        {
            this.chainblock.Add(this.testTransaction);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.chainblock.GetAllReceiversWithTransactionStatus(TransactionStatus.Aborted);
            }, "There are no transactions with the provided status!");
        }

        [Test]
        public void GetAllReceiversWithTransactionStatusShouldReturnOneNameWhenThereIsOneTransactionWithGivenStatus()
        {
            this.chainblock.Add(this.testTransaction);

            TransactionStatus wantedStatus = this.testTransaction.Status;
            ICollection<string> expectedOutput = new string[] { this.testTransaction.To };

            ICollection<string> actualOutput = this.chainblock
                .GetAllReceiversWithTransactionStatus(wantedStatus)
                .ToArray();

            CollectionAssert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void GetAllReceiversWithTransactionStatusShouldReturnManyNamesOrderedWhenThereAreMoreTransactionsWithGivenStatus()
        {
            ICollection<ITransaction> transactionsToAppend = new HashSet<ITransaction>()
            {
                this.testTransaction,
                new Transaction(2, TransactionStatus.Failed, "Aaa", "Bbb", 100),
                new Transaction(3, TransactionStatus.Successfull, "Ccc", "Ddd", 500),
                new Transaction(4, TransactionStatus.Successfull, "Eee", "Ggg", 700)
            };
            foreach (ITransaction tx in transactionsToAppend)
            {
                this.chainblock.Add(tx);
            }

            //Act
            TransactionStatus wantedStatus = TransactionStatus.Successfull;
            ICollection<string> expectedOutput = transactionsToAppend
                .Where(tx => tx.Status == wantedStatus)
                .OrderBy(tx => tx.Amount)
                .Select(tx => tx.To)
                .ToArray();
            ICollection<string> actualOutput = this.chainblock
                .GetAllReceiversWithTransactionStatus(wantedStatus)
                .ToArray();

            CollectionAssert.AreEqual(expectedOutput, actualOutput);
        }

        //Test whether we receive duplicated names
        [Test]
        public void GetAllReceiversWithTransactionStatusShouldReturnManyNamesDuplicatedOrderedWhenThereAreMoreTransactionsWithGivenStatus()
        {
            ICollection<ITransaction> transactionsToAppend = new HashSet<ITransaction>()
            {
                this.testTransaction,
                new Transaction(2, TransactionStatus.Failed, "Aaa", "Bbb", 100),
                new Transaction(3, TransactionStatus.Successfull, "Ccc", "Gosho", 500),
                new Transaction(4, TransactionStatus.Successfull, "Eee", "Ggg", 700)
            };
            foreach (ITransaction tx in transactionsToAppend)
            {
                this.chainblock.Add(tx);
            }

            //Act
            TransactionStatus wantedStatus = TransactionStatus.Successfull;
            ICollection<string> expectedOutput = transactionsToAppend
                .Where(tx => tx.Status == wantedStatus)
                .OrderBy(tx => tx.Amount)
                .Select(tx => tx.To)
                .ToArray();
            ICollection<string> actualOutput = this.chainblock
                .GetAllReceiversWithTransactionStatus(wantedStatus)
                .ToArray();

            CollectionAssert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void GetAllOrderedByAmountDescThenByIdShouldReturnEmptyCollectionWhenNoTransactions()
        {
            ICollection<ITransaction> actualTransactions = this.chainblock
                .GetAllOrderedByAmountDescendingThenById()
                .ToArray();

            CollectionAssert.IsEmpty(actualTransactions);
        }

        [Test]
        public void GetAllOrderedByAmountDescThenByIdShouldReturnManyTransactionsOrderedByAmountDesc()
        {
            ICollection<ITransaction> transactionsToAppend = new HashSet<ITransaction>()
            {
                this.testTransaction,
                new Transaction(2, TransactionStatus.Failed, "Aaa", "Bbb", 100),
                new Transaction(3, TransactionStatus.Successfull, "Ccc", "Gosho", 500),
                new Transaction(4, TransactionStatus.Successfull, "Eee", "Ggg", 700)
            };
            foreach (ITransaction tx in transactionsToAppend)
            {
                this.chainblock.Add(tx);
            }

            ICollection<ITransaction> expectedOutput = transactionsToAppend
                .OrderByDescending(tx => tx.Amount)
                .ThenBy(tx => tx.Id)
                .ToArray();
            ICollection<ITransaction> actualOutput = this.chainblock
                .GetAllOrderedByAmountDescendingThenById()
                .ToArray();

            CollectionAssert.AreEqual(expectedOutput, actualOutput);
        }

        //Same amount => order by id
        [Test]
        public void GetAllOrderedByAmountDescThenByIdShouldReturnManyTransactionsOrderedByAmountDescThenByIdIfSameAmount()
        {
            ICollection<ITransaction> transactionsToAppend = new HashSet<ITransaction>()
            {
                this.testTransaction,
                new Transaction(2, TransactionStatus.Failed, "Aaa", "Bbb", 100),
                new Transaction(4, TransactionStatus.Successfull, "Eee", "Ggg", 500),
                new Transaction(3, TransactionStatus.Successfull, "Ccc", "Gosho", 500),
            };
            foreach (ITransaction tx in transactionsToAppend)
            {
                this.chainblock.Add(tx);
            }

            ICollection<ITransaction> expectedOutput = transactionsToAppend
                .OrderByDescending(tx => tx.Amount)
                .ThenBy(tx => tx.Id)
                .ToArray();
            ICollection<ITransaction> actualOutput = this.chainblock
                .GetAllOrderedByAmountDescendingThenById()
                .ToArray();

            CollectionAssert.AreEqual(expectedOutput, actualOutput);
        }
    }
}
