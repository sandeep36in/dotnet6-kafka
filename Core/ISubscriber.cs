using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface ISubscriber<T>:IDisposable where T : class
    {
        T Consume(CancellationToken cancellationToken);
    }
}
