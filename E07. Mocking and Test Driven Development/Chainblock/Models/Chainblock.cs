namespace Chainblock.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Exceptions;

    public class Chainblock : IChainblock
    {
        private ICollection<ITransaction> transactions;

        public Chainblock()
        {
            this.transactions = new HashSet<ITransaction>();
        }

        public int Count
            => this.transactions.Count;

        public void Add(ITransaction tx)
        {
            if (this.Contains(tx))
            {
                throw new InvalidOperationException(ExceptionMessages.AddingExistingTransactionExceptionMessage); ;
            }

            this.transactions.Add(tx);
        }

        public void ChangeTransactionStatus(int id, TransactionStatus newStatus)
        {
            try
            {
                ITransaction transaction = this.GetById(id);

                transaction.Status = newStatus;
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException(ExceptionMessages.ChangeStatusOfNonExistingTransactionExceptionMessage);
            }
        }

        public bool Contains(ITransaction tx)
            => this.Contains(tx.Id);

        public bool Contains(int id)
            => this.transactions.Any(tx => tx.Id == id);

        public IEnumerable<ITransaction> GetAllInAmountRange(double lo, double hi)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ITransaction> GetAllOrderedByAmountDescendingThenById()
            => this.transactions
            .OrderByDescending(tx => tx.Amount)
            .ThenBy(tx => tx.Id)
            .ToArray();

        public IEnumerable<string> GetAllReceiversWithTransactionStatus(TransactionStatus status)
        {
            ICollection<string> sendersWithGivenStatus = this.transactions
                .Where(tx => tx.Status == status)
                .OrderBy(tx => tx.Amount)
                .Select(tx => tx.To)
                .ToArray();

            if (sendersWithGivenStatus.Count == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.GetByStatusNoTransactionsExceptionMessage);
            }

            return sendersWithGivenStatus;
        }

        public IEnumerable<string> GetAllSendersWithTransactionStatus(TransactionStatus status)
        {
            ICollection<string> sendersWithGivenStatus = this.transactions
                .Where(tx => tx.Status == status)
                .OrderBy(tx => tx.Amount)
                .Select(tx => tx.From)
                .ToArray();

            if (sendersWithGivenStatus.Count == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.GetByStatusNoTransactionsExceptionMessage);
            }

            return sendersWithGivenStatus;
        }

        public ITransaction GetById(int id)
        {
            ITransaction transaction = this.transactions
                .FirstOrDefault(tx => tx.Id == id);
            if (transaction == null)
            {
                throw new InvalidOperationException(ExceptionMessages.NonExistingTransactionExceptionMessage);
            }

            return transaction;
        }

        public IEnumerable<ITransaction> GetByReceiverAndAmountRange(string receiver, double lo, double hi)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ITransaction> GetByReceiverOrderedByAmountThenById(string receiver)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ITransaction> GetBySenderAndMinimumAmountDescending(string sender, double amount)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ITransaction> GetBySenderOrderedByAmountDescending(string sender)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ITransaction> GetByTransactionStatus(TransactionStatus status)
        {
            ICollection<ITransaction> transactionsWanted = this.transactions
                .Where(tx => tx.Status == status)
                .OrderByDescending(tx => tx.Amount)
                .ToArray();

            if (transactionsWanted.Count == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.GetByStatusNoTransactionsExceptionMessage);
            }

            return transactionsWanted;
        }

        public IEnumerable<ITransaction> GetByTransactionStatusAndMaximumAmount(TransactionStatus status, double amount)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveTransactionById(int id)
        {
            try
            {
                ITransaction transaction = this.GetById(id);

                this.transactions.Remove(transaction);
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException(ExceptionMessages.RemoveNonExistingIdTransactionExceptionMessage); ;
            }
        }

        public IEnumerator<ITransaction> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}
