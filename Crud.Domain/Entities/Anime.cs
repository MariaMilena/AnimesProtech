using Crud.Domain.Validation;

namespace Crud.Domain.Entities;

public class Anime : Entity
{
    public string? Name { get; set; }
    public string? Summary { get; set; }
    public string? Director { get; set; }

    public Anime(string name, string summary, string director)
    {
        ValidateDomain(name, summary, director);
    }

    private void ValidateDomain(string name, string summary, string director)
    {
        DomainValidation.When(string.IsNullOrEmpty(name),
            "Nome inválido.");

        DomainValidation.When(string.IsNullOrEmpty(summary),
            "Resumo inválido.");

        DomainValidation.When(string.IsNullOrEmpty(director),
            "Diretor inválido.");

        Name = name;
        Summary = summary;
        Director = director;
    }
}
