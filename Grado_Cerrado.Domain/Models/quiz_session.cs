namespace Grado_Cerrado.Domain.Models;

public partial class quiz_session
{
    public Guid id { get; set; }

    public Guid user_id { get; set; }

    public string modo { get; set; } = null!;

    public int? total_pregs { get; set; }

    public DateTime started_at { get; set; }

    public DateTime? finished_at { get; set; }

    public virtual ICollection<attempt> attempts { get; set; } = new List<attempt>();

    public virtual user user { get; set; } = null!;
}
