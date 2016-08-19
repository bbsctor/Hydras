using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HydrasBase.BaseModelImpl.BaseDataModelImpl;
using HydrasBase.BaseModelImpl.BaseViewModelImpl;
using HydrasBase.BaseModelImpl.ModelConverterImpl;

namespace HydrasUI_WPF.Util
{
    public class LogFileBuilder
    {
        public static string buildLogFileName(SondeInfoViewModel sondeViewModel,
            LogFileBaseInfoViewModel logBaseViewModel)
        {
            List<char> temp = new List<char>();
            for (int i = 0; i < logBaseViewModel.LogFileName.Length; i++)
            {
                if (Char.IsSymbol(logBaseViewModel.LogFileName[i]) == false)
                {
                    temp.Add(logBaseViewModel.LogFileName[i]);
                }
            }
            string result = new string(temp.ToArray());
            result += "[" + sondeViewModel.Model + " - " + sondeViewModel.SerialNum + "]";

            return result;
        }

        public static string buildLogFileContent(SondeInfoViewModel sondeViewModel, 
            LogFileBaseInfoViewModel logBaseViewModel,
            LogFileSettingFieldListViewModel logSettingFieldListViewModel,
            LogFileParameterInfoListViewModel logParameterInfoListViewModel,
            LogFileParameterValueListViewModel logParameterValueListViewModel)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(sondeViewModel.Model + " " + sondeViewModel.SerialNum);
            result.AppendLine("LogFile Name:" + logBaseViewModel.LogFileName);
            for (int i = 0; i < logSettingFieldListViewModel.Count; i++)
            {
                if (logSettingFieldListViewModel[i].Sn == 0x01 || logSettingFieldListViewModel[i].Sn == 0x02)
                {
                    result.AppendLine(logSettingFieldListViewModel[i].SettingName + ": "
                        + logSettingFieldListViewModel[i].SettingValue);
                }
                else if (logSettingFieldListViewModel[i].Sn == 0x03 || logSettingFieldListViewModel[i].Sn == 0x04 ||
                    logSettingFieldListViewModel[i].Sn == 0x05)
                {
                    result.AppendLine(logSettingFieldListViewModel[i].SettingName + ": "
                        + ((DateTime)logSettingFieldListViewModel[i].SettingValue).TimeOfDay);
                }
                else if (logSettingFieldListViewModel[i].Sn == 0x06)
                {
                    string status = "";
                    if (((byte[])logSettingFieldListViewModel[i].SettingValue)[0] == 0x00)
                    {
                        status = "off";
                    }
                    else
                    {
                        status = "on";
                    }
                    result.AppendLine(logSettingFieldListViewModel[i].SettingName + ": "
                        + status);
                }
            }
            
            for (int i = 0; i < logParameterInfoListViewModel.Count; i++)
            {
                if (i < logParameterInfoListViewModel.Count - 1)
                {
                    result.Append(logParameterInfoListViewModel[i].ParaName + ", ");
                }
                else
                {
                    result.AppendLine(logParameterInfoListViewModel[i].ParaName);
                }
            }

            for (int i = 0; i < logParameterInfoListViewModel.Count; i++)
            {
                if (i < logParameterInfoListViewModel.Count - 1)
                {
                    result.Append(logParameterInfoListViewModel[i].CalUnit + ", ");
                }
                else
                {
                    result.AppendLine(logParameterInfoListViewModel[i].CalUnit);
                }
            }

            LogFileParameterValueViewModel temp = null;
            for (int i = 0; i < logParameterValueListViewModel.Count; i++)
            {
                temp = logParameterValueListViewModel[i];
                for (int j = 0; j < temp.values.Count; j++)
                {
                    if (j < temp.values.Count - 1)
                    {
                        result.Append(temp.values[j] + ", ");
                    }
                    else
                    {
                        result.AppendLine(temp.values[j]);
                    }
                }
            }

            return result.ToString();
        }
    }
}
