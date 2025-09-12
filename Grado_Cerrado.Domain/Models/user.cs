namespace Grado_Cerrado.Domain.Models;

public partial class user
{
    public Guid id { get; set; }

    public string email { get; set; } = null!;

    public string? nombre { get; set; }

    public DateTime created_at { get; set; }

    public virtual ICollection<quiz_session> quiz_sessions { get; set; } = new List<quiz_session>();

    public virtual ICollection<user_badge> user_badges { get; set; } = new List<user_badge>();

    public virtual user_level? user_level { get; set; }

    public virtual user_setting? user_setting { get; set; }

    public virtual ICollection<user_topic_stat> user_topic_stats { get; set; } = new List<user_topic_stat>();
}
