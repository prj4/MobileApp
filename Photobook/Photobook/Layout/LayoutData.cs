using Xamarin.Forms;

namespace Photobook.Layout
{
    internal struct LayoutData
    {
        public int VisibleChildCount { get; }

        public Size CellSize { get; }

        public int Rows { get; }

        public int Columns { get; }

        public LayoutData(int visibleChildCount, Size cellSize, int rows, int columns)
        {
            VisibleChildCount = visibleChildCount;
            CellSize = cellSize;
            Rows = rows;
            Columns = columns;
        }
    }
}