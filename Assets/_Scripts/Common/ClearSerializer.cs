namespace Eliminate.Common
{
    using System;

    public class ClearSerializer : ISerializer
    {
        public override ISerializer Parse(ref int value)
        {
            value = -1;
            return this;
        }

        public override ISerializer Parse(ref long value)
        {
            value = -1L;
            return this;
        }

        public override ISerializer Parse(ref float value)
        {
            value = 0f;
            return this;
        }

        public override ISerializer Parse(ref string value)
        {
            value = null;
            return this;
        }

        public override ISerializer Parse(ref uint value)
        {
            value = uint.MaxValue;
            return this;
        }

        public override ISerializer Parse(ref ulong value)
        {
            value = ulong.MaxValue;
            return this;
        }

        public override ISerializer Parse(ref int[] value)
        {
            return this;
        }

        public override ISerializer Parse(ref float[] value)
        {
            return this;
        }
        public override ISerializer Parse(ref string[] value)
        {
            return this;
        }

        public override void SetCheckColumn(bool isCheck)
        {
        }

        public override void SetCurrentID(int id)
        {
        }

        public override void SkipField()
        {
        }
    }
}

