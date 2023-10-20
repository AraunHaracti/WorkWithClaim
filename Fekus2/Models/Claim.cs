using System;

namespace Fekus2.Models;

public class Claim
{
    public int ClaimId { get; set; }

    public DateTime DateAdded { get; set; }

    public int? EquipmentId { get; set; }

    public int? TypeOfFaultId { get; set; }

    public string? Description { get; set; }

    public int CustomerId { get; set; }

    public int StatusId { get; set; }

    public DateTime? CompletionDate { get; set; }

    public int PriorityId { get; set; }

    public int? WorkerId { get; set; }
}
