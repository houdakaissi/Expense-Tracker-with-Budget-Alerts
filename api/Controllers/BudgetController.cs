using api.Data;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public BudgetController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Budget
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Budget>>> GetBudgets()
        {
            return await _context.Budgets.Include(b => b.User).ToListAsync();
        }

        // GET: api/Budget/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Budget>> GetBudget(int id)
        {
            var budget = await _context.Budgets.Include(b => b.User).FirstOrDefaultAsync(b => b.Id == id);

            if (budget == null)
            {
                return NotFound();
            }

            // Calculate remaining budget and progress bar percentage
            decimal remainingBudget = budget.MonthlyBudget - budget.CurrentSpending;
            decimal remainingBudgetPercentage = (budget.MonthlyBudget == 0) ? 0 : (remainingBudget / budget.MonthlyBudget) * 100;

            // Returning budget data along with progress bar information
            return Ok(new 
            {
                Budget = budget,
                RemainingBudget = remainingBudget,
                RemainingBudgetPercentage = remainingBudgetPercentage
            });
        }

        // POST: api/Budget
        [HttpPost]
        public async Task<ActionResult<Budget>> PostBudget(Budget budget)
        {
            _context.Budgets.Add(budget);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBudget", new { id = budget.Id }, budget);
        }

        // PUT: api/Budget/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBudget(int id, Budget budget)
        {
            if (id != budget.Id)
            {
                return BadRequest();
            }

            _context.Entry(budget).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BudgetExists(id))
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

        // DELETE: api/Budget/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Budget>> DeleteBudget(int id)
        {
            var budget = await _context.Budgets.FindAsync(id);
            if (budget == null)
            {
                return NotFound();
            }

            _context.Budgets.Remove(budget);
            await _context.SaveChangesAsync();

            return budget;
        }

        private bool BudgetExists(int id)
        {
            return _context.Budgets.Any(e => e.Id == id);
        }
    }
}
