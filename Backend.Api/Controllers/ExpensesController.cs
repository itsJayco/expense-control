using Backend.Api.Data;
using Backend.Domain.Entities;
using Backend.Api.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ExpensesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Expenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetAll()
        {
            var expenses = await _context.Expenses
                .Include(e => e.Budget)
                .Select(e => new ExpenseDto
                {
                    ExpenseId = e.ExpenseId,
                    BudgetId = e.BudgetId,
                    Date = e.Date,
                    Amount = e.Amount,
                    Description = e.Description
                })
                .OrderByDescending(e => e.Date)
                .ToListAsync();

            return Ok(expenses);
        }

        // GET: api/Expenses/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ExpenseDto>> GetById(int id)
        {
            var expense = await _context.Expenses
                .Include(e => e.Budget)
                .Where(e => e.ExpenseId == id)
                .Select(e => new ExpenseDto
                {
                    ExpenseId = e.ExpenseId,
                    BudgetId = e.BudgetId,
                    Date = e.Date,
                    Amount = e.Amount,
                    Description = e.Description
                })
                .FirstOrDefaultAsync();

            if (expense == null)
                return NotFound();

            return Ok(expense);
        }

        // POST: api/Expenses
        [HttpPost]
        public async Task<ActionResult<ExpenseDto>> Create([FromBody] ExpenseCreateDto dto)
        {
            // Validar que el presupuesto exista
            var budget = await _context.Budgets.FindAsync(dto.BudgetId);
            if (budget == null)
                return BadRequest("Budget not found.");

            var expense = new Expense
            {
                BudgetId = dto.BudgetId,
                Date = dto.Date,
                Amount = dto.Amount,
                Description = dto.Description
            };

            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            var result = new ExpenseDto
            {
                ExpenseId = expense.ExpenseId,
                BudgetId = expense.BudgetId,
                Date = expense.Date,
                Amount = expense.Amount,
                Description = expense.Description
            };

            return CreatedAtAction(nameof(GetById), new { id = expense.ExpenseId }, result);
        }

        // PUT: api/Expenses/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ExpenseCreateDto dto)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
                return NotFound();

            expense.Date = dto.Date;
            expense.Amount = dto.Amount;
            expense.Description = dto.Description;
            expense.BudgetId = dto.BudgetId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Expenses/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
                return NotFound();

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
