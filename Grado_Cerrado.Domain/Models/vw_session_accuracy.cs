namespace Grado_Cerrado.Domain.Models;

public partial class vw_session_accuracy
{
    public Guid? session_id { get; set; }

    public Guid? user_id { get; set; }

    public decimal? pct_acierto { get; set; }

    public long? preguntas_resueltas { get; set; }
}
