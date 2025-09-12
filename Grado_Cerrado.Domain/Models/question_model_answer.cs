namespace Grado_Cerrado.Domain.Models;

public partial class question_model_answer
{
    public Guid question_id { get; set; }

    public string texto_modelo { get; set; } = null!;

    public string refs { get; set; } = null!;

    public virtual question question { get; set; } = null!;
}
