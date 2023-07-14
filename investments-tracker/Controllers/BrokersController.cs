using AutoMapper;
using investments_tracker.Models;
using investments_tracker.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace investments_tracker.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class BrokersController : ControllerBase
{
    private readonly InvestmentsTrackerContext _context;
    private readonly IMapper _mapper;
    
    public BrokersController(InvestmentsTrackerContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet(Name = "GetBrokers")]
    public IEnumerable<BrokerViewModel> Get()
    {
        var brokers = _context.Brokers.ToList();
        return _mapper.Map<IEnumerable<BrokerViewModel>>(brokers);
    }
    
    [HttpGet("{id}", Name = "GetBroker")]
    public ActionResult<BrokerViewModel> Get(Guid id)
    {
        var broker = _context.Brokers.Find(id);

        if (broker == null)
        {
            return NotFound();
        }

        return _mapper.Map<BrokerViewModel>(broker);
    }
    
    [HttpPost(Name = "CreateBroker")]
    public ActionResult<BrokerViewModel> Create(CreateBrokerViewModel createBrokerViewModel)
    {
        var broker = _mapper.Map<Broker>(createBrokerViewModel);
        _context.Brokers.Add(broker);
        _context.SaveChanges();

        return CreatedAtRoute("GetBroker", new { id = broker.Id }, broker);
    }
    
    [HttpPut("{id}", Name = "UpdateBroker")]
    public IActionResult Update(Guid id, UpdateBrokerViewModel broker)
    {
        var brokerToUpdate = _context.Brokers.Find(id);

        if (brokerToUpdate == null)
        {
            return NotFound();
        }

        brokerToUpdate.Name = broker.Name;
        brokerToUpdate.Website = broker.Website;

        _context.Brokers.Update(brokerToUpdate);
        _context.SaveChanges();

        return NoContent();
    }
    
    [HttpDelete("{id}", Name = "DeleteBroker")]
    public IActionResult Delete(Guid id)
    {
        var broker = _context.Brokers.Find(id);

        if (broker == null)
        {
            return NotFound();
        }

        _context.Brokers.Remove(broker);
        _context.SaveChanges();

        return NoContent();
    }
}