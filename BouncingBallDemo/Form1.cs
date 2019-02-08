using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BouncingBallDemo
{
    public partial class Form1 : Form
    {
        private int ball_Size = 8; //FRACTION OF BALL SIZE
        private int move_Size = 4; //FRACTION OF CLIENT AREA
        private Bitmap btmp;
        private int ball_PositionX;
        private int ball_PositionY;
        private int ball_RadiusX;
        private int ball_RadiusY;
        private int ball_MoveX;
        private int ball_MoveY;
        private int ball_BitmapWidth;
        private int ball_BitmapHeight;
        private int bitmap_WidthMargin;
        private int bitmap_HeightMargin;

        public Form1()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {


            //DECLARE A VARIABLE TO OBTAIN THE GRAPHICS OBJECT.
            Graphics grafx = CreateGraphics();
            //DRAW THE BALL IN THE FORM.
            grafx.DrawImage(btmp, (int)(ball_PositionX - ball_BitmapWidth / 2), (int)(ball_PositionY - ball_BitmapHeight / 2), ball_BitmapWidth, ball_BitmapHeight);

            grafx.Dispose();

            //INCREAMENT THE POSITION OF THE BALL BY ITS DISTANCE TO MOVED BOTH X AND Y AXIS.
            ball_PositionX += ball_MoveX;
            ball_PositionY += ball_MoveY;

            //REVERSE THE DIRECTION OF THE BALL WHEN IT HITS TO THE BOUNDARY.
            if (ball_PositionX + ball_RadiusX >= ClientSize.Width | ball_PositionX - ball_RadiusX <= 0)
            {
                ball_MoveX = System.Convert.ToInt32(-ball_MoveX);
            }
            //SET THE Y BOUNDARY TO 90 SO THAT IT WILL NOT EXCEED TO THE TITLE OF THE FORM.
            if (ball_PositionY + ball_RadiusY >= ClientSize.Height | ball_PositionY - ball_RadiusY <= 0)
            {
                ball_MoveY = System.Convert.ToInt32(-ball_MoveY);
            }

        }
        
        protected override void OnResize(EventArgs ev_arg)
        {


            Graphics grafx = CreateGraphics();
            //ERASE ANY DRAWINGS.
            grafx.Clear(BackColor);


            //DECLARE A VARIBLE THAT HOLDS THE RADIUS OF THE BALL
            //THEN SET THE WIDTH OR THE HIEGHT OF IT TO A FRACTION WHICHEVER IS LESS TO THE CLIENT AREA.
            double dbl_Radius = Math.Min(ClientSize.Width / grafx.DpiX, ClientSize.Height / grafx.DpiY) / ball_Size;


            //SET THE HIEGHT AND WIDTH OF THE BALL.
            ball_RadiusX = (int)(dbl_Radius * grafx.DpiX);
            ball_RadiusY = (int)(dbl_Radius * grafx.DpiY);

            grafx.Dispose();
            //SET THE DISTANCE THAT THE BALL MOVES INTO 1 PIXEL OR THE BALL SIZE WHICHEVER IS GREATER.
            //THIS MEANS THAT THE DISTANCE OF THE BALL MOVES EACH TIME IS PROPORTIONAL TO ITS SIZE,
            //WHICH IS ALSO PROPORTIONAL TO THE SIZE OF THE CLIENT AREA.
            //THE BALL SLOWS DOWN WHENEVER THE CLIENT AREA IS SHRUNK
            //AND THE BALL SPEEDS UP WHEN IT IS INCREASED.


            ball_MoveX = (int)(Math.Max(1, ball_RadiusX / move_Size));
            ball_MoveY = (int)(Math.Max(1, ball_RadiusY / move_Size));
            //THE VALUE OF THE BALL'S MOVEMENT SERVES AS THE MARGIN AROUND THE BALL,
            //THAT DETERMINES THE ACTUAL SIZE OF BITMAP ON WHICH THE BALL IS DRAWN.
            //THE DISTANCE OF THE BALL MOVES IS EQUAL TO THE SIZE OF THE BITMAP,
            //WHICH ALLOWS THE PREVIOUS BALL'S IMAGE TO BE ERASED BEFORE THE NEXT IMAGE IS DRAWN

            bitmap_WidthMargin = ball_MoveX;
            bitmap_HeightMargin = ball_MoveY;

            //TO FIND OUT THE ACTUAL SIZE OF THE BITMAP ON WHICH THE BALL IS DRAWN
            //PLUS THE MARGINS TO THE BALL'S DIMENSIONS.
            ball_BitmapWidth = 2 * (ball_RadiusX + bitmap_WidthMargin);
            ball_BitmapHeight = 2 * (ball_RadiusY + bitmap_HeightMargin);

            //CREATE A NEW WIDTH AND HEIGHT OF THE BITMAP.
            btmp = new Bitmap(ball_BitmapWidth, ball_BitmapHeight);
            //OBTAIN THE GRAFIX OBJECT SHOWN BY THE BITMAP.
            grafx = Graphics.FromImage(btmp);
            //CLEAR THE EXISTING BALL AND DRAW A NEW BALL.
            grafx.Clear(BackColor);
            grafx.FillEllipse(Brushes.Blue, new Rectangle(ball_MoveX, ball_MoveY, System.Convert.ToInt32(2 * ball_RadiusX), System.Convert.ToInt32(2 * ball_RadiusY)));
            grafx.Dispose();

            //RESET THE POSITION OF THE BALL TO THE CENTER OF THE CLIENT AREA.
            ball_PositionX = (int)(ClientSize.Width / 2);
            ball_PositionY = (int)(ClientSize.Height / 2);

        }
    }
}
