using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaleCore.Models;

namespace DaleCore.Interfaces
{
    public interface ICliente
    {
        List<Cliente> GetClientes();
    }
}
