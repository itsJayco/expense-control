using AutoMapper;
using Backend.Api.DTOs;
using Backend.Api.Data;
using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Api.Services
{
    public class ExpenseTypeService : IExpenseTypeService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ExpenseTypeService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ExpenseTypeDto>> GetAllAsync()
        {
            var list = await _context.ExpenseTypes.ToListAsync();
            return _mapper.Map<IEnumerable<ExpenseTypeDto>>(list);
        }

        public async Task<ExpenseTypeDto?> GetByIdAsync(int id)
        {
            var entity = await _context.ExpenseTypes.FindAsync(id);
            return entity == null ? null : _mapper.Map<ExpenseTypeDto>(entity);
        }

        public async Task<ExpenseTypeDto> CreateAsync(ExpenseTypeCreateDto dto)
        {
            var entity = _mapper.Map<ExpenseType>(dto);

            _context.ExpenseTypes.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<ExpenseTypeDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ExpenseTypes.FindAsync(id);
            if (entity == null) return false;

            _context.ExpenseTypes.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
