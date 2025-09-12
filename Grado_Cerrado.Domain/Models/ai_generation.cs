namespace Grado_Cerrado.Domain.Models;

public partial class ai_generation
{
    public Guid id { get; set; }

    public Guid? question_id { get; set; }

    public string prompt { get; set; } = null!;

    public string? provider { get; set; }

    public string? model { get; set; }

    public decimal? temperature { get; set; }

    public decimal? top_p { get; set; }

    public string? raw_response { get; set; }

    public DateTime created_at { get; set; }

    public virtual question? question { get; set; }
}
