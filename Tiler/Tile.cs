using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tiler
{
    public class Tile : IComparable
    {
        public string SortValue;        // Sortable value field for scrambling
        public Bitmap Face;             // Tile image field
        
        public int CompareTo(object sortObject)
        {
            Tile tile = null;
            
            // Test and cast parameter to Tile
            if (sortObject != null)
                tile = sortObject as Tile;
            
            // Perform recursive sort or throw exception
            if (tile != null)
                return this.SortValue.CompareTo(tile.SortValue);
            else
                throw new ArgumentException("Object is not a Tile");
        }
    }


}