internal class bdsvd
{
  public static bool rmatrixbdsvd(ref double[] d, double[] e, int n, bool isupper, bool isfractionalaccuracyrequired, ref double[,] u, int nru, ref double[,] c, int ncc, ref double[,] vt, int ncvt)
  {
    double[] numArray = new double[0];
    double[] e1 = new double[0];
    e = (double[]) e.Clone();
    double[] d1 = new double[n + 1];
    int num1 = -1;
    for (int index = 1; index <= n; ++index)
      d1[index] = d[index + num1];
    if (n > 1)
    {
      e1 = new double[n - 1 + 1];
      int num2 = -1;
      for (int index = 1; index <= n - 1; ++index)
        e1[index] = e[index + num2];
    }
    bool flag = bdsvd.bidiagonalsvddecompositioninternal(ref d1, e1, n, isupper, isfractionalaccuracyrequired, ref u, 0, nru, ref c, 0, ncc, ref vt, 0, ncvt);
    int num3 = 1;
    for (int index = 0; index <= n - 1; ++index)
      d[index] = d1[index + num3];
    return flag;
  }

  public static bool bidiagonalsvddecomposition(ref double[] d, double[] e, int n, bool isupper, bool isfractionalaccuracyrequired, ref double[,] u, int nru, ref double[,] c, int ncc, ref double[,] vt, int ncvt)
  {
    e = (double[]) e.Clone();
    return bdsvd.bidiagonalsvddecompositioninternal(ref d, e, n, isupper, isfractionalaccuracyrequired, ref u, 1, nru, ref c, 1, ncc, ref vt, 1, ncvt);
  }

