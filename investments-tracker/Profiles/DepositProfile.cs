using AutoMapper;
using investments_tracker.Models;
using investments_tracker.ViewModels;

namespace investments_tracker.Profiles;

public class DepositProfile : Profile
{
    public DepositProfile()
    {
        CreateMap<Deposit, DepositViewModel>();
    }
}