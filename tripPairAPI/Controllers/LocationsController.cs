﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using tripPairAPI.Data;
using tripPairAPI.Interfaces;
using tripPairAPI.Models;

namespace tripPairAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationsController : Controller
{
    private readonly ILocationRepository _locationRepository;
    private readonly IMapper _mapper;

    public LocationsController( ILocationRepository locationRepository, IMapper mapper)
    {
        _locationRepository = locationRepository;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<LocationDto>))]
    public async Task<IActionResult> GetAllLocations()
    {
        var locations = _mapper.Map<List<LocationDto>>(await _locationRepository.GetAllLocations());
        return Ok(locations);
    }
    
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<LocationDto>))]
    [ProducesResponseType(404)]
    [Route("{id:int}")]
    public async Task<IActionResult> GetLocation([FromRoute] int id)
    {
        if (!_locationRepository.LocationExists(id)) return NotFound();
        var location = _mapper.Map <LocationDto>(await _locationRepository.GetLocationById(id));
        return Ok(location);
    }
    
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Month>))]
    [ProducesResponseType(404)]
    [Route("{locationId:int}/Months")]
    public async Task<IActionResult> GetLocationMonths([FromRoute] int locationId)
    {
        if (!_locationRepository.LocationExists(locationId)) return NotFound();
        var locationMonths = _mapper.Map<List<MonthDto>>(await _locationRepository.GetLocationMonths(locationId));
        return Ok(locationMonths);
    }

    [HttpPut]
    [ProducesResponseType(200, Type = typeof(Location))]
    [ProducesResponseType(404)]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateLocation([FromRoute] int id, LocationCreateDto locationToUpdate)
    {
        if (!_locationRepository.LocationExists(id)) return NotFound();
        var updatedLocation = _locationRepository.UpdateLocation(id, locationToUpdate);
        return Ok(await updatedLocation);
    }
    
    [HttpPost]
    [ProducesResponseType(200, Type = typeof(Location))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateLocation(LocationCreateDto locationTo)
    {
        if (!ModelState.IsValid) return BadRequest();
        var newLocation = await _locationRepository.CreateLocation(locationTo);
        return Ok(newLocation);
    }

    [HttpDelete]
    [ProducesResponseType(200, Type = typeof(Location))]
    [ProducesResponseType(404)]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteLocation([FromRoute] int id)
    {
        if (!_locationRepository.LocationExists(id)) return NotFound();
        return Ok(await _locationRepository.DeleteLocation(id));
    }
    
}