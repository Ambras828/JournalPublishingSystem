using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CustomeExceptions
{
    public class CountryAlreadyExistsException:Exception
    {
        public CountryAlreadyExistsException( string message):base(message) { }
    }
}
