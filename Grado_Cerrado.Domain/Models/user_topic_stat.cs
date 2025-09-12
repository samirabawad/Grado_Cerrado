namespace Grado_Cerrado.Domain.Models;

public partial class user_topic_stat
{
    public Guid user_id { get; set; }

    public Guid topic_id { get; set; }

    public int preguntas { get; set; }

    public int aciertos { get; set; }

    public DateTime last_update { get; set; }

    public virtual topic topic { get; set; } = null!;

    public virtual user user { get; set; } = null!;
}
