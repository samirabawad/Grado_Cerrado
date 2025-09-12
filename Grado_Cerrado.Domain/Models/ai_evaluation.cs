namespace Grado_Cerrado.Domain.Models;

public partial class ai_evaluation
{
    public Guid id { get; set; }

    public Guid attempt_id { get; set; }

    public decimal? score { get; set; }

    public string? feedback { get; set; }

    public string? raw { get; set; }

    public DateTime created_at { get; set; }

    public virtual attempt attempt { get; set; } = null!;
}
