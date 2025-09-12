namespace Grado_Cerrado.Domain.Models;

public partial class subject
{
    public Guid id { get; set; }

    public string nombre { get; set; } = null!;

    public virtual ICollection<topic> topics { get; set; } = new List<topic>();

    public virtual ICollection<vector_chunks_ref> vector_chunks_refs { get; set; } = new List<vector_chunks_ref>();
}
