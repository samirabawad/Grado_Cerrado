namespace Grado_Cerrado.Domain.Models;

public partial class question_tts_cache
{
    public Guid id { get; set; }

    public Guid question_id { get; set; }

    public string voice { get; set; } = null!;

    public decimal rate { get; set; }

    public string audio_url { get; set; } = null!;

    public int? duration_ms { get; set; }

    public string text_hash { get; set; } = null!;

    public DateTime created_at { get; set; }

    public DateTime? expires_at { get; set; }

    public virtual question question { get; set; } = null!;
}
