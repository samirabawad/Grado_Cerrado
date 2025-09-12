namespace Grado_Cerrado.Domain.Models;

public partial class attempt
{
    public Guid id { get; set; }

    public Guid quiz_session_id { get; set; }

    public Guid question_id { get; set; }

    public string? respuesta_opciones { get; set; }

    public bool? respuesta_vf { get; set; }

    public string? respuesta_texto { get; set; }

    public bool? es_correcta { get; set; }

    public int? tiempo_ms { get; set; }

    public string source { get; set; } = null!;

    public DateTime created_at { get; set; }

    public virtual ICollection<ai_evaluation> ai_evaluations { get; set; } = new List<ai_evaluation>();

    public virtual ICollection<answer_medium> answer_media { get; set; } = new List<answer_medium>();

    public virtual question question { get; set; } = null!;

    public virtual quiz_session quiz_session { get; set; } = null!;
}
