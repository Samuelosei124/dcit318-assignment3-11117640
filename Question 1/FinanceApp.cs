using System;
using System.Collections.Generic;

namespace FinanceAppMain
{
    public class FinanceApp
    {
    private List<Transaction> _transactions = new();

    public void Run()
    {
        // Instantiate SavingsAccount
        var account = new SavingsAccount("SA-1001", 1000m);

        // Create transactions
        var t1 = new Transaction(1, DateTime.Now, 150m, "Groceries");
        var t2 = new Transaction(2, DateTime.Now, 200m, "Utilities");
        var t3 = new Transaction(3, DateTime.Now, 300m, "Entertainment");

        // Processors
        ITransactionProcessor p1 = new MobileMoneyProcessor();
        ITransactionProcessor p2 = new BankTransferProcessor();
        ITransactionProcessor p3 = new CryptoWalletProcessor();

        // Process and apply transactions
        p1.Process(t1);
        account.ApplyTransaction(t1);
        _transactions.Add(t1);

        p2.Process(t2);
        account.ApplyTransaction(t2);
        _transactions.Add(t2);

        p3.Process(t3);
        account.ApplyTransaction(t3);
        _transactions.Add(t3);
    }

    public static void Main()
    {
        var app = new FinanceApp();
        app.Run();
    }
}
