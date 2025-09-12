namespace Grado_Cerrado.Domain.Models;

public partial class question
{
    public Guid id { get; set; }

    public Guid? topic_id { get; set; }

    public string enunciado { get; set; } = null!;

    public string tipo { get; set; } = null!;

    public short dificultad { get; set; }

    public bool? vf_correct { get; set; }

    public bool is_active { get; set; }

    public DateTime created_at { get; set; }

    public virtual ICollection<ai_generation> ai_generations { get; set; } = new List<ai_generation>();

    public virtual ICollection<attempt> attempts { get; set; } = new List<attempt>();

    public virtual question_model_answer? question_model_answer { get; set; }

    public virtual ICollection<question_option> question_options { get; set; } = new List<question_option>();

    public virtual ICollection<question_source> question_sources { get; set; } = new List<question_source>();

    public virtual ICollection<question_tts_cache> question_tts_caches { get; set; } = new List<question_tts_cache>();

    public virtual topic? topic { get; set; }
}
