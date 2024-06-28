using ABPD_TEST_2.DTOs;
using ABPD_TEST_2.Helpers;
using ABPD_TEST_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ABPD_TEST_2.Controllers;

[ApiController]
[Route("api/drivers")]
public class DriverController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DriverController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DriverDto>>> GetAllAnimals(CancellationToken cancellationToken = default,
        [FromQuery] string sortBy = "FirstName")
    {
        var query = _context.Drivers.AsQueryable();

        switch (sortBy.ToLower())
        {
            case "lastname":
                query = query.OrderBy(d => d.LastName);
                break;
            
            case "birthday":
                query = query.OrderBy(d => d.Birthday);
                break;
            
            default:
                query = query.OrderBy(d => d.FirstName);
                break;
        }

        var drivers = await query
            .Select(d => new DriverDto
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Birthday = d.Birthday,
                CarId = d.CarId
            }).ToListAsync(cancellationToken);

        return Ok(drivers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DriverDetailDto>> GetDriverById(int id,
        CancellationToken cancellationToken = default)
    {
        var driver = await _context.Drivers
            .Include(d => d.Car)
            .ThenInclude(c => c.CarManufacturer)
            .Where(d => d.Id == id)
            .Select(d => new DriverDetailDto
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Birthday = d.Birthday,
                CarNumber = d.Car.Number,
                ManufacturerName = d.Car.CarManufacturer.Name,
                ModelName = d.Car.ModelName
            }).FirstOrDefaultAsync(cancellationToken);

        if (driver == null)
        {
            throw new KeyNotFoundException("Driver not found");
        }

        return Ok(driver);
    }

    [HttpGet("competitions")]
    public async Task<ActionResult<IEnumerable<CompetitionDto>>> GetAllCompetitions(
        CancellationToken cancellationToken = default)
    {
        var competitions = await _context.DriverCompetitions
            .Include(dc => dc.Competition)
            .Select(dc => new CompetitionDto
            {
                CompetitionName = dc.Competition.Name,
                CompetitionDate = dc.Date
            }).Distinct()
            .ToListAsync(cancellationToken);

        return Ok(competitions);
    }

    [HttpPost]
    public async Task<ActionResult<Driver>> CreateDriver([FromBody] CreateDriverDto createDriverDto, 
        CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var car = await _context.Cars.FindAsync(createDriverDto.CarId);

        if (car == null)
        {
            return NotFound($"Car with ID {createDriverDto.CarId} not found.");
        }

        var driver = new Driver
        {
            FirstName = createDriverDto.FirstName,
            LastName = createDriverDto.LastName,
            Birthday = createDriverDto.Birthday,
            CarId = createDriverDto.CarId
        };

        _context.Drivers.Add(driver);
        await _context.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(GetDriverById), new { id = driver.Id }, driver);
    }

    [HttpPost("competitions")]
    public async Task<ActionResult> AssignDriverToCompetition([FromBody] AssignDriverToCompetitionDto
        assignDriverDto, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var driver = await _context.Drivers.FindAsync(assignDriverDto.DriverId);
        if (driver == null)
        {
            return NotFound($"Driver with ID {assignDriverDto.DriverId} not found.");
        }
        
        var competition = await _context.Competitions.FindAsync(assignDriverDto.CompetitionId);
        if (competition == null)
        {
            return NotFound($"Competition with ID {assignDriverDto.CompetitionId} not found.");
        }

        var driverCompetition = new DriverCompetition
        {
            DriverId = assignDriverDto.DriverId,
            CompetitionId = assignDriverDto.CompetitionId
        };

        _context.DriverCompetitions.Add(driverCompetition);
        await _context.SaveChangesAsync(cancellationToken);

        return Ok();
    }
}