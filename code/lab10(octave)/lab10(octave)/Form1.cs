using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace lab10_octave_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private float y_value(float x_value)
        {
            if (x_value == 1)
            {
                throw new Exception("Деление на ноль!");
            }
            return (float) Math.Log(((x_value + 1) / (x_value - 1)));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ErrorLabel.Text = "";
            try
            {
                Graphics g = this.CreateGraphics();
                Pen myPen = new Pen(Color.Black);
                
                float xmin = (float)niStart.Value, xmax = (float)niFinish.Value;
                float xstep = (float)niStep.Value;
                if (xmin >= xmax)
                {
                    throw new Exception("Начало обычно идет раньше конца");
                }
                int nmbInterv = (int)((xmax - xmin) / xstep);
                if (nmbInterv > 10000)
                {
                    throw new Exception("Слишком много точек");
                } else if (nmbInterv < 2) {
                    throw new Exception("Слишком мало точек");
                }
                if (xstep <= 0)
                {
                    throw new Exception("Шаг должен быть больше нуля");
                }

                float kx = (xmin < 0)? this.Width / (xmax + xmin): this.Width / (xmax - xmin);
                float x = xmin, y = y_value(x);

                PointF[] pts = new PointF[nmbInterv+1];

                for (int i = 0; i <= nmbInterv; i++)
                {
                    pts[i] = new PointF(x, y);
                    x += xstep;
                    y = y_value(x);
                }

                float yMax = pts.Select(p => p.Y).Max();
                float yMin = pts.Select(p => p.Y).Min();

                if (yMin < 0)
                {
                    float ky = this.Height / (yMax + yMin);

                    if (xmin < 0)
                    {
                        for (int i = 0; i <= nmbInterv; i++)
                        {
                            pts[i] = new PointF((pts[i].X+xmin) * kx, (pts[i].Y + yMin) * ky);
                        }
                    }
                    else
                    {
                        for (int i = 0; i <= nmbInterv; i++)
                        {
                            pts[i] = new PointF((pts[i].X - xmin) * kx, (pts[i].Y + yMin) * ky);
                        }
                    }
                }
                else
                {
                    float ky = this.Height / (yMax - yMin);

                    if (xmin < 0)
                    {
                        for (int i = 0; i <= nmbInterv; i++)
                        {
                            pts[i] = new PointF((pts[i].X + xmin) * kx, (pts[i].Y - yMin) * ky);
                        }
                    }
                    else
                    {
                        for (int i = 0; i <= nmbInterv; i++)
                        {
                            pts[i] = new PointF((pts[i].X - xmin) * kx, (pts[i].Y - yMin) * ky);
                        }
                    }
                }

                for (int i = 0; i < nmbInterv-1; i++)
                {
                    g.DrawLine(myPen, pts[i], pts[i+1]);
                }

            }
            catch (Exception error)
            {
                ErrorLabel.Text = error.Message;
            }
        }
    }
}