  private static bool bidiagonalsvddecompositioninternal(ref double[] d, double[] e, int n, bool isupper, bool isfractionalaccuracyrequired, ref double[,] u, int ustart, int nru, ref double[,] c, int cstart, int ncc, ref double[,] vt, int vstart, int ncvt)
  {
    int index1 = 0;
    double num1 = 0.0;
    double num2 = 0.0;
    double cs1 = 0.0;
    double sn1 = 0.0;
    double num3 = 0.0;
    double ssmin1 = 0.0;
    double ssmin2 = 0.0;
    double ssmax = 0.0;
    double num4 = 0.0;
    double num5 = 0.0;
    double sn2 = 0.0;
    double[] numArray1 = new double[0];
    double[] numArray2 = new double[0];
    double[] numArray3 = new double[0];
    double[] numArray4 = new double[0];
    double[] numArray5 = new double[0];
    double[] numArray6 = new double[0];
    double[] numArray7 = new double[0];
    double[] numArray8 = new double[0];
    double r = 0.0;
    e = (double[]) e.Clone();
    bool flag1 = true;
    if (n == 0)
      return flag1;
    if (n == 1)
    {
      if (d[1] < 0.0)
      {
        d[1] = -d[1];
        if (ncvt > 0)
        {
          for (int index2 = vstart; index2 <= vstart + ncvt - 1; ++index2)
            vt[vstart, index2] = -1.0 * vt[vstart, index2];
        }
      }
      return flag1;
    }
    double[] c1 = new double[n - 1 + 1];
    double[] s1 = new double[n - 1 + 1];
    double[] c2 = new double[n - 1 + 1];
    double[] s2 = new double[n - 1 + 1];
    int m2 = ustart + System.Math.Max(nru - 1, 0);
    int n2_1 = vstart + System.Math.Max(ncvt - 1, 0);
    int n2_2 = cstart + System.Math.Max(ncc - 1, 0);
    double[] work1 = new double[m2 + 1];
    double[] work2 = new double[n2_1 + 1];
    double[] work3 = new double[n2_2 + 1];
    int num6 = 12;
    bool isforward = true;
    double[] numArray9 = new double[n + 1];
    for (int index2 = 1; index2 <= n - 1; ++index2)
      numArray9[index2] = e[index2];
    e = new double[n + 1];
    for (int index2 = 1; index2 <= n - 1; ++index2)
      e[index2] = numArray9[index2];
    e[n] = 0.0;
    int num7 = 0;
    double num8 = 5E-16;
    double num9 = 1E-300;
    if (!isupper)
    {
      for (int index2 = 1; index2 <= n - 1; ++index2)
      {
        rotations.generaterotation(d[index2], e[index2], ref cs1, ref sn2, ref num3);
        d[index2] = num3;
        e[index2] = sn2 * d[index2 + 1];
        d[index2 + 1] = cs1 * d[index2 + 1];
        c1[index2] = cs1;
        s1[index2] = sn2;
      }
      if (nru > 0)
        rotations.applyrotationsfromtheright(isforward, ustart, m2, 1 + ustart - 1, n + ustart - 1, ref c1, ref s1, ref u, ref work1);
      if (ncc > 0)
        rotations.applyrotationsfromtheleft(isforward, 1 + cstart - 1, n + cstart - 1, cstart, n2_2, ref c1, ref s1, ref c, ref work3);
    }
    double num10 = System.Math.Max(10.0, System.Math.Min(100.0, System.Math.Pow(num8, -0.125))) * num8;
    if (!isfractionalaccuracyrequired)
      num10 = -num10;
    double val1_1 = 0.0;
    for (int index2 = 1; index2 <= n; ++index2)
      val1_1 = System.Math.Max(val1_1, System.Math.Abs(d[index2]));
    for (int index2 = 1; index2 <= n - 1; ++index2)
      val1_1 = System.Math.Max(val1_1, System.Math.Abs(e[index2]));
    double val1_2 = 0.0;
    double num11;
    if (num10 >= 0.0)
    {
      double val1_3 = System.Math.Abs(d[1]);
      if (val1_3 != 0.0)
      {
        double val2 = val1_3;
        for (int index2 = 2; index2 <= n; ++index2)
        {
          val2 = System.Math.Abs(d[index2]) * (val2 / (val2 + System.Math.Abs(e[index2 - 1])));
          val1_3 = System.Math.Min(val1_3, val2);
          if (val1_3 == 0.0)
            break;
        }
      }
      double num12 = val1_3 / System.Math.Sqrt((double) n);
      num11 = System.Math.Max(num10 * num12, (double) (num6 * n * n) * num9);
    }
    else
      num11 = System.Math.Max(System.Math.Abs(num10) * val1_1, (double) (num6 * n * n) * num9);
    int num13 = num6 * n * n;
    int num14 = 0;
    int num15 = -1;
    int num16 = -1;
    int index3 = n;
    while (index3 > 1)
    {
      if (num14 > num13)
        return false;
      if (num10 < 0.0 & System.Math.Abs(d[index3]) <= num11)
        d[index3] = 0.0;
      double val1_3 = System.Math.Abs(d[index3]);
      double val1_4 = val1_3;
      bool flag2 = false;
      for (int index2 = 1; index2 <= index3 - 1; ++index2)
      {
        index1 = index3 - index2;
        double num12 = System.Math.Abs(d[index1]);
        double val2 = System.Math.Abs(e[index1]);
        if (num10 < 0.0 & num12 <= num11)
          d[index1] = 0.0;
        if (val2 <= num11)
        {
          flag2 = true;
          break;
        }
        val1_4 = System.Math.Min(val1_4, num12);
        val1_3 = System.Math.Max(val1_3, System.Math.Max(num12, val2));
      }
      if (!flag2)
      {
        index1 = 0;
      }
      else
      {
        e[index1] = 0.0;
        if (index1 == index3 - 1)
        {
          --index3;
          continue;
        }
      }
      ++index1;
      if (index1 == index3 - 1)
      {
        bdsvd.svdv2x2(d[index3 - 1], e[index3 - 1], d[index3], ref ssmin2, ref ssmax, ref num5, ref num2, ref num4, ref num1);
        d[index3 - 1] = ssmax;
        e[index3 - 1] = 0.0;
        d[index3] = ssmin2;
        if (ncvt > 0)
        {
          int index2 = index3 + (vstart - 1);
          int index4 = index3 - 1 + (vstart - 1);
          for (int index5 = vstart; index5 <= n2_1; ++index5)
            work2[index5] = num2 * vt[index4, index5];
          for (int index5 = vstart; index5 <= n2_1; ++index5)
            work2[index5] = work2[index5] + num5 * vt[index2, index5];
          for (int index5 = vstart; index5 <= n2_1; ++index5)
            vt[index2, index5] = num2 * vt[index2, index5];
          for (int index5 = vstart; index5 <= n2_1; ++index5)
            vt[index2, index5] = vt[index2, index5] - num5 * vt[index4, index5];
          for (int index5 = vstart; index5 <= n2_1; ++index5)
            vt[index4, index5] = work2[index5];
        }
        if (nru > 0)
        {
          int index2 = index3 + ustart - 1;
          int index4 = index3 - 1 + ustart - 1;
          for (int index5 = ustart; index5 <= m2; ++index5)
            work1[index5] = num1 * u[index5, index4];
          for (int index5 = ustart; index5 <= m2; ++index5)
            work1[index5] = work1[index5] + num4 * u[index5, index2];
          for (int index5 = ustart; index5 <= m2; ++index5)
            u[index5, index2] = num1 * u[index5, index2];
          for (int index5 = ustart; index5 <= m2; ++index5)
            u[index5, index2] = u[index5, index2] - num4 * u[index5, index4];
          for (int index5 = ustart; index5 <= m2; ++index5)
            u[index5, index4] = work1[index5];
        }
        if (ncc > 0)
        {
          int index2 = index3 + cstart - 1;
          int index4 = index3 - 1 + cstart - 1;
          for (int index5 = cstart; index5 <= n2_2; ++index5)
            work3[index5] = num1 * c[index4, index5];
          for (int index5 = cstart; index5 <= n2_2; ++index5)
            work3[index5] = work3[index5] + num4 * c[index2, index5];
          for (int index5 = cstart; index5 <= n2_2; ++index5)
            c[index2, index5] = num1 * c[index2, index5];
          for (int index5 = cstart; index5 <= n2_2; ++index5)
            c[index2, index5] = c[index2, index5] - num4 * c[index4, index5];
          for (int index5 = cstart; index5 <= n2_2; ++index5)
            c[index4, index5] = work3[index5];
        }
        index3 -= 2;
      }
      else
      {
        bool flag3 = false;
        if (num7 == 1 & System.Math.Abs(d[index1]) < 0.001 * System.Math.Abs(d[index3]))
          flag3 = true;
        if (num7 == 2 & System.Math.Abs(d[index3]) < 0.001 * System.Math.Abs(d[index1]))
          flag3 = true;
        if (index1 != num15 | index3 != num16 | flag3)
          num7 = System.Math.Abs(d[index1]) < System.Math.Abs(d[index3]) ? 2 : 1;
        if (num7 == 1)
        {
          if (System.Math.Abs(e[index3 - 1]) <= System.Math.Abs(num10) * System.Math.Abs(d[index3]) | num10 < 0.0 & System.Math.Abs(e[index3 - 1]) <= num11)
          {
            e[index3 - 1] = 0.0;
            continue;
          }
          if (num10 >= 0.0)
          {
            double val2 = System.Math.Abs(d[index1]);
            val1_2 = val2;
            bool flag4 = false;
            for (int index2 = index1; index2 <= index3 - 1; ++index2)
            {
              if (System.Math.Abs(e[index2]) <= num10 * val2)
              {
                e[index2] = 0.0;
                flag4 = true;
                break;
              }
              val2 = System.Math.Abs(d[index2 + 1]) * (val2 / (val2 + System.Math.Abs(e[index2])));
              val1_2 = System.Math.Min(val1_2, val2);
            }
            if (flag4)
              continue;
          }
        }
        else
        {
          if (System.Math.Abs(e[index1]) <= System.Math.Abs(num10) * System.Math.Abs(d[index1]) | num10 < 0.0 & System.Math.Abs(e[index1]) <= num11)
          {
            e[index1] = 0.0;
            continue;
          }
          if (num10 >= 0.0)
          {
            double val2 = System.Math.Abs(d[index3]);
            val1_2 = val2;
            bool flag4 = false;
            for (int index2 = index3 - 1; index2 >= index1; --index2)
            {
              if (System.Math.Abs(e[index2]) <= num10 * val2)
              {
                e[index2] = 0.0;
                flag4 = true;
                break;
              }
              val2 = System.Math.Abs(d[index2]) * (val2 / (val2 + System.Math.Abs(e[index2])));
              val1_2 = System.Math.Min(val1_2, val2);
            }
            if (flag4)
              continue;
          }
        }
        num15 = index1;
        num16 = index3;
        if (num10 >= 0.0 & (double) n * num10 * (val1_2 / val1_3) <= System.Math.Max(num8, 0.01 * num10))
        {
          ssmin1 = 0.0;
        }
        else
        {
          double num12;
          if (num7 == 1)
          {
            num12 = System.Math.Abs(d[index1]);
            bdsvd.svd2x2(d[index3 - 1], e[index3 - 1], d[index3], ref ssmin1, ref num3);
          }
          else
          {
            num12 = System.Math.Abs(d[index3]);
            bdsvd.svd2x2(d[index1], e[index1], d[index1 + 1], ref ssmin1, ref num3);
          }
          if (num12 > 0.0 && AP.Math.Sqr(ssmin1 / num12) < num8)
            ssmin1 = 0.0;
        }
        num14 = num14 + index3 - index1;
        double cs2;
        if (ssmin1 == 0.0)
        {
          if (num7 == 1)
          {
            cs1 = 1.0;
            cs2 = 1.0;
            for (int index2 = index1; index2 <= index3 - 1; ++index2)
            {
              rotations.generaterotation(d[index2] * cs1, e[index2], ref cs1, ref sn2, ref num3);
              if (index2 > index1)
                e[index2 - 1] = sn1 * num3;
              rotations.generaterotation(cs2 * num3, d[index2 + 1] * sn2, ref cs2, ref sn1, ref r);
              d[index2] = r;
              c1[index2 - index1 + 1] = cs1;
              s1[index2 - index1 + 1] = sn2;
              c2[index2 - index1 + 1] = cs2;
              s2[index2 - index1 + 1] = sn1;
            }
            double num12 = d[index3] * cs1;
            d[index3] = num12 * cs2;
            e[index3 - 1] = num12 * sn1;
            if (ncvt > 0)
              rotations.applyrotationsfromtheleft(isforward, index1 + vstart - 1, index3 + vstart - 1, vstart, n2_1, ref c1, ref s1, ref vt, ref work2);
            if (nru > 0)
              rotations.applyrotationsfromtheright(isforward, ustart, m2, index1 + ustart - 1, index3 + ustart - 1, ref c2, ref s2, ref u, ref work1);
            if (ncc > 0)
              rotations.applyrotationsfromtheleft(isforward, index1 + cstart - 1, index3 + cstart - 1, cstart, n2_2, ref c2, ref s2, ref c, ref work3);
            if (System.Math.Abs(e[index3 - 1]) <= num11)
              e[index3 - 1] = 0.0;
          }
          else
          {
            cs1 = 1.0;
            cs2 = 1.0;
            for (int index2 = index3; index2 >= index1 + 1; --index2)
            {
              rotations.generaterotation(d[index2] * cs1, e[index2 - 1], ref cs1, ref sn2, ref num3);
              if (index2 < index3)
                e[index2] = sn1 * num3;
              rotations.generaterotation(cs2 * num3, d[index2 - 1] * sn2, ref cs2, ref sn1, ref r);
              d[index2] = r;
              c1[index2 - index1] = cs1;
              s1[index2 - index1] = -sn2;
              c2[index2 - index1] = cs2;
              s2[index2 - index1] = -sn1;
            }
            double num12 = d[index1] * cs1;
            d[index1] = num12 * cs2;
            e[index1] = num12 * sn1;
            if (ncvt > 0)
              rotations.applyrotationsfromtheleft(!isforward, index1 + vstart - 1, index3 + vstart - 1, vstart, n2_1, ref c2, ref s2, ref vt, ref work2);
            if (nru > 0)
              rotations.applyrotationsfromtheright(!isforward, ustart, m2, index1 + ustart - 1, index3 + ustart - 1, ref c1, ref s1, ref u, ref work1);
            if (ncc > 0)
              rotations.applyrotationsfromtheleft(!isforward, index1 + cstart - 1, index3 + cstart - 1, cstart, n2_2, ref c1, ref s1, ref c, ref work3);
            if (System.Math.Abs(e[index1]) <= num11)
              e[index1] = 0.0;
          }
        }
        else if (num7 == 1)
        {
          double f1 = (System.Math.Abs(d[index1]) - ssmin1) * (bdsvd.extsignbdsqr(1.0, d[index1]) + ssmin1 / d[index1]);
          double g = e[index1];
          for (int index2 = index1; index2 <= index3 - 1; ++index2)
          {
            rotations.generaterotation(f1, g, ref num2, ref num5, ref num3);
            if (index2 > index1)
              e[index2 - 1] = num3;
            double f2 = num2 * d[index2] + num5 * e[index2];
            e[index2] = num2 * e[index2] - num5 * d[index2];
            g = num5 * d[index2 + 1];
            d[index2 + 1] = num2 * d[index2 + 1];
            rotations.generaterotation(f2, g, ref num1, ref num4, ref num3);
            d[index2] = num3;
            f1 = num1 * e[index2] + num4 * d[index2 + 1];
            d[index2 + 1] = num1 * d[index2 + 1] - num4 * e[index2];
            if (index2 < index3 - 1)
            {
              g = num4 * e[index2 + 1];
              e[index2 + 1] = num1 * e[index2 + 1];
            }
            c1[index2 - index1 + 1] = num2;
            s1[index2 - index1 + 1] = num5;
            c2[index2 - index1 + 1] = num1;
            s2[index2 - index1 + 1] = num4;
          }
          e[index3 - 1] = f1;
          if (ncvt > 0)
            rotations.applyrotationsfromtheleft(isforward, index1 + vstart - 1, index3 + vstart - 1, vstart, n2_1, ref c1, ref s1, ref vt, ref work2);
          if (nru > 0)
            rotations.applyrotationsfromtheright(isforward, ustart, m2, index1 + ustart - 1, index3 + ustart - 1, ref c2, ref s2, ref u, ref work1);
          if (ncc > 0)
            rotations.applyrotationsfromtheleft(isforward, index1 + cstart - 1, index3 + cstart - 1, cstart, n2_2, ref c2, ref s2, ref c, ref work3);
          if (System.Math.Abs(e[index3 - 1]) <= num11)
            e[index3 - 1] = 0.0;
        }
        else
        {
          double f1 = (System.Math.Abs(d[index3]) - ssmin1) * (bdsvd.extsignbdsqr(1.0, d[index3]) + ssmin1 / d[index3]);
          double g = e[index3 - 1];
          for (int index2 = index3; index2 >= index1 + 1; --index2)
          {
            rotations.generaterotation(f1, g, ref num2, ref num5, ref num3);
            if (index2 < index3)
              e[index2] = num3;
            double f2 = num2 * d[index2] + num5 * e[index2 - 1];
            e[index2 - 1] = num2 * e[index2 - 1] - num5 * d[index2];
            g = num5 * d[index2 - 1];
            d[index2 - 1] = num2 * d[index2 - 1];
            rotations.generaterotation(f2, g, ref num1, ref num4, ref num3);
            d[index2] = num3;
            f1 = num1 * e[index2 - 1] + num4 * d[index2 - 1];
            d[index2 - 1] = num1 * d[index2 - 1] - num4 * e[index2 - 1];
            if (index2 > index1 + 1)
            {
              g = num4 * e[index2 - 2];
              e[index2 - 2] = num1 * e[index2 - 2];
            }
            c1[index2 - index1] = num2;
            s1[index2 - index1] = -num5;
            c2[index2 - index1] = num1;
            s2[index2 - index1] = -num4;
          }
          e[index1] = f1;
          if (System.Math.Abs(e[index1]) <= num11)
            e[index1] = 0.0;
          if (ncvt > 0)
            rotations.applyrotationsfromtheleft(!isforward, index1 + vstart - 1, index3 + vstart - 1, vstart, n2_1, ref c2, ref s2, ref vt, ref work2);
          if (nru > 0)
            rotations.applyrotationsfromtheright(!isforward, ustart, m2, index1 + ustart - 1, index3 + ustart - 1, ref c1, ref s1, ref u, ref work1);
          if (ncc > 0)
            rotations.applyrotationsfromtheleft(!isforward, index1 + cstart - 1, index3 + cstart - 1, cstart, n2_2, ref c1, ref s1, ref c, ref work3);
        }
      }
    }
    for (int index2 = 1; index2 <= n; ++index2)
    {
      if (d[index2] < 0.0)
      {
        d[index2] = -d[index2];
        if (ncvt > 0)
        {
          for (int index4 = vstart; index4 <= n2_1; ++index4)
            vt[index2 + vstart - 1, index4] = -1.0 * vt[index2 + vstart - 1, index4];
        }
      }
    }
    for (int index2 = 1; index2 <= n - 1; ++index2)
    {
      int index4 = 1;
      double num12 = d[1];
      for (int index5 = 2; index5 <= n + 1 - index2; ++index5)
      {
        if (d[index5] <= num12)
        {
          index4 = index5;
          num12 = d[index5];
        }
      }
      if (index4 != n + 1 - index2)
      {
        d[index4] = d[n + 1 - index2];
        d[n + 1 - index2] = num12;
        if (ncvt > 0)
        {
          int num17 = n + 1 - index2;
          for (int index5 = vstart; index5 <= n2_1; ++index5)
            work2[index5] = vt[index4 + vstart - 1, index5];
          for (int index5 = vstart; index5 <= n2_1; ++index5)
            vt[index4 + vstart - 1, index5] = vt[num17 + vstart - 1, index5];
          for (int index5 = vstart; index5 <= n2_1; ++index5)
            vt[num17 + vstart - 1, index5] = work2[index5];
        }
        if (nru > 0)
        {
          int num17 = n + 1 - index2;
          for (int index5 = ustart; index5 <= m2; ++index5)
            work1[index5] = u[index5, index4 + ustart - 1];
          for (int index5 = ustart; index5 <= m2; ++index5)
            u[index5, index4 + ustart - 1] = u[index5, num17 + ustart - 1];
          for (int index5 = ustart; index5 <= m2; ++index5)
            u[index5, num17 + ustart - 1] = work1[index5];
        }
        if (ncc > 0)
        {
          int num17 = n + 1 - index2;
          for (int index5 = cstart; index5 <= n2_2; ++index5)
            work3[index5] = c[index4 + cstart - 1, index5];
          for (int index5 = cstart; index5 <= n2_2; ++index5)
            c[index4 + cstart - 1, index5] = c[num17 + cstart - 1, index5];
          for (int index5 = cstart; index5 <= n2_2; ++index5)
            c[num17 + cstart - 1, index5] = work3[index5];
        }
      }
    }
    return flag1;
  }

