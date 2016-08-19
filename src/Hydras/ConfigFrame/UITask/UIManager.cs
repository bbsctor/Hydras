using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigFrame.UITask
{
    public class UIManager
    {
        public enum UpdateType { ALL, PART };
        public UpdateType updateType;
        public List<string> updateList;
        private Dictionary<string, IBlockUIManager> mapper;
        public UIManager()
        {
            mapper = new Dictionary<string, IBlockUIManager>();
            updateType = UpdateType.ALL;
            updateList = new List<string>();
        }

        public void add(string name, IBlockUIManager um)
        {
            mapper.Add(name, um);
        }

        public void updateUI()
        {
            updateModel("ui");
        }

        public void updateDataModel()
        {
            updateModel("data");
        }

        private void updateModel(string modelType)
        {
            if (updateType == UpdateType.ALL)
            {
                foreach (IBlockUIManager um in mapper.Values)
                {
                    if (modelType.Equals("ui") == true)
                    {
                        um.updateUI();
                    }
                    else if (modelType.Equals("data") == true)
                    {
                        um.updateDataModel();
                    }
                }
            }
            else if (updateType == UpdateType.PART && updateList != null)
            {
                for (int i = 0; i < updateList.Count; i++)
                {
                    IBlockUIManager temp;
                    mapper.TryGetValue(updateList[i], out temp);
                    if (temp != null)
                    {
                        if (modelType.Equals("ui") == true)
                        {
                            temp.updateUI();
                        }
                        else if (modelType.Equals("data") == true)
                        {
                            temp.updateDataModel();
                        }
                    }
                }
            }
        }
    }
}
