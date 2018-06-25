internal class blas
{
  public static double vectornorm2(ref double[] x, int i1, int i2)
  {
    int num1 = i2 - i1 + 1;
    if (num1 < 1)
      return 0.0;
    if (num1 == 1)
      return System.Math.Abs(x[i1]);
    double num2 = 0.0;
    double d = 1.0;
    for (int index = i1; index <= i2; ++index)
    {
      if (x[index] != 0.0)
      {
        double num3 = System.Math.Abs(x[index]);
        if (num2 < num3)
        {
          d = 1.0 + d * AP.Math.Sqr(num2 / num3);
          num2 = num3;
        }
        else
          d += AP.Math.Sqr(num3 / num2);
      }
    }
    return num2 * System.Math.Sqrt(d);
  }

  public static int vectoridxabsmax(ref double[] x, int i1, int i2)
  {
    int index1 = i1;
    System.Math.Abs(x[index1]);
    for (int index2 = i1 + 1; index2 <= i2; ++index2)
    {
      if (System.Math.Abs(x[index2]) > System.Math.Abs(x[index1]))
        index1 = index2;
    }
    return index1;
  }

  public static int columnidxabsmax(ref double[,] x, int i1, int i2, int j)
  {
    int index1 = i1;
    System.Math.Abs(x[index1, j]);
    for (int index2 = i1 + 1; index2 <= i2; ++index2)
    {
      if (System.Math.Abs(x[index2, j]) > System.Math.Abs(x[index1, j]))
        index1 = index2;
    }
    return index1;
  }

  public static int rowidxabsmax(ref double[,] x, int j1, int j2, int i)
  {
    int index1 = j1;
    System.Math.Abs(x[i, index1]);
    for (int index2 = j1 + 1; index2 <= j2; ++index2)
    {
      if (System.Math.Abs(x[i, index2]) > System.Math.Abs(x[i, index1]))
        index1 = index2;
    }
    return index1;
  }

  public static double upperhessenberg1norm(ref double[,] a, int i1, int i2, int j1, int j2, ref double[] work)
  {
    for (int index = j1; index <= j2; ++index)
      work[index] = 0.0;
    for (int index1 = i1; index1 <= i2; ++index1)
    {
      for (int index2 = System.Math.Max(j1, j1 + index1 - i1 - 1); index2 <= j2; ++index2)
        work[index2] = work[index2] + System.Math.Abs(a[index1, index2]);
    }
    double val1 = 0.0;
    for (int index = j1; index <= j2; ++index)
      val1 = System.Math.Max(val1, work[index]);
    return val1;
  }

  public static void copymatrix(ref double[,] a, int is1, int is2, int js1, int js2, ref double[,] b, int id1, int id2, int jd1, int jd2)
  {
    if (is1 > is2 | js1 > js2)
      return;
    for (int index1 = is1; index1 <= is2; ++index1)
    {
      int index2 = index1 - is1 + id1;
      int num = js1 - jd1;
      for (int index3 = jd1; index3 <= jd2; ++index3)
        b[index2, index3] = a[index1, index3 + num];
    }
  }

  public static void inplacetranspose(ref double[,] a, int i1, int i2, int j1, int j2, ref double[] work)
  {
    if (i1 > i2 | j1 > j2)
      return;
    for (int index1 = i1; index1 <= i2 - 1; ++index1)
    {
      int index2 = j1 + index1 - i1;
      int num1 = index1 + 1;
      int num2 = j1 + num1 - i1;
      int num3 = i2 - index1;
      int num4 = num1 - 1;
      for (int index3 = 1; index3 <= num3; ++index3)
        work[index3] = a[index3 + num4, index2];
      int num5 = num2 - num1;
      for (int index3 = num1; index3 <= i2; ++index3)
        a[index3, index2] = a[index1, index3 + num5];
      int num6 = 1 - num2;
      for (int index3 = num2; index3 <= j2; ++index3)
        a[index1, index3] = work[index3 + num6];
    }
  }

  public static void copyandtranspose(ref double[,] a, int is1, int is2, int js1, int js2, ref double[,] b, int id1, int id2, int jd1, int jd2)
  {
    if (is1 > is2 | js1 > js2)
      return;
    for (int index1 = is1; index1 <= is2; ++index1)
    {
      int index2 = index1 - is1 + jd1;
      int num = js1 - id1;
      for (int index3 = id1; index3 <= id2; ++index3)
        b[index3, index2] = a[index1, index3 + num];
    }
  }

  public static void matrixvectormultiply(ref double[,] a, int i1, int i2, int j1, int j2, bool trans, ref double[] x, int ix1, int ix2, double alpha, ref double[] y, int iy1, int iy2, double beta)
  {
    if (!trans)
    {
      if (i1 > i2 | j1 > j2)
        return;
      if (beta == 0.0)
      {
        for (int index = iy1; index <= iy2; ++index)
          y[index] = 0.0;
      }
      else
      {
        for (int index = iy1; index <= iy2; ++index)
          y[index] = beta * y[index];
      }
      for (int index1 = i1; index1 <= i2; ++index1)
      {
        int num1 = ix1 - j1;
        double num2 = 0.0;
        for (int index2 = j1; index2 <= j2; ++index2)
          num2 += a[index1, index2] * x[index2 + num1];
        y[iy1 + index1 - i1] = y[iy1 + index1 - i1] + alpha * num2;
      }
    }
    else
    {
      if (i1 > i2 | j1 > j2)
        return;
      if (beta == 0.0)
      {
        for (int index = iy1; index <= iy2; ++index)
          y[index] = 0.0;
      }
      else
      {
        for (int index = iy1; index <= iy2; ++index)
          y[index] = beta * y[index];
      }
      for (int index1 = i1; index1 <= i2; ++index1)
      {
        double num1 = alpha * x[ix1 + index1 - i1];
        int num2 = j1 - iy1;
        for (int index2 = iy1; index2 <= iy2; ++index2)
          y[index2] = y[index2] + num1 * a[index1, index2 + num2];
      }
    }
  }

  public static double pythag2(double x, double y)
  {
    double val1 = System.Math.Abs(x);
    double val2 = System.Math.Abs(y);
    double num1 = System.Math.Max(val1, val2);
    double num2 = System.Math.Min(val1, val2);
    return num2 != 0.0 ? num1 * System.Math.Sqrt(1.0 + AP.Math.Sqr(num2 / num1)) : num1;
  }

  public static void matrixmatrixmultiply(ref double[,] a, int ai1, int ai2, int aj1, int aj2, bool transa, ref double[,] b, int bi1, int bi2, int bj1, int bj2, bool transb, double alpha, ref double[,] c, int ci1, int ci2, int cj1, int cj2, double beta, ref double[] work)
  {
    int index1 = 0;
    int val1_1;
    int val2_1;
    if (!transa)
    {
      val1_1 = ai2 - ai1 + 1;
      val2_1 = aj2 - aj1 + 1;
    }
    else
    {
      val1_1 = aj2 - aj1 + 1;
      val2_1 = ai2 - ai1 + 1;
    }
    int val1_2;
    int val2_2;
    if (!transb)
    {
      val1_2 = bi2 - bi1 + 1;
      val2_2 = bj2 - bj1 + 1;
    }
    else
    {
      val1_2 = bj2 - bj1 + 1;
      val2_2 = bi2 - bi1 + 1;
    }
    if (val1_1 <= 0 | val2_1 <= 0 | val1_2 <= 0 | val2_2 <= 0)
      return;
    int num1 = val1_1;
    int val2_3 = System.Math.Max(val1_1, val2_1);
    int index2 = System.Math.Max(System.Math.Max(val1_2, val2_3), val2_2);
    work[1] = 0.0;
    work[index2] = 0.0;
    if (beta == 0.0)
    {
      for (int index3 = ci1; index3 <= ci2; ++index3)
      {
        for (int index4 = cj1; index4 <= cj2; ++index4)
          c[index3, index4] = 0.0;
      }
    }
    else
    {
      for (int index3 = ci1; index3 <= ci2; ++index3)
      {
        for (int index4 = cj1; index4 <= cj2; ++index4)
          c[index3, index4] = beta * c[index3, index4];
      }
    }
    if (!transa & !transb)
    {
      for (int index3 = ai1; index3 <= ai2; ++index3)
      {
        for (int index4 = bi1; index4 <= bi2; ++index4)
        {
          double num2 = alpha * a[index3, aj1 + index4 - bi1];
          int index5 = ci1 + index3 - ai1;
          int num3 = bj1 - cj1;
          for (int index6 = cj1; index6 <= cj2; ++index6)
            c[index5, index6] = c[index5, index6] + num2 * b[index4, index6 + num3];
        }
      }
    }
    else if (!transa & transb)
    {
      if (val1_1 * val2_1 < val1_2 * val2_2)
      {
        for (int index3 = bi1; index3 <= bi2; ++index3)
        {
          for (int index4 = ai1; index4 <= ai2; ++index4)
          {
            int num2 = bj1 - aj1;
            double num3 = 0.0;
            for (int index5 = aj1; index5 <= aj2; ++index5)
              num3 += a[index4, index5] * b[index3, index5 + num2];
            c[ci1 + index4 - ai1, cj1 + index3 - bi1] = c[ci1 + index4 - ai1, cj1 + index3 - bi1] + alpha * num3;
          }
        }
      }
      else
      {
        for (int index3 = ai1; index3 <= ai2; ++index3)
        {
          for (int index4 = bi1; index4 <= bi2; ++index4)
          {
            int num2 = bj1 - aj1;
            double num3 = 0.0;
            for (int index5 = aj1; index5 <= aj2; ++index5)
              num3 += a[index3, index5] * b[index4, index5 + num2];
            c[ci1 + index3 - ai1, cj1 + index4 - bi1] = c[ci1 + index3 - ai1, cj1 + index4 - bi1] + alpha * num3;
          }
        }
      }
    }
    else if (transa & !transb)
    {
      for (int index3 = aj1; index3 <= aj2; ++index3)
      {
        for (int index4 = bi1; index4 <= bi2; ++index4)
        {
          double num2 = alpha * a[ai1 + index4 - bi1, index3];
          int index5 = ci1 + index3 - aj1;
          int num3 = bj1 - cj1;
          for (int index6 = cj1; index6 <= cj2; ++index6)
            c[index5, index6] = c[index5, index6] + num2 * b[index4, index6 + num3];
        }
      }
    }
    else
    {
      if (!(transa & transb))
        return;
      if (val1_1 * val2_1 < val1_2 * val2_2)
      {
        for (int index3 = bi1; index3 <= bi2; ++index3)
        {
          for (int index4 = 1; index4 <= num1; ++index4)
            work[index4] = 0.0;
          for (int index4 = ai1; index4 <= ai2; ++index4)
          {
            double num2 = alpha * b[index3, bj1 + index4 - ai1];
            index1 = cj1 + index3 - bi1;
            int num3 = aj1 - 1;
            for (int index5 = 1; index5 <= num1; ++index5)
              work[index5] = work[index5] + num2 * a[index4, index5 + num3];
          }
          int num4 = 1 - ci1;
          for (int index4 = ci1; index4 <= ci2; ++index4)
            c[index4, index1] = c[index4, index1] + work[index4 + num4];
        }
      }
      else
      {
        for (int index3 = aj1; index3 <= aj2; ++index3)
        {
          int num2 = ai2 - ai1 + 1;
          int num3 = ai1 - 1;
          for (int index4 = 1; index4 <= num2; ++index4)
            work[index4] = a[index4 + num3, index3];
          for (int index4 = bi1; index4 <= bi2; ++index4)
          {
            int num4 = bj1 - 1;
            double num5 = 0.0;
            for (int index5 = 1; index5 <= num2; ++index5)
              num5 += work[index5] * b[index4, index5 + num4];
            c[ci1 + index3 - aj1, cj1 + index4 - bi1] = c[ci1 + index3 - aj1, cj1 + index4 - bi1] + alpha * num5;
          }
        }
      }
    }
  }
}
