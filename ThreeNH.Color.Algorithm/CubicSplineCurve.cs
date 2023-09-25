using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeNH.Color.Algorithm
{
    public class CubicSplineCurve
    {
        /// <summary>
        /// 节点个数
        /// </summary>
        public int n { get; set; }

        /// <summary>
        /// 用于拟合曲线的X值
        /// </summary>
        public double[] X { get; set; }

        /// <summary>
        /// 用于拟合曲线的Y值
        /// </summary>
        public double[] Y { get; set; }

        /// <summary>
        /// 拟合得到的系数
        /// </summary>
        public double[] M { get; set; }

        public double CubicSplineCurve_GetItem(double x)
        {
            int i = 0;
            double y, hi, xi, xi_1, Ai, Bi;

            if (x < X[0])
            {
                i = 0;
            }
            else if (x > X[n - 1])
            {
                i = n - 2;
            }
            else
            {
                for (i = 0; i < n - 2; i++)
                {
                    if (x >= X[i] && x <= X[i + 1])
                        break;
                }
            }

            hi = X[i + 1] - X[i];
            xi_1 = X[i + 1] - x;
            xi = x - X[i];
            y = Math.Pow(xi_1, 3.0) * M[i] / (6 * hi);
            y += (Math.Pow(xi, 3.0) * M[i + 1] / (6 * hi));
            Ai = (Y[i + 1] - Y[i]) / hi - (M[i + 1] - M[i]) * hi / 6.0;
            y += Ai * x;
            Bi = Y[i + 1] - M[i + 1] * hi * hi / 6.0 - Ai * X[i + 1];
            y += Bi;
            return y;
        }
    }

    public class CubicSplineCurveHelper
    {
        public static CubicSplineCurve CubicSplineCurve_Simulate_f(int n, double[] x, double[] y, int boundType, float b1, float b2)
        {
            int i;
            double[] h = null;
            double[] u = null;
            double[] v = null;
            double[] d = null;
            double[] m = null;

            //if (!curve || n < 3 || !x || !y || (boundType != 1 && boundType != 2)) {
            if (n < 3 || x == null || y == null || (boundType != 1 && boundType != 2))
            {
                return null;
            }

            var curve = new CubicSplineCurve();

            h = new double[n];
            u = new double[n];
            v = new double[n];
            d = new double[n];

            for (i = 0; i < n - 1; i++)
            {
                h[i] = x[i + 1] - x[i];
            }
            for (i = 1; i < n - 1; i++)
            {
                u[i] = h[i - 1] / (h[i - 1] + h[i]);
                v[i] = 1.0f - u[i];
                d[i] = 6.0f * ((y[i + 1] - y[i]) / h[i] -
                    (y[i] - y[i - 1]) / h[i - 1]) / (h[i - 1] + h[i]);
            }
            if (boundType == 1)
            {  // 第一类边界条件
                v[0] = 1.0f;
                u[n - 1] = 1.0f;
                d[0] = 6.0f * ((y[1] - y[0]) / h[0] - b1) / h[0];
                d[n - 1] = 6.0f * (b2 - (y[n] - y[n - 1]) / h[n - 1]) / h[n - 1];
            }
            else
            { // 第二类边界条件
                v[0] = 0.0f;
                u[n - 1] = 0.0f;
                d[0] = 2.0f * b1;
                d[n - 1] = 2.0f * b2;
            }

            for (i = 0; i < n; i++)
            {
                h[i] = 2.0f;
            }
            m = Resolve(n, u, h, v, d);

            curve.n = n;
            curve.X = u;
            curve.Y = v;
            curve.M = m;

            for (i = 0; i < n; i++)
            {
                curve.X[i] = (double)x[i];
                curve.Y[i] = (double)y[i];
            }
            return curve;
        }

        private static double[] Resolve(int n, double[] b, double[] a, double[] c, double[] d)
        {
            int i;
            double[] l = new double[n];
            double[] u = new double[n];
            double[] y = new double[n];
            double[] x = new double[n];


            l[0] = a[0];
            u[0] = c[0] / l[0];
            for (i = 1; i < n; i++)
            {
                l[i] = a[i] - b[i] * u[i - 1];
                u[i] = c[i] / l[i];
            }

            y[0] = d[0] / l[0];
            for (i = 1; i < n; i++)
            {
                y[i] = (d[i] - b[i] * y[i - 1]) / l[i];
            }

            x = l;
            x[n - 1] = y[n - 1];
            for (i = n - 2; i >= 0; i--)
            {
                x[i] = y[i] - u[i] * x[i + 1];
            }
            return x;
        }
    }
}
