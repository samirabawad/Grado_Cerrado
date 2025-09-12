namespace Grado_Cerrado.Domain.Models;

public partial class user_setting
{
    public Guid user_id { get; set; }

    public string? tz { get; set; }

    public string? idioma { get; set; }

    public string? tts_voice { get; set; }

    public decimal? tts_rate { get; set; }

    public string accesibilidad { get; set; } = null!;

    public DateTime updated_at { get; set; }

    public virtual user user { get; set; } = null!;
}
