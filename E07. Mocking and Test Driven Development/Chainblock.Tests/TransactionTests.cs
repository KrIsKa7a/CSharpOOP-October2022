namespace Chainblock.Tests
{
    using System;

    using Contracts;
    using Models;

    using NUnit.Framework;

    [TestFixture]
    public class TransactionTests
    {
        [Test]
        public void ConstructorShouldInitializeIdProperly()
        {
            int expectedId = 1;

            ITransaction transaction = new Transaction(expectedId, TransactionStatus.Successfull, "Pesho", "Gosho", 1000);

            int actualId = transaction.Id;
            Assert.AreEqual(expectedId, actualId);
        }

        [Test]
        public void ConstructorShouldInitializeStatusProperly()
        {
            TransactionStatus expectedStatus = TransactionStatus.Unauthorized;

            ITransaction transaction = new Transaction(1, expectedStatus, "Pesho", "Gosho", 1000);

            TransactionStatus actualStatus = transaction.Status;
            Assert.AreEqual(expectedStatus, actualStatus);
        }

        [Test]
        public void ConstructorShouldInitializeSenderProperly()
        {
            string expectedSender = "Pesho";

            ITransaction transaction = new Transaction(1, TransactionStatus.Successfull, expectedSender, "Gosho", 1000);

            string acutalSender = transaction.From;
            Assert.AreEqual(expectedSender, acutalSender);
        }

        [Test]
        public void ConstructorShouldInitializeReceiverProperly()
        {
            string expectedReceiver = "Gosho";

            ITransaction transaction = new Transaction(1, TransactionStatus.Successfull, "Pesho", expectedReceiver, 1000);

            string acutalReceiver = transaction.To;
            Assert.AreEqual(expectedReceiver, acutalReceiver);
        }

        [Test]
        public void ConstructorShouldInitializeAmountProperly()
        {
            decimal expectedAmont = 1000;

            ITransaction transaction = new Transaction(1, TransactionStatus.Successfull, "Pesho", "Gosho", expectedAmont);

            decimal acutalAmount = transaction.Amount;
            Assert.AreEqual(expectedAmont, acutalAmount);
        }

        [TestCase(-100)]
        [TestCase(-1)]
        [TestCase(0)]
        public void IdSetterShouldThrowExceptionWithZeroOrNegativeId(int id)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                ITransaction transaction = new Transaction(id, TransactionStatus.Successfull, "Pesho", "Gosho", 1000);
            }, "Id should be a positive number!");
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("     ")]
        public void SenderSetterShouldThrowExceptionWithNullOrWhiteSpaceString(string from)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                ITransaction transaction = new Transaction(1, TransactionStatus.Successfull, from, "Gosho", 1000);
            }, "Sender name cannot be null or whitespace string!");
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("     ")]
        public void ReceiverSetterShouldThrowExceptionWithNullOrWhiteSpaceString(string to)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                ITransaction transaction = new Transaction(1, TransactionStatus.Successfull, "Pesho", to, 1000);
            }, "Receiver name cannot be null or whitespace string!");
        }

        [TestCase(-500)]
        [TestCase(-0.0000000001)]
        [TestCase(0)]
        public void AmountSetterShouldThrowExceptionWithZeroOrNegativeAmount(decimal amount)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                ITransaction transaction = new Transaction(1, TransactionStatus.Successfull, "Pesho", "Gosho", amount);
            }, "Amount must be a positive number!");
        }
    }
}
