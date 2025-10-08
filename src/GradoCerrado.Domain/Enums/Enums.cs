// src/GradoCerrado.Domain/Enums/Enums.cs - VERSI�N UNIFICADA
namespace GradoCerrado.Domain.Entities;

public enum LegalDocumentType
{
    Law,              // Ley
    Decree,           // Decreto
    Jurisprudence,    // Jurisprudencia
    Doctrine,         // Doctrina
    Constitution,     // Constituci�n
    Code,             // C�digo
    StudyMaterial,    // Material de estudio
    CaseStudy,        // Caso de estudio
    Regulation        // Reglamento
}

public enum DifficultyLevel
{
    Basic,         // B�sico
    Intermediate,  // Intermedio
    Advanced       // Avanzado
}

public enum QuestionType
{
    MultipleChoice,  // Selecci�n m�ltiple
    TrueFalse,       // Verdadero/Falso
    OpenEnded // ?? NECESITAS AGREGAR ESTE PARA PREGUNTAS ORALES
}