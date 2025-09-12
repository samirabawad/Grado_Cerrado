namespace Grado_Cerrado.Domain.Models;

public partial class answer_medium
{
    public Guid id { get; set; }

    public Guid attempt_id { get; set; }

    public string tipo { get; set; } = null!;

    public string blob_url { get; set; } = null!;

    public int? duracion_ms { get; set; }

    public string? texto_stt { get; set; }

    public decimal? stt_confidence { get; set; }

    public string? stt_lang { get; set; }

    public string? stt_model { get; set; }

    public DateTime created_at { get; set; }

    public virtual attempt attempt { get; set; } = null!;
}
