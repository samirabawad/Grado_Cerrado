namespace Grado_Cerrado.Domain.Models;

public partial class topic
{
    public Guid id { get; set; }

    public Guid subject_id { get; set; }

    public string nombre { get; set; } = null!;

    public Guid? parent_topic_id { get; set; }

    public virtual ICollection<topic> Inverseparent_topic { get; set; } = new List<topic>();

    public virtual topic? parent_topic { get; set; }

    public virtual ICollection<question> questions { get; set; } = new List<question>();

    public virtual subject subject { get; set; } = null!;

    public virtual ICollection<user_topic_stat> user_topic_stats { get; set; } = new List<user_topic_stat>();

    public virtual ICollection<vector_chunks_ref> vector_chunks_refs { get; set; } = new List<vector_chunks_ref>();
}
