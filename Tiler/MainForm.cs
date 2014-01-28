using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tiler
{
    public partial class MainForm : Form
    {

        private Bitmap _image = null;       // Game image

        private Tiles _myTiles = null;              // Tiles object
        private PictureBox[,] _imageGrid = null;    // Picturebox two-dimensional array
        private int _numberOfRows = 4;              // Number of rows in the game
        private int _numberOfColumns = 4;           // Number of columns in the game

        private PictureBox _firstPictureBox = null; // First selected PictureBox

        public MainForm()
        {

            // Load the image
            _image = new Bitmap("WaterLilies-400x400.jpg");


            // Create tiles
            _myTiles = new Tiles(_numberOfRows, _numberOfColumns, _image);

            // Create image grid
            createImageGrid();

            InitializeComponent();


        }


        private void createImageGrid()
        {
            // Allocate a two-dimensional array (grid) for our grid
            _imageGrid = new PictureBox[_numberOfRows, _numberOfColumns];

            // Create each PictureBox control in our array
            int tileCount = 0;
            for (int row = 0; row < _numberOfRows; row++)
            {
                for (int col = 0; col < _numberOfColumns; col++)
                {
                    // Create and assign new PictureBox
                    PictureBox pictureBox = new PictureBox()
                    {
                        Image = _myTiles[tileCount].Face,
                        BackColor = Color.Black,
                        BorderStyle = BorderStyle.FixedSingle,
                        Cursor = Cursors.Hand,
                        Tag = tileCount
                    };
                    _imageGrid[row, col] = pictureBox;

                    // Increment tile counter
                    tileCount++;

                    // Add single event handler for all PictureBox MouseUp events
                    _imageGrid[row, col].MouseUp += new MouseEventHandler(imageMouseUpHandler);
                }
            }
        }

        private void restartGame(int rows, int cols)
        {
            _myTiles.reset();



            // Get the height and width for each PictureBox using the inside height and width of the Panel control
            int pictureBoxWidth = mainPanel.ClientRectangle.Width / cols;
            int pictureBoxHeight = mainPanel.ClientRectangle.Height / rows;

            // Loop through and set the location and dimensions of each PictureBox control
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                    _imageGrid[row, col].SetBounds(pictureBoxWidth * col, pictureBoxHeight * row, pictureBoxWidth, pictureBoxHeight);
            }

            // Add the Picture controls to the Panel Controls collection
            if (mainPanel.Controls.Count == 0)
            {
                for (int row = 0; row < rows; row++)
                {
                    for (int col = 0; col < cols; col++)
                        mainPanel.Controls.Add(_imageGrid[row, col]);
                }
            }


            // Display tiles
            int tileCount = 0;
            for (int row = 0; rows < row; row++)
            {
                for (int col = 0; cols < col; col++)
                {
                    _imageGrid[row, col].Image = _myTiles[tileCount++].Face;
                }
            }




        }


        void imageMouseUpHandler(object sender, MouseEventArgs e)
        {
            // Get the PictureBox control we're responding to
            PictureBox pictureBox = sender as PictureBox;

            if (_firstPictureBox == null)
            {
                // First pick
                _firstPictureBox = pictureBox;

                // Draw border to indicate selected tile
                pictureBox.CreateGraphics().DrawRectangle(
                    new Pen(Color.Red, 3.0F), 0, 0, pictureBox.ClientRectangle.Width - 1, pictureBox.ClientRectangle.Height - 1);
            }
            else
            {
                // Get array indices
                int firstIndex = (int)_firstPictureBox.Tag;
                int currentIndex = (int)pictureBox.Tag;

                // Swap positions, reset images
                _myTiles.swap(firstIndex, currentIndex);
                _firstPictureBox.Image = _myTiles[firstIndex].Face;
                pictureBox.Image = _myTiles[currentIndex].Face;
                _firstPictureBox = null;
                if (_myTiles.isSolved())
                    MessageBox.Show("Solved!", this.Text);
            }
        }

        private void resetToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // Reset tiles
            _myTiles.reset();

            // Display tiles
            int tileCount = 0;
            for (int row = 0; row < _numberOfRows; row++)
            {
                for (int col = 0; col < _numberOfColumns; col++)
                {
                    _imageGrid[row, col].Image = _myTiles[tileCount++].Face;
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Exit the application
            this.Close();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            // Set the Form dimensions
            int formHeight = this.Height;
            int formWidth = this.Width;

            // Get the Panel inner dimensions
            int panelHeight = mainPanel.ClientRectangle.Height;
            int panelWidth = mainPanel.ClientRectangle.Width;

            // Get the image dimensions
            int imageHeight = _image.Height;
            int imageWidth = _image.Width;

            // Adjust Form dimensions to allow space for entire image
            this.Height = formHeight - panelHeight + imageHeight;
            this.Width = formWidth - panelWidth + imageWidth;

            // Get the height and width for each PictureBox using the inside height and width of the Panel control
            int pictureBoxWidth = mainPanel.ClientRectangle.Width / _numberOfColumns;
            int pictureBoxHeight = mainPanel.ClientRectangle.Height / _numberOfRows;

            // Loop through and set the location and dimensions of each PictureBox control
            for (int row = 0; row < _numberOfRows; row++)
            {
                for (int col = 0; col < _numberOfColumns; col++)
                    _imageGrid[row, col].SetBounds(pictureBoxWidth * col, pictureBoxHeight * row, pictureBoxWidth, pictureBoxHeight);
            }

            // Add the Picture controls to the Panel Controls collection
            if (mainPanel.Controls.Count == 0)
            {
                for (int row = 0; row < _numberOfRows; row++)
                {
                    for (int col = 0; col < _numberOfColumns; col++)
                        mainPanel.Controls.Add(_imageGrid[row, col]);
                }
            }
        }

        private void x2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            restartGame(2, 2);

        }

        private void x3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            restartGame(3, 3);

        }

        private void x4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            restartGame(4, 4);
        }

        private void liliesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // Reset tiles
            _myTiles.reset();

            _image = new Bitmap("WaterLilies-400x400.jpg");
            // Create tiles
            _myTiles = new Tiles(_numberOfRows, _numberOfColumns, _image);
            

            // Display tiles
            int tileCount = 0;
            for (int row = 0; row < _numberOfRows; row++)
            {
                for (int col = 0; col < _numberOfColumns; col++)
                {
                    _imageGrid[row, col].Image = _myTiles[tileCount++].Face;
                }
            }
        }

        private void sunsetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Reset tiles
            _myTiles.reset();

            _image = new Bitmap("Sunset-400x400.jpg");
            // Create tiles
            _myTiles = new Tiles(_numberOfRows, _numberOfColumns, _image);


            // Display tiles
            int tileCount = 0;
            for (int row = 0; row < _numberOfRows; row++)
            {
                for (int col = 0; col < _numberOfColumns; col++)
                {
                    _imageGrid[row, col].Image = _myTiles[tileCount++].Face;
                }
            }
        }

        private void winterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Reset tiles
            _myTiles.reset();

            _image = new Bitmap("Winter-400x400.jpg");
            // Create tiles
            _myTiles = new Tiles(_numberOfRows, _numberOfColumns, _image);


            // Display tiles
            int tileCount = 0;
            for (int row = 0; row < _numberOfRows; row++)
            {
                for (int col = 0; col < _numberOfColumns; col++)
                {
                    _imageGrid[row, col].Image = _myTiles[tileCount++].Face;
                }
            }
        }

    }
}
