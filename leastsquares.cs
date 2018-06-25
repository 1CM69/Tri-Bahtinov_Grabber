internal class leastsquares
{
  public static void buildgeneralleastsquares(ref double[] y, ref double[] w, ref double[,] fmatrix, int n, int m, ref double[] c)
  {
    double[,] numArray1 = new double[0, 0];
    double[,] numArray2 = new double[0, 0];
    double[,] numArray3 = new double[0, 0];
    double[] numArray4 = new double[0];
    double[] tau = new double[0];
    double[,] numArray5 = new double[0, 0];
    double[] tauq = new double[0];
    double[] taup = new double[0];
    double[] d = new double[0];
    double[] e = new double[0];
    bool isupper = false;
    int val1 = n;
    int num1 = m;
    c = new double[num1 - 1 + 1];
    double[,] numArray6 = new double[num1 + 1, System.Math.Max(val1, num1) + 1];
    double[] numArray7 = new double[System.Math.Max(val1, num1) + 1];
    for (int index = 1; index <= val1; ++index)
      numArray7[index] = w[index - 1] * y[index - 1];
    for (int index = val1 + 1; index <= num1; ++index)
      numArray7[index] = 0.0;
    for (int index1 = 1; index1 <= num1; ++index1)
    {
      int num2 = -1;
      for (int index2 = 1; index2 <= val1; ++index2)
        numArray6[index1, index2] = fmatrix[index2 + num2, index1 - 1];
    }
    for (int index1 = 1; index1 <= num1; ++index1)
    {
      for (int index2 = val1 + 1; index2 <= num1; ++index2)
        numArray6[index1, index2] = 0.0;
    }
    for (int index1 = 1; index1 <= num1; ++index1)
    {
      for (int index2 = 1; index2 <= val1; ++index2)
        numArray6[index1, index2] = numArray6[index1, index2] * w[index2 - 1];
    }
    int n1 = System.Math.Max(val1, num1);
    lq.lqdecomposition(ref numArray6, num1, n1, ref tau);
    lq.unpackqfromlq(ref numArray6, num1, n1, ref tau, num1, ref numArray2);
    double[,] numArray8 = new double[2, num1 + 1];
    for (int index = 1; index <= num1; ++index)
      numArray8[1, index] = 0.0;
    for (int index1 = 1; index1 <= num1; ++index1)
    {
      double num2 = 0.0;
      for (int index2 = 1; index2 <= n1; ++index2)
        num2 += numArray7[index2] * numArray2[index1, index2];
      numArray8[1, index1] = num2;
    }
    for (int index1 = 1; index1 <= num1 - 1; ++index1)
    {
      for (int index2 = index1 + 1; index2 <= num1; ++index2)
        numArray6[index1, index2] = numArray6[index2, index1];
    }
    for (int index1 = 2; index1 <= num1; ++index1)
    {
      for (int index2 = 1; index2 <= index1 - 1; ++index2)
        numArray6[index1, index2] = 0.0;
    }
    bidiagonal.tobidiagonal(ref numArray6, num1, num1, ref tauq, ref taup);
    bidiagonal.multiplybyqfrombidiagonal(ref numArray6, num1, num1, ref tauq, ref numArray8, 1, num1, true, false);
    bidiagonal.unpackptfrombidiagonal(ref numArray6, num1, num1, ref taup, num1, ref numArray3);
    bidiagonal.unpackdiagonalsfrombidiagonal(ref numArray6, num1, num1, ref isupper, ref d, ref e);
    if (!bdsvd.bidiagonalsvddecomposition(ref d, e, num1, isupper, false, ref numArray8, 1, ref numArray2, 0, ref numArray3, num1))
    {
      for (int index = 0; index <= num1 - 1; ++index)
        c[index] = 0.0;
    }
    else
    {
      if (d[1] != 0.0)
      {
        for (int index = 1; index <= num1; ++index)
          numArray8[1, index] = d[index] <= 5E-15 * System.Math.Sqrt((double) num1) * d[1] ? 0.0 : numArray8[1, index] / d[index];
      }
      for (int index = 1; index <= num1; ++index)
        numArray7[index] = 0.0;
      for (int index1 = 1; index1 <= num1; ++index1)
      {
        double num2 = numArray8[1, index1];
        for (int index2 = 1; index2 <= num1; ++index2)
          numArray7[index2] = numArray7[index2] + num2 * numArray3[index1, index2];
      }
      for (int index = 1; index <= num1; ++index)
        c[index - 1] = numArray7[index];
    }
  }

  public static void buildlinearleastsquares(ref double[] x, ref double[] y, int n, ref double a, ref double b)
  {
    double num1 = (double) n;
    double num2 = 0.0;
    double num3 = 0.0;
    double num4 = 0.0;
    double num5 = 0.0;
    for (int index = 0; index <= n - 1; ++index)
    {
      num3 += x[index];
      num2 += AP.Math.Sqr(x[index]);
      num4 += y[index];
      num5 += x[index] * y[index];
    }
    double num6 = System.Math.Atan2(2.0 * num3, num2 - num1) / 2.0;
    double X1 = System.Math.Cos(num6);
    double X2 = System.Math.Sin(num6);
    double num7 = AP.Math.Sqr(X1) * num1 + AP.Math.Sqr(X2) * num2 - 2.0 * X2 * X1 * num3;
    double num8 = AP.Math.Sqr(X2) * num1 + AP.Math.Sqr(X1) * num2 + 2.0 * X2 * X1 * num3;
    double num9 = System.Math.Abs(num7) <= System.Math.Abs(num8) ? System.Math.Abs(num8) : System.Math.Abs(num7);
    double num10 = X1 * num4 - X2 * num5;
    double num11 = X2 * num4 + X1 * num5;
    double num12 = System.Math.Abs(num7) <= num9 * 5E-16 * 1000.0 ? 0.0 : num10 / num7;
    double num13 = System.Math.Abs(num8) <= num9 * 5E-16 * 1000.0 ? 0.0 : num11 / num8;
    a = X1 * num12 + X2 * num13;
    b = -(X2 * num12) + X1 * num13;
  }

  public static void buildsplineleastsquares(ref double[] x, ref double[] y, ref double[] w, double a, double b, int n, int m, ref double[] ctbl)
  {
    double[,] numArray1 = new double[0, 0];
    double[,] numArray2 = new double[0, 0];
    double[,] numArray3 = new double[0, 0];
    double[] numArray4 = new double[0];
    double[] tau = new double[0];
    double[,] numArray5 = new double[0, 0];
    double[] tauq = new double[0];
    double[] taup = new double[0];
    double[] d = new double[0];
    double[] e = new double[0];
    bool isupper = false;
    double[] numArray6 = new double[0];
    double[] numArray7 = new double[0];
    int val1 = n;
    int num1 = m;
    double[] x1 = new double[num1 - 1 + 1];
    double[] y1 = new double[num1 - 1 + 1];
    double[,] numArray8 = new double[num1 + 1, System.Math.Max(val1, num1) + 1];
    double[] numArray9 = new double[System.Math.Max(val1, num1) + 1];
    for (int index = 0; index <= num1 - 1; ++index)
      x1[index] = a + (b - a) * (double) index / (double) (num1 - 1);
    for (int index1 = 0; index1 <= num1 - 1; ++index1)
    {
      for (int index2 = 0; index2 <= num1 - 1; ++index2)
        y1[index2] = 0.0;
      y1[index1] = 1.0;
      spline3.buildcubicspline(x1, y1, num1, 0, 0.0, 0, 0.0, ref ctbl);
      for (int index2 = 0; index2 <= val1 - 1; ++index2)
        numArray8[index1 + 1, index2 + 1] = w[index2] * spline3.splineinterpolation(ref ctbl, x[index2]);
    }
    for (int index1 = 1; index1 <= num1; ++index1)
    {
      for (int index2 = val1 + 1; index2 <= num1; ++index2)
        numArray8[index1, index2] = 0.0;
    }
    for (int index = 0; index <= val1 - 1; ++index)
      numArray9[index + 1] = w[index] * y[index];
    for (int index = val1 + 1; index <= num1; ++index)
      numArray9[index] = 0.0;
    int n1 = System.Math.Max(val1, num1);
    lq.lqdecomposition(ref numArray8, num1, n1, ref tau);
    lq.unpackqfromlq(ref numArray8, num1, n1, ref tau, num1, ref numArray2);
    double[,] numArray10 = new double[2, num1 + 1];
    for (int index = 1; index <= num1; ++index)
      numArray10[1, index] = 0.0;
    for (int index1 = 1; index1 <= num1; ++index1)
    {
      double num2 = 0.0;
      for (int index2 = 1; index2 <= n1; ++index2)
        num2 += numArray9[index2] * numArray2[index1, index2];
      numArray10[1, index1] = num2;
    }
    for (int index1 = 1; index1 <= num1 - 1; ++index1)
    {
      for (int index2 = index1 + 1; index2 <= num1; ++index2)
        numArray8[index1, index2] = numArray8[index2, index1];
    }
    for (int index1 = 2; index1 <= num1; ++index1)
    {
      for (int index2 = 1; index2 <= index1 - 1; ++index2)
        numArray8[index1, index2] = 0.0;
    }
    bidiagonal.tobidiagonal(ref numArray8, num1, num1, ref tauq, ref taup);
    bidiagonal.multiplybyqfrombidiagonal(ref numArray8, num1, num1, ref tauq, ref numArray10, 1, num1, true, false);
    bidiagonal.unpackptfrombidiagonal(ref numArray8, num1, num1, ref taup, num1, ref numArray3);
    bidiagonal.unpackdiagonalsfrombidiagonal(ref numArray8, num1, num1, ref isupper, ref d, ref e);
    if (!bdsvd.bidiagonalsvddecomposition(ref d, e, num1, isupper, false, ref numArray10, 1, ref numArray2, 0, ref numArray3, num1))
    {
      for (int index1 = 1; index1 <= num1; ++index1)
      {
        d[index1] = 0.0;
        numArray10[1, index1] = 0.0;
        for (int index2 = 1; index2 <= num1; ++index2)
          numArray3[index1, index2] = index1 != index2 ? 0.0 : 1.0;
      }
      numArray10[1, 1] = 1.0;
    }
    for (int index = 1; index <= num1; ++index)
      numArray10[1, index] = d[index] <= 5E-15 * System.Math.Sqrt((double) num1) * d[1] ? 0.0 : numArray10[1, index] / d[index];
    for (int index = 1; index <= num1; ++index)
      numArray9[index] = 0.0;
    for (int index1 = 1; index1 <= num1; ++index1)
    {
      double num2 = numArray10[1, index1];
      for (int index2 = 1; index2 <= num1; ++index2)
        numArray9[index2] = numArray9[index2] + num2 * numArray3[index1, index2];
    }
    for (int index = 0; index <= num1 - 1; ++index)
      y1[index] = numArray9[index + 1];
    spline3.buildcubicspline(x1, y1, num1, 0, 0.0, 0, 0.0, ref ctbl);
  }

  public static void buildpolynomialleastsquares(ref double[] x, ref double[] y, int n, int m, ref double[] c)
  {
    double[] ctbl = new double[0];
    double[] numArray1 = new double[0];
    double[] numArray2 = new double[0];
    double[] numArray3 = new double[0];
    double[] numArray4 = new double[0];
    double b = x[0];
    double a = x[0];
    for (int index = 1; index <= n - 1; ++index)
    {
      if (x[index] > b)
        b = x[index];
      if (x[index] < a)
        a = x[index];
    }
    if (a == b)
    {
      a -= 0.5;
      b += 0.5;
    }
    double[] w = new double[n - 1 + 1];
    for (int index = 0; index <= n - 1; ++index)
      w[index] = 1.0;
    leastsquares.buildchebyshevleastsquares(ref x, ref y, ref w, a, b, n, m, ref ctbl);
    double[] numArray5 = new double[m + 1];
    for (int index = 0; index <= m; ++index)
      numArray5[index] = 0.0;
    double num1 = 0.0;
    for (int index1 = 0; index1 <= m; ++index1)
    {
      for (int index2 = index1; index2 <= m; ++index2)
      {
        double num2 = numArray5[index2];
        numArray5[index2] = 0.0;
        if (index1 <= 1 & index2 == index1)
        {
          numArray5[index2] = 1.0;
        }
        else
        {
          if (index1 != 0)
            numArray5[index2] = 2.0 * num1;
          if (index2 > index1 + 1)
            numArray5[index2] = numArray5[index2] - numArray5[index2 - 2];
        }
        num1 = num2;
      }
      num1 = numArray5[index1];
      double num3 = 0.0;
      int index3 = index1;
      while (index3 <= m)
      {
        num3 += numArray5[index3] * ctbl[index3];
        index3 += 2;
      }
      numArray5[index1] = num3;
    }
    double num4 = 2.0 / (ctbl[m + 2] - ctbl[m + 1]);
    double num5 = -(2.0 * ctbl[m + 1] / (ctbl[m + 2] - ctbl[m + 1])) - 1.0;
    c = new double[m + 1];
    double[] numArray6 = new double[m + 1];
    double[] numArray7 = new double[m + 1];
    c[0] = numArray5[0];
    numArray7[0] = 1.0;
    numArray6[0] = 1.0;
    for (int index = 1; index <= m; ++index)
    {
      numArray6[index] = 1.0;
      numArray7[index] = num5 * numArray7[index - 1];
      c[0] = c[0] + numArray5[index] * numArray7[index];
    }
    for (int index1 = 1; index1 <= m; ++index1)
    {
      numArray6[0] = num4 * numArray6[0];
      c[index1] = numArray5[index1] * numArray6[0];
      for (int index2 = index1 + 1; index2 <= m; ++index2)
      {
        int index3 = index2 - index1;
        numArray6[index3] = num4 * numArray6[index3] + numArray6[index3 - 1];
        c[index1] = c[index1] + numArray5[index2] * numArray6[index3] * numArray7[index3];
      }
    }
  }

  public static void buildchebyshevleastsquares(ref double[] x, ref double[] y, ref double[] w, double a, double b, int n, int m, ref double[] ctbl)
  {
    double[,] numArray1 = new double[0, 0];
    double[,] numArray2 = new double[0, 0];
    double[,] numArray3 = new double[0, 0];
    double[] numArray4 = new double[0];
    double[] tau = new double[0];
    double[,] numArray5 = new double[0, 0];
    double[] tauq = new double[0];
    double[] taup = new double[0];
    double[] d = new double[0];
    double[] e = new double[0];
    bool isupper = false;
    int val1 = n;
    int index1 = m + 1;
    double[,] numArray6 = new double[index1 + 1, System.Math.Max(val1, index1) + 1];
    double[] numArray7 = new double[System.Math.Max(val1, index1) + 1];
    for (int index2 = 1; index2 <= index1; ++index2)
    {
      for (int index3 = 1; index3 <= val1; ++index3)
      {
        double num = 2.0 * (x[index3 - 1] - a) / (b - a) - 1.0;
        if (index2 == 1)
          numArray6[index2, index3] = 1.0;
        if (index2 == 2)
          numArray6[index2, index3] = num;
        if (index2 > 2)
          numArray6[index2, index3] = 2.0 * num * numArray6[index2 - 1, index3] - numArray6[index2 - 2, index3];
      }
    }
    for (int index2 = 1; index2 <= index1; ++index2)
    {
      for (int index3 = 1; index3 <= val1; ++index3)
        numArray6[index2, index3] = w[index3 - 1] * numArray6[index2, index3];
    }
    for (int index2 = 1; index2 <= index1; ++index2)
    {
      for (int index3 = val1 + 1; index3 <= index1; ++index3)
        numArray6[index2, index3] = 0.0;
    }
    for (int index2 = 0; index2 <= val1 - 1; ++index2)
      numArray7[index2 + 1] = w[index2] * y[index2];
    for (int index2 = val1 + 1; index2 <= index1; ++index2)
      numArray7[index2] = 0.0;
    int n1 = System.Math.Max(val1, index1);
    lq.lqdecomposition(ref numArray6, index1, n1, ref tau);
    lq.unpackqfromlq(ref numArray6, index1, n1, ref tau, index1, ref numArray2);
    double[,] numArray8 = new double[2, index1 + 1];
    for (int index2 = 1; index2 <= index1; ++index2)
      numArray8[1, index2] = 0.0;
    for (int index2 = 1; index2 <= index1; ++index2)
    {
      double num = 0.0;
      for (int index3 = 1; index3 <= n1; ++index3)
        num += numArray7[index3] * numArray2[index2, index3];
      numArray8[1, index2] = num;
    }
    for (int index2 = 1; index2 <= index1 - 1; ++index2)
    {
      for (int index3 = index2 + 1; index3 <= index1; ++index3)
        numArray6[index2, index3] = numArray6[index3, index2];
    }
    for (int index2 = 2; index2 <= index1; ++index2)
    {
      for (int index3 = 1; index3 <= index2 - 1; ++index3)
        numArray6[index2, index3] = 0.0;
    }
    bidiagonal.tobidiagonal(ref numArray6, index1, index1, ref tauq, ref taup);
    bidiagonal.multiplybyqfrombidiagonal(ref numArray6, index1, index1, ref tauq, ref numArray8, 1, index1, true, false);
    bidiagonal.unpackptfrombidiagonal(ref numArray6, index1, index1, ref taup, index1, ref numArray3);
    bidiagonal.unpackdiagonalsfrombidiagonal(ref numArray6, index1, index1, ref isupper, ref d, ref e);
    if (!bdsvd.bidiagonalsvddecomposition(ref d, e, index1, isupper, false, ref numArray8, 1, ref numArray2, 0, ref numArray3, index1))
    {
      for (int index2 = 1; index2 <= index1; ++index2)
      {
        d[index2] = 0.0;
        numArray8[1, index2] = 0.0;
        for (int index3 = 1; index3 <= index1; ++index3)
          numArray3[index2, index3] = index2 != index3 ? 0.0 : 1.0;
      }
      numArray8[1, 1] = 1.0;
    }
    for (int index2 = 1; index2 <= index1; ++index2)
      numArray8[1, index2] = d[index2] <= 5E-15 * System.Math.Sqrt((double) index1) * d[1] ? 0.0 : numArray8[1, index2] / d[index2];
    for (int index2 = 1; index2 <= index1; ++index2)
      numArray7[index2] = 0.0;
    for (int index2 = 1; index2 <= index1; ++index2)
    {
      double num = numArray8[1, index2];
      for (int index3 = 1; index3 <= index1; ++index3)
        numArray7[index3] = numArray7[index3] + num * numArray3[index2, index3];
    }
    ctbl = new double[index1 + 1 + 1];
    for (int index2 = 1; index2 <= index1; ++index2)
      ctbl[index2 - 1] = numArray7[index2];
    ctbl[index1] = a;
    ctbl[index1 + 1] = b;
  }

  public static bool buildchebyshevleastsquaresconstrained(ref double[] x, ref double[] y, ref double[] w, double a, double b, int n, ref double[] xc, ref double[] yc, ref int[] dc, int nc, int m, ref double[] ctbl)
  {
    double[,] numArray1 = new double[0, 0];
    double[] numArray2 = new double[0];
    double[,] numArray3 = new double[0, 0];
    double[,] numArray4 = new double[0, 0];
    double[,] u = new double[0, 0];
    double[,] vt = new double[0, 0];
    double[] numArray5 = new double[0];
    double[] numArray6 = new double[0];
    double[] w1 = new double[0];
    double[] numArray7 = new double[0];
    double[] numArray8 = new double[0];
    double[] numArray9 = new double[0];
    double[] numArray10 = new double[0];
    double[] numArray11 = new double[0];
    double[,] numArray12 = new double[0, 0];
    bool flag = true;
    double[,] a1 = new double[System.Math.Max(n, m + 1) + 1, m + 1 + 1];
    double[] numArray13 = new double[System.Math.Max(n, m + 1) + 1];
    for (int index1 = 1; index1 <= n; ++index1)
    {
      for (int index2 = 1; index2 <= m + 1; ++index2)
      {
        double num = 2.0 * (x[index1 - 1] - a) / (b - a) - 1.0;
        if (index2 == 1)
          a1[index1, index2] = 1.0;
        if (index2 == 2)
          a1[index1, index2] = num;
        if (index2 > 2)
          a1[index1, index2] = 2.0 * num * a1[index1, index2 - 1] - a1[index1, index2 - 2];
      }
    }
    for (int index1 = 1; index1 <= n; ++index1)
    {
      for (int index2 = 1; index2 <= m + 1; ++index2)
        a1[index1, index2] = w[index1 - 1] * a1[index1, index2];
    }
    for (int index1 = n + 1; index1 <= m + 1; ++index1)
    {
      for (int index2 = 1; index2 <= m + 1; ++index2)
        a1[index1, index2] = 0.0;
    }
    for (int index = 0; index <= n - 1; ++index)
      numArray13[index + 1] = w[index] * y[index];
    for (int index = n + 1; index <= m + 1; ++index)
      numArray13[index] = 0.0;
    n = System.Math.Max(n, m + 1);
    double[,] b1 = new double[m + 1 + 1, m + 1 + 1];
    double[] numArray14 = new double[m + 1 + 1];
    int num1;
    double[] work;
    if (nc == 0)
    {
      for (int index1 = 1; index1 <= m + 1; ++index1)
      {
        for (int index2 = 1; index2 <= m + 1; ++index2)
          b1[index1, index2] = 0.0;
        numArray14[index1] = 0.0;
      }
      for (int index = 1; index <= m + 1; ++index)
        b1[index, index] = 1.0;
      num1 = m + 1;
    }
    else
    {
      double[,] a2 = new double[nc + 1, m + 1 + 1];
      double[] numArray15 = new double[nc + 1];
      double[] numArray16 = new double[m + 1];
      double[] numArray17 = new double[m + 1];
      double[] numArray18 = new double[m + 1];
      for (int index1 = 0; index1 <= nc - 1; ++index1)
      {
        double num2 = 2.0 * (xc[index1] - a) / (b - a) - 1.0;
        for (int index2 = 0; index2 <= m; ++index2)
        {
          if (index2 == 0)
          {
            numArray16[index2] = 1.0;
            numArray17[index2] = 1.0;
            numArray18[index2] = 0.0;
          }
          if (index2 == 1)
          {
            numArray16[index2] = num2;
            numArray17[index2] = 2.0 * num2;
            numArray18[index2] = 1.0;
          }
          if (index2 > 1)
          {
            numArray16[index2] = 2.0 * num2 * numArray16[index2 - 1] - numArray16[index2 - 2];
            numArray17[index2] = 2.0 * num2 * numArray17[index2 - 1] - numArray17[index2 - 2];
            numArray18[index2] = (double) index2 * numArray17[index2 - 1];
          }
          if (dc[index1] == 0)
            a2[index1 + 1, index2 + 1] = numArray16[index2];
          if (dc[index1] == 1)
            a2[index1 + 1, index2 + 1] = numArray18[index2];
        }
        numArray15[index1 + 1] = yc[index1];
      }
      if (!svd.svddecomposition(a2, nc, m + 1, 2, 2, 2, ref w1, ref u, ref vt) || w1[1] == 0.0 | w1[nc] <= 5E-15 * System.Math.Sqrt((double) nc) * w1[1])
        return false;
      b1 = new double[m + 1 + 1, m + 1 - nc + 1];
      numArray14 = new double[m + 1 + 1];
      for (int index1 = 1; index1 <= m + 1 - nc; ++index1)
      {
        for (int index2 = 1; index2 <= m + 1; ++index2)
          b1[index2, index1] = vt[nc + index1, index2];
      }
      double[] numArray19 = new double[nc + 1];
      for (int index1 = 1; index1 <= nc; ++index1)
      {
        double num2 = 0.0;
        for (int index2 = 1; index2 <= nc; ++index2)
          num2 += u[index2, index1] * numArray15[index2];
        numArray19[index1] = num2 / w1[index1];
      }
      for (int index = 1; index <= m + 1; ++index)
        numArray14[index] = 0.0;
      for (int index1 = 1; index1 <= nc; ++index1)
      {
        double num2 = numArray19[index1];
        for (int index2 = 1; index2 <= m + 1; ++index2)
          numArray14[index2] = numArray14[index2] + num2 * vt[index1, index2];
      }
      for (int index1 = 1; index1 <= n; ++index1)
      {
        double num2 = 0.0;
        for (int index2 = 1; index2 <= m + 1; ++index2)
          num2 += a1[index1, index2] * numArray14[index2];
        numArray13[index1] = numArray13[index1] - num2;
      }
      num1 = m + 1 - nc;
      double[,] numArray20 = new double[n + 1, num1 + 1];
      work = new double[n + 1];
      blas.matrixmatrixmultiply(ref a1, 1, n, 1, m + 1, false, ref b1, 1, m + 1, 1, num1, false, 1.0, ref numArray20, 1, n, 1, num1, 0.0, ref work);
      blas.copymatrix(ref numArray20, 1, n, 1, num1, ref a1, 1, n, 1, num1);
    }
    if (!svd.svddecomposition(a1, n, num1, 1, 1, 2, ref w1, ref u, ref vt))
      return false;
    work = new double[num1 + 1];
    double[] numArray21 = new double[num1 + 1];
    for (int index = 1; index <= num1; ++index)
      work[index] = 0.0;
    for (int index1 = 1; index1 <= n; ++index1)
    {
      double num2 = numArray13[index1];
      for (int index2 = 1; index2 <= num1; ++index2)
        work[index2] = work[index2] + num2 * u[index1, index2];
    }
    for (int index = 1; index <= num1; ++index)
      work[index] = !(w1[index] != 0.0 & w1[index] > 5E-15 * System.Math.Sqrt((double) nc) * w1[1]) ? 0.0 : work[index] / w1[index];
    for (int index = 1; index <= num1; ++index)
      numArray21[index] = 0.0;
    for (int index1 = 1; index1 <= num1; ++index1)
    {
      double num2 = work[index1];
      for (int index2 = 1; index2 <= num1; ++index2)
        numArray21[index2] = numArray21[index2] + num2 * vt[index1, index2];
    }
    ctbl = new double[m + 2 + 1];
    for (int index1 = 1; index1 <= m + 1; ++index1)
    {
      double num2 = 0.0;
      for (int index2 = 1; index2 <= num1; ++index2)
        num2 += b1[index1, index2] * numArray21[index2];
      ctbl[index1 - 1] = num2 + numArray14[index1];
    }
    ctbl[m + 1] = a;
    ctbl[m + 2] = b;
    return flag;
  }

  public static double calculatechebyshevleastsquares(int m, ref double[] a, double x)
  {
    x = 2.0 * (x - a[m + 1]) / (a[m + 2] - a[m + 1]) - 1.0;
    double num1 = 0.0;
    double num2 = 0.0;
    int index = m;
    double num3;
    do
    {
      num3 = 2.0 * x * num1 - num2 + a[index];
      num2 = num1;
      num1 = num3;
      --index;
    }
    while (index >= 0);
    return num3 - x * num2;
  }
}
