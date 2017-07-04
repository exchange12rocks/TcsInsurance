using System;
using System.Data.Entity;
namespace TcsInsurance.Helpers
{
    public static class DatabaseExtensions
    {
        public static void UsingTransaction(this Database database, Action action)
        {
            if (database.CurrentTransaction == null)
            {
                using (DbContextTransaction contextTransaction = database.BeginTransaction())
                {
                    action();
                    contextTransaction.Commit();
                }
            }
            else
            {
                action();
            }
        }
        public static T UsingTransaction<T>(this Database database, Func<T> func)
        {
            if (database.CurrentTransaction == null)
            {
                using (DbContextTransaction contextTransaction = database.BeginTransaction())
                {
                    T result = func();
                    contextTransaction.Commit();
                    return result;
                }
            }
            else
            {
                return func();
            }
        }
    }
}