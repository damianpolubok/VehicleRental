using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;
using VehicleRental.API.Repositories.Queries.IQueries;

namespace VehicleRental.API.Features.Reservations.Queries
{
    public class GetAvailableVehiclesQueryHandler : IRequestHandler<GetAvailableVehiclesQuery, IEnumerable<AvailableVehicleDto>>
    {
        private readonly IVehicleQueryRepository _vehicleQueryRepository;
        private readonly IMapper _mapper;

        public GetAvailableVehiclesQueryHandler(IVehicleQueryRepository vehicleQueryRepository, IMapper mapper)
        {
            _vehicleQueryRepository = vehicleQueryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AvailableVehicleDto>> Handle(GetAvailableVehiclesQuery request, CancellationToken cancellationToken)
        {
            if (request.StartDate >= request.EndDate)
            {
                throw new ValidationException("Start date must be earlier than end date.");
            }

            var availableVehicles = await _vehicleQueryRepository.GetAvailableVehiclesAsync(request.StartDate, request.EndDate, cancellationToken);
            return _mapper.Map<IEnumerable<AvailableVehicleDto>>(availableVehicles);
        }
    }
}