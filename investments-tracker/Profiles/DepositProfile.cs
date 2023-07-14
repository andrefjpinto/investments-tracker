using AutoMapper;
using investments_tracker.Models;
using investments_tracker.ViewModels;

namespace investments_tracker.Profiles;

public class BrokerProfile : Profile
{
    public BrokerProfile()
    {
        CreateMap<CreateBrokerViewModel, Broker>();
        CreateMap<UpdateBrokerViewModel, Broker>();
        CreateMap<Broker, BrokerViewModel>();
    }
}