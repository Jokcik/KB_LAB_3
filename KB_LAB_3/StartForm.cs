using System;
using System.Drawing;
using System.Windows.Forms;

namespace KB_LAB_3
{
    public class StartForm : Form
    {
        public float angle = 0;
        
        private void InitializeComponent()
        {
            // Включаем двойную буферизацию
            SetStyle(ControlStyles.DoubleBuffer |
                     ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.ResizeRedraw, // Перерисовывать при изменении размера окна
                true);
            UpdateStyles();

            Timer timer = new Timer {Interval = 2};
            timer.Tick += (sender, args) =>
            {
                angle -= 0.1f;
                Invalidate();       
            };
            timer.Start();
        }

        public StartForm()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;

            var bounds = g.VisibleClipBounds;
            g.FillRectangle(Brushes.White, bounds);

            // Радиус большей окружности
            int radius;
            if (bounds.Width > bounds.Height)
                radius = (int) bounds.Height / 5; // 
            else radius = (int) bounds.Width / 5;

            // Если размеры окна маленькие, ничего не выводить
            if (bounds.Width < 30 || bounds.Height < 30)
                return;

            LeftPicture(bounds, radius, g);
            g.DrawLine(Pens.Gray, new PointF(bounds.Width / 2, bounds.Top),  new PointF(bounds.Width / 2, bounds.Bottom));
            RightPicture(bounds, radius, g);


            base.OnPaint(e);
        }

        private void RightPicture(RectangleF bounds, int radius, Graphics g)
        {
            // Координаты центра окружности
            var center = new Point((int) (3 * bounds.Width) / 4, (int) bounds.Height / 2);

            // Рисуем круг
            var rect = new Rectangle(center.X - radius, center.Y - radius, radius * 2, radius * 2);
            g.DrawEllipse(Pens.Black, rect);
            g.DrawPie(Pens.Black, rect, -angle + -120, 60);
            g.DrawPie(Pens.Black, rect, -angle + 120, -60);

            var smallRadius = radius / 3;
            var rectSmall = new RectangleF(center.X - smallRadius, center.Y - smallRadius, smallRadius * 2,
                smallRadius * 2);
            g.FillEllipse(Brushes.White, rectSmall);
            g.DrawEllipse(new Pen(Brushes.Black, 3), rectSmall);

            var d = new Matrix2D();
            d.Rotate(angle, center);
            d.ReflectionX();
            createRect(g, center, radius, d);
        }

        private void LeftPicture(RectangleF bounds, int radius, Graphics g)
        {
            // Координаты центра окружности
            var center = new Point((int) bounds.Width / 4, (int) bounds.Height / 2);

            // Рисуем круг
            var rect = new Rectangle(center.X - radius, center.Y - radius, radius * 2, radius * 2);
            g.DrawEllipse(Pens.Black, rect);
            g.DrawPie(Pens.Black, rect, angle + -120, 60);
            g.DrawPie(Pens.Black, rect, angle + 120, -60);

            var smallRadius = radius / 3;
            var rectSmall = new RectangleF(center.X - smallRadius, center.Y - smallRadius, smallRadius * 2,
                smallRadius * 2);
            g.FillEllipse(Brushes.White, rectSmall);
            g.DrawEllipse(new Pen(Brushes.Black, 3), rectSmall);

            var d = new Matrix2D();
            d.Rotate(angle, center);
            createRect(g, center, radius, d);
        }

        private void createRect(Graphics g, Point center, int radius, Matrix2D d)
        {
            var centerRect = new Point(center.X, center.Y - radius);
            var wL = (float)Math.PI * radius / 180 * 120; 
            var w = (2 * (float)Math.PI * radius - wL) / (2 * 50);

            var p1 = new PointF(centerRect.X - w, centerRect.Y);
            var p2 = new PointF(centerRect.X + w, centerRect.Y);
            var p3 = new PointF(centerRect.X + w, centerRect.Y - 2 * w);
            var p4 = new PointF(centerRect.X - w, centerRect.Y - 2 * w);
            
            var t11 = new PointF(p3.X + w / 1.5f, p3.Y);
            var t12 = new PointF(p4.X - w / 1.5f, p4.Y);
            var t13 = new PointF(centerRect.X, p4.Y - w * 3);
            
            var t21 = new PointF(t11.X, centerRect.Y - w * 4);
            var t22 = new PointF(t12.X, centerRect.Y - w * 4);
            var t23 = new PointF(t13.X, t13.Y - w * 2);

            var fox = new DrawFox(g, d, center, radius);
            fox.Draw(angle);
            
            for (var i = 0; i < 5; ++i)
            {
                d.Rotate(angle + -45 - 22 * i, center);
                d.DrawPolygon(g, new []{p1, p2, p3, p4});
                if (i % 2 == 0)
                {
                    d.DrawPolygon(g, new[] {t21, t22, t23});
                }
                d.FillPolygon(g, new []{t11, t12, t13});
                d.DrawPolygon(g, new []{t11, t12, t13});
            }
            
            for (var i = 0; i < 5; ++i)
            {
                d.Rotate(angle + 45 + 22 * i, center);
                d.DrawPolygon(g, new []{p1, p2, p3, p4});
                if (i % 2 == 0)
                {
                    d.DrawPolygon(g, new[] {t21, t22, t23});
                }
                d.FillPolygon(g, new []{t11, t12, t13});
                d.DrawPolygon(g, new []{t11, t12, t13});
            }


        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            angle += e.Delta > 0 ? 1 : -1;
            Invalidate();
            base.OnMouseWheel(e);
        }
    }
}