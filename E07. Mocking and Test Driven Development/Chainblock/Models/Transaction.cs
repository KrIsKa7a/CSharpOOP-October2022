namespace Chainblock.Models
{
    using System;

    using Contracts;
    using Exceptions;

    public class Transaction : ITransaction
    {
        private int id;
        private string from;
        private string to;
        private decimal amount;

        public Transaction(int id, TransactionStatus status, string from, string to, decimal amount)
        {
            this.Id = id;
            this.Status = status;
            this.From = from;
            this.To = to;
            this.Amount = amount;
        }

        public int Id 
        { 
            get
            {
                return this.id;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.ZeroOrNegativeIdExceptionMessage);
                }

                this.id = value;
            }
        }

        public TransactionStatus Status { get ; set; }

        public string From 
        { 
            get 
            {
                return this.from;
            } 
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.SenderNullOrWhitespaceExceptionMessage);
                }

                this.from = value;
            }
        }

        public string To 
        { 
            get
            {
                return this.to;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.ReceiverNullOrWhitespaceExceptionMessage);
                }

                this.to = value;
            }
        }

        public decimal Amount 
        { 
            get
            {
                return this.amount;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.ZeroOrNegativeAmountExceptionMessage);
                }

                this.amount = value;
            }
        }
    }
}
