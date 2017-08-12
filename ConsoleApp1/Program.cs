
// Linear System Space State Model Numerical Intergration Using RK4 method
// Fully written by Kihoon, Lee

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            double[][] A = new double[2][];
            A[0] = new double[]{0, 1};
            A[1] = new double[]{-1,-1};

            double[] B = { 0, 1 };
            double u = 1;

            double[] x = { 0, 0 };
            double[] x0 = { 0, 0 };

            double dt = 0.1;
            double[] h = { 0, dt / 2, dt / 2, dt };
            double[] k = { dt / 6, dt / 3, dt / 3, dt / 6 };

            double[] xdot = { 0, 0 };
            double[] dx = { 0, 0 };

            double tf = 100;
            double t = 0;

            List<List<double>> data = new List<List<double>>();

            while (t < tf)
            {
                for (int i = 0; i < 4; i++)
                {
                    x = Add(x0, Multiply(h[i], xdot));
                    xdot = Add(Multiply(A, x), Multiply(B, u));
                    dx = Add(dx, Multiply(k[i], xdot));
                }

                x0 = Add(x0, dx);
                t += dt;

                data.Add(new[] { Math.Round(t,1), Math.Round(x0[0],4), Math.Round(x0[1],4) }.ToList());
                dx.SetValue(0, 0);
                dx.SetValue(0, 1);
            }

            Show(data);

            Console.ReadKey();
        }

        public static void Show(List<List<double>> data)
        {
            for (int i = 0; i < data.Count; i++)
            {
                Console.WriteLine(" ");

                for (int j = 0; j < data[i].Count; j++)
                    Console.Write(data[i][j] + " ");
            }
        }

        public static double[] Add(double[] x, double[] y)
        {
            double[] z = new double[x.Length];
            for (int i = 0; i < x.Length; i++)
                z[i] = x[i] + y[i];

            return z;
        }

        public static double[] Multiply(double r, double[] x)
        {
            double[] y = new double[x.Length];

            for (int i = 0; i < x.Length; i++)
                y[i] = r * x[i];

            return y;
        }

        public static double[] Multiply(double[] x, double r)
        {
            return Multiply(r, x);
        }

        public static double[] Multiply(double[][] A, double[] x)
        {
            double[] y = new double[x.Length];

            for (int i = 0; i < A.Length; i++)
                for (int j = 0; j < x.Length; j++)
                    y[i] += A[i][j] * x[j];

            return y;
        }
    }
}