  private static double extsignbdsqr(double a, double b)
  {
    return b < 0.0 ? -System.Math.Abs(a) : System.Math.Abs(a);
  }

  private static void svd2x2(double f, double g, double h, ref double ssmin, ref double ssmax)
  {
    double val1_1 = System.Math.Abs(f);
    double val2_1 = System.Math.Abs(g);
    double val2_2 = System.Math.Abs(h);
    double num1 = System.Math.Min(val1_1, val2_2);
    double val1_2 = System.Math.Max(val1_1, val2_2);
    if (num1 == 0.0)
    {
      ssmin = 0.0;
      if (val1_2 == 0.0)
        ssmax = val2_1;
      else
        ssmax = System.Math.Max(val1_2, val2_1) * System.Math.Sqrt(1.0 + AP.Math.Sqr(System.Math.Min(val1_2, val2_1) / System.Math.Max(val1_2, val2_1)));
    }
    else if (val2_1 < val1_2)
    {
      double num2 = 1.0 + num1 / val1_2;
      double num3 = (val1_2 - num1) / val1_2;
      double num4 = AP.Math.Sqr(val2_1 / val1_2);
      double num5 = 2.0 / (System.Math.Sqrt(num2 * num2 + num4) + System.Math.Sqrt(num3 * num3 + num4));
      ssmin = num1 * num5;
      ssmax = val1_2 / num5;
    }
    else
    {
      double num2 = val1_2 / val2_1;
      if (num2 == 0.0)
      {
        ssmin = num1 * val1_2 / val2_1;
        ssmax = val2_1;
      }
      else
      {
        double num3 = 1.0 + num1 / val1_2;
        double num4 = (val1_2 - num1) / val1_2;
        double num5 = 1.0 / (System.Math.Sqrt(1.0 + AP.Math.Sqr(num3 * num2)) + System.Math.Sqrt(1.0 + AP.Math.Sqr(num4 * num2)));
        ssmin = num1 * num5 * num2;
        ssmin = ssmin + ssmin;
        ssmax = val2_1 / (num5 + num5);
      }
    }
  }

