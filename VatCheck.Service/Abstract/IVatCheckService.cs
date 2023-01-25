using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VatCheck.Service.Abstract
{
    public interface IVatCheckService
    {
        Task<string> CheckVat(string vatNumber);
    }
}
