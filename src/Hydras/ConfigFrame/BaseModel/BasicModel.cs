using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ConfigFrame.BaseModel
{
    public class BasicModel : IModel
    {
        bool modified;
        public List<string> ModifiedFieldList
        {
            get { return modifiedFieldList; }
        }
        private List<string> modifiedFieldList;

        public bool Modified
        {
            get{return modified;}
            set{modified = value;}
        }

        public BasicModel()
        {
            modifiedFieldList = new List<string>();
        }

        public virtual void update(IModel newModel)
        {
            ModelSupport.update((BasicModel)this, (BasicModel)newModel, null);
        }

        public virtual string[] update(IModel newModel, string[] fields)
        {
            return ModelSupport.update((BasicModel)this, (BasicModel)newModel, fields);
        }

        public virtual string[] compare(IModel model)
        {
            return ModelSupport.compare((BasicModel)this, (BasicModel)model);
        }

        public static bool arrayEqual<T>(IList<T> a, IList<T> b)
        {
            return ModelSupport.arrayEqual(a, b);
        }
    }
}