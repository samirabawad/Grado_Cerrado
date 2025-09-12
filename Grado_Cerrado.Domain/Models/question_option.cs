namespace Grado_Cerrado.Domain.Models;

public partial class question_option
{
    public Guid id { get; set; }

    public Guid question_id { get; set; }

    public char opt_key { get; set; }

    public string texto { get; set; } = null!;

    public bool is_correct { get; set; }

    public virtual question question { get; set; } = null!;
}
