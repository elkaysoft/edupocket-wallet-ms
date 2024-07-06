using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edupocket.Domain.Enums
{
    public enum characterSet
    {
        NUMERIC = 1,
        ALPHA_NUMERIC_NON_CASE =2,
        ALPHA_NUMERCIC_CASE = 3,
        HEX_STRING = 4,
        GUID = 5,
        UPPER_ALPHABETS_ONLY = 6,
        LOWER_ALPHABETS_ONLY = 7
    }
}
