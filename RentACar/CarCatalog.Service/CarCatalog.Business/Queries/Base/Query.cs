using System;
using System.Collections.Generic;
using System.Text;

namespace CarCatalog.Business.Queries.Base
{
    public abstract class Query
    {
        public abstract string Name
        {
            get;
        }
    }
}
