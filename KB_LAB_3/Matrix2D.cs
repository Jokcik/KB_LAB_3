using System;
using System.Drawing;
using System.Linq;

namespace KB_LAB_3
{
    public class Matrix2D
    {
        private float[][] _t;
        private float[][] _r;
        private float[][] _d;
        
        public Matrix2D()
        {
            Reset();
        }

        public void Rotate(float angle, Point point)
        {
            Rotate(angle, new PointF(point.X, point.Y));
        }
        public void Rotate(float angle, PointF point)
        {
            angle = (float) Math.PI/ 180 * angle;
            
            _r[0][0] = (float)Math.Cos(angle);
            _r[0][1] = (float)Math.Sin(angle);
            _r[1][0] = -(float)Math.Sin(angle);
            _r[1][1] = (float)Math.Cos(angle);

            _t[2][0] = point.X;
            _t[2][1] = point.Y;
        }

        public void resize(float sizeX, float sizeY)
        {
            _d[0][0] = sizeX;
            _d[1][1] = sizeY;
        }

        public void Reset()
        {
            _t = initMatr();
            _r = initMatr();
            _d = initMatr();
        }

        private float[][] initMatr()
        {
            return new []{
                new[] {1f, 0f, 0f},
                new[] {0f, 1f, 0f},
                new[] {0f, 0f, 1f}
            };
        }

        public void DrawPolygon(Graphics g, Point[] points)
        {
            DrawPolygon(g, points.Select(point => new PointF(point.X, point.Y)).ToArray());
        }
        
        public void DrawPolygon(Graphics g, PointF[] points, Pen pen = null)
        {
            if (pen == null)
            {
                pen = new Pen(Color.Black, 3);
            }
            var vector = GetVector(points);
            g.DrawPolygon(pen, vector);
        }
        
        public void FillPolygon(Graphics g, PointF[] points, Brush brush = null)
        {
            if (brush == null)
            {                
                brush = Brushes.White;
            }
            var vector = GetVector(points);
            g.FillPolygon(brush, vector);
        }

        public PointF[] GetVector(PointF[] points)
        {
            var vectors = points.Select(f => new[] {f.X, f.Y, 1}).ToArray();

            _t[2][0] = -_t[2][0];
            _t[2][1] = -_t[2][1];

            var m = MultipleMatrix3x3(_t, _r);
            m = MultipleMatrix3x3(m, _d);
            _t[2][0] = -_t[2][0];
            _t[2][1] = -_t[2][1];
            m = MultipleMatrix3x3(m, _t);

            var vector = vectors.Select(floats => MultipleMatrix1x3(new[] {floats}, m)[0]).ToArray();
            return vector.Select(floats => new PointF(floats[0], floats[1])).ToArray();
        }

        public float[][] MultipleMatrix1x3(float[][] matrL, float[][] matrR)
        {
            var a11 = matrL[0][0] * matrR[0][0] + matrL[0][1] * matrR[1][0] + matrL[0][2] * matrR[2][0];
            var a12 = matrL[0][0] * matrR[0][1] + matrL[0][1] * matrR[1][1] + matrL[0][2] * matrR[2][1];
            var a13 = matrL[0][0] * matrR[0][2] + matrL[0][1] * matrR[1][2] + matrL[0][2] * matrR[2][2];

            return new[] {new[] {a11, a12, a13}};
        }
        
        public float[][] MultipleMatrix3x3(float[][] matrL, float[][] matrR)
        {
            var a11 = matrL[0][0] * matrR[0][0] + matrL[0][1] * matrR[1][0] + matrL[0][2] * matrR[2][0];
            var a12 = matrL[0][0] * matrR[0][1] + matrL[0][1] * matrR[1][1] + matrL[0][2] * matrR[2][1];
            var a13 = matrL[0][0] * matrR[0][2] + matrL[0][1] * matrR[1][2] + matrL[0][2] * matrR[2][2];
            
            var a21 = matrL[1][0] * matrR[0][0] + matrL[1][1] * matrR[1][0] + matrL[1][2] * matrR[2][0];
            var a22 = matrL[1][0] * matrR[0][1] + matrL[1][1] * matrR[1][1] + matrL[1][2] * matrR[2][1];
            var a23 = matrL[1][0] * matrR[0][2] + matrL[1][1] * matrR[1][2] + matrL[1][2] * matrR[2][2];
            
            var a31 = matrL[2][0] * matrR[0][0] + matrL[2][1] * matrR[1][0] + matrL[2][2] * matrR[2][0];
            var a32 = matrL[2][0] * matrR[0][1] + matrL[2][1] * matrR[1][1] + matrL[2][2] * matrR[2][1];
            var a33 = matrL[2][0] * matrR[0][2] + matrL[2][1] * matrR[1][2] + matrL[2][2] * matrR[2][2];
            
            return new []
            {
                new [] {a11, a12, a13},
                new [] {a21, a22, a23},
                new [] {a31, a32, a33}
            };
        }
    }
}