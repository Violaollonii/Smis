using Smis.Models;

namespace Smis.Interfaces
{
    public interface IUseriRepository
    {
        ICollection<Useri> GetUsers();
    }
}
