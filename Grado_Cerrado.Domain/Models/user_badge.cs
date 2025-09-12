namespace Grado_Cerrado.Domain.Models;

public partial class user_badge
{
    public Guid id { get; set; }

    public Guid user_id { get; set; }

    public string badge_code { get; set; } = null!;

    public DateTime granted_at { get; set; }

    public virtual user user { get; set; } = null!;
}
