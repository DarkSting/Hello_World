using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.CustomServices
{
    public interface ICustomService
    {
        void ReadKeys();
        string GetPublicKey();
        string GetSecretKey();
    }
}
