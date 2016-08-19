using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigFrame.BaseModel
{
    public interface IModel
    {
        void update(IModel model);
        string[] update(IModel model, string[] fields);
        string[] compare(IModel model);

        bool Modified
        {
            get;
            set;
        }

        List<string> ModifiedFieldList
        {
            get;
        }
    }
}
