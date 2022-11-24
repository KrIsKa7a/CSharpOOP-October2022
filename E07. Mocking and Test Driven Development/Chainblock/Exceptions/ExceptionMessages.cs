namespace Chainblock.Exceptions
{
    public static class ExceptionMessages
    {
        public const string ZeroOrNegativeIdExceptionMessage =
            "Id should be a positive number!";

        public const string SenderNullOrWhitespaceExceptionMessage =
            "Sender name cannot be null or whitespace string!";

        public const string ReceiverNullOrWhitespaceExceptionMessage =
            "Receiver name cannot be null or whitespace string!";

        public const string ZeroOrNegativeAmountExceptionMessage =
            "Amount must be a positive number!";

        public const string AddingExistingTransactionExceptionMessage =
            "You cannot add already existing transaction!";

        public const string NonExistingTransactionExceptionMessage =
            "Transaction with provided id does not exist!";

        public const string ChangeStatusOfNonExistingTransactionExceptionMessage =
            "You cannot change the status of non-existing transaction!";

        public const string RemoveNonExistingIdTransactionExceptionMessage =
            "You cannot remove non-existing transaction!";

        public const string GetByStatusNoTransactionsExceptionMessage =
            "There are no transactions with the provided status!";
    }
}
