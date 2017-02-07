using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigEgg.Tools.ConsoleExtension.Parameters.Errors
{
    internal class DuplicatePropertyError : Error
    {
        public DuplicatePropertyError(string propertyName)
            : base(ErrorType.DuplicateProperty, true)
        {
            PropertyName = propertyName;
        }

        public string PropertyName { get; private set; }
    }
}
