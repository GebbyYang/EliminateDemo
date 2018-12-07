namespace Eliminate.Common
{
	using System.Collections;
	using System.Collections.Generic;
	using Eliminate.Table;

	public class TableDataBase : TSingleTon<TableDataBase>  {
		
		public Table<TablePiece> PieceTable = new Table<TablePiece>();

		private TableDataBase()
		{

		}

		public void LoadBaseTables()
		{
			PieceTable.Load("Piece");
		}

	}
}

