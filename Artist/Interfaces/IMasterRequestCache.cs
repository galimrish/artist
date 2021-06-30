using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artist.Interfaces
{
    public interface IMasterRequestCache
    {
        ICollection<IMasterRequest> GetMasterRequests();

        void ClearCache();
    }
}
