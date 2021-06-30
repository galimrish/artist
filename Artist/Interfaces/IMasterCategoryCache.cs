using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artist.Interfaces
{
    public interface IMasterCategoryCache
    {
        ICollection<IMasterCategory> GetMasterCategories();

        void ClearCache();
    }
}
