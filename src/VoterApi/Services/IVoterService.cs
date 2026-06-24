using VoterApi.Models;

namespace VoterApi.Services;

public interface IVoterService
{
    Task<IReadOnlyList<VoterResponse>> GetAllAsync(CancellationToken ct = default);
    Task<VoterResponse?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<VoterResponse> CreateAsync(VoterRequest request, CancellationToken ct = default);
    Task<VoterResponse?> UpdateAsync(int id, VoterRequest request, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}
