namespace Eliminate.Common
{
	using System.Collections;
	using System.Collections.Generic;
	using Eliminate.Table;

	public class TableDataBase : TSingleTon<TableDataBase>  {
		
		// Piece表
		public Table<TablePiece> PieceTable = new Table<TablePiece>();

		private TableDataBase()
		{

		}

		/// <summary>
		/// 加载数据表
		/// </summary>
		public void LoadBaseTables()
		{
			PieceTable.Load("Piece");
		}

	}
}

