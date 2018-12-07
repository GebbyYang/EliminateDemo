namespace Eliminate.Common
{
    using System;

    public abstract class TableInterface
    {
        private int m_Index;

        public TableInterface()
        {
            this.MapData(TableRow.g_ClearSerializer);
        }

        public int GetIndex()
        {
            return this.m_Index;
        }

        public abstract void MapData(ISerializer s);
        public int MapIndex(ISerializer s)
        {
            s.Parse(ref this.m_Index);
            s.SetCurrentID(this.m_Index);
            return this.m_Index;
        }
    }
}

