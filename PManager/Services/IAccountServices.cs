using PManager.Models;
using System.Threading.Tasks;

namespace PManager.Services
{
    public interface IAccountServices
    {
        Task<(bool, LoginModels)> Validate(string username, string password);
    }
}