using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigFrame.BaseService
{
    public class ActionStrAssistant
    {
        public class ParameterValue
        {
            public string name;
            public string value;

            public ParameterValue(string name, string value)
            {
                this.name = name;
                this.value = value;
            }
        }
        public static string buildActionStr(string action, ParameterValue[] paras)
        {
            string temp = action;
            if (paras.Length > 0)
            {
                temp += "?";
                for (int i = 0; i < paras.Length; i++)
                {

                    temp += paras[i].name + "=" + paras[i].value;
                  
                    if (i < paras.Length - 1)
                    {
                        temp += ";";
                    }
                }
            }

            return temp;
        }

        public static string getParameterStrValue(string action, string paraName)
        {
            Dictionary<string, string> paraMapper = new Dictionary<string, string>();
            string temp = action.Substring(action.IndexOf('?') + 1);
            string[] paras = temp.Split(';');
            for (int i = 0; i < paras.Length; i++)
            {
                string[] pair = paras[i].Split('=');
                paraMapper.Add(pair[0], pair[1]);
            }

            paraMapper.TryGetValue(paraName, out temp);
            return temp;
        }
    }
}
