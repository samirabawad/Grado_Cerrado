namespace Grado_Cerrado.Domain.Models;

public partial class vector_chunks_ref
{
    public Guid id { get; set; }

    public Guid doc_id { get; set; }

    public string collection { get; set; } = null!;

    public string point_id { get; set; } = null!;

    public int chunk_idx { get; set; }

    public string? text_sha256 { get; set; }

    public Guid? subject_id { get; set; }

    public Guid? topic_id { get; set; }

    public DateTime created_at { get; set; }

    public virtual legal_doc doc { get; set; } = null!;

    public virtual subject? subject { get; set; }

    public virtual topic? topic { get; set; }
}
