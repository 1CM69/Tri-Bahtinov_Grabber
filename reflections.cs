using System;

internal class reflections
{
  public static void generatereflection(ref double[] x, int n, ref double tau)
  {
    if (n <= 1)
    {
      tau = 0.0;
    }
    else
    {
      double num1 = x[1];
      double val2 = 0.0;
      for (int index = 2; index <= n; ++index)
        val2 = System.Math.Max(System.Math.Abs(x[index]), val2);
      double d = 0.0;
      if (val2 != 0.0)
      {
        for (int index = 2; index <= n; ++index)
          d += AP.Math.Sqr(x[index] / val2);
        d = System.Math.Sqrt(d) * val2;
      }
      if (d == 0.0)
      {
        tau = 0.0;
      }
      else
      {
        double num2 = System.Math.Max(System.Math.Abs(num1), System.Math.Abs(d));
        double num3 = -(num2 * System.Math.Sqrt(AP.Math.Sqr(num1 / num2) + AP.Math.Sqr(d / num2)));
        if (num1 < 0.0)
          num3 = -num3;
        tau = (num3 - num1) / num3;
        double num4 = 1.0 / (num1 - num3);
        for (int index = 2; index <= n; ++index)
          x[index] = num4 * x[index];
        x[1] = num3;
      }
    }
  }

  public static void applyreflectionfromtheleft(ref double[,] c, double tau, ref double[] v, int m1, int m2, int n1, int n2, ref double[] work)
  {
    if (tau == 0.0 | n1 > n2 | m1 > m2)
      return;
    for (int index = n1; index <= n2; ++index)
      work[index] = 0.0;
    for (int index1 = m1; index1 <= m2; ++index1)
    {
      double num = v[index1 + 1 - m1];
      for (int index2 = n1; index2 <= n2; ++index2)
        work[index2] = work[index2] + num * c[index1, index2];
    }
    for (int index1 = m1; index1 <= m2; ++index1)
    {
      double num = v[index1 - m1 + 1] * tau;
      for (int index2 = n1; index2 <= n2; ++index2)
        c[index1, index2] = c[index1, index2] - num * work[index2];
    }
  }

  public static void applyreflectionfromtheright(ref double[,] c, double tau, ref double[] v, int m1, int m2, int n1, int n2, ref double[] work)
  {
    if (tau == 0.0 | n1 > n2 | m1 > m2)
      return;
    for (int index1 = m1; index1 <= m2; ++index1)
    {
      int num1 = 1 - n1;
      double num2 = 0.0;
      for (int index2 = n1; index2 <= n2; ++index2)
        num2 += c[index1, index2] * v[index2 + num1];
      work[index1] = num2;
    }
    for (int index1 = m1; index1 <= m2; ++index1)
    {
      double num1 = work[index1] * tau;
      int num2 = 1 - n1;
      for (int index2 = n1; index2 <= n2; ++index2)
        c[index1, index2] = c[index1, index2] - num1 * v[index2 + num2];
    }
  }

