using Application.DTOs.Complains;
using Domain.Entities;

namespace Application.Abstractions.Complains;

public interface IComplainRepository
{
    public Task<bool> CreateComplain(Complain complain);
    public Task<GetComplainDTO> GetComplain(int id);
    public Task<IEnumerable<GetComplainDTO>> GetComplainList();
}
