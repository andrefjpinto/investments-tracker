using AutoMapper;
using investments_tracker.Models;
using investments_tracker.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace investments_tracker.Controllers;

[ApiController]
[Route("[controller]")]
public class DepositsController : ControllerBase
{
    private readonly InvestmentsTrackerContext _context;
    private readonly IMapper _mapper;
    
    public DepositsController(IMapper mapper, InvestmentsTrackerContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    
    [HttpGet(Name = "GetDeposits")]
    public IEnumerable<DepositViewModel> Get([FromQuery] Guid? broker)
    {
        var query = _context.Deposits.AsQueryable();
        
        if (broker != null)
        {
            query = query.Where(deposit => deposit.BrokerId == broker);
        }

        var deposits = query.ToList();
        return _mapper.Map<IEnumerable<DepositViewModel>>(deposits);
    }
    
    [HttpGet("{id}", Name = "GetDeposit")]
    public ActionResult<DepositViewModel> Get(Guid id)
    {
        var deposit = _context.Deposits.Find(id);

        if (deposit == null)
        {
            return NotFound();
        }

        return _mapper.Map<DepositViewModel>(deposit);
    }
    
    [HttpPost(Name = "CreateDeposit")]
    public ActionResult<DepositViewModel> Create(CreateDepositViewModel createDepositViewModel)
    {
        var deposit = _mapper.Map<Deposit>(createDepositViewModel);
        _context.Deposits.Add(deposit);
        _context.SaveChanges();

        return CreatedAtRoute("GetDeposit", new { id = deposit.Id }, deposit);
    }
    
    [HttpPut("{id}", Name = "UpdateDeposit")]
    public IActionResult Update(Guid id, UpdateDepositViewModel deposit)
    {
        var depositToUpdate = _context.Deposits.Find(id);

        if (depositToUpdate == null)
        {
            return NotFound();
        }

        _mapper.Map(deposit, depositToUpdate);
        _context.Deposits.Update(depositToUpdate);
        _context.SaveChanges();

        return NoContent();
    }
    
    [HttpDelete("{id}", Name = "DeleteDeposit")]
    public IActionResult Delete(Guid id)
    {
        var deposit = _context.Deposits.Find(id);

        if (deposit == null)
        {
            return NotFound();
        }

        _context.Deposits.Remove(deposit);
        _context.SaveChanges();

        return NoContent();
    }
}