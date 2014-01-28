using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tiler
{
    public class Tiles
    {
        private int _numberOfTiles;
        private Tile[] _tilesScrambled;
        private Tile[] _tilesSorted;

        private int _numberOfRows;
        private int _numberOfColumns;
        private Bitmap _image;
        public Bitmap Image
        {
            get { return _image; }
        }

        // Constructorpopop
        public Tiles(int numberOfRows, int numberOfColumns, Bitmap image)
        {
            // Set class variables
            _numberOfRows = numberOfRows;
            _numberOfColumns = numberOfColumns;
            _numberOfTiles = numberOfRows * numberOfColumns;
            _image = image;

            // Create tile arrays
            _tilesScrambled = new Tile[_numberOfTiles];
            _tilesSorted = new Tile[_numberOfTiles];

            // Reset the tiles
            reset();
        }

        // Indexer
        public Tile this[int index]
        {
            get
            {
                return _tilesScrambled[index];
            }
            set
            {
                _tilesScrambled[index] = value;
            }
        }

        public void reset()
        {
            // Clear out all old tiles
            Array.Clear(_tilesScrambled, 0, _tilesScrambled.Length);
            Array.Clear(_tilesSorted, 0, _tilesSorted.Length);

            // Calculate image chunks
            int heightChunk = _image.Height / _numberOfRows;
            int widthChunk = _image.Width / _numberOfColumns;

            // Generate new tiles in sorted order, adding a GUID for later scrambling
            int imageCount = 0;
            for (int row = 0; row < _numberOfRows; row++)
            {
                for (int col = 0; col < _numberOfColumns; col++)
                {
                    // Determine image chunk dimensions
                    Rectangle chunkRectangle = new Rectangle(widthChunk * col, heightChunk * row, widthChunk, heightChunk);

                    // Create new Tile
                    _tilesSorted[imageCount++] = new Tile()
                    {
                        Face = _image.Clone(chunkRectangle, _image.PixelFormat),
                        SortValue = Guid.NewGuid().ToString()
                    };
                }
            }

            // Clone our sorted array
            _tilesScrambled = (Tile[])_tilesSorted.Clone();

            // Sort to unscramble
            Array.Sort(_tilesScrambled);
        }

        public void swap(int firstIndex, int secondIndex)
        {
            if (firstIndex != secondIndex && firstIndex >= 0
                    && firstIndex < _tilesScrambled.Length
                    && secondIndex >= 0 && secondIndex < _tilesScrambled.Length)
            {
                Tile holdTile = _tilesScrambled[secondIndex];
                _tilesScrambled[secondIndex] = _tilesScrambled[firstIndex];
                _tilesScrambled[firstIndex] = holdTile;
            }
        }

        public bool isSolved()
        {
            bool result = true;

            // Compare sorted and scrambled, and if they match, solved
            for (int i = 0; i < _numberOfColumns; i++)
            {
                if (_tilesScrambled[i].SortValue != _tilesSorted[i].SortValue)
                {
                    result = false;
                    break;
                }
            }

            return result;
        }
    }
}