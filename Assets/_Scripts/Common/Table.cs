namespace Eliminate.Common
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class Table<T> where T: TableInterface, new()
    {
        private Dictionary<string, int> m_ColumnNameMap;
        private int m_Count;
        private string m_FileName;
        private int m_LastIndex;
        private T[] m_Rows;

        public Table()
        {
            this.m_ColumnNameMap = new Dictionary<string, int>();
        }

        private int BinarySearch(int startIndex, int endIndex, int key)
        {
            if (this.m_Rows == null)
            {
                return -1;
            }
            int num = startIndex;
            int num2 = endIndex;
            int index = (num2 + num) / 2;
            while ((num < num2) && (this.m_Rows[index].GetIndex() != key))
            {
                if (this.m_Rows[index].GetIndex() < key)
                {
                    num = index + 1;
                }
                else
                {
                    num2 = index - 1;
                }
                index = (num2 + num) / 2;
            }
            return ((this.m_Rows[index].GetIndex() != key) ? -1 : index);
        }

        private void CleanBeforeReload()
        {
            if (this.m_Rows != null)
            {
                for (int i = 0; i < this.m_Rows.Length; i++)
                {
                    this.m_Rows[i] = null;
                }
                this.m_Rows = null;
                this.m_Count = 0;
                this.m_LastIndex = -1;
                this.m_ColumnNameMap.Clear();
            }
        }

        public bool ContainsRow(int ID)
        {
            int num = this.BinarySearch(0, this.m_Count - 1, ID);
            if (num >= 0)
            {
                this.m_LastIndex = num;
            }
            return (num >= 0);
        }

        //public T Find(FindDelegate<T> fd)
        //{
        //    int index = 0;
        //    int num2 = this.RowCount();
        //    while (index < num2)
        //    {
        //        T rowByIndex = this.GetRowByIndex(index);
        //        if (fd(rowByIndex))
        //        {
        //            return rowByIndex;
        //        }
        //        index++;
        //    }
        //    return null;
        //}

        public int GetColumnIndexByName(string name)
        {
            if (!this.m_ColumnNameMap.ContainsKey(name))
            {
                return -1;
            }
            return this.m_ColumnNameMap[name];
        }

        public int GetIndexByID(int ID)
        {
            if (((this.m_LastIndex >= 0) && (this.m_LastIndex < this.m_Count)) && (this.m_Rows[this.m_LastIndex].GetIndex() == ID))
            {
                return this.m_LastIndex;
            }
            return this.BinarySearch(0, this.m_Count - 1, ID);
        }

        public T GetRowByIndex(int index)
        {
            if ((index >= 0) && (index < this.m_Count))
            {
                return this.m_Rows[index];
            }
            ToolFunctions.LogException(new Exception(this.m_FileName + " can't find row! row index = " + index));
            return null;
        }

        public bool Load(string fileName)
        {
            this.CleanBeforeReload();
            this.m_FileName = fileName;
            if (string.IsNullOrEmpty(this.m_FileName))
            {
                return false;
            }
            TableSerializer s = new TableSerializer();
            s.SetCheckColumn(true);
            if (!s.OpenRead(fileName))
            {
                ToolFunctions.LogError(fileName + "Open Read Table Failed.");
                return false;
            }
            // 
            s.PreprocessTable();
            int lineCount = s.GetLineCount();
            if (lineCount <= 0)
            {
                return false;
            }
            this.m_Rows = new T[lineCount];
            for (int i = 0; i < lineCount; i++)
            {
                this.m_Rows[i] = Activator.CreateInstance<T>();
            }
            this.m_Count = 0;
            while (this.m_Count < lineCount)
            {
                s.SetCurrentLine(this.m_Count);
                this.m_Rows[this.m_Count].MapData(s);
                if ((this.m_Count > 0) && (this.m_Rows[this.m_Count].GetIndex() <= this.m_Rows[this.m_Count - 1].GetIndex()))
                {
                    ToolFunctions.LogError(string.Concat(new object[] { fileName, ": the id of rows is out of order(), lien[", this.m_Count, "]" }));
                    return false;
                }
                this.m_Count++;
            }
            this.SetColumnNames(s.GetColumnNames(), s.GetColumnCount());
            s.Close();
            return true;
        }

        public bool LoadAsDictionary(string filename, Dictionary<int, T> mDic, List<int> mList)
        {
            if (File.Exists(filename))
            {
                this.Load(filename);
                this.TableToDictionary(mDic, mList);
            }
            else if (mDic == null)
            {
                return false;
            }
            return true;
        }

        private T Row(int ID)
        {
            if (((this.m_LastIndex >= 0) && (this.m_LastIndex < this.m_Count)) && (this.m_Rows[this.m_LastIndex].GetIndex() == ID))
            {
                return this.m_Rows[this.m_LastIndex];
            }
            int index = this.BinarySearch(0, this.m_Count - 1, ID);
            if (index < 0)
            {
                ToolFunctions.LogException(new Exception(this.m_FileName + " can't find row! rowID = " + ID));
                return null;
            }
            this.m_LastIndex = index;
            return this.m_Rows[index];
        }

        public int RowCount()
        {
            return this.m_Count;
        }

        private void SetColumnNames(string[] columnNames, int columnCount)
        {
            if ((columnNames != null) && (columnCount > 0))
            {
                for (int i = 0; i < columnCount; i++)
                {
                    if (columnNames[i] == null)
                    {
                        break;
                    }
                    if (this.m_ColumnNameMap.ContainsKey(columnNames[i]))
                    {
                        ToolFunctions.LogError(this.m_FileName + string.Concat(new object[] { "duplicate columnName = ", columnNames[i], " new columnCount = ", i + 1, " old columnCount = ", this.m_ColumnNameMap[columnNames[i]] + 1 }));
                        break;
                    }
                    this.m_ColumnNameMap.Add(columnNames[i], i);
                }
            }
        }

        public void TableToDictionary(Dictionary<int, T> mDict, List<int> mList)
        {
            for (int i = 0; i < this.m_Count; i++)
            {
                T rowByIndex = this.GetRowByIndex(i);
                mDict.Add(rowByIndex.GetIndex(), rowByIndex);
                mList.Add(rowByIndex.GetIndex());
            }
        }

        public T this[int ID]
        {
            get
            {
                return this.Row(ID);
            }
        }

        public delegate bool FindDelegate(T ele);
    }
}

