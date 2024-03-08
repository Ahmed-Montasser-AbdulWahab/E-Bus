
using ServiceContracts.DTOs.TransportationDTO;


namespace ServiceContracts
{
    public interface ITransportationsService
    {
        public Task<List<TransportationResponse>?> GetAllAsync();
    }
}
