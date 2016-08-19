using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigFrame.AppInterface
{
    public interface IInteractionValidator
    {
        bool validate(Object send, Object resp);
    }
}
