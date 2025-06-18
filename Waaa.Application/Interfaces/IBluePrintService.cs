using Waaa.Application.Dto;

namespace Waaa.Application.Interfaces
{
    public interface IBluePrintService
    {
        Task<int> RegisterForBluePrintAsync(BluePrintDto bluePrintDto);
    }
}