  private static void testreflections()
  {
    double[] numArray1 = new double[0];
    double[] numArray2 = new double[0];
    double[] numArray3 = new double[0];
    double[,] numArray4 = new double[0, 0];
    double[,] numArray5 = new double[0, 0];
    double[,] numArray6 = new double[0, 0];
    double[,] numArray7 = new double[0, 0];
    double num1 = 0.0;
    double tau = 0.0;
    int num2 = 1000;
    double val1_1 = 0.0;
    double val1_2 = 0.0;
    double val1_3 = 0.0;
    for (int index1 = 1; index1 <= num2; ++index1)
    {
      int num3 = 1 + AP.Math.RandomInteger(10);
      int num4 = 1 + AP.Math.RandomInteger(10);
      int num5 = System.Math.Max(num4, num3);
      double[] numArray8 = new double[num5 + 1];
      double[] numArray9 = new double[num5 + 1];
      double[] work = new double[num5 + 1];
      double[,] numArray10 = new double[num5 + 1, num5 + 1];
      double[,] numArray11 = new double[num5 + 1, num5 + 1];
      double[,] c = new double[num5 + 1, num5 + 1];
      double[,] numArray12 = new double[num5 + 1, num5 + 1];
      for (int index2 = 1; index2 <= num3; ++index2)
      {
        numArray8[index2] = 2.0 * AP.Math.RandomReal() - 1.0;
        numArray9[index2] = numArray8[index2];
      }
      reflections.generatereflection(ref numArray9, num3, ref tau);
      double num6 = numArray9[1];
      numArray9[1] = 1.0;
      for (int index2 = 1; index2 <= num3; ++index2)
      {
        for (int index3 = 1; index3 <= num3; ++index3)
          numArray10[index2, index3] = index2 != index3 ? -(tau * numArray9[index2] * numArray9[index3]) : 1.0 - tau * numArray9[index2] * numArray9[index3];
      }
      double num7 = 0.0;
      for (int index2 = 1; index2 <= num3; ++index2)
      {
        double num8 = 0.0;
        for (int index3 = 1; index3 <= num3; ++index3)
          num8 += numArray10[index2, index3] * numArray8[index3];
        num7 = index2 != 1 ? System.Math.Max(num7, System.Math.Abs(num8)) : System.Math.Max(num7, System.Math.Abs(num8 - num6));
      }
      val1_3 = System.Math.Max(val1_3, num7);
      for (int index2 = 1; index2 <= num4; ++index2)
      {
        numArray8[index2] = 2.0 * AP.Math.RandomReal() - 1.0;
        numArray9[index2] = numArray8[index2];
      }
      for (int index2 = 1; index2 <= num4; ++index2)
      {
        for (int index3 = 1; index3 <= num3; ++index3)
        {
          numArray11[index2, index3] = 2.0 * AP.Math.RandomReal() - 1.0;
          c[index2, index3] = numArray11[index2, index3];
        }
      }
      reflections.generatereflection(ref numArray9, num4, ref tau);
      num1 = numArray9[1];
      numArray9[1] = 1.0;
      reflections.applyreflectionfromtheleft(ref c, tau, ref numArray9, 1, num4, 1, num3, ref work);
      for (int index2 = 1; index2 <= num4; ++index2)
      {
        for (int index3 = 1; index3 <= num4; ++index3)
          numArray10[index2, index3] = index2 != index3 ? -(tau * numArray9[index2] * numArray9[index3]) : 1.0 - tau * numArray9[index2] * numArray9[index3];
      }
      for (int index2 = 1; index2 <= num4; ++index2)
      {
        for (int index3 = 1; index3 <= num3; ++index3)
        {
          double num8 = 0.0;
          for (int index4 = 1; index4 <= num4; ++index4)
            num8 += numArray10[index2, index4] * numArray11[index4, index3];
          numArray12[index2, index3] = num8;
        }
      }
      double num9 = 0.0;
      for (int index2 = 1; index2 <= num4; ++index2)
      {
        for (int index3 = 1; index3 <= num3; ++index3)
          num9 = System.Math.Max(num9, System.Math.Abs(c[index2, index3] - numArray12[index2, index3]));
      }
      val1_2 = System.Math.Max(val1_2, num9);
      for (int index2 = 1; index2 <= num3; ++index2)
      {
        numArray8[index2] = 2.0 * AP.Math.RandomReal() - 1.0;
        numArray9[index2] = numArray8[index2];
      }
      for (int index2 = 1; index2 <= num4; ++index2)
      {
        for (int index3 = 1; index3 <= num3; ++index3)
        {
          numArray11[index2, index3] = 2.0 * AP.Math.RandomReal() - 1.0;
          c[index2, index3] = numArray11[index2, index3];
        }
      }
      reflections.generatereflection(ref numArray9, num3, ref tau);
      num1 = numArray9[1];
      numArray9[1] = 1.0;
      reflections.applyreflectionfromtheright(ref c, tau, ref numArray9, 1, num4, 1, num3, ref work);
      for (int index2 = 1; index2 <= num3; ++index2)
      {
        for (int index3 = 1; index3 <= num3; ++index3)
          numArray10[index2, index3] = index2 != index3 ? -(tau * numArray9[index2] * numArray9[index3]) : 1.0 - tau * numArray9[index2] * numArray9[index3];
      }
      for (int index2 = 1; index2 <= num4; ++index2)
      {
        for (int index3 = 1; index3 <= num3; ++index3)
        {
          double num8 = 0.0;
          for (int index4 = 1; index4 <= num3; ++index4)
            num8 += numArray11[index2, index4] * numArray10[index4, index3];
          numArray12[index2, index3] = num8;
        }
      }
      double num10 = 0.0;
      for (int index2 = 1; index2 <= num4; ++index2)
      {
        for (int index3 = 1; index3 <= num3; ++index3)
          num10 = System.Math.Max(num10, System.Math.Abs(c[index2, index3] - numArray12[index2, index3]));
      }
      val1_1 = System.Math.Max(val1_1, num10);
    }
    numArray1 = new double[11];
    double[] x = new double[11];
    for (int index = 1; index <= 10; ++index)
      x[index] = 1E+298 * (2.0 * AP.Math.RandomReal() - 1.0);
    reflections.generatereflection(ref x, 10, ref tau);
    Console.Write("TESTING REFLECTIONS");
    Console.WriteLine();
    Console.Write("Pass count is ");
    Console.Write("{0,0:d}", (object) num2);
    Console.WriteLine();
    Console.Write("Generate     absolute error is       ");
    Console.Write("{0,5:E3}", (object) val1_3);
    Console.WriteLine();
    Console.Write("Apply(Left)  absolute error is       ");
    Console.Write("{0,5:E3}", (object) val1_2);
    Console.WriteLine();
    Console.Write("Apply(Right) absolute error is       ");
    Console.Write("{0,5:E3}", (object) val1_1);
    Console.WriteLine();
    Console.Write("Overflow crash test passed");
    Console.WriteLine();
  }
}
