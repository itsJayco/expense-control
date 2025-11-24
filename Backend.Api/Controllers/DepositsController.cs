using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Api.Data;
using Backend.Api.DTOs.Deposits;
using Backend.Domain.Entities;

namespace Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepositsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DepositsController(AppDbContext context)
        {
            _context = context;
        }

        // -------------------------------------------------------------
        // GET ALL
        // -------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepositDto>>> GetAll()
        {
            var deposits = await _context.Deposits
                .Include(d => d.MonetaryFund)
                .OrderByDescending(d => d.DepositDate)
                .Select(d => new DepositDto
                {
                    DepositId = d.DepositId,
                    MonetaryFundId = d.MonetaryFundId,
                    MonetaryFundName = d.MonetaryFund.Name,
                    MonetaryFundCode = d.MonetaryFund.Code,
                    Description = d.Description,
                    Amount = d.Amount,
                    DepositDate = d.DepositDate,
                    CreatedAt = d.CreatedAt,
                    CreatedBy = d.CreatedBy,
                    UpdatedAt = d.UpdatedAt,
                    UpdatedBy = d.UpdatedBy
                })
                .ToListAsync();

            return Ok(deposits);
        }

        // -------------------------------------------------------------
        // GET BY ID
        // -------------------------------------------------------------
        [HttpGet("{id}")]
        public async Task<ActionResult<DepositDto>> GetById(int id)
        {
            var d = await _context.Deposits
                .Include(x => x.MonetaryFund)
                .FirstOrDefaultAsync(x => x.DepositId == id);

            if (d == null)
                return NotFound();

            return Ok(new DepositDto
            {
                DepositId = d.DepositId,
                MonetaryFundId = d.MonetaryFundId,
                MonetaryFundName = d.MonetaryFund.Name,
                MonetaryFundCode = d.MonetaryFund.Code,
                Description = d.Description,
                Amount = d.Amount,
                DepositDate = d.DepositDate,
                CreatedAt = d.CreatedAt,
                CreatedBy = d.CreatedBy,
                UpdatedAt = d.UpdatedAt,
                UpdatedBy = d.UpdatedBy
            });
        }

        // -------------------------------------------------------------
        // POST
        // -------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult> Create(DepositCreateDto dto)
        {
            var fund = await _context.MonetaryFunds.FindAsync(dto.MonetaryFundId);
            if (fund == null)
                return BadRequest("Invalid MonetaryFundId.");

            // Creamos depósito
            var deposit = new Deposit
            {
                MonetaryFundId = dto.MonetaryFundId,
                Description = dto.Description,
                Amount = dto.Amount,
                DepositDate = dto.DepositDate,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "system"
            };

            // SUMAMOS al balance
            fund.Balance += dto.Amount;

            _context.Deposits.Add(deposit);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Deposit registered successfully." });
        }

        // -------------------------------------------------------------
        // PUT
        // -------------------------------------------------------------
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DepositCreateDto dto)
        {
            var deposit = await _context.Deposits.FindAsync(id);

            if (deposit == null)
                return NotFound();

            // Si cambia el monto, ajustamos el balance
            var fund = await _context.MonetaryFunds.FindAsync(deposit.MonetaryFundId);

            if (fund == null)
                return BadRequest("Invalid MonetaryFundId.");

            if (deposit.Amount != dto.Amount)
            {
                decimal difference = dto.Amount - deposit.Amount;
                fund.Balance += difference;
            }

            // Actualizamos datos
            deposit.Description = dto.Description;
            deposit.Amount = dto.Amount;
            deposit.DepositDate = dto.DepositDate;
            deposit.UpdatedAt = DateTime.UtcNow;
            deposit.UpdatedBy = "system";

            await _context.SaveChangesAsync();

            return Ok(new { message = "Deposit updated successfully." });
        }

        // -------------------------------------------------------------
        // DELETE
        // -------------------------------------------------------------
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deposit = await _context.Deposits.FindAsync(id);
            if (deposit == null)
                return NotFound();

            var fund = await _context.MonetaryFunds.FindAsync(deposit.MonetaryFundId);
            if (fund == null)
                return BadRequest("Invalid MonetaryFundId.");

            // REVERSA: restamos el monto
            fund.Balance -= deposit.Amount;

            _context.Deposits.Remove(deposit);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Deposit deleted successfully." });
        }
    }
}
