using System;

internal class lq
{
  public static void rmatrixlq(ref double[,] a, int m, int n, ref double[] tau)
  {
    double[] numArray1 = new double[0];
    double[] numArray2 = new double[0];
    double tau1 = 0.0;
    int num1 = Math.Min(m, n);
    Math.Max(m, n);
    double[] work = new double[m + 1];
    double[] numArray3 = new double[n + 1];
    tau = new double[num1 - 1 + 1];
    int num2 = Math.Min(m, n);
    for (int n1 = 0; n1 <= num2 - 1; ++n1)
    {
      int num3 = n1 - 1;
      for (int index = 1; index <= n - n1; ++index)
        numArray3[index] = a[n1, index + num3];
      reflections.generatereflection(ref numArray3, n - n1, ref tau1);
      tau[n1] = tau1;
      int num4 = 1 - n1;
      for (int index = n1; index <= n - 1; ++index)
        a[n1, index] = numArray3[index + num4];
      numArray3[1] = 1.0;
      if (n1 < n)
        reflections.applyreflectionfromtheright(ref a, tau[n1], ref numArray3, n1 + 1, m - 1, n1, n - 1, ref work);
    }
  }

  public static void rmatrixlqunpackq(ref double[,] a, int m, int n, ref double[] tau, int qrows, ref double[,] q)
  {
    double[] numArray1 = new double[0];
    double[] numArray2 = new double[0];
    if (m <= 0 | n <= 0 | qrows <= 0)
      return;
    int num1 = Math.Min(Math.Min(m, n), qrows);
    q = new double[qrows - 1 + 1, n - 1 + 1];
    double[] v = new double[n + 1];
    double[] work = new double[qrows + 1];
    for (int index1 = 0; index1 <= qrows - 1; ++index1)
    {
      for (int index2 = 0; index2 <= n - 1; ++index2)
        q[index1, index2] = index1 != index2 ? 0.0 : 1.0;
    }
    for (int n1 = num1 - 1; n1 >= 0; --n1)
    {
      int num2 = n1 - 1;
      for (int index = 1; index <= n - n1; ++index)
        v[index] = a[n1, index + num2];
      v[1] = 1.0;
      reflections.applyreflectionfromtheright(ref q, tau[n1], ref v, 0, qrows - 1, n1, n - 1, ref work);
    }
  }

  public static void rmatrixlqunpackl(ref double[,] a, int m, int n, ref double[,] l)
  {
    if (m <= 0 | n <= 0)
      return;
    l = new double[m - 1 + 1, n - 1 + 1];
    for (int index = 0; index <= n - 1; ++index)
      l[0, index] = 0.0;
    for (int index1 = 1; index1 <= m - 1; ++index1)
    {
      for (int index2 = 0; index2 <= n - 1; ++index2)
        l[index1, index2] = l[0, index2];
    }
    for (int val1 = 0; val1 <= m - 1; ++val1)
    {
      int num = Math.Min(val1, n - 1);
      for (int index = 0; index <= num; ++index)
        l[val1, index] = a[val1, index];
    }
  }

  public static void lqdecomposition(ref double[,] a, int m, int n, ref double[] tau)
  {
    double[] numArray1 = new double[0];
    double[] numArray2 = new double[0];
    double tau1 = 0.0;
    int num1 = Math.Min(m, n);
    Math.Max(m, n);
    double[] work = new double[m + 1];
    double[] numArray3 = new double[n + 1];
    tau = new double[num1 + 1];
    int num2 = Math.Min(m, n);
    for (int n1 = 1; n1 <= num2; ++n1)
    {
      int n2 = n - n1 + 1;
      int num3 = n1 - 1;
      for (int index = 1; index <= n2; ++index)
        numArray3[index] = a[n1, index + num3];
      reflections.generatereflection(ref numArray3, n2, ref tau1);
      tau[n1] = tau1;
      int num4 = 1 - n1;
      for (int index = n1; index <= n; ++index)
        a[n1, index] = numArray3[index + num4];
      numArray3[1] = 1.0;
      if (n1 < n)
        reflections.applyreflectionfromtheright(ref a, tau[n1], ref numArray3, n1 + 1, m, n1, n, ref work);
    }
  }

  public static void unpackqfromlq(ref double[,] a, int m, int n, ref double[] tau, int qrows, ref double[,] q)
  {
    double[] numArray1 = new double[0];
    double[] numArray2 = new double[0];
    if (m == 0 | n == 0 | qrows == 0)
      return;
    int num1 = Math.Min(Math.Min(m, n), qrows);
    q = new double[qrows + 1, n + 1];
    double[] v = new double[n + 1];
    double[] work = new double[qrows + 1];
    for (int index1 = 1; index1 <= qrows; ++index1)
    {
      for (int index2 = 1; index2 <= n; ++index2)
        q[index1, index2] = index1 != index2 ? 0.0 : 1.0;
    }
    for (int n1 = num1; n1 >= 1; --n1)
    {
      int num2 = n - n1 + 1;
      int num3 = n1 - 1;
      for (int index = 1; index <= num2; ++index)
        v[index] = a[n1, index + num3];
      v[1] = 1.0;
      reflections.applyreflectionfromtheright(ref q, tau[n1], ref v, 1, qrows, n1, n, ref work);
    }
  }

  public static void lqdecompositionunpacked(double[,] a, int m, int n, ref double[,] l, ref double[,] q)
  {
    double[] tau = new double[0];
    a = (double[,]) a.Clone();
    if (n <= 0)
      return;
    q = new double[n + 1, n + 1];
    l = new double[m + 1, n + 1];
    lq.lqdecomposition(ref a, m, n, ref tau);
    for (int index1 = 1; index1 <= m; ++index1)
    {
      for (int index2 = 1; index2 <= n; ++index2)
        l[index1, index2] = index2 <= index1 ? a[index1, index2] : 0.0;
    }
    lq.unpackqfromlq(ref a, m, n, ref tau, n, ref q);
  }
}
