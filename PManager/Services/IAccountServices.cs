using PManager.Models;
using System.Threading.Tasks;

namespace PManager.Services
{
    public interface IAccountServices
    {
        Task<(bool, RegisterModels)> Validate(string username, string password);
    }
}