using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBookCQRS.Infra.CrossCutting.Identity.Helpers
{
    public interface IUserProvider
    {
        Guid GetUserId();
    }
}
