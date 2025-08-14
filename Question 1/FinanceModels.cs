using System;

namespace FinanceAppMain
{
    // Record for Transaction
    public record Transaction(int Id, DateTime Date, decimal Amount, string Category);

    // Interface for transaction processing
    public interface ITransactionProcessor
    {
    void Process(Transaction transaction);
    }
}
