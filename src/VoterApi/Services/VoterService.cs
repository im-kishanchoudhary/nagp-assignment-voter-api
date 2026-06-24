using VoterApi.Data;
using VoterApi.Models;

namespace VoterApi.Services;

public class VoterService : IVoterService
{
    private readonly IVoterRepository _repository;
    private readonly ILogger<VoterService> _logger;

    public VoterService(IVoterRepository repository, ILogger<VoterService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IReadOnlyList<VoterResponse>> GetAllAsync(CancellationToken ct = default)
    {
        var voters = await _repository.GetAllAsync(ct);
        return voters.Select(ToResponse).ToList();
    }

    public async Task<VoterResponse?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var voter = await _repository.GetByIdAsync(id, ct);
        return voter is null ? null : ToResponse(voter);
    }

    public async Task<VoterResponse> CreateAsync(VoterRequest request, CancellationToken ct = default)
    {
        if (await _repository.IdNumberExistsAsync(request.IdNumber, ct))
        {
            throw new DuplicateVoterException(request.IdNumber);
        }

        var now = DateTime.UtcNow;
        var voter = new Voter
        {
            IdNumber = request.IdNumber,
            FullName = request.FullName,
            Age = request.Age,
            Gender = request.Gender,
            Address = request.Address,
            HasVoted = request.HasVoted,
            CreatedAt = now,
            UpdatedAt = now
        };

        var created = await _repository.AddAsync(voter, ct);
        _logger.LogInformation("Created voter {Id} ({IdNumber})", created.Id, created.IdNumber);
        return ToResponse(created);
    }

    public async Task<VoterResponse?> UpdateAsync(int id, VoterRequest request, CancellationToken ct = default)
    {
        var voter = await _repository.GetByIdAsync(id, ct);
        if (voter is null)
        {
            return null;
        }

        voter.FullName = request.FullName;
        voter.Age = request.Age;
        voter.Gender = request.Gender;
        voter.Address = request.Address;
        voter.HasVoted = request.HasVoted;
        voter.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(voter, ct);
        _logger.LogInformation("Updated voter {Id}", id);
        return ToResponse(voter);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        var voter = await _repository.GetByIdAsync(id, ct);
        if (voter is null)
        {
            return false;
        }

        await _repository.RemoveAsync(voter, ct);
        _logger.LogInformation("Deleted voter {Id}", id);
        return true;
    }

    private static VoterResponse ToResponse(Voter v) => new()
    {
        Id = v.Id,
        IdNumber = v.IdNumber,
        FullName = v.FullName,
        Age = v.Age,
        Gender = v.Gender,
        Address = v.Address,
        HasVoted = v.HasVoted,
        CreatedAt = v.CreatedAt,
        UpdatedAt = v.UpdatedAt
    };
}
