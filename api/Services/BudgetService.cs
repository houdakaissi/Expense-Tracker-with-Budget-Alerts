using api.Data;
using Microsoft.EntityFrameworkCore;

public class BudgetService
{
    private readonly ApplicationDBContext _context;

    public BudgetService(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<string> CheckBudgetExceedance(int userId)
    {
        // Get the user's budget
        var budget = await _context.Budgets.FirstOrDefaultAsync(b => b.UserId == userId);
        if (budget == null)
        {
            return "Budget not found.";
        }

        // Get the total expenses for the user
        var totalExpenses = await _context.Expenses.Where(e => e.UserId == userId).SumAsync(e => e.Amount);

        // Check if the total expenses exceed the monthly budget
        if (totalExpenses > budget.MonthlyBudget)
        {
            return "You have exceeded your budget!";
        }
        else
        {
            return "You are within your budget.";
        }
    }
}
