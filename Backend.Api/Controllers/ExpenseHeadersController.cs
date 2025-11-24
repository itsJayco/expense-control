using Backend.Api.Data;
using Backend.Api.DTOs;
using Backend.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseHeadersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ExpenseHeadersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ExpenseHeaders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseHeaderDto>>> GetAll(
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromQuery] int? monetaryFundId,
            [FromQuery] int? expenseTypeId)
        {
            var query = _context.ExpenseHeaders
                .Include(h => h.MonetaryFund)
                .Include(h => h.Details)
                    .ThenInclude(d => d.ExpenseType)
                .Include(h => h.Details)
                    .ThenInclude(d => d.Budget)
                        .ThenInclude(b => b.MonetaryFund)
                .AsQueryable();

            if (startDate.HasValue)
                query = query.Where(h => h.Date >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(h => h.Date <= endDate.Value);

            if (monetaryFundId.HasValue)
                query = query.Where(h => h.MonetaryFundId == monetaryFundId.Value);

            if (expenseTypeId.HasValue)
                query = query.Where(h => h.Details.Any(d => d.ExpenseTypeId == expenseTypeId.Value));

            var headers = await query.ToListAsync();

            return Ok(headers.Select(MapToDto));
        }

        // GET: api/ExpenseHeaders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseHeaderDto>> GetById(int id)
        {
            var header = await _context.ExpenseHeaders
                .Include(h => h.MonetaryFund)
                .Include(h => h.Details)
                    .ThenInclude(d => d.ExpenseType)
                .Include(h => h.Details)
                    .ThenInclude(d => d.Budget)
                        .ThenInclude(b => b.MonetaryFund)
                .FirstOrDefaultAsync(h => h.ExpenseHeaderId == id);

            if (header == null)
                return NotFound();

            return Ok(MapToDto(header));
        }

        // POST: api/ExpenseHeaders
        [HttpPost]
        public async Task<ActionResult<ExpenseHeaderDto>> Create([FromBody] ExpenseHeaderCreateDto dto)
        {
            if (dto.Details == null || !dto.Details.Any())
                return BadRequest("Debe ingresar al menos un detalle de gasto.");

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var header = new ExpenseHeader
                {
                    Date = dto.Date,
                    MonetaryFundId = dto.MonetaryFundId,
                    CommerceName = dto.CommerceName,
                    Observations = dto.Observations,
                    DocumentType = dto.DocumentType
                };

                _context.ExpenseHeaders.Add(header);
                await _context.SaveChangesAsync();

                foreach (var detailDto in dto.Details)
                {
                    var budget = await _context.Budgets
                        .Include(b => b.MonetaryFund)
                        .Include(b => b.ExpenseType)
                        .FirstOrDefaultAsync(b => b.BudgetId == detailDto.BudgetId);

                    if (budget == null)
                        return BadRequest($"El BudgetId {detailDto.BudgetId} no existe.");

                    // Total gastado actualmente
                    var totalSpent = await _context.ExpenseDetails
                        .Where(d => d.BudgetId == budget.BudgetId)
                        .SumAsync(d => d.Amount);

                    if (totalSpent + detailDto.Amount > budget.Amount)
                    {
                        await transaction.RollbackAsync();
                        return BadRequest(
                            $"Presupuesto excedido para el tipo '{budget.ExpenseType.Name}'. " +
                            $"Presupuesto: ${budget.Amount}, Gastado: ${totalSpent}, Intento: ${detailDto.Amount}"
                        );
                    }

                    _context.ExpenseDetails.Add(new ExpenseDetail
                    {
                        ExpenseHeaderId = header.ExpenseHeaderId,
                        BudgetId = budget.BudgetId,
                        ExpenseTypeId = budget.ExpenseTypeId,
                        Amount = detailDto.Amount
                    });
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var createdHeader = await _context.ExpenseHeaders
                    .Include(h => h.MonetaryFund)
                    .Include(h => h.Details)
                        .ThenInclude(d => d.ExpenseType)
                    .Include(h => h.Details)
                        .ThenInclude(d => d.Budget)
                            .ThenInclude(b => b.MonetaryFund)
                    .FirstOrDefaultAsync(h => h.ExpenseHeaderId == header.ExpenseHeaderId);

                return CreatedAtAction(nameof(GetById), new { id = header.ExpenseHeaderId }, MapToDto(createdHeader!));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/ExpenseHeaders/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ExpenseHeaderDto>> Update(int id, [FromBody] ExpenseHeaderCreateDto dto)
        {
            var header = await _context.ExpenseHeaders
                .Include(h => h.Details)
                .FirstOrDefaultAsync(h => h.ExpenseHeaderId == id);

            if (header == null)
                return NotFound();

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                header.Date = dto.Date;
                header.MonetaryFundId = dto.MonetaryFundId;
                header.CommerceName = dto.CommerceName;
                header.Observations = dto.Observations;
                header.DocumentType = dto.DocumentType;

                // Borrar detalles anteriores
                _context.ExpenseDetails.RemoveRange(header.Details);
                await _context.SaveChangesAsync();

                foreach (var detailDto in dto.Details)
                {
                    var budget = await _context.Budgets
                        .Include(b => b.ExpenseType)
                        .FirstOrDefaultAsync(b => b.BudgetId == detailDto.BudgetId);

                    if (budget == null)
                        return BadRequest($"El BudgetId {detailDto.BudgetId} no existe.");

                    var totalSpent = await _context.ExpenseDetails
                        .Where(d => d.BudgetId == budget.BudgetId)
                        .SumAsync(d => d.Amount);

                    if (totalSpent + detailDto.Amount > budget.Amount)
                    {
                        await transaction.RollbackAsync();
                        return BadRequest(
                            $"Presupuesto excedido para '{budget.ExpenseType.Name}'. " +
                            $"Presupuesto: ${budget.Amount}, Gastado: ${totalSpent}, Intento: ${detailDto.Amount}"
                        );
                    }

                    _context.ExpenseDetails.Add(new ExpenseDetail
                    {
                        ExpenseHeaderId = header.ExpenseHeaderId,
                        BudgetId = budget.BudgetId,
                        ExpenseTypeId = budget.ExpenseTypeId,
                        Amount = detailDto.Amount
                    });
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var updatedHeader = await _context.ExpenseHeaders
                    .Include(h => h.MonetaryFund)
                    .Include(h => h.Details)
                        .ThenInclude(d => d.ExpenseType)
                    .Include(h => h.Details)
                        .ThenInclude(d => d.Budget)
                            .ThenInclude(b => b.MonetaryFund)
                    .FirstOrDefaultAsync(h => h.ExpenseHeaderId == id);

                return Ok(MapToDto(updatedHeader!));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest(ex.Message);
            }
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var header = await _context.ExpenseHeaders
                .Include(h => h.Details)
                .FirstOrDefaultAsync(h => h.ExpenseHeaderId == id);

            if (header == null)
                return NotFound();

            _context.ExpenseDetails.RemoveRange(header.Details);
            _context.ExpenseHeaders.Remove(header);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Helper mapper
        private ExpenseHeaderDto MapToDto(ExpenseHeader header)
        {
            return new ExpenseHeaderDto
            {
                ExpenseHeaderId = header.ExpenseHeaderId,
                Date = header.Date,
                MonetaryFundId = header.MonetaryFundId,
                MonetaryFundName = header.MonetaryFund?.Name ?? "",
                CommerceName = header.CommerceName,
                Observations = header.Observations,
                DocumentType = header.DocumentType,
                Details = header.Details.Select(d => new ExpenseDetailDto
                {
                    ExpenseDetailId = d.ExpenseDetailId,
                    BudgetId = d.BudgetId,
                    ExpenseTypeId = d.ExpenseTypeId,
                    ExpenseTypeName = d.ExpenseType?.Name ?? "",
                    Amount = d.Amount,
                    Year = d.Budget?.Year ?? 0,
                    Month = d.Budget?.Month ?? 0,
                    MonetaryFundName = d.Budget?.MonetaryFund?.Name ?? ""
                }).ToList()
            };
        }
    }
}
