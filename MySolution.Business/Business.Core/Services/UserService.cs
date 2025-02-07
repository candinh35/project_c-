using Business.Core.Interfaces;
using Framework.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using Model.Core.Entities;

namespace Business.Core;

public class UserService : IUsers
{
    private readonly IRepository<User> _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _userRepository = unitOfWork.GetRepository<User>();
    }
    
    public async  Task<List<User>> GetAll()
    {
        return await _userRepository.GetAll().ToListAsync();
    }
}