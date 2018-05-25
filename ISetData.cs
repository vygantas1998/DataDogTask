using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDog
{
    public interface ISetData<T>
    {
        void SetData(string[] data);
    }
}
