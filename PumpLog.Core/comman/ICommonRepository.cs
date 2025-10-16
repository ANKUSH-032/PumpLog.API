using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumpLog.Core.comman
{
    public interface ICommonRepository
    {
        Task<ClsResponse<MasterList>> MasterListAsync(string type);
    }
}
