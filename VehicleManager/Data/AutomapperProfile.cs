using AutoMapper;
using VehicleManager.Models;
using VehicleManager.ViewModels;

namespace VehicleManager.Data
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Rental, RentalCustomerVM>().ReverseMap();
            CreateMap<Customer, RentalCustomerVM>().ReverseMap();
            CreateMap<Car, RentalCustomerVM>()
                .ForMember(r=>r.CarId,o=>o.MapFrom(c=>c.Id))
                .ReverseMap();
        }
    }
}
