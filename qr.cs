using System;

internal class qr
{
  public static void rmatrixqr(ref double[,] a, int m, int n, ref double[] tau)
  {
    double[] numArray1 = new double[0];
    double[] numArray2 = new double[0];
    double tau1 = 0.0;
    if (m <= 0 | n <= 0)
      return;
    int num1 = Math.Min(m, n);
    double[] work = new double[n - 1 + 1];
    double[] numArray3 = new double[m + 1];
    tau = new double[num1 - 1 + 1];
    int num2 = num1;
    for (int m1 = 0; m1 <= num2 - 1; ++m1)
    {
      int num3 = m1 - 1;
      for (int index = 1; index <= m - m1; ++index)
        numArray3[index] = a[index + num3, m1];
      reflections.generatereflection(ref numArray3, m - m1, ref tau1);
      tau[m1] = tau1;
      int num4 = 1 - m1;
      for (int index = m1; index <= m - 1; ++index)
        a[index, m1] = numArray3[index + num4];
      numArray3[1] = 1.0;
      if (m1 < n)
        reflections.applyreflectionfromtheleft(ref a, tau[m1], ref numArray3, m1, m - 1, m1 + 1, n - 1, ref work);
    }
  }

  public static void rmatrixqrunpackq(ref double[,] a, int m, int n, ref double[] tau, int qcolumns, ref double[,] q)
  {
    double[] numArray1 = new double[0];
    double[] numArray2 = new double[0];
    if (m <= 0 | n <= 0 | qcolumns <= 0)
      return;
    int num1 = Math.Min(Math.Min(m, n), qcolumns);
    q = new double[m - 1 + 1, qcolumns - 1 + 1];
    double[] v = new double[m + 1];
    double[] work = new double[qcolumns - 1 + 1];
    for (int index1 = 0; index1 <= m - 1; ++index1)
    {
      for (int index2 = 0; index2 <= qcolumns - 1; ++index2)
        q[index1, index2] = index1 != index2 ? 0.0 : 1.0;
    }
    for (int m1 = num1 - 1; m1 >= 0; --m1)
    {
      int num2 = m1 - 1;
      for (int index = 1; index <= m - m1; ++index)
        v[index] = a[index + num2, m1];
      v[1] = 1.0;
      reflections.applyreflectionfromtheleft(ref q, tau[m1], ref v, m1, m - 1, 0, qcolumns - 1, ref work);
    }
  }

  public static void rmatrixqrunpackr(ref double[,] a, int m, int n, ref double[,] r)
  {
    if (m <= 0 | n <= 0)
      return;
    int num = Math.Min(m, n);
    r = new double[m - 1 + 1, n - 1 + 1];
    for (int index = 0; index <= n - 1; ++index)
      r[0, index] = 0.0;
    for (int index1 = 1; index1 <= m - 1; ++index1)
    {
      for (int index2 = 0; index2 <= n - 1; ++index2)
        r[index1, index2] = r[0, index2];
    }
    for (int index1 = 0; index1 <= num - 1; ++index1)
    {
      for (int index2 = index1; index2 <= n - 1; ++index2)
        r[index1, index2] = a[index1, index2];
    }
  }

  public static void qrdecomposition(ref double[,] a, int m, int n, ref double[] tau)
  {
    double[] numArray1 = new double[0];
    double[] numArray2 = new double[0];
    double tau1 = 0.0;
    int num1 = Math.Min(m, n);
    double[] work = new double[n + 1];
    double[] numArray3 = new double[m + 1];
    tau = new double[num1 + 1];
    int num2 = Math.Min(m, n);
    for (int m1 = 1; m1 <= num2; ++m1)
    {
      int n1 = m - m1 + 1;
      int num3 = m1 - 1;
      for (int index = 1; index <= n1; ++index)
        numArray3[index] = a[index + num3, m1];
      reflections.generatereflection(ref numArray3, n1, ref tau1);
      tau[m1] = tau1;
      int num4 = 1 - m1;
      for (int index = m1; index <= m; ++index)
        a[index, m1] = numArray3[index + num4];
      numArray3[1] = 1.0;
      if (m1 < n)
        reflections.applyreflectionfromtheleft(ref a, tau[m1], ref numArray3, m1, m, m1 + 1, n, ref work);
    }
  }

  public static void unpackqfromqr(ref double[,] a, int m, int n, ref double[] tau, int qcolumns, ref double[,] q)
  {
    double[] numArray1 = new double[0];
    double[] numArray2 = new double[0];
    if (m == 0 | n == 0 | qcolumns == 0)
      return;
    int num1 = Math.Min(Math.Min(m, n), qcolumns);
    q = new double[m + 1, qcolumns + 1];
    double[] v = new double[m + 1];
    double[] work = new double[qcolumns + 1];
    for (int index1 = 1; index1 <= m; ++index1)
    {
      for (int index2 = 1; index2 <= qcolumns; ++index2)
        q[index1, index2] = index1 != index2 ? 0.0 : 1.0;
    }
    for (int m1 = num1; m1 >= 1; --m1)
    {
      int num2 = m - m1 + 1;
      int num3 = m1 - 1;
      for (int index = 1; index <= num2; ++index)
        v[index] = a[index + num3, m1];
      v[1] = 1.0;
      reflections.applyreflectionfromtheleft(ref q, tau[m1], ref v, m1, m, 1, qcolumns, ref work);
    }
  }

  public static void qrdecompositionunpacked(double[,] a, int m, int n, ref double[,] q, ref double[,] r)
  {
    double[] tau = new double[0];
    a = (double[,]) a.Clone();
    int num = Math.Min(m, n);
    if (n <= 0)
      return;
    q = new double[m + 1, m + 1];
    r = new double[m + 1, n + 1];
    qr.qrdecomposition(ref a, m, n, ref tau);
    for (int index = 1; index <= n; ++index)
      r[1, index] = 0.0;
    for (int index1 = 2; index1 <= m; ++index1)
    {
      for (int index2 = 1; index2 <= n; ++index2)
        r[index1, index2] = r[1, index2];
    }
    for (int index1 = 1; index1 <= num; ++index1)
    {
      for (int index2 = index1; index2 <= n; ++index2)
        r[index1, index2] = a[index1, index2];
    }
    qr.unpackqfromqr(ref a, m, n, ref tau, m, ref q);
  }
}
