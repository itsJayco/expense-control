using Backend.Api.Data;
using Backend.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseTypesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ExpenseTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ExpenseTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseType>>> GetAll()
        {
            var items = await _context.ExpenseTypes
                .OrderBy(e => e.ExpenseTypeId)
                .ToListAsync();

            return Ok(items);
        }

        // GET: api/ExpenseTypes/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ExpenseType>> GetById(int id)
        {
            var expenseType = await _context.ExpenseTypes.FindAsync(id);
            if (expenseType == null) return NotFound();
            return Ok(expenseType);
        }

        // POST: api/ExpenseTypes
        [HttpPost]
        public async Task<ActionResult<ExpenseType>> Create([FromBody] ExpenseType dto)
        {
            // Generar Code automático: ET-0001, ET-0002, etc.
            var maxId = await _context.ExpenseTypes
                .MaxAsync(e => (int?)e.ExpenseTypeId) ?? 0;

            dto.Code = $"ET-{(maxId + 1).ToString("D4")}";

            _context.ExpenseTypes.Add(dto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = dto.ExpenseTypeId }, dto);
        }

        // PUT: api/ExpenseTypes/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ExpenseType model)
        {
            var existing = await _context.ExpenseTypes.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Name = model.Name;
            existing.Description = model.Description;
            existing.IsActive = model.IsActive;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/ExpenseTypes/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _context.ExpenseTypes.FindAsync(id);
            if (existing == null) return NotFound();

            _context.ExpenseTypes.Remove(existing);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
