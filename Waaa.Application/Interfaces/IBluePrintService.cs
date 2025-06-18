using Waaa.Application.Models;

namespace Waaa.Application.Interfaces
{
    public interface IBluePrintService
    {
        Task<int> RegisterForBluePrintAsync(BluePrintDto bluePrintDto);
    }
}
