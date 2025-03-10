using Domain.Entities;
using Domain.Enums;

namespace Application.Commands;

public static class OperationCommands
{
    public static void CreateExpenseOrIncomeOperation(Operation operation, Account account) =>
         account.Value = operation.Type.Equals(Types.Wydatek.GetHashCode()) ?
         account.Value -= operation.Value : account.Value += operation.Value;

    public static void DeleteExpenseOrIncomeOperation(Operation operation, Account account) =>
        account.Value = operation.Type.Equals(Types.Wydatek.GetHashCode()) ?
        account.Value += operation.Value : account.Value -= operation.Value;

    public static void EditExpenseOrIncomeOperation(Operation actualOperation, Operation operation, Account account)
    {
        if (operation.Type.Equals(Types.Wydatek.GetHashCode()))
        {
            account.Value = actualOperation.Value > operation.Value ?
                account.Value -= actualOperation.Value - operation.Value :
                account.Value += operation.Value - actualOperation.Value;
        }
        else
        {
            account.Value = actualOperation.Value > operation.Value ?
                account.Value += actualOperation.Value - operation.Value :
                account.Value -= operation.Value - actualOperation.Value;
        }
    }
}
