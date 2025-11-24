using Backend.Api.Data;
using Backend.Api.DTOs;
using Backend.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MonetaryFundsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MonetaryFundsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/MonetaryFunds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MonetaryFundDto>>> GetAll()
        {
            var funds = await _context.MonetaryFunds
                .Include(f => f.ExpenseType)
                .OrderBy(f => f.FundId)
                .Select(f => new MonetaryFundDto
                {
                    Id = f.FundId,
                    Code = f.Code,
                    Name = f.Name,
                    ExpenseTypeId = f.ExpenseTypeId,
                    ExpenseTypeName = f.ExpenseType!.Name,
                    Balance = f.Balance,
                    IsActive = f.IsActive
                })
                .ToListAsync();

            return Ok(funds);
        }

        // GET: api/MonetaryFunds/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<MonetaryFundDto>> GetById(int id)
        {
            var fund = await _context.MonetaryFunds
                .Include(f => f.ExpenseType)
                .Where(f => f.FundId == id)
                .Select(f => new MonetaryFundDto
                {
                    Id = f.FundId,
                    Code = f.Code,
                    Name = f.Name,
                    ExpenseTypeId = f.ExpenseTypeId,
                    ExpenseTypeName = f.ExpenseType!.Name,
                    Balance = f.Balance,
                    IsActive = f.IsActive
                })
                .FirstOrDefaultAsync();

            if (fund == null) return NotFound();
            return Ok(fund);
        }

        private async Task<string> GenerateNextFundCode()
        {
            var lastCode = await _context.MonetaryFunds
                .OrderByDescending(f => f.FundId)
                .Select(f => f.Code)
                .FirstOrDefaultAsync();

            if (string.IsNullOrEmpty(lastCode))
                return "MF-0001";

            int lastNumber = int.Parse(lastCode.Split('-')[1]);
            return $"MF-{(lastNumber + 1).ToString("D4")}";
        }

        // POST: api/MonetaryFunds
        [HttpPost]
        public async Task<ActionResult<MonetaryFundDto>> Create([FromBody] MonetaryFundCreateDto dto)
        {
            var newCode = await GenerateNextFundCode();

            var fund = new MonetaryFund
            {
                Code = newCode,
                Name = dto.Name,
                ExpenseTypeId = dto.ExpenseTypeId,
                Balance = dto.Balance,
                IsActive = dto.IsActive
            };

            _context.MonetaryFunds.Add(fund);
            await _context.SaveChangesAsync();

            var expenseType = await _context.ExpenseTypes.FindAsync(dto.ExpenseTypeId);

            return CreatedAtAction(nameof(GetById), new { id = fund.FundId }, new MonetaryFundDto
            {
                Id = fund.FundId,
                Code = fund.Code,
                Name = fund.Name,
                ExpenseTypeId = fund.ExpenseTypeId,
                ExpenseTypeName = expenseType?.Name ?? "",
                Balance = fund.Balance,
                IsActive = fund.IsActive
            });
        }

        // PUT: api/MonetaryFunds/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] MonetaryFundDto dto)
        {
            var existing = await _context.MonetaryFunds.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Name = dto.Name;
            existing.ExpenseTypeId = dto.ExpenseTypeId;
            existing.Balance = dto.Balance;
            existing.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/MonetaryFunds/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _context.MonetaryFunds.FindAsync(id);
            if (existing == null) return NotFound();

            _context.MonetaryFunds.Remove(existing);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
