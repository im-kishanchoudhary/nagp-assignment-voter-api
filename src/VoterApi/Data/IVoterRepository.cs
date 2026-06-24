using VoterApi.Models;

namespace VoterApi.Data;

public interface IVoterRepository
{
    Task<IReadOnlyList<Voter>> GetAllAsync(CancellationToken ct = default);
    Task<Voter?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<bool> IdNumberExistsAsync(string idNumber, CancellationToken ct = default);
    Task<Voter> AddAsync(Voter voter, CancellationToken ct = default);
    Task UpdateAsync(Voter voter, CancellationToken ct = default);
    Task RemoveAsync(Voter voter, CancellationToken ct = default);
}
