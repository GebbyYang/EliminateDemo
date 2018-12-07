namespace Eliminate.Common
{
    using System;

    public abstract class ISerializer
    {
        protected ISerializer()
        {
        }

        public abstract ISerializer Parse(ref int value);
        public abstract ISerializer Parse(ref long value);
        public abstract ISerializer Parse(ref float value);
        public abstract ISerializer Parse(ref string value);
        public abstract ISerializer Parse(ref uint value);
        public abstract ISerializer Parse(ref ulong value);
        public abstract ISerializer Parse(ref int[] value);
        public abstract ISerializer Parse(ref float[] value);
        public abstract ISerializer Parse(ref string[] value);
        public abstract void SetCheckColumn(bool isCheck);
        public abstract void SetCurrentID(int id);
        public abstract void SkipField();
    }
}

