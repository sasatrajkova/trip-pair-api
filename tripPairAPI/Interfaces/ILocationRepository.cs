using tripPairAPI.Data;
using tripPairAPI.Models;

namespace tripPairAPI.Interfaces;

public interface ILocationRepository
{
    Task<IEnumerable<Location>> GetAllLocations();
    Task<Location> GetLocationById(int id);
    Task<Location> UpdateLocation(int id, LocationDto locationToUpdate);
    Task<Location> CreateLocation(LocationDto locationToCreate);
    Task<Location> DeleteLocation(int id);
    bool LocationExists(int id);
}