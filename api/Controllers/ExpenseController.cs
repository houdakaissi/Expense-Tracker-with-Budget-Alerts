using api.Data;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public ExpenseController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Expense
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
        {
            return await _context.Expenses.Include(e => e.User).ToListAsync();
        }

        // GET: api/Expense/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpense(int id)
        {
            var expense = await _context.Expenses.Include(e => e.User).FirstOrDefaultAsync(e => e.Id == id);

            if (expense == null)
            {
                return NotFound();
            }

            return expense;
        }

        // POST: api/Expense
        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense(Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            // Check if the user's total expenses exceed their monthly budget
            var user = await _context.Users.Include(u => u.Budgets).FirstOrDefaultAsync(u => u.Id == expense.UserId);

            if (user != null)
            {
                // Get the current month and year from the expense's date
                int expenseMonth = expense.Date.Month;
                int expenseYear = expense.Date.Year;

                // Find the budget for the specific month and year
                var monthlyBudget = user.Budgets.FirstOrDefault(b => b.MonthYear.Month == expenseMonth && b.MonthYear.Year == expenseYear);

                if (monthlyBudget != null)
                {
                    // Calculate the total expenses for the same month and year
                    var totalExpenses = _context.Expenses.Where(e => e.UserId == expense.UserId && e.Date.Month == expenseMonth && e.Date.Year == expenseYear)
                                                         .Sum(e => e.Amount);

                    // Check if total expenses exceed the budget
                    if (totalExpenses > monthlyBudget.MonthlyBudget)
                    {
                        return BadRequest($"Total expenses have exceeded the monthly budget for {expenseMonth}/{expenseYear}. Current total: {totalExpenses}, Budget: {monthlyBudget.MonthlyBudget}");
                    }
                }
            }

            return CreatedAtAction("GetExpense", new { id = expense.Id }, expense);
        }

        // PUT: api/Expense/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpense(int id, Expense expense)
        {
            if (id != expense.Id)
            {
                return BadRequest();
            }

            _context.Entry(expense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                // Check if the user's total expenses exceed their monthly budget
                var user = await _context.Users.Include(u => u.Budgets).FirstOrDefaultAsync(u => u.Id == expense.UserId);

                if (user != null)
                {
                    // Get the current month and year from the expense's date
                    int expenseMonth = expense.Date.Month;
                    int expenseYear = expense.Date.Year;

                    // Find the budget for the specific month and year
                    var monthlyBudget = user.Budgets.FirstOrDefault(b => b.MonthYear.Month == expenseMonth && b.MonthYear.Year == expenseYear);

                    if (monthlyBudget != null)
                    {
                        // Calculate the total expenses for the same month and year
                        var totalExpenses = _context.Expenses.Where(e => e.UserId == expense.UserId && e.Date.Month == expenseMonth && e.Date.Year == expenseYear)
                                                             .Sum(e => e.Amount);

                        // Check if total expenses exceed the budget
                        if (totalExpenses > monthlyBudget.MonthlyBudget)
                        {
                            return BadRequest($"Total expenses have exceeded the monthly budget for {expenseMonth}/{expenseYear}. Current total: {totalExpenses}, Budget: {monthlyBudget.MonthlyBudget}");
                        }
                    }
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Expense/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Expense>> DeleteExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();

            return expense;
        }

        private bool ExpenseExists(int id)
        {
            return _context.Expenses.Any(e => e.Id == id);
        }
    }
}
