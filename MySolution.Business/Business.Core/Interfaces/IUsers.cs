using Model.Core.Entities;

namespace Business.Core.Interfaces;

public interface IUsers
{
    Task<List<User>> GetAll();
}