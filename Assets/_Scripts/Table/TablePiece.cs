namespace Eliminate.Table
{
	using System.Collections;
	using System.Collections.Generic;
	using Eliminate.Common;

	public class TablePiece : TableInterface {
		
		public int m_ID;
		public string m_PrefabName;
		public string m_Name;
		public int m_Layer;
		public int m_PreLoad;

		public override void MapData(ISerializer s)
		{
			this.m_ID = base.MapIndex(s);
			s.Parse(ref m_PrefabName);
			s.Parse(ref m_Name);
			s.Parse(ref m_Layer);
			s.Parse(ref m_PreLoad);
		}

	}
}

