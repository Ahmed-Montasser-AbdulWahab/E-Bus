using E_Bus.Entities.Entities;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTOs.TransportationDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TransportationsService : ITransportationsService
    {
        private readonly IGetterRepository<Transportation> _getterTransportationRepository;

        public TransportationsService(IGetterRepository<Transportation> getterTransportationRepository)
        {
            _getterTransportationRepository = getterTransportationRepository;
        }

        public async Task<List<TransportationResponse>?> GetAllAsync()
        {
            return (await _getterTransportationRepository.GetAllAsync())?.Select(trans => trans.ToResponse()).ToList();
        }
    }
}
