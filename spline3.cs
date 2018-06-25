internal class spline3
{
  public static void buildlinearspline(double[] x, double[] y, int n, ref double[] c)
  {
    x = (double[]) x.Clone();
    y = (double[]) y.Clone();
    spline3.heapsortpoints(ref x, ref y, n);
    int num = 3 + n + (n - 1) * 4;
    c = new double[num - 1 + 1];
    c[0] = (double) num;
    c[1] = 3.0;
    c[2] = (double) n;
    for (int index = 0; index <= n - 1; ++index)
      c[3 + index] = x[index];
    for (int index = 0; index <= n - 2; ++index)
    {
      c[3 + n + 4 * index] = y[index];
      c[3 + n + 4 * index + 1] = (y[index + 1] - y[index]) / (x[index + 1] - x[index]);
      c[3 + n + 4 * index + 2] = 0.0;
      c[3 + n + 4 * index + 3] = 0.0;
    }
  }

  public static void buildcubicspline(double[] x, double[] y, int n, int boundltype, double boundl, int boundrtype, double boundr, ref double[] c)
  {
    double[] numArray1 = new double[0];
    double[] numArray2 = new double[0];
    double[] numArray3 = new double[0];
    double[] numArray4 = new double[0];
    double[] x1 = new double[0];
    x = (double[]) x.Clone();
    y = (double[]) y.Clone();
    double[] a = new double[n - 1 + 1];
    double[] b = new double[n - 1 + 1];
    double[] c1 = new double[n - 1 + 1];
    double[] d = new double[n - 1 + 1];
    if (n == 2 & boundltype == 0 & boundrtype == 0)
    {
      boundltype = 2;
      boundl = 0.0;
      boundrtype = 2;
      boundr = 0.0;
    }
    spline3.heapsortpoints(ref x, ref y, n);
    if (boundltype == 0)
    {
      a[0] = 0.0;
      b[0] = 1.0;
      c1[0] = 1.0;
      d[0] = 2.0 * (y[1] - y[0]) / (x[1] - x[0]);
    }
    if (boundltype == 1)
    {
      a[0] = 0.0;
      b[0] = 1.0;
      c1[0] = 0.0;
      d[0] = boundl;
    }
    if (boundltype == 2)
    {
      a[0] = 0.0;
      b[0] = 2.0;
      c1[0] = 1.0;
      d[0] = 3.0 * (y[1] - y[0]) / (x[1] - x[0]) - 0.5 * boundl * (x[1] - x[0]);
    }
    for (int index = 1; index <= n - 2; ++index)
    {
      a[index] = x[index + 1] - x[index];
      b[index] = 2.0 * (x[index + 1] - x[index - 1]);
      c1[index] = x[index] - x[index - 1];
      d[index] = 3.0 * (y[index] - y[index - 1]) / (x[index] - x[index - 1]) * (x[index + 1] - x[index]) + 3.0 * (y[index + 1] - y[index]) / (x[index + 1] - x[index]) * (x[index] - x[index - 1]);
    }
    if (boundrtype == 0)
    {
      a[n - 1] = 1.0;
      b[n - 1] = 1.0;
      c1[n - 1] = 0.0;
      d[n - 1] = 2.0 * (y[n - 1] - y[n - 2]) / (x[n - 1] - x[n - 2]);
    }
    if (boundrtype == 1)
    {
      a[n - 1] = 0.0;
      b[n - 1] = 1.0;
      c1[n - 1] = 0.0;
      d[n - 1] = boundr;
    }
    if (boundrtype == 2)
    {
      a[n - 1] = 1.0;
      b[n - 1] = 2.0;
      c1[n - 1] = 0.0;
      d[n - 1] = 3.0 * (y[n - 1] - y[n - 2]) / (x[n - 1] - x[n - 2]) + 0.5 * boundr * (x[n - 1] - x[n - 2]);
    }
    spline3.solvetridiagonal(a, b, c1, d, n, ref x1);
    spline3.buildhermitespline(x, y, x1, n, ref c);
  }

  public static void buildhermitespline(double[] x, double[] y, double[] d, int n, ref double[] c)
  {
    x = (double[]) x.Clone();
    y = (double[]) y.Clone();
    d = (double[]) d.Clone();
    spline3.heapsortdpoints(ref x, ref y, ref d, n);
    int num1 = 3 + n + (n - 1) * 4;
    c = new double[num1 - 1 + 1];
    c[0] = (double) num1;
    c[1] = 3.0;
    c[2] = (double) n;
    for (int index = 0; index <= n - 1; ++index)
      c[3 + index] = x[index];
    for (int index = 0; index <= n - 2; ++index)
    {
      double X = x[index + 1] - x[index];
      double num2 = AP.Math.Sqr(X);
      double num3 = X * num2;
      c[3 + n + 4 * index] = y[index];
      c[3 + n + 4 * index + 1] = d[index];
      c[3 + n + 4 * index + 2] = (3.0 * (y[index + 1] - y[index]) - 2.0 * d[index] * X - d[index + 1] * X) / num2;
      c[3 + n + 4 * index + 3] = (2.0 * (y[index] - y[index + 1]) + d[index] * X + d[index + 1] * X) / num3;
    }
  }

  public static void buildakimaspline(double[] x, double[] y, int n, ref double[] c)
  {
    double[] numArray1 = new double[0];
    double[] numArray2 = new double[0];
    double[] numArray3 = new double[0];
    x = (double[]) x.Clone();
    y = (double[]) y.Clone();
    spline3.heapsortpoints(ref x, ref y, n);
    double[] numArray4 = new double[n - 2 + 1];
    double[] numArray5 = new double[n - 2 + 1];
    for (int index = 0; index <= n - 2; ++index)
      numArray5[index] = (y[index + 1] - y[index]) / (x[index + 1] - x[index]);
    for (int index = 1; index <= n - 2; ++index)
      numArray4[index] = System.Math.Abs(numArray5[index] - numArray5[index - 1]);
    double[] d = new double[n - 1 + 1];
    for (int index = 2; index <= n - 3; ++index)
      d[index] = System.Math.Abs(numArray4[index - 1]) + System.Math.Abs(numArray4[index + 1]) == 0.0 ? ((x[index + 1] - x[index]) * numArray5[index - 1] + (x[index] - x[index - 1]) * numArray5[index]) / (x[index + 1] - x[index - 1]) : (numArray4[index + 1] * numArray5[index - 1] + numArray4[index - 1] * numArray5[index]) / (numArray4[index + 1] + numArray4[index - 1]);
    d[0] = spline3.diffthreepoint(x[0], x[0], y[0], x[1], y[1], x[2], y[2]);
    d[1] = spline3.diffthreepoint(x[1], x[0], y[0], x[1], y[1], x[2], y[2]);
    d[n - 2] = spline3.diffthreepoint(x[n - 2], x[n - 3], y[n - 3], x[n - 2], y[n - 2], x[n - 1], y[n - 1]);
    d[n - 1] = spline3.diffthreepoint(x[n - 1], x[n - 3], y[n - 3], x[n - 2], y[n - 2], x[n - 1], y[n - 1]);
    spline3.buildhermitespline(x, y, d, n, ref c);
  }

  public static double splineinterpolation(ref double[] c, double x)
  {
    int num1 = (int) System.Math.Round(c[2]);
    int index1 = 3;
    int num2 = 3 + num1 - 2 + 1;
    while (index1 != num2 - 1)
    {
      int index2 = (index1 + num2) / 2;
      if (c[index2] >= x)
        num2 = index2;
      else
        index1 = index2;
    }
    x -= c[index1];
    int index3 = 3 + num1 + 4 * (index1 - 3);
    return c[index3] + x * (c[index3 + 1] + x * (c[index3 + 2] + x * c[index3 + 3]));
  }

  public static void splinedifferentiation(ref double[] c, double x, ref double s, ref double ds, ref double d2s)
  {
    int num1 = (int) System.Math.Round(c[2]);
    int index1 = 3;
    int num2 = 3 + num1 - 2 + 1;
    while (index1 != num2 - 1)
    {
      int index2 = (index1 + num2) / 2;
      if (c[index2] >= x)
        num2 = index2;
      else
        index1 = index2;
    }
    x -= c[index1];
    int index3 = 3 + num1 + 4 * (index1 - 3);
    s = c[index3] + x * (c[index3 + 1] + x * (c[index3 + 2] + x * c[index3 + 3]));
    ds = c[index3 + 1] + 2.0 * x * c[index3 + 2] + 3.0 * AP.Math.Sqr(x) * c[index3 + 3];
    d2s = 2.0 * c[index3 + 2] + 6.0 * x * c[index3 + 3];
  }

  public static void splinecopy(ref double[] c, ref double[] cc)
  {
    int num = (int) System.Math.Round(c[0]);
    cc = new double[num - 1 + 1];
    for (int index = 0; index <= num - 1; ++index)
      cc[index] = c[index];
  }

  public static void splineunpack(ref double[] c, ref int n, ref double[,] tbl)
  {
    n = (int) System.Math.Round(c[2]);
    tbl = new double[n - 2 + 1, 6];
    for (int index = 0; index <= n - 2; ++index)
    {
      tbl[index, 0] = c[3 + index];
      tbl[index, 1] = c[3 + index + 1];
      tbl[index, 2] = c[3 + n + 4 * index];
      tbl[index, 3] = c[3 + n + 4 * index + 1];
      tbl[index, 4] = c[3 + n + 4 * index + 2];
      tbl[index, 5] = c[3 + n + 4 * index + 3];
    }
  }

  public static void splinelintransx(ref double[] c, double a, double b)
  {
    double s = 0.0;
    double ds = 0.0;
    double d2s = 0.0;
    double[] numArray1 = new double[0];
    double[] numArray2 = new double[0];
    double[] numArray3 = new double[0];
    int n = (int) System.Math.Round(c[2]);
    if (a == 0.0)
    {
      double num = spline3.splineinterpolation(ref c, b);
      for (int index = 0; index <= n - 2; ++index)
      {
        c[3 + n + 4 * index] = num;
        c[3 + n + 4 * index + 1] = 0.0;
        c[3 + n + 4 * index + 2] = 0.0;
        c[3 + n + 4 * index + 3] = 0.0;
      }
    }
    else
    {
      double[] x = new double[n - 1 + 1];
      double[] y = new double[n - 1 + 1];
      double[] d = new double[n - 1 + 1];
      for (int index = 0; index <= n - 1; ++index)
      {
        x[index] = c[3 + index];
        spline3.splinedifferentiation(ref c, x[index], ref s, ref ds, ref d2s);
        x[index] = (x[index] - b) / a;
        y[index] = s;
        d[index] = a * ds;
      }
      spline3.buildhermitespline(x, y, d, n, ref c);
    }
  }

  public static void splinelintransy(ref double[] c, double a, double b)
  {
    int num = (int) System.Math.Round(c[2]);
    for (int index = 0; index <= num - 2; ++index)
    {
      c[3 + num + 4 * index] = a * c[3 + num + 4 * index] + b;
      c[3 + num + 4 * index + 1] = a * c[3 + num + 4 * index + 1];
      c[3 + num + 4 * index + 2] = a * c[3 + num + 4 * index + 2];
      c[3 + num + 4 * index + 3] = a * c[3 + num + 4 * index + 3];
    }
  }

  public static double splineintegration(ref double[] c, double x)
  {
    int num1 = (int) System.Math.Round(c[2]);
    int index1 = 3;
    int num2 = 3 + num1 - 2 + 1;
    while (index1 != num2 - 1)
    {
      int index2 = (index1 + num2) / 2;
      if (c[index2] >= x)
        num2 = index2;
      else
        index1 = index2;
    }
    double num3 = 0.0;
    for (int index2 = 3; index2 <= index1 - 1; ++index2)
    {
      double X = c[index2 + 1] - c[index2];
      int index3 = 3 + num1 + 4 * (index2 - 3);
      num3 = num3 + c[index3] * X + c[index3 + 1] * AP.Math.Sqr(X) / 2.0 + c[index3 + 2] * AP.Math.Sqr(X) * X / 3.0 + c[index3 + 3] * AP.Math.Sqr(AP.Math.Sqr(X)) / 4.0;
    }
    double X1 = x - c[index1];
    int index4 = 3 + num1 + 4 * (index1 - 3);
    return num3 + c[index4] * X1 + c[index4 + 1] * AP.Math.Sqr(X1) / 2.0 + c[index4 + 2] * AP.Math.Sqr(X1) * X1 / 3.0 + c[index4 + 3] * AP.Math.Sqr(AP.Math.Sqr(X1)) / 4.0;
  }

  public static void spline3buildtable(int n, int diffn, double[] x, double[] y, double boundl, double boundr, ref double[,] ctbl)
  {
    x = (double[]) x.Clone();
    y = (double[]) y.Clone();
    --n;
    int num1 = (n + 1) / 2;
    do
    {
      int num2 = num1;
      do
      {
        int index = num2 - num1;
        bool flag = true;
        do
        {
          if (x[index] <= x[index + num1])
          {
            flag = false;
          }
          else
          {
            double num3 = x[index];
            x[index] = x[index + num1];
            x[index + num1] = num3;
            double num4 = y[index];
            y[index] = y[index + num1];
            y[index + num1] = num4;
          }
          --index;
        }
        while (index >= 0 & flag);
        ++num2;
      }
      while (num2 <= n);
      num1 /= 2;
    }
    while (num1 > 0);
    ctbl = new double[5, n + 1];
    ++n;
    double num5;
    double num6;
    double num7;
    double num8;
    if (diffn == 1)
    {
      num5 = 1.0;
      num6 = 6.0 / (x[1] - x[0]) * ((y[1] - y[0]) / (x[1] - x[0]) - boundl);
      num7 = 1.0;
      num8 = 6.0 / (x[n - 1] - x[n - 2]) * (boundr - (y[n - 1] - y[n - 2]) / (x[n - 1] - x[n - 2]));
    }
    else
    {
      num5 = 0.0;
      num6 = 2.0 * boundl;
      num7 = 0.0;
      num8 = 2.0 * boundr;
    }
    int num9 = n - 1;
    if (n < 2)
      return;
    if (n > 2)
    {
      double num2 = x[1] - x[0];
      double num3 = y[1] - y[0];
      for (int index = 2; index <= num9; ++index)
      {
        double num4 = x[index] - x[index - 1];
        double num10 = y[index] - y[index - 1];
        double num11 = num2 + num4;
        ctbl[1, index - 1] = num4 / num11;
        ctbl[2, index - 1] = 1.0 - ctbl[1, index - 1];
        ctbl[3, index - 1] = 6.0 * (num10 / num4 - num3 / num2) / num11;
        num2 = num4;
        num3 = num10;
      }
    }
    ctbl[1, 0] = -(num5 / 2.0);
    ctbl[2, 0] = num6 / 2.0;
    if (n != 2)
    {
      for (int index = 2; index <= num9; ++index)
      {
        double num2 = ctbl[2, index - 1] * ctbl[1, index - 2] + 2.0;
        ctbl[1, index - 1] = -(ctbl[1, index - 1] / num2);
        ctbl[2, index - 1] = (ctbl[3, index - 1] - ctbl[2, index - 1] * ctbl[2, index - 2]) / num2;
      }
    }
    double num12 = (num8 - num7 * ctbl[2, num9 - 1]) / (num7 * ctbl[1, num9 - 1] + 2.0);
    for (int index1 = 1; index1 <= num9; ++index1)
    {
      int index2 = n - index1;
      double num2 = ctbl[1, index2 - 1] * num12 + ctbl[2, index2 - 1];
      double num3 = x[index2] - x[index2 - 1];
      ctbl[3, index2 - 1] = (num12 - num2) / num3 / 6.0;
      ctbl[2, index2 - 1] = num2 / 2.0;
      ctbl[1, index2 - 1] = (y[index2] - y[index2 - 1]) / num3 - (ctbl[2, index2 - 1] + ctbl[3, index2 - 1] * num3) * num3;
      num12 = num2;
    }
    for (int index = 1; index <= n; ++index)
    {
      ctbl[0, index - 1] = y[index - 1];
      ctbl[4, index - 1] = x[index - 1];
    }
  }

  public static double spline3interpolate(int n, ref double[,] c, double x)
  {
    --n;
    int num1 = n;
    int num2 = 0;
    while (num1 > 0)
    {
      int num3 = num1 / 2;
      int index = num2 + num3;
      if (c[4, index] < x)
      {
        num2 = index + 1;
        num1 = num1 - num3 - 1;
      }
      else
        num1 = num3;
    }
    int index1 = num2 - 1;
    if (index1 < 0)
      index1 = 0;
    return c[0, index1] + (x - c[4, index1]) * (c[1, index1] + (x - c[4, index1]) * (c[2, index1] + c[3, index1] * (x - c[4, index1])));
  }

  private static void heapsortpoints(ref double[] x, ref double[] y, int n)
  {
    bool flag1 = true;
    bool flag2 = true;
    for (int index = 1; index <= n - 1; ++index)
    {
      flag1 &= x[index] > x[index - 1];
      flag2 &= x[index] < x[index - 1];
    }
    if (flag1)
      return;
    if (flag2)
    {
      for (int index1 = 0; index1 <= n - 1; ++index1)
      {
        int index2 = n - 1 - index1;
        if (index2 <= index1)
          break;
        double num1 = x[index1];
        x[index1] = x[index2];
        x[index2] = num1;
        double num2 = y[index1];
        y[index1] = y[index2];
        y[index2] = num2;
      }
    }
    else
    {
      if (n == 1)
        return;
      int num1 = 2;
      do
      {
        int num2 = num1;
        while (num2 != 1)
        {
          int num3 = num2 / 2;
          if (x[num3 - 1] >= x[num2 - 1])
          {
            num2 = 1;
          }
          else
          {
            double num4 = x[num3 - 1];
            x[num3 - 1] = x[num2 - 1];
            x[num2 - 1] = num4;
            double num5 = y[num3 - 1];
            y[num3 - 1] = y[num2 - 1];
            y[num2 - 1] = num5;
            num2 = num3;
          }
        }
        ++num1;
      }
      while (num1 <= n);
      int index1 = n - 1;
      do
      {
        double num2 = x[index1];
        x[index1] = x[0];
        x[0] = num2;
        double num3 = y[index1];
        y[index1] = y[0];
        y[0] = num3;
        int num4 = 1;
        while (num4 != 0)
        {
          int index2 = 2 * num4;
          if (index2 > index1)
          {
            num4 = 0;
          }
          else
          {
            if (index2 < index1 && x[index2] > x[index2 - 1])
              ++index2;
            if (x[num4 - 1] >= x[index2 - 1])
            {
              num4 = 0;
            }
            else
            {
              double num5 = x[index2 - 1];
              x[index2 - 1] = x[num4 - 1];
              x[num4 - 1] = num5;
              double num6 = y[index2 - 1];
              y[index2 - 1] = y[num4 - 1];
              y[num4 - 1] = num6;
              num4 = index2;
            }
          }
        }
        --index1;
      }
      while (index1 >= 1);
    }
  }

  private static void heapsortdpoints(ref double[] x, ref double[] y, ref double[] d, int n)
  {
    bool flag1 = true;
    bool flag2 = true;
    for (int index = 1; index <= n - 1; ++index)
    {
      flag1 &= x[index] > x[index - 1];
      flag2 &= x[index] < x[index - 1];
    }
    if (flag1)
      return;
    if (flag2)
    {
      for (int index1 = 0; index1 <= n - 1; ++index1)
      {
        int index2 = n - 1 - index1;
        if (index2 <= index1)
          break;
        double num1 = x[index1];
        x[index1] = x[index2];
        x[index2] = num1;
        double num2 = y[index1];
        y[index1] = y[index2];
        y[index2] = num2;
        double num3 = d[index1];
        d[index1] = d[index2];
        d[index2] = num3;
      }
    }
    else
    {
      if (n == 1)
        return;
      int num1 = 2;
      do
      {
        int num2 = num1;
        while (num2 != 1)
        {
          int num3 = num2 / 2;
          if (x[num3 - 1] >= x[num2 - 1])
          {
            num2 = 1;
          }
          else
          {
            double num4 = x[num3 - 1];
            x[num3 - 1] = x[num2 - 1];
            x[num2 - 1] = num4;
            double num5 = y[num3 - 1];
            y[num3 - 1] = y[num2 - 1];
            y[num2 - 1] = num5;
            double num6 = d[num3 - 1];
            d[num3 - 1] = d[num2 - 1];
            d[num2 - 1] = num6;
            num2 = num3;
          }
        }
        ++num1;
      }
      while (num1 <= n);
      int index1 = n - 1;
      do
      {
        double num2 = x[index1];
        x[index1] = x[0];
        x[0] = num2;
        double num3 = y[index1];
        y[index1] = y[0];
        y[0] = num3;
        double num4 = d[index1];
        d[index1] = d[0];
        d[0] = num4;
        int num5 = 1;
        while (num5 != 0)
        {
          int index2 = 2 * num5;
          if (index2 > index1)
          {
            num5 = 0;
          }
          else
          {
            if (index2 < index1 && x[index2] > x[index2 - 1])
              ++index2;
            if (x[num5 - 1] >= x[index2 - 1])
            {
              num5 = 0;
            }
            else
            {
              double num6 = x[index2 - 1];
              x[index2 - 1] = x[num5 - 1];
              x[num5 - 1] = num6;
              double num7 = y[index2 - 1];
              y[index2 - 1] = y[num5 - 1];
              y[num5 - 1] = num7;
              double num8 = d[index2 - 1];
              d[index2 - 1] = d[num5 - 1];
              d[num5 - 1] = num8;
              num5 = index2;
            }
          }
        }
        --index1;
      }
      while (index1 >= 1);
    }
  }

  private static void solvetridiagonal(double[] a, double[] b, double[] c, double[] d, int n, ref double[] x)
  {
    a = (double[]) a.Clone();
    b = (double[]) b.Clone();
    c = (double[]) c.Clone();
    d = (double[]) d.Clone();
    x = new double[n - 1 + 1];
    a[0] = 0.0;
    c[n - 1] = 0.0;
    for (int index = 1; index <= n - 1; ++index)
    {
      double num = a[index] / b[index - 1];
      b[index] = b[index] - num * c[index - 1];
      d[index] = d[index] - num * d[index - 1];
    }
    x[n - 1] = d[n - 1] / b[n - 1];
    for (int index = n - 2; index >= 0; --index)
      x[index] = (d[index] - c[index] * x[index + 1]) / b[index];
  }

  private static double diffthreepoint(double t, double x0, double f0, double x1, double f1, double x2, double f2)
  {
    t -= x0;
    x1 -= x0;
    x2 -= x0;
    double num1 = (f2 - f0 - x2 / x1 * (f1 - f0)) / (AP.Math.Sqr(x2) - x1 * x2);
    double num2 = (f1 - f0 - num1 * AP.Math.Sqr(x1)) / x1;
    return 2.0 * num1 * t + num2;
  }
}
