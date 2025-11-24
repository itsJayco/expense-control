using Backend.Api.DTOs;

namespace Backend.Api.Services
{
    public interface IExpenseTypeService
    {
        Task<IEnumerable<ExpenseTypeDto>> GetAllAsync();
        Task<ExpenseTypeDto?> GetByIdAsync(int id);
        Task<ExpenseTypeDto> CreateAsync(ExpenseTypeCreateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
