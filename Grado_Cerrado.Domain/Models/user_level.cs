namespace Grado_Cerrado.Domain.Models;

public partial class user_level
{
    public Guid user_id { get; set; }

    public int level { get; set; }

    public int xp { get; set; }

    public virtual user user { get; set; } = null!;
}
