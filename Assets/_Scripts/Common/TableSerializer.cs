namespace Eliminate.Common
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    // test
    using UnityEngine;

    internal class TableSerializer : ISerializer
    {
        public static readonly string[] COLUMN_TYPE_TO_STRING = new string[] { "INT", "UINT", "INT64", "UINT64", "FLOAT", "STRING", "STRING_INT", "STRING_FLOAT", "STRING_STRING" };
        public const string DEFAUL_TABLE_FIELD = "-1";
        private string[] m_ColumnNames;
        private EM_TYPE_COLUMN[] m_ColumnTypes;
        private int m_CurrentColumn = 0;
        private int m_CurrentID = -1;
        private int m_CurrentLine = 0;
        private MemoryStream m_File = null;
        private string m_FileName;
        private bool m_IsCheckColumn = false;
        private TablePreprocess m_PreprocessData = new TablePreprocess();
        private byte[] m_ReadBuffer;
        public const int MIN_LINE_COUNT = 2;
        public const string TABLE_COLUMN_SEPARATOR = "\t";
        public static readonly string[] TABLE_LINE_SEPARATOR = new string[] { "\r\n", "\n" };
        public const char TABLE_SKIP_CHAR = '#';
        public const string TABLE_STRING_ARRAY_SEPARATOR = "|";

        private bool _CheckColumnType(EM_TYPE_COLUMN columnType)
        {
            // Debug.Log(m_ColumnTypes[m_CurrentColumn]);
            if (this.m_IsCheckColumn && (this.m_ColumnTypes[this.m_CurrentColumn] != columnType))
            {
                ToolFunctions.LogError(string.Concat(new object[] { "table [", this.m_FileName, "], line [", this.m_CurrentLine, "] column [", this.m_CurrentColumn, "] isn't match! in struct TableRow : [", COLUMN_TYPE_TO_STRING[(int)columnType], "] in .tab file : [", COLUMN_TYPE_TO_STRING[(int)this.m_ColumnTypes[this.m_CurrentColumn]], "]" }));
                return false;
            }
            return true;
        }

        private void _EndParseColumn()
        {
            this.m_CurrentColumn++;
        }

        private string _GetSubString(byte[] sourceString, int off, int length, ByteSection section)
        {
            return this._GetSubString(sourceString, off, length, section.m_SectionBeginPos, section.m_SectionLength);
        }

        private string _GetSubString(byte[] sourceString, int off, int length, int sectionPos, int sectionLength)
        {
            if ((sectionPos < off) || ((sectionPos + sectionLength) > (off + length)))
            {
                ToolFunctions.LogError(string.Concat(new object[] { "SourceString:[", sourceString, "], Off:[", off, "], Length:[", length, "], StringSectionBegin:[", sectionPos, "], StringSectionLength:[", sectionLength, "]" }));
                return null;
            }
            if (sectionLength <= 0)
            {
                return string.Empty;
            }
            return Encoding.UTF8.GetString(sourceString, sectionPos, sectionLength);
        }

        private EM_TYPE_COLUMN _ParseColumnType(ByteSection fieldSection)
        {
            string str = this._GetSubString(this.m_ReadBuffer, 0, this.m_ReadBuffer.Length, fieldSection);
            for (int i = 0; i < 9; i++)
            {
                if (COLUMN_TYPE_TO_STRING[i] == str)
                {
                    return (EM_TYPE_COLUMN)i;
                }
            }
            return EM_TYPE_COLUMN.INVALID;
        }

        private void _ReadFileToMemory()
        {
            if (this.m_File != null)
            {
                long length = this.m_File.Length;
                if (length > 0L)
                {
                    this.m_ReadBuffer = new byte[length];
                    if (this.m_File.Read(this.m_ReadBuffer, 0, this.m_ReadBuffer.Length) <= 0)
                    {
                    }
                }
            }
        }

        private void _ReadLine()
        {
            List<ByteSection> list = null;
            for (int i = 0; i < TABLE_LINE_SEPARATOR.Length; i++)
            {
                list = _SeparateString(this.m_ReadBuffer, 0, this.m_ReadBuffer.Length, TABLE_LINE_SEPARATOR[i], true);
                if (list.Count >= 2)
                {
                    break;
                }
            }
            if ((list == null) || (list.Count < 2))
            {
                ToolFunctions.LogError(string.Concat(new object[] { "table [", this.m_FileName, "], line count[", list.Count, "]" }));
            }
            else
            {
                int num2 = 0;
                ByteSection section = list[num2];
                ByteSection section2 = list[num2];
                List<ByteSection> columnField = _SeparateString(this.m_ReadBuffer, section.m_SectionBeginPos, section2.m_SectionLength, "\t", false);
                this.m_PreprocessData.m_ColumnName = new TableLine(list[num2], columnField);
                this.m_PreprocessData.m_ColumnCount = this.m_PreprocessData.m_ColumnName.m_ColumnField.Count;
                num2++;
                ByteSection section3 = list[num2];
                ByteSection section4 = list[num2];
                List<ByteSection> list3 = _SeparateString(this.m_ReadBuffer, section3.m_SectionBeginPos, section4.m_SectionLength, "\t", false);
                this.m_PreprocessData.m_ColumnType = new TableLine(list[num2], list3);
                int count = this.m_PreprocessData.m_ColumnType.m_ColumnField.Count;
                if (count != this.m_PreprocessData.m_ColumnCount)
                {
                    ToolFunctions.LogError(string.Concat(new object[] { this.m_FileName, " ColumnNameCount[", this.m_PreprocessData.m_ColumnCount, "], ColumnTypeCount[", count, "]" }));
                }
                else
                {
                    num2++;
                    this.m_PreprocessData.m_Data = new List<TableLine>();
                    while (num2 < list.Count)
                    {
                        ByteSection section5 = list[num2];
                        if (this.m_ReadBuffer[section5.m_SectionBeginPos] != 0x23)
                        {
                            ByteSection section6 = list[num2];
                            ByteSection section7 = list[num2];
                            List<ByteSection> list4 = _SeparateString(this.m_ReadBuffer, section6.m_SectionBeginPos, section7.m_SectionLength, "\t", false);
                            if (list4.Count != this.m_PreprocessData.m_ColumnCount)
                            {
                                ToolFunctions.LogError(string.Concat(new object[] { this.m_FileName, " ColumnNameCount[", this.m_PreprocessData.m_ColumnCount, "], ColumnCount of line[", num2, "] is [", list4.Count, "]" }));
                            }
                            else
                            {
                                this.m_PreprocessData.m_Data.Add(new TableLine(list[num2], list4));
                            }
                        }
                        num2++;
                    }
                }
            }
        }

        private void _SaveColumnNames()
        {
            this.m_ColumnNames = new string[this.GetColumnCount()];
            for (int i = 0; i < this.m_ColumnNames.Length; i++)
            {
                this.m_ColumnNames[i] = this._GetSubString(this.m_ReadBuffer, 0, this.m_ReadBuffer.Length, this.m_PreprocessData.m_ColumnName.m_ColumnField[i]);
            }
        }

        private void _SaveColumnTypes()
        {
            // 类型数组
            this.m_ColumnTypes = new EM_TYPE_COLUMN[this.GetColumnCount()];
            for (int i = 0; i < this.m_PreprocessData.m_ColumnName.m_ColumnField.Count; i++)
            {
                this.m_ColumnTypes[i] = this._ParseColumnType(this.m_PreprocessData.m_ColumnType.m_ColumnField[i]);
                if ((this.m_ColumnTypes[i] <= EM_TYPE_COLUMN.INVALID) || (this.m_ColumnTypes[i] >= EM_TYPE_COLUMN.COUNT))
                {
                    ToolFunctions.LogError(string.Concat(new object[] { "table [", this.m_FileName, "] invalid column type = [", this.m_PreprocessData.m_ColumnType.m_ColumnField[i], "]in column[", i, "]" }));
                }
            }
        }

        private static List<int> _SearchBytePattern(byte[] bytes, int off, int length, byte[] pattern)
        {
            if ((bytes == null) || (pattern == null))
            {
                return null;
            }
            List<int> list = new List<int>();
            int num = pattern.Length;
            byte num2 = pattern[0];
            for (int i = off; i < (off + length); i++)
            {
                if ((num2 != bytes[i]) || (((off + length) - i) < num))
                {
                    continue;
                }
                bool flag = true;
                for (int j = 1; j < pattern.Length; j++)
                {
                    if (pattern[j] != bytes[i + j])
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    list.Add(i);
                    i += pattern.Length - 1;
                }
            }
            return list;
        }

        private static List<ByteSection> _SeparateString(byte[] sourceString, int off, int length, string separateString, bool isLine = false)
        {
            if ((sourceString == null) || string.IsNullOrEmpty(separateString))
            {
                return null;
            }
            List<int> list = _SearchBytePattern(sourceString, off, length, Encoding.UTF8.GetBytes(separateString));
            if (list == null)
            {
                return null;
            }
            List<ByteSection> list2 = new List<ByteSection>();
            if (list.Count <= 0)
            {
                if (!isLine)
                {
                    list2.Add(new ByteSection(off, length));
                }
                return list2;
            }
            list2.Add(new ByteSection(off, list[0] - off));
            int num = separateString.Length;
            for (int i = 1; i < list.Count; i++)
            {
                int beginPos = list[i - 1] + num;
                list2.Add(new ByteSection(beginPos, list[i] - beginPos));
            }
            if (!isLine)
            {
                int num4 = list[list.Count - 1] + num;
                list2.Add(new ByteSection(num4, (off + length) - num4));
            }
            return list2;
        }

        private void _Set(ref int value)
        {
            if (this.m_CurrentColumn >= this.m_PreprocessData.m_ColumnCount)
            {
                ToolFunctions.LogError(string.Concat(new object[] { "table[", this.m_FileName, "] id[", this.m_CurrentID, "] line[", this.m_CurrentLine, "], column[", this.m_CurrentColumn, "], max column count[", this.m_PreprocessData.m_ColumnCount, "]" }));
                this._EndParseColumn();
            }
            else if (!this._CheckColumnType(EM_TYPE_COLUMN.INT))
            {
                this._EndParseColumn();
            }
            else
            {
                string str = this._GetSubString(this.m_ReadBuffer, 0, this.m_ReadBuffer.Length, this.m_PreprocessData.m_Data[this.m_CurrentLine].m_ColumnField[this.m_CurrentColumn]);
                if (string.IsNullOrEmpty(str))
                {
                    this._EndParseColumn();
                    ToolFunctions.LogError(string.Concat(new object[] { "table[", this.m_FileName, "] id[", this.m_CurrentID, "] has invalid value in line[", this.m_CurrentLine, "] and column[", this.m_CurrentColumn, "]" }));
                }
                else
                {
                    value = StringUtils.StringToInt(str);
                    this._EndParseColumn();
                }
            }
        }

        private void _Set(ref long value)
        {
            if (this.m_CurrentColumn >= this.m_PreprocessData.m_ColumnCount)
            {
                ToolFunctions.LogError(string.Concat(new object[] { "table[", this.m_FileName, "] id[", this.m_CurrentID, "] line[", this.m_CurrentLine, "], column[", this.m_CurrentColumn, "], max column count[", this.m_PreprocessData.m_ColumnCount, "]" }));
                this._EndParseColumn();
            }
            else if (!this._CheckColumnType(EM_TYPE_COLUMN.INT64))
            {
                this._EndParseColumn();
            }
            else
            {
                string str = this._GetSubString(this.m_ReadBuffer, 0, this.m_ReadBuffer.Length, this.m_PreprocessData.m_Data[this.m_CurrentLine].m_ColumnField[this.m_CurrentColumn]);
                if (string.IsNullOrEmpty(str))
                {
                    ToolFunctions.LogError(string.Concat(new object[] { "table[", this.m_FileName, "] id[", this.m_CurrentID, " has invalid value in line[", this.m_CurrentLine, "] and column[", this.m_CurrentColumn, "]" }));
                    this._EndParseColumn();
                }
                else
                {
                    value = StringUtils.StringToLong(str);
                    this._EndParseColumn();
                }
            }
        }

        private void _Set(ref float value)
        {
            if (this.m_CurrentColumn >= this.m_PreprocessData.m_ColumnCount)
            {
                ToolFunctions.LogError(string.Concat(new object[] { "table[", this.m_FileName, "] id[", this.m_CurrentID, "] line[", this.m_CurrentLine, "], column[", this.m_CurrentColumn, "], max column count[", this.m_PreprocessData.m_ColumnCount, "]" }));
                this._EndParseColumn();
            }
            else if (!this._CheckColumnType(EM_TYPE_COLUMN.FLOAT))
            {
                this._EndParseColumn();
            }
            else
            {
                string str = this._GetSubString(this.m_ReadBuffer, 0, this.m_ReadBuffer.Length, this.m_PreprocessData.m_Data[this.m_CurrentLine].m_ColumnField[this.m_CurrentColumn]);
                if (string.IsNullOrEmpty(str))
                {
                    ToolFunctions.LogError(string.Concat(new object[] { "table[", this.m_FileName, "] id[", this.m_CurrentID, " has invalid value in line[", this.m_CurrentLine, "] and column[", this.m_CurrentColumn, "]" }));
                    this._EndParseColumn();
                }
                else
                {
                    value = StringUtils.StringToFloat(str);
                    this._EndParseColumn();
                }
            }
        }

        private void _Set(ref string value)
        {
            if (this.m_CurrentColumn >= this.m_PreprocessData.m_ColumnCount)
            {
                ToolFunctions.LogError(string.Concat(new object[] { "table[", this.m_FileName, "] id[", this.m_CurrentID, "] line[", this.m_CurrentLine, "], column[", this.m_CurrentColumn, "], max column count[", this.m_PreprocessData.m_ColumnCount, "]" }));
                this._EndParseColumn();
            }
            else if (!this._CheckColumnType(EM_TYPE_COLUMN.STRING))
            {
                this._EndParseColumn();
            }
            else
            {
                string str = this._GetSubString(this.m_ReadBuffer, 0, this.m_ReadBuffer.Length, this.m_PreprocessData.m_Data[this.m_CurrentLine].m_ColumnField[this.m_CurrentColumn]);
                if (str == null)
                {
                    ToolFunctions.LogError(string.Concat(new object[] { "table[", this.m_FileName, "] id[", this.m_CurrentID, " has invalid value in line[", this.m_CurrentLine, "] and column[", this.m_CurrentColumn, "]" }));
                    this._EndParseColumn();
                }
                else
                {
                    value = str;
                    this._EndParseColumn();
                }
            }
        }

        private void _Set(ref uint value)
        {
            if (this.m_CurrentColumn >= this.m_PreprocessData.m_ColumnCount)
            {
                ToolFunctions.LogError(string.Concat(new object[] { "table[", this.m_FileName, "] id[", this.m_CurrentID, "] line[", this.m_CurrentLine, "], column[", this.m_CurrentColumn, "], max column count[", this.m_PreprocessData.m_ColumnCount, "]" }));
                this._EndParseColumn();
            }
            else if (!this._CheckColumnType(EM_TYPE_COLUMN.UINT))
            {
                this._EndParseColumn();
            }
            else
            {
                string str = this._GetSubString(this.m_ReadBuffer, 0, this.m_ReadBuffer.Length, this.m_PreprocessData.m_Data[this.m_CurrentLine].m_ColumnField[this.m_CurrentColumn]);
                if (string.IsNullOrEmpty(str))
                {
                    this._EndParseColumn();
                    ToolFunctions.LogError(string.Concat(new object[] { "table[", this.m_FileName, "] id[", this.m_CurrentID, " has invalid value in line[", this.m_CurrentLine, "] and column[", this.m_CurrentColumn, "]" }));
                }
                else
                {
                    value = StringUtils.StringToUInt(str);
                    this._EndParseColumn();
                }
            }
        }

        private void _Set(ref ulong value)
        {
            if (this.m_CurrentColumn >= this.m_PreprocessData.m_ColumnCount)
            {
                ToolFunctions.LogError(string.Concat(new object[] { "table[", this.m_FileName, "] id[", this.m_CurrentID, "] line[", this.m_CurrentLine, "], column[", this.m_CurrentColumn, "], max column count[", this.m_PreprocessData.m_ColumnCount, "]" }));
                this._EndParseColumn();
            }
            else if (!this._CheckColumnType(EM_TYPE_COLUMN.UINT64))
            {
                this._EndParseColumn();
            }
            else
            {
                string str = this._GetSubString(this.m_ReadBuffer, 0, this.m_ReadBuffer.Length, this.m_PreprocessData.m_Data[this.m_CurrentLine].m_ColumnField[this.m_CurrentColumn]);
                if (string.IsNullOrEmpty(str))
                {
                    ToolFunctions.LogError(string.Concat(new object[] { "table[", this.m_FileName, "] id[", this.m_CurrentID, " has invalid value in line[", this.m_CurrentLine, "] and column[", this.m_CurrentColumn, "]" }));
                    this._EndParseColumn();
                }
                else
                {
                    value = StringUtils.StringToULong(str);
                    this._EndParseColumn();
                }
            }
        }

        private void _Set(ref int[] value)
        {
            if (this.m_CurrentColumn >= this.m_PreprocessData.m_ColumnCount)
            {
                ToolFunctions.LogError(string.Concat(new object[] { "table[", this.m_FileName, "] id[", this.m_CurrentID, "] line[", this.m_CurrentLine, "], column[", this.m_CurrentColumn, "], max column count[", this.m_PreprocessData.m_ColumnCount, "]" }));
                this._EndParseColumn();
            }
            else if (!this._CheckColumnType(EM_TYPE_COLUMN.INT_ARRAY))
            {
                this._EndParseColumn();
            }
            else
            {
                string str = this._GetSubString(this.m_ReadBuffer, 0, this.m_ReadBuffer.Length, this.m_PreprocessData.m_Data[this.m_CurrentLine].m_ColumnField[this.m_CurrentColumn]);
                if (str == null)
                {
                    ToolFunctions.LogError(string.Concat(new object[] { "table[", this.m_FileName, "] id[", this.m_CurrentID, " has invalid value in line[", this.m_CurrentLine, "] and column[", this.m_CurrentColumn, "]" }));
                    this._EndParseColumn();
                }
                else
                {
                    if (("-1" == str) || string.IsNullOrEmpty(str))
                    {
                        value = new int[0];
                    }
                    else
                    {
                        string[] strArray = str.Split("|".ToCharArray());
                        value = new int[strArray.Length];
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            if (string.IsNullOrEmpty(strArray[i]))
                            {
                                ToolFunctions.LogError(string.Concat(new object[] { "table[", this.m_FileName, "] id[", this.m_CurrentID, " has invalid value in line[", this.m_CurrentLine, "] and column[", this.m_CurrentColumn, "]" }));
                            }
                            value[i] = StringUtils.StringToInt(strArray[i]);
                        }
                    }
                    this._EndParseColumn();
                }
            }
        }

        private void _Set(ref float[] value)
        {
            if (this.m_CurrentColumn >= this.m_PreprocessData.m_ColumnCount)
            {
                ToolFunctions.LogError(string.Concat(new object[] { "table[", this.m_FileName, "] id[", this.m_CurrentID, "] line[", this.m_CurrentLine, "], column[", this.m_CurrentColumn, "], max column count[", this.m_PreprocessData.m_ColumnCount, "]" }));
                this._EndParseColumn();
            }
            else if (!this._CheckColumnType(EM_TYPE_COLUMN.FLOAT_ARRAY))
            {
                this._EndParseColumn();
            }
            else
            {
                string str = this._GetSubString(this.m_ReadBuffer, 0, this.m_ReadBuffer.Length, this.m_PreprocessData.m_Data[this.m_CurrentLine].m_ColumnField[this.m_CurrentColumn]);
                if (str == null)
                {
                    ToolFunctions.LogError(string.Concat(new object[] { "table[", this.m_FileName, "] id[", this.m_CurrentID, " has invalid value in line[", this.m_CurrentLine, "] and column[", this.m_CurrentColumn, "]" }));
                    this._EndParseColumn();
                }
                else
                {
                    if (("-1" == str) || string.IsNullOrEmpty(str))
                    {
                        value = new float[0];
                    }
                    else
                    {
                        string[] strArray = str.Split("|".ToCharArray());
                        value = new float[strArray.Length];
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            if (string.IsNullOrEmpty(strArray[i]))
                            {
                                ToolFunctions.LogError(string.Concat(new object[] { "table[", this.m_FileName, "] id[", this.m_CurrentID, " has invalid value in line[", this.m_CurrentLine, "] and column[", this.m_CurrentColumn, "]" }));
                            }
                            value[i] = StringUtils.StringToFloat(strArray[i]);
                        }
                    }
                    this._EndParseColumn();
                }
            }
        }
        private void _Set(ref string[] value)
        {
            if (this.m_CurrentColumn >= this.m_PreprocessData.m_ColumnCount)
            {
                ToolFunctions.LogError(string.Concat(new object[] { "table[", this.m_FileName, "] id[", this.m_CurrentID, "] line[", this.m_CurrentLine, "], column[", this.m_CurrentColumn, "], max column count[", this.m_PreprocessData.m_ColumnCount, "]" }));
                this._EndParseColumn();
            }
            else if (!this._CheckColumnType(EM_TYPE_COLUMN.STRING_ARRAY))
            {
                this._EndParseColumn();
            }
            else
            {
                string str = this._GetSubString(this.m_ReadBuffer, 0, this.m_ReadBuffer.Length, this.m_PreprocessData.m_Data[this.m_CurrentLine].m_ColumnField[this.m_CurrentColumn]);
                if (str == null)
                {
                    ToolFunctions.LogError(string.Concat(new object[] { "table[", this.m_FileName, "] id[", this.m_CurrentID, " has invalid value in line[", this.m_CurrentLine, "] and column[", this.m_CurrentColumn, "]" }));
                    this._EndParseColumn();
                }
                else
                {
                    if (("-1" == str) || string.IsNullOrEmpty(str))
                    {
                        value = new string[0];
                    }
                    else
                    {
                        string[] strArray = str.Split("|".ToCharArray());
                        value = new string[strArray.Length];
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            if (string.IsNullOrEmpty(strArray[i]))
                            {
                                ToolFunctions.LogError(string.Concat(new object[] { "table[", this.m_FileName, "] id[", this.m_CurrentID, " has invalid value in line[", this.m_CurrentLine, "] and column[", this.m_CurrentColumn, "]" }));
                            }
                            value[i] = strArray[i];
                        }
                    }
                    this._EndParseColumn();
                }
            }
        }

        public void Close()
        {
            if (this.m_File != null)
            {
                this.m_File.Close();
            }
        }

        public int GetColumnCount()
        {
            return this.m_PreprocessData.m_ColumnCount;
        }

        public string[] GetColumnNames()
        {
            return this.m_ColumnNames;
        }

        public int GetLineCount()
        {
            return this.m_PreprocessData.m_Data.Count;
        }

        public bool OpenRead(string fileName)
        {
            this.m_FileName = string.Copy(fileName);
            this.m_File = FileUtils.ReadTable(this.m_FileName, true);
            if (this.m_File == null)
            {
                return false;
            }
            this.m_File.Seek(0L, SeekOrigin.Begin);
            byte[] buffer = new byte[2];
            this.m_File.Read(buffer, 0, 2);
            if (((buffer[0] != 0xff) || (buffer[1] != 0xfe)) && ((buffer[0] != 0xfe) || (buffer[1] != 0xff)))
            {
                this.m_File.Seek(0L, SeekOrigin.Begin);
            }
            return true;
        }

        public override ISerializer Parse(ref int value)
        {
            this._Set(ref value);
            return this;
        }

        public override ISerializer Parse(ref long value)
        {
            this._Set(ref value);
            return this;
        }

        public override ISerializer Parse(ref float value)
        {
            this._Set(ref value);
            return this;
        }

        public override ISerializer Parse(ref string value)
        {
            this._Set(ref value);
            return this;
        }

        public override ISerializer Parse(ref uint value)
        {
            this._Set(ref value);
            return this;
        }

        public override ISerializer Parse(ref ulong value)
        {
            this._Set(ref value);
            return this;
        }

        public override ISerializer Parse(ref int[] value)
        {
            this._Set(ref value);
            return this;
        }

        public override ISerializer Parse(ref float[] value)
        {
            this._Set(ref value);
            return this;
        }
        public override ISerializer Parse(ref string[] value)
        {
            this._Set(ref value);
            return this;
        }

        public void PreprocessTable()
        {
            this.m_PreprocessData = new TablePreprocess();
            this._ReadFileToMemory();
            this._ReadLine();
            this._SaveColumnNames();
            this._SaveColumnTypes();
        }

        public override void SetCheckColumn(bool isCheck)
        {
            this.m_IsCheckColumn = isCheck;
        }

        public void SetCurrentColumn(int column)
        {
            this.m_CurrentColumn = column;
        }

        public override void SetCurrentID(int id)
        {
            this.m_CurrentID = id;
        }

        public void SetCurrentLine(int line)
        {
            this.m_CurrentLine = line;
            this.m_CurrentColumn = 0;
        }

        public override void SkipField()
        {
            this._EndParseColumn();
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ByteSection
        {
            public int m_SectionBeginPos;
            public int m_SectionLength;
            public ByteSection(int beginPos, int length)
            {
                this.m_SectionBeginPos = beginPos;
                this.m_SectionLength = length;
            }

            public int GetSectionEnd()
            {
                return (this.m_SectionBeginPos + this.m_SectionLength);
            }
        }

        public enum EM_TYPE_COLUMN
        {
            COUNT = 9,
            FLOAT = 4,
            FLOAT_ARRAY = 7,
            INT = 0,
            INT_ARRAY = 6,
            INT64 = 2,
            INVALID = -1,
            STRING = 5,
            UINT = 1,
            UINT64 = 3,
            STRING_ARRAY = 8
        }

        public class TableLine
        {
            public List<TableSerializer.ByteSection> m_ColumnField;
            public TableSerializer.ByteSection m_LineSection;

            public TableLine(TableSerializer.ByteSection section, List<TableSerializer.ByteSection> columnField)
            {
                this.m_LineSection = section;
                this.m_ColumnField = columnField;
            }
        }

        public class TablePreprocess
        {
            public int m_ColumnCount;
            public TableSerializer.TableLine m_ColumnName;
            public TableSerializer.TableLine m_ColumnType;
            public List<TableSerializer.TableLine> m_Data;
        }
    }
}

