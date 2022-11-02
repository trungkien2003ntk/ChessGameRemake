using System;
using System.Windows.Forms;
using System.Drawing;

namespace ChessGameRemake
{
    class MainProgram
    {
        private static MyForm f = new MyForm();
        static void Main(string[] args)
        {
            ChessBoard b = new ChessBoard(f);

            Application.Run(f);
        }


    }

    class MyForm : Form
    {
        public const int FORM_WIDTH = 800;
        public const int FORM_HEIGHT = 700;
        public Color DEFAULT_BACK_COLOR = ColorTranslator.FromHtml("#D3D3D3");

        public MyForm()
        {
            Width = FORM_WIDTH;
            Height = FORM_HEIGHT;
            BackColor = DEFAULT_BACK_COLOR;

            // disable window resize
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            StartPosition = FormStartPosition.CenterScreen;
        }
    }
}
