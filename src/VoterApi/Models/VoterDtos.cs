using System.ComponentModel.DataAnnotations;

namespace VoterApi.Models;

public class VoterRequest
{
    [Required]
    [StringLength(20)]
    public string IdNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(120)]
    public string FullName { get; set; } = string.Empty;

    [Range(18, 120)]
    public int Age { get; set; }

    [StringLength(20)]
    public string? Gender { get; set; }

    [StringLength(120)]
    public string? Address { get; set; }

    public bool HasVoted { get; set; }
}

public class VoterResponse
{
    public int Id { get; set; }
    public string IdNumber { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public int Age { get; set; }
    public string? Gender { get; set; }
    public string? Address { get; set; }
    public bool HasVoted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
