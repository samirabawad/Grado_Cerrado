namespace Grado_Cerrado.Domain.Models;

public partial class question_source
{
    public Guid question_id { get; set; }

    public string collection { get; set; } = null!;

    public string point_id { get; set; } = null!;

    public decimal? relevance { get; set; }

    public string? payload { get; set; }

    public virtual question question { get; set; } = null!;
}
