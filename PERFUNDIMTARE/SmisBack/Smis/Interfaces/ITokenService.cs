using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smis.Models;

namespace Smis.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(Useri user);
        RefreshToken GenerateRefreshToken();
    }
}