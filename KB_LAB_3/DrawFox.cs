using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace KB_LAB_3
{
    public class DrawFox
    {
        private Graphics g;
        private Matrix2D d;
        private Point center;
        private int radius;

        public DrawFox(Graphics g, Matrix2D d, Point center, int radius)
        {
            this.g = g;
            this.d = d;
            this.center = center;
            this.radius = radius;
        }

        public void Draw(float angle)
        {
            var c = new PointF(center.X, center.Y - radius * 1.2f);

            var t11 = new PointF(c.X - radius * 0.2f, c.Y);
            var t12 = new PointF(c.X + radius * 0.2f, c.Y);
            var t13 = new PointF(c.X - radius * 0.2f, c.Y + radius * 0.1f);
            
            var footR = new Matrix2D();
            var t23 = new PointF(t13.X, t13.Y);
            var t21 = new PointF(c.X - radius * 0.2f, c.Y + radius * 0.16f);
            var t22 = new PointF(c.X - radius * 0.18f, c.Y + radius * 0.14f);

            var del = (int)Math.Abs(angle / 50) % 2 == 1 ? 1 : -1 ;
            footR.Rotate(del == 1 ? -50 + (Math.Abs(angle) % 50) : -Math.Abs(angle) % 50, t23);
            var foot1Points = footR.GetVector(new[] {t23, t21, t22});
            
            var footL = new Matrix2D();
            var t31 = new PointF(t12.X + radius * 0.01f, c.Y + radius * 0.13f);
            var t32 = new PointF(t12.X + radius * 0.05f, c.Y + radius * 0.2f);
            var t33 = new PointF(t12.X + radius * 0.025f, c.Y + radius * 0.025f);
            
            footL.Rotate(del == 1 ? -50 + (Math.Abs(angle) % 50) : -Math.Abs(angle) % 50, t33);
            var foot2Points = footL.GetVector(new[] {t31, t32, t33});
            
            
            var th1 = new PointF(t11.X + radius * 0.05f, c.Y - radius * 0.02f);
            var th2 = new PointF(t11.X - radius * 0.15f, c.Y + radius * 0.1f);
            var th3 = new PointF(t11.X - radius * 0.17f, c.Y - radius * 0.1f);

            var ear = new Matrix2D();
            var r11 = new PointF(t11.X - radius * 0.03f, c.Y - radius * 0.05f);
            var r12 = new PointF(r11.X + radius * 0.06f, r11.Y);
            var r13 = new PointF(r11.X + radius * 0.06f, r11.Y - radius * 0.06f);
            var r14 = new PointF(r11.X, r11.Y - radius * 0.06f);
            ear.Rotate(60, new PointF((r12.X + r11.X) / 2f, (r11.Y + r13.Y) / 2f));
            var pointFs1 = ear.GetVector(new[] {r11, r12, r13, r14});
                
            var r21 = new PointF(t11.X - radius * 0.15f, c.Y - radius * 0.095f);
            var r22 = new PointF(r21.X + radius * 0.06f, r21.Y);
            var r23 = new PointF(r21.X + radius * 0.06f, r21.Y - radius * 0.06f);
            var r24 = new PointF(r21.X, r21.Y - radius * 0.06f);
            ear.Rotate(60, new PointF((r22.X + r21.X) / 2f, (r21.Y + r23.Y) / 2f));
            var pointFs2 = ear.GetVector(new[] {r21, r22, r23, r24});
            
            d.FillPolygon(g, new[]{ t11,t12, t13}, Brushes.CornflowerBlue);
            d.DrawPolygon(g, new[]{ t11,t12, t13}, new Pen(Color.DodgerBlue, 3));
            d.FillPolygon(g, foot1Points, Brushes.CornflowerBlue);
            d.DrawPolygon(g, foot1Points, new Pen(Color.DodgerBlue, 3));
            d.FillPolygon(g, foot2Points, Brushes.CornflowerBlue);
            d.DrawPolygon(g, foot2Points, new Pen(Color.DodgerBlue, 3));
            d.FillPolygon(g, new[] {th1, th2, th3}, Brushes.CornflowerBlue);
            d.DrawPolygon(g, new[] {th1, th2, th3}, new Pen(Color.DodgerBlue, 3));
            d.FillPolygon(g, pointFs1, Brushes.CornflowerBlue);
            d.DrawPolygon(g, pointFs1, new Pen(Color.DodgerBlue, 3));
            d.FillPolygon(g, pointFs2, Brushes.CornflowerBlue);
            d.DrawPolygon(g, pointFs2, new Pen(Color.DodgerBlue, 3));
        }
    }
}