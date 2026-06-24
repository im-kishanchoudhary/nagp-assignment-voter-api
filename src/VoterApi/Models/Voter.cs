using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoterApi.Models;

[Table("voters")]
public class Voter
{
    [Column("id")]
    public int Id { get; set; }

    [Column("id_number")]
    public string IdNumber { get; set; } = string.Empty;

    [Column("full_name")]
    public string FullName { get; set; } = string.Empty;

    [Column("age")]
    public int Age { get; set; }

    [Column("gender")]
    public string? Gender { get; set; }

    [Column("address")]
    public string? Address { get; set; }

    [Column("has_voted")]
    public bool HasVoted { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}
