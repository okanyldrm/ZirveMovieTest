using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirve.Core.Utilities.Results;
using Zirve.Entity.Concrete;

namespace Zirve.Business.Abstract
{
    public interface INoteService
    {
        IResult Add(Note entity);
    }
}