  private static void svdv2x2(double f, double g, double h, ref double ssmin, ref double ssmax, ref double snr, ref double csr, ref double snl, ref double csl)
  {
    double num1 = 0.0;
    double num2 = 0.0;
    double num3 = 0.0;
    double num4 = 0.0;
    double b1 = 0.0;
    double b2 = f;
    double num5 = System.Math.Abs(b2);
    double num6 = h;
    double num7 = System.Math.Abs(h);
    int num8 = 1;
    bool flag1 = num7 > num5;
    if (flag1)
    {
      num8 = 3;
      double num9 = b2;
      b2 = num6;
      num6 = num9;
      double num10 = num5;
      num5 = num7;
      num7 = num10;
    }
    double b3 = g;
    double num11 = System.Math.Abs(b3);
    if (num11 == 0.0)
    {
      ssmin = num7;
      ssmax = num5;
      num1 = 1.0;
      num2 = 1.0;
      num3 = 0.0;
      num4 = 0.0;
    }
    else
    {
      bool flag2 = true;
      if (num11 > num5)
      {
        num8 = 2;
        if (num5 / num11 < 5E-16)
        {
          flag2 = false;
          ssmax = num11;
          if (num7 > 1.0)
          {
            double num9 = num11 / num7;
            ssmin = num5 / num9;
          }
          else
          {
            double num9 = num5 / num11;
            ssmin = num9 * num7;
          }
          num1 = 1.0;
          num3 = num6 / b3;
          num4 = 1.0;
          num2 = b2 / b3;
        }
      }
      if (flag2)
      {
        double a = num5 - num7;
        double num9 = a != num5 ? a / num5 : 1.0;
        double num10 = b3 / b2;
        double num12 = 2.0 - num9;
        double num13 = num10 * num10;
        double num14 = System.Math.Sqrt(num12 * num12 + num13);
        double num15 = num9 != 0.0 ? System.Math.Sqrt(num9 * num9 + num13) : System.Math.Abs(num10);
        double num16 = 0.5 * (num14 + num15);
        ssmin = num7 / num16;
        ssmax = num5 * num16;
        double num17 = num13 != 0.0 ? (num10 / (num14 + num12) + num10 / (num15 + num9)) * (1.0 + num16) : (num9 != 0.0 ? b3 / bdsvd.extsignbdsqr(a, b2) + num10 / num12 : bdsvd.extsignbdsqr(2.0, b2) * bdsvd.extsignbdsqr(1.0, b3));
        double num18 = System.Math.Sqrt(num17 * num17 + 4.0);
        num2 = 2.0 / num18;
        num4 = num17 / num18;
        num1 = (num2 + num4 * num10) / num16;
        num3 = num6 / b2 * num4 / num16;
      }
    }
    if (flag1)
    {
      csl = num4;
      snl = num2;
      csr = num3;
      snr = num1;
    }
    else
    {
      csl = num1;
      snl = num3;
      csr = num2;
      snr = num4;
    }
    if (num8 == 1)
      b1 = bdsvd.extsignbdsqr(1.0, csr) * bdsvd.extsignbdsqr(1.0, csl) * bdsvd.extsignbdsqr(1.0, f);
    if (num8 == 2)
      b1 = bdsvd.extsignbdsqr(1.0, snr) * bdsvd.extsignbdsqr(1.0, csl) * bdsvd.extsignbdsqr(1.0, g);
    if (num8 == 3)
      b1 = bdsvd.extsignbdsqr(1.0, snr) * bdsvd.extsignbdsqr(1.0, snl) * bdsvd.extsignbdsqr(1.0, h);
    ssmax = bdsvd.extsignbdsqr(ssmax, b1);
    ssmin = bdsvd.extsignbdsqr(ssmin, b1 * bdsvd.extsignbdsqr(1.0, f) * bdsvd.extsignbdsqr(1.0, h));
  }
}
