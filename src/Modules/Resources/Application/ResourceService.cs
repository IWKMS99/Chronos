using Chronos.Resources.Application.Contracts;
using Chronos.Resources.Domain;
using Chronos.Resources.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Chronos.Resources.Application;

public class ResourceService {
    private readonly ResourcesDbContext _dbContext;

    public ResourceService(ResourcesDbContext dbContext) {
        _dbContext = dbContext;
    }

    public async Task<Guid> CreateAsync(string name, int capacity) {
        var resource = new Resource(Guid.NewGuid(), name, capacity);
        
        await _dbContext.Resources.AddAsync(resource);
        await _dbContext.SaveChangesAsync();

        return resource.Id;
    }

    public async Task<ResourceDto?> GetByIdAsync(Guid id) {
        var resource = await _dbContext.Resources
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);
        
        return resource is null 
            ? null 
            : new ResourceDto(resource.Id, resource.Name, resource.Capacity);
    }

    public async Task<IEnumerable<ResourceDto>> GetAllAsync() {
        return await _dbContext.Resources
            .AsNoTracking()
            .Select(r => new ResourceDto(r.Id, r.Name, r.Capacity))
            .ToListAsync();
    }
}