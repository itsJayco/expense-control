using Backend.Api.Data;
using Backend.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Api.DTOs;

namespace Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BudgetsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BudgetsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Budgets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BudgetDto>>> GetAll()
        {
            var budgets = await _context.Budgets
                .Include(b => b.ExpenseType)
                .Include(b => b.MonetaryFund)
                .OrderBy(b => b.Year)
                .ThenBy(b => b.Month)
                .ToListAsync();

            var result = budgets.Select(b => new BudgetDto
            {
                BudgetId = b.BudgetId,
                Year = b.Year,
                Month = b.Month,
                Amount = b.Amount,
                ExpenseTypeId = b.ExpenseTypeId,
                ExpenseTypeName = b.ExpenseType?.Name ?? "",
                MonetaryFundId = b.MonetaryFundId,
                MonetaryFundName = b.MonetaryFund?.Name ?? ""
            }).ToList();

            return Ok(result);
        }

        // GET: api/Budgets/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<BudgetDto>> GetById(int id)
        {
            var budget = await _context.Budgets
                .Include(b => b.ExpenseType)
                .Include(b => b.MonetaryFund)
                .FirstOrDefaultAsync(b => b.BudgetId == id);

            if (budget == null)
                return NotFound();

            var dto = new BudgetDto
            {
                BudgetId = budget.BudgetId,
                Year = budget.Year,
                Month = budget.Month,
                Amount = budget.Amount,
                ExpenseTypeId = budget.ExpenseTypeId,
                ExpenseTypeName = budget.ExpenseType?.Name ?? "",
                MonetaryFundId = budget.MonetaryFundId,
                MonetaryFundName = budget.MonetaryFund?.Name ?? ""
            };

            return Ok(dto);
        }

        // POST: api/Budgets
        [HttpPost]
        public async Task<ActionResult<BudgetDto>> Create([FromBody] BudgetCreateDto dto)
        {
            int userId = 1; // Usuario fijo por ahora (mock)

            var exists = await _context.Budgets.AnyAsync(b =>
                b.UserId == userId &&
                b.ExpenseTypeId == dto.ExpenseTypeId &&
                b.MonetaryFundId == dto.MonetaryFundId &&
                b.Year == dto.Year &&
                b.Month == dto.Month
            );

            if (exists)
                return Conflict("A budget for this expense type, month, and fund already exists.");

            var entity = new Budget
            {
                UserId = userId,
                ExpenseTypeId = dto.ExpenseTypeId,
                MonetaryFundId = dto.MonetaryFundId,
                Year = dto.Year,
                Month = dto.Month,
                Amount = dto.Amount
            };

            _context.Budgets.Add(entity);
            await _context.SaveChangesAsync();

            var createdDto = new BudgetDto
            {
                BudgetId = entity.BudgetId,
                Year = entity.Year,
                Month = entity.Month,
                Amount = entity.Amount,
                ExpenseTypeId = entity.ExpenseTypeId,
                MonetaryFundId = entity.MonetaryFundId
            };

            return CreatedAtAction(nameof(GetById), new { id = entity.BudgetId }, createdDto);
        }

        // PUT: api/Budgets/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] BudgetCreateDto dto)
        {
            var existing = await _context.Budgets.FindAsync(id);
            if (existing == null)
                return NotFound();

            existing.Year = dto.Year;
            existing.Month = dto.Month;
            existing.Amount = dto.Amount;
            existing.ExpenseTypeId = dto.ExpenseTypeId;
            existing.MonetaryFundId = dto.MonetaryFundId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Budgets/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _context.Budgets.FindAsync(id);
            if (existing == null)
                return NotFound();

            _context.Budgets.Remove(existing);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
