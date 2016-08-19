using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ConfigFrame.BaseModel
{
    public class ModelSupport
    {
        public static bool arrayEqual<T>(IList<T> a, IList<T> b)
        {

            if (a != null && b != null && a.Count == b.Count)
            {
                for (int i = 0; i < a.Count; i++)
                {
                    if (!a[i].Equals(b[i]))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        public static string[] update(BasicModel oldModel, BasicModel newModel, string[] fields)
        {
            if (oldModel.GetType() == newModel.GetType())
            {
                List<string> temp = new List<string>();
                if (fields == null)
                {
                    FieldInfo[] fInfos = oldModel.GetType().GetFields();
                    for (int i = 0; i < fInfos.Length; i++)
                    {
                        System.Reflection.FieldInfo fInfo = fInfos[i];
                        temp.Add(fInfo.Name);
                    }
                }
                else
                {
                    temp.AddRange(fields);
                }

                string[] fieldName = temp.ToArray();

                for (int i = 0; i < fieldName.Length; i++)
                {
                    FieldInfo fInfo = oldModel.GetType().GetField(fieldName[i]);
                    if (fInfo != null)
                    {
                        Type subType = fInfo.FieldType;
                        if (subType.IsArray == true)
                        {
                            if ("Byte[]".Equals(subType.Name))
                            {
                                bool isEqual = arrayEqual((Byte[])fInfo.GetValue(oldModel),
                                (Byte[])fInfo.GetValue(newModel));
                                if (isEqual == false)
                                {
                                    oldModel.ModifiedFieldList.Add(fInfo.Name);
                                    fInfo.SetValue(oldModel, fInfo.GetValue(newModel));
                                    oldModel.Modified = true;
                                }
                            }
                            else
                            {
                                throw new NotImplementedException();
                            }
                            
                        }
                        else if (fInfo.IsStatic || fInfo.GetValue(oldModel) == null ||
                            fInfo.GetValue(oldModel).Equals(fInfo.GetValue(newModel)) == false)
                        {
                            oldModel.ModifiedFieldList.Add(fInfo.Name);
                            fInfo.SetValue(oldModel, fInfo.GetValue(newModel));
                            oldModel.Modified = true;
                        }
                    }
                }
                return null;
            }
            return null;
        }

        public static string[] compare(BasicModel oldModel, BasicModel newModel)
        {
            List<string> temp = new List<string>();
            if (oldModel.GetType() == newModel.GetType())
            {
                FieldInfo[] fInfos = oldModel.GetType().GetFields();
                for (int i = 0; i < fInfos.Length; i++)
                {
                    System.Reflection.FieldInfo fInfo = fInfos[i];
                    if (!fInfo.GetValue(oldModel).Equals(fInfo.GetValue(newModel)))
                    {
                        temp.Add(fInfo.Name);
                    }
                }
            }
            return temp.ToArray();
        }
    }
}
