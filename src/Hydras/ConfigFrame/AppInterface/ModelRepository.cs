using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.BaseModel;

namespace ConfigFrame.AppInterface
{
    public class ModelRepository
    {
        public static List<IModel> modelList;

        static ModelRepository()
        {
            modelList = new List<IModel>();
        }

        public static void cleanRepositoty()
        {
            modelList.Clear();
        }

        public void addModel(IModel model)
        {
            modelList.Add(model);
        }

        public static IModel getModelByType(Type type)
        {
            for (int i = 0; i < modelList.Count; i++)
            {
                if (modelList[i].GetType() == type)
                {
                    return modelList[i];
                }
            }
            return null;
        }

        public static void updateModel(IModel model)
        {
            Type t = model.GetType();
            for (int i = 0; i < modelList.Count; i++)
            {
                if (modelList[i].GetType() == t)
                {
                    modelList[i].update(model);
                }
            }
        }
    }
}