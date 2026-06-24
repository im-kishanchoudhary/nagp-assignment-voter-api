using Microsoft.EntityFrameworkCore;
using VoterApi.Models;

namespace VoterApi.Data;

public class VoterRepository : IVoterRepository
{
    private readonly VoterDbContext _db;

    public VoterRepository(VoterDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<Voter>> GetAllAsync(CancellationToken ct = default)
    {
        return await _db.Voters.AsNoTracking().OrderBy(v => v.Id).ToListAsync(ct);
    }

    public Task<Voter?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return _db.Voters.FirstOrDefaultAsync(v => v.Id == id, ct);
    }

    public Task<bool> IdNumberExistsAsync(string idNumber, CancellationToken ct = default)
    {
        return _db.Voters.AnyAsync(v => v.IdNumber == idNumber, ct);
    }

    public async Task<Voter> AddAsync(Voter voter, CancellationToken ct = default)
    {
        _db.Voters.Add(voter);
        await _db.SaveChangesAsync(ct);
        return voter;
    }

    public Task UpdateAsync(Voter voter, CancellationToken ct = default)
    {
        _db.Voters.Update(voter);
        return _db.SaveChangesAsync(ct);
    }

    public Task RemoveAsync(Voter voter, CancellationToken ct = default)
    {
        _db.Voters.Remove(voter);
        return _db.SaveChangesAsync(ct);
    }
}
