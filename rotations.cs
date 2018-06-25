using System;

internal class rotations
{
  public static void applyrotationsfromtheleft(bool isforward, int m1, int m2, int n1, int n2, ref double[] c, ref double[] s, ref double[,] a, ref double[] work)
  {
    if (m1 > m2 | n1 > n2)
      return;
    if (isforward)
    {
      if (n1 != n2)
      {
        for (int index1 = m1; index1 <= m2 - 1; ++index1)
        {
          double num1 = c[index1 - m1 + 1];
          double num2 = s[index1 - m1 + 1];
          if (num1 != 1.0 | num2 != 0.0)
          {
            int index2 = index1 + 1;
            for (int index3 = n1; index3 <= n2; ++index3)
              work[index3] = num1 * a[index2, index3];
            for (int index3 = n1; index3 <= n2; ++index3)
              work[index3] = work[index3] - num2 * a[index1, index3];
            for (int index3 = n1; index3 <= n2; ++index3)
              a[index1, index3] = num1 * a[index1, index3];
            for (int index3 = n1; index3 <= n2; ++index3)
              a[index1, index3] = a[index1, index3] + num2 * a[index2, index3];
            for (int index3 = n1; index3 <= n2; ++index3)
              a[index2, index3] = work[index3];
          }
        }
      }
      else
      {
        for (int index = m1; index <= m2 - 1; ++index)
        {
          double num1 = c[index - m1 + 1];
          double num2 = s[index - m1 + 1];
          if (num1 != 1.0 | num2 != 0.0)
          {
            double num3 = a[index + 1, n1];
            a[index + 1, n1] = num1 * num3 - num2 * a[index, n1];
            a[index, n1] = num2 * num3 + num1 * a[index, n1];
          }
        }
      }
    }
    else if (n1 != n2)
    {
      for (int index1 = m2 - 1; index1 >= m1; --index1)
      {
        double num1 = c[index1 - m1 + 1];
        double num2 = s[index1 - m1 + 1];
        if (num1 != 1.0 | num2 != 0.0)
        {
          int index2 = index1 + 1;
          for (int index3 = n1; index3 <= n2; ++index3)
            work[index3] = num1 * a[index2, index3];
          for (int index3 = n1; index3 <= n2; ++index3)
            work[index3] = work[index3] - num2 * a[index1, index3];
          for (int index3 = n1; index3 <= n2; ++index3)
            a[index1, index3] = num1 * a[index1, index3];
          for (int index3 = n1; index3 <= n2; ++index3)
            a[index1, index3] = a[index1, index3] + num2 * a[index2, index3];
          for (int index3 = n1; index3 <= n2; ++index3)
            a[index2, index3] = work[index3];
        }
      }
    }
    else
    {
      for (int index = m2 - 1; index >= m1; --index)
      {
        double num1 = c[index - m1 + 1];
        double num2 = s[index - m1 + 1];
        if (num1 != 1.0 | num2 != 0.0)
        {
          double num3 = a[index + 1, n1];
          a[index + 1, n1] = num1 * num3 - num2 * a[index, n1];
          a[index, n1] = num2 * num3 + num1 * a[index, n1];
        }
      }
    }
  }

  public static void applyrotationsfromtheright(bool isforward, int m1, int m2, int n1, int n2, ref double[] c, ref double[] s, ref double[,] a, ref double[] work)
  {
    if (isforward)
    {
      if (m1 != m2)
      {
        for (int index1 = n1; index1 <= n2 - 1; ++index1)
        {
          double num1 = c[index1 - n1 + 1];
          double num2 = s[index1 - n1 + 1];
          if (num1 != 1.0 | num2 != 0.0)
          {
            int index2 = index1 + 1;
            for (int index3 = m1; index3 <= m2; ++index3)
              work[index3] = num1 * a[index3, index2];
            for (int index3 = m1; index3 <= m2; ++index3)
              work[index3] = work[index3] - num2 * a[index3, index1];
            for (int index3 = m1; index3 <= m2; ++index3)
              a[index3, index1] = num1 * a[index3, index1];
            for (int index3 = m1; index3 <= m2; ++index3)
              a[index3, index1] = a[index3, index1] + num2 * a[index3, index2];
            for (int index3 = m1; index3 <= m2; ++index3)
              a[index3, index2] = work[index3];
          }
        }
      }
      else
      {
        for (int index = n1; index <= n2 - 1; ++index)
        {
          double num1 = c[index - n1 + 1];
          double num2 = s[index - n1 + 1];
          if (num1 != 1.0 | num2 != 0.0)
          {
            double num3 = a[m1, index + 1];
            a[m1, index + 1] = num1 * num3 - num2 * a[m1, index];
            a[m1, index] = num2 * num3 + num1 * a[m1, index];
          }
        }
      }
    }
    else if (m1 != m2)
    {
      for (int index1 = n2 - 1; index1 >= n1; --index1)
      {
        double num1 = c[index1 - n1 + 1];
        double num2 = s[index1 - n1 + 1];
        if (num1 != 1.0 | num2 != 0.0)
        {
          int index2 = index1 + 1;
          for (int index3 = m1; index3 <= m2; ++index3)
            work[index3] = num1 * a[index3, index2];
          for (int index3 = m1; index3 <= m2; ++index3)
            work[index3] = work[index3] - num2 * a[index3, index1];
          for (int index3 = m1; index3 <= m2; ++index3)
            a[index3, index1] = num1 * a[index3, index1];
          for (int index3 = m1; index3 <= m2; ++index3)
            a[index3, index1] = a[index3, index1] + num2 * a[index3, index2];
          for (int index3 = m1; index3 <= m2; ++index3)
            a[index3, index2] = work[index3];
        }
      }
    }
    else
    {
      for (int index = n2 - 1; index >= n1; --index)
      {
        double num1 = c[index - n1 + 1];
        double num2 = s[index - n1 + 1];
        if (num1 != 1.0 | num2 != 0.0)
        {
          double num3 = a[m1, index + 1];
          a[m1, index + 1] = num1 * num3 - num2 * a[m1, index];
          a[m1, index] = num2 * num3 + num1 * a[m1, index];
        }
      }
    }
  }

