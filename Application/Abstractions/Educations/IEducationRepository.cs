using Domain.Entities;
using System.Collections.Generic;

namespace Application.Abstractions.Educations;

public interface IEducationRepository
{
    public Task<IEnumerable<Education>> GetEducationtList();
}
