using Waaa.Domain.Entities;

namespace Waaa.Domain.Interfaces
{
    public interface IBluePrintRepository
    {
        Task<int> AddBluePrintAsync(BluePrint bluePrint);
        Task<BluePrint> GetRegistrationsByUserIdAsync(int userId);
    }
}
