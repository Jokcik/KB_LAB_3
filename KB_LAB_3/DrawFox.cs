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

        public void Draw()
        {
            var c = new PointF(center.X, center.Y - radius * 1.2f);

            var t11 = new PointF(c.X - radius * 0.2f, c.Y);
            var t12 = new PointF(c.X + radius * 0.2f, c.Y);
            var t13 = new PointF(c.X - radius * 0.2f, c.Y + radius * 0.1f);

            var t21 = new PointF(c.X - radius * 0.2f, c.Y + radius * 0.16f);
            var t22 = new PointF(c.X - radius * 0.18f, c.Y + radius * 0.14f);

            var t31 = new PointF(t12.X + radius * 0.01f, c.Y + radius * 0.13f);
            var t32 = new PointF(t12.X + radius * 0.05f, c.Y + radius * 0.2f);
            
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
            d.FillPolygon(g, new[] {t13, t21, t22}, Brushes.CornflowerBlue);
            d.FillPolygon(g, new[] {t12, t31, t32}, Brushes.CornflowerBlue);
            d.FillPolygon(g, new[] {th1, th2, th3}, Brushes.CornflowerBlue);
            d.FillPolygon(g, pointFs1, Brushes.CornflowerBlue);
            d.FillPolygon(g, pointFs2, Brushes.CornflowerBlue);
        }
    }
}