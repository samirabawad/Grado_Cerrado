namespace Grado_Cerrado.Domain.Models;

public partial class legal_doc
{
    public Guid id { get; set; }

    public string titulo { get; set; } = null!;

    public string? tipo { get; set; }

    public DateOnly? fecha { get; set; }

    public string? storage_url { get; set; }

    public DateTime created_at { get; set; }

    public virtual ICollection<vector_chunks_ref> vector_chunks_refs { get; set; } = new List<vector_chunks_ref>();
}