  public static void generaterotation(double f, double g, ref double cs, ref double sn, ref double r)
  {
    if (g == 0.0)
    {
      cs = 1.0;
      sn = 0.0;
      r = f;
    }
    else if (f == 0.0)
    {
      cs = 0.0;
      sn = 1.0;
      r = g;
    }
    else
    {
      double X1 = f;
      double X2 = g;
      r = System.Math.Sqrt(AP.Math.Sqr(X1) + AP.Math.Sqr(X2));
      cs = X1 / r;
      sn = X2 / r;
      if (!(System.Math.Abs(f) > System.Math.Abs(g) & cs < 0.0))
        return;
      cs = -cs;
      sn = -sn;
      r = -r;
    }
  }

  private static void testrotations()
  {
    double[,] numArray1 = new double[0, 0];
    double[,] numArray2 = new double[0, 0];
    double[,] numArray3 = new double[0, 0];
    double[,] numArray4 = new double[0, 0];
    double[] numArray5 = new double[0];
    double[] numArray6 = new double[0];
    double[] numArray7 = new double[0];
    double[] numArray8 = new double[0];
    double[] numArray9 = new double[0];
    int num1 = 1000;
    double val2_1 = 0.0;
    for (int index1 = 1; index1 <= num1; ++index1)
    {
      int num2 = 2 + AP.Math.RandomInteger(50);
      int num3 = 2 + AP.Math.RandomInteger(50);
      bool isforward = AP.Math.RandomReal() > 0.5;
      int num4 = System.Math.Max(num2, num3);
      double[,] a1 = new double[num2 + 1, num3 + 1];
      double[,] a2 = new double[num2 + 1, num3 + 1];
      double[,] a3 = new double[num2 + 1, num3 + 1];
      double[,] a4 = new double[num2 + 1, num3 + 1];
      double[] c1 = new double[num2 - 1 + 1];
      double[] s1 = new double[num2 - 1 + 1];
      double[] c2 = new double[num3 - 1 + 1];
      double[] s2 = new double[num3 - 1 + 1];
      double[] work = new double[num4 + 1];
      for (int index2 = 1; index2 <= num2; ++index2)
      {
        for (int index3 = 1; index3 <= num3; ++index3)
        {
          a1[index2, index3] = 2.0 * AP.Math.RandomReal() - 1.0;
          a2[index2, index3] = a1[index2, index3];
          a3[index2, index3] = a1[index2, index3];
          a4[index2, index3] = a1[index2, index3];
        }
      }
      for (int index2 = 1; index2 <= num2 - 1; ++index2)
      {
        double num5 = 2.0 * System.Math.PI * AP.Math.RandomReal();
        c1[index2] = System.Math.Cos(num5);
        s1[index2] = System.Math.Sin(num5);
      }
      for (int index2 = 1; index2 <= num3 - 1; ++index2)
      {
        double num5 = 2.0 * System.Math.PI * AP.Math.RandomReal();
        c2[index2] = System.Math.Cos(num5);
        s2[index2] = System.Math.Sin(num5);
      }
      rotations.applyrotationsfromtheleft(isforward, 1, num2, 1, num3, ref c1, ref s1, ref a1, ref work);
      for (int index2 = 1; index2 <= num3; ++index2)
        rotations.applyrotationsfromtheleft(isforward, 1, num2, index2, index2, ref c1, ref s1, ref a2, ref work);
      double val1_1 = 0.0;
      for (int index2 = 1; index2 <= num2; ++index2)
      {
        for (int index3 = 1; index3 <= num3; ++index3)
          val1_1 = System.Math.Max(val1_1, System.Math.Abs(a1[index2, index3] - a2[index2, index3]));
      }
      double val2_2 = System.Math.Max(val1_1, val2_1);
      rotations.applyrotationsfromtheright(isforward, 1, num2, 1, num3, ref c2, ref s2, ref a3, ref work);
      for (int index2 = 1; index2 <= num2; ++index2)
        rotations.applyrotationsfromtheright(isforward, index2, index2, 1, num3, ref c2, ref s2, ref a4, ref work);
      double val1_2 = 0.0;
      for (int index2 = 1; index2 <= num2; ++index2)
      {
        for (int index3 = 1; index3 <= num3; ++index3)
          val1_2 = System.Math.Max(val1_2, System.Math.Abs(a3[index2, index3] - a4[index2, index3]));
      }
      val2_1 = System.Math.Max(val1_2, val2_2);
    }
    Console.Write("TESTING ROTATIONS");
    Console.WriteLine();
    Console.Write("Pass count ");
    Console.Write("{0,0:d}", (object) num1);
    Console.WriteLine();
    Console.Write("Error is ");
    Console.Write("{0,5:E3}", (object) val2_1);
    Console.WriteLine();
  }
}
