using System;

internal class bidiagonal
{
  public static void rmatrixbd(ref double[,] a, int m, int n, ref double[] tauq, ref double[] taup)
  {
    double[] numArray1 = new double[0];
    double[] numArray2 = new double[0];
    double tau = 0.0;
    if (n <= 0 | m <= 0)
      return;
    Math.Min(m, n);
    int num1 = Math.Max(m, n);
    double[] work = new double[num1 + 1];
    double[] numArray3 = new double[num1 + 1];
    if (m >= n)
    {
      tauq = new double[n - 1 + 1];
      taup = new double[n - 1 + 1];
    }
    else
    {
      tauq = new double[m - 1 + 1];
      taup = new double[m - 1 + 1];
    }
    if (m >= n)
    {
      for (int m1 = 0; m1 <= n - 1; ++m1)
      {
        int num2 = m1 - 1;
        for (int index = 1; index <= m - m1; ++index)
          numArray3[index] = a[index + num2, m1];
        reflections.generatereflection(ref numArray3, m - m1, ref tau);
        tauq[m1] = tau;
        int num3 = 1 - m1;
        for (int index = m1; index <= m - 1; ++index)
          a[index, m1] = numArray3[index + num3];
        numArray3[1] = 1.0;
        reflections.applyreflectionfromtheleft(ref a, tau, ref numArray3, m1, m - 1, m1 + 1, n - 1, ref work);
        if (m1 < n - 1)
        {
          int num4 = m1 + 1 - 1;
          for (int index = 1; index <= n - m1 - 1; ++index)
            numArray3[index] = a[m1, index + num4];
          reflections.generatereflection(ref numArray3, n - 1 - m1, ref tau);
          taup[m1] = tau;
          int num5 = 1 - (m1 + 1);
          for (int index = m1 + 1; index <= n - 1; ++index)
            a[m1, index] = numArray3[index + num5];
          numArray3[1] = 1.0;
          reflections.applyreflectionfromtheright(ref a, tau, ref numArray3, m1 + 1, m - 1, m1 + 1, n - 1, ref work);
        }
        else
          taup[m1] = 0.0;
      }
    }
    else
    {
      for (int n1 = 0; n1 <= m - 1; ++n1)
      {
        int num2 = n1 - 1;
        for (int index = 1; index <= n - n1; ++index)
          numArray3[index] = a[n1, index + num2];
        reflections.generatereflection(ref numArray3, n - n1, ref tau);
        taup[n1] = tau;
        int num3 = 1 - n1;
        for (int index = n1; index <= n - 1; ++index)
          a[n1, index] = numArray3[index + num3];
        numArray3[1] = 1.0;
        reflections.applyreflectionfromtheright(ref a, tau, ref numArray3, n1 + 1, m - 1, n1, n - 1, ref work);
        if (n1 < m - 1)
        {
          int num4 = n1 + 1 - 1;
          for (int index = 1; index <= m - 1 - n1; ++index)
            numArray3[index] = a[index + num4, n1];
          reflections.generatereflection(ref numArray3, m - 1 - n1, ref tau);
          tauq[n1] = tau;
          int num5 = 1 - (n1 + 1);
          for (int index = n1 + 1; index <= m - 1; ++index)
            a[index, n1] = numArray3[index + num5];
          numArray3[1] = 1.0;
          reflections.applyreflectionfromtheleft(ref a, tau, ref numArray3, n1 + 1, m - 1, n1 + 1, n - 1, ref work);
        }
        else
          tauq[n1] = 0.0;
      }
    }
  }

  public static void rmatrixbdunpackq(ref double[,] qp, int m, int n, ref double[] tauq, int qcolumns, ref double[,] q)
  {
    if (m == 0 | n == 0 | qcolumns == 0)
      return;
    q = new double[m - 1 + 1, qcolumns - 1 + 1];
    for (int index1 = 0; index1 <= m - 1; ++index1)
    {
      for (int index2 = 0; index2 <= qcolumns - 1; ++index2)
        q[index1, index2] = index1 != index2 ? 0.0 : 1.0;
    }
    bidiagonal.rmatrixbdmultiplybyq(ref qp, m, n, ref tauq, ref q, m, qcolumns, false, false);
  }

  public static void rmatrixbdmultiplybyq(ref double[,] qp, int m, int n, ref double[] tauq, ref double[,] z, int zrows, int zcolumns, bool fromtheright, bool dotranspose)
  {
    double[] numArray1 = new double[0];
    double[] numArray2 = new double[0];
    if (m <= 0 | n <= 0 | zrows <= 0 | zcolumns <= 0)
      return;
    int num1 = Math.Max(Math.Max(Math.Max(m, n), zrows), zcolumns);
    double[] v = new double[num1 + 1];
    double[] work = new double[num1 + 1];
    if (m >= n)
    {
      int num2;
      int num3;
      int num4;
      if (fromtheright)
      {
        num2 = 0;
        num3 = n - 1;
        num4 = 1;
      }
      else
      {
        num2 = n - 1;
        num3 = 0;
        num4 = -1;
      }
      if (dotranspose)
      {
        int num5 = num2;
        num2 = num3;
        num3 = num5;
        num4 = -num4;
      }
      int index1 = num2;
      do
      {
        int num5 = index1 - 1;
        for (int index2 = 1; index2 <= m - index1; ++index2)
          v[index2] = qp[index2 + num5, index1];
        v[1] = 1.0;
        if (fromtheright)
          reflections.applyreflectionfromtheright(ref z, tauq[index1], ref v, 0, zrows - 1, index1, m - 1, ref work);
        else
          reflections.applyreflectionfromtheleft(ref z, tauq[index1], ref v, index1, m - 1, 0, zcolumns - 1, ref work);
        index1 += num4;
      }
      while (index1 != num3 + num4);
    }
    else
    {
      int num2;
      int num3;
      int num4;
      if (fromtheright)
      {
        num2 = 0;
        num3 = m - 2;
        num4 = 1;
      }
      else
      {
        num2 = m - 2;
        num3 = 0;
        num4 = -1;
      }
      if (dotranspose)
      {
        int num5 = num2;
        num2 = num3;
        num3 = num5;
        num4 = -num4;
      }
      if (m - 1 <= 0)
        return;
      int index1 = num2;
      do
      {
        int num5 = index1 + 1 - 1;
        for (int index2 = 1; index2 <= m - index1 - 1; ++index2)
          v[index2] = qp[index2 + num5, index1];
        v[1] = 1.0;
        if (fromtheright)
          reflections.applyreflectionfromtheright(ref z, tauq[index1], ref v, 0, zrows - 1, index1 + 1, m - 1, ref work);
        else
          reflections.applyreflectionfromtheleft(ref z, tauq[index1], ref v, index1 + 1, m - 1, 0, zcolumns - 1, ref work);
        index1 += num4;
      }
      while (index1 != num3 + num4);
    }
  }

  public static void rmatrixbdunpackpt(ref double[,] qp, int m, int n, ref double[] taup, int ptrows, ref double[,] pt)
  {
    if (m == 0 | n == 0 | ptrows == 0)
      return;
    pt = new double[ptrows - 1 + 1, n - 1 + 1];
    for (int index1 = 0; index1 <= ptrows - 1; ++index1)
    {
      for (int index2 = 0; index2 <= n - 1; ++index2)
        pt[index1, index2] = index1 != index2 ? 0.0 : 1.0;
    }
    bidiagonal.rmatrixbdmultiplybyp(ref qp, m, n, ref taup, ref pt, ptrows, n, true, true);
  }

  public static void rmatrixbdmultiplybyp(ref double[,] qp, int m, int n, ref double[] taup, ref double[,] z, int zrows, int zcolumns, bool fromtheright, bool dotranspose)
  {
    double[] numArray1 = new double[0];
    double[] numArray2 = new double[0];
    if (m <= 0 | n <= 0 | zrows <= 0 | zcolumns <= 0)
      return;
    int num1 = Math.Max(Math.Max(Math.Max(m, n), zrows), zcolumns);
    numArray1 = new double[num1 + 1];
    numArray2 = new double[num1 + 1];
    double[] v = new double[num1 + 1];
    double[] work = new double[num1 + 1];
    if (m >= n)
    {
      int num2;
      int num3;
      int num4;
      if (fromtheright)
      {
        num2 = n - 2;
        num3 = 0;
        num4 = -1;
      }
      else
      {
        num2 = 0;
        num3 = n - 2;
        num4 = 1;
      }
      if (!dotranspose)
      {
        int num5 = num2;
        num2 = num3;
        num3 = num5;
        num4 = -num4;
      }
      if (n - 1 <= 0)
        return;
      int index1 = num2;
      do
      {
        int num5 = index1 + 1 - 1;
        for (int index2 = 1; index2 <= n - 1 - index1; ++index2)
          v[index2] = qp[index1, index2 + num5];
        v[1] = 1.0;
        if (fromtheright)
          reflections.applyreflectionfromtheright(ref z, taup[index1], ref v, 0, zrows - 1, index1 + 1, n - 1, ref work);
        else
          reflections.applyreflectionfromtheleft(ref z, taup[index1], ref v, index1 + 1, n - 1, 0, zcolumns - 1, ref work);
        index1 += num4;
      }
      while (index1 != num3 + num4);
    }
    else
    {
      int num2;
      int num3;
      int num4;
      if (fromtheright)
      {
        num2 = m - 1;
        num3 = 0;
        num4 = -1;
      }
      else
      {
        num2 = 0;
        num3 = m - 1;
        num4 = 1;
      }
      if (!dotranspose)
      {
        int num5 = num2;
        num2 = num3;
        num3 = num5;
        num4 = -num4;
      }
      int index1 = num2;
      do
      {
        int num5 = index1 - 1;
        for (int index2 = 1; index2 <= n - index1; ++index2)
          v[index2] = qp[index1, index2 + num5];
        v[1] = 1.0;
        if (fromtheright)
          reflections.applyreflectionfromtheright(ref z, taup[index1], ref v, 0, zrows - 1, index1, n - 1, ref work);
        else
          reflections.applyreflectionfromtheleft(ref z, taup[index1], ref v, index1, n - 1, 0, zcolumns - 1, ref work);
        index1 += num4;
      }
      while (index1 != num3 + num4);
    }
  }

  public static void rmatrixbdunpackdiagonals(ref double[,] b, int m, int n, ref bool isupper, ref double[] d, ref double[] e)
  {
    isupper = m >= n;
    if (m <= 0 | n <= 0)
      return;
    if (isupper)
    {
      d = new double[n - 1 + 1];
      e = new double[n - 1 + 1];
      for (int index = 0; index <= n - 2; ++index)
      {
        d[index] = b[index, index];
        e[index] = b[index, index + 1];
      }
      d[n - 1] = b[n - 1, n - 1];
    }
    else
    {
      d = new double[m - 1 + 1];
      e = new double[m - 1 + 1];
      for (int index = 0; index <= m - 2; ++index)
      {
        d[index] = b[index, index];
        e[index] = b[index + 1, index];
      }
      d[m - 1] = b[m - 1, m - 1];
    }
  }

  public static void tobidiagonal(ref double[,] a, int m, int n, ref double[] tauq, ref double[] taup)
  {
    double[] numArray1 = new double[0];
    double[] numArray2 = new double[0];
    double tau = 0.0;
    int num1 = Math.Min(m, n);
    int num2 = Math.Max(m, n);
    double[] work = new double[num2 + 1];
    double[] numArray3 = new double[num2 + 1];
    taup = new double[num1 + 1];
    tauq = new double[num1 + 1];
    if (m >= n)
    {
      for (int m1 = 1; m1 <= n; ++m1)
      {
        int n1 = m - m1 + 1;
        int num3 = m1 - 1;
        for (int index = 1; index <= n1; ++index)
          numArray3[index] = a[index + num3, m1];
        reflections.generatereflection(ref numArray3, n1, ref tau);
        tauq[m1] = tau;
        int num4 = 1 - m1;
        for (int index = m1; index <= m; ++index)
          a[index, m1] = numArray3[index + num4];
        numArray3[1] = 1.0;
        reflections.applyreflectionfromtheleft(ref a, tau, ref numArray3, m1, m, m1 + 1, n, ref work);
        if (m1 < n)
        {
          int n2 = n - m1;
          int num5 = m1 + 1;
          int num6 = num5 - 1;
          for (int index = 1; index <= n2; ++index)
            numArray3[index] = a[m1, index + num6];
          reflections.generatereflection(ref numArray3, n2, ref tau);
          taup[m1] = tau;
          int num7 = 1 - num5;
          for (int index = num5; index <= n; ++index)
            a[m1, index] = numArray3[index + num7];
          numArray3[1] = 1.0;
          reflections.applyreflectionfromtheright(ref a, tau, ref numArray3, m1 + 1, m, m1 + 1, n, ref work);
        }
        else
          taup[m1] = 0.0;
      }
    }
    else
    {
      for (int n1 = 1; n1 <= m; ++n1)
      {
        int n2 = n - n1 + 1;
        int num3 = n1 - 1;
        for (int index = 1; index <= n2; ++index)
          numArray3[index] = a[n1, index + num3];
        reflections.generatereflection(ref numArray3, n2, ref tau);
        taup[n1] = tau;
        int num4 = 1 - n1;
        for (int index = n1; index <= n; ++index)
          a[n1, index] = numArray3[index + num4];
        numArray3[1] = 1.0;
        reflections.applyreflectionfromtheright(ref a, tau, ref numArray3, n1 + 1, m, n1, n, ref work);
        if (n1 < m)
        {
          int n3 = m - n1;
          int num5 = n1 + 1;
          int num6 = num5 - 1;
          for (int index = 1; index <= n3; ++index)
            numArray3[index] = a[index + num6, n1];
          reflections.generatereflection(ref numArray3, n3, ref tau);
          tauq[n1] = tau;
          int num7 = 1 - num5;
          for (int index = num5; index <= m; ++index)
            a[index, n1] = numArray3[index + num7];
          numArray3[1] = 1.0;
          reflections.applyreflectionfromtheleft(ref a, tau, ref numArray3, n1 + 1, m, n1 + 1, n, ref work);
        }
        else
          tauq[n1] = 0.0;
      }
    }
  }

  public static void unpackqfrombidiagonal(ref double[,] qp, int m, int n, ref double[] tauq, int qcolumns, ref double[,] q)
  {
    double[] numArray1 = new double[0];
    double[] numArray2 = new double[0];
    if (m == 0 | n == 0 | qcolumns == 0)
      return;
    q = new double[m + 1, qcolumns + 1];
    double[] v = new double[m + 1];
    double[] work = new double[qcolumns + 1];
    for (int index1 = 1; index1 <= m; ++index1)
    {
      for (int index2 = 1; index2 <= qcolumns; ++index2)
        q[index1, index2] = index1 != index2 ? 0.0 : 1.0;
    }
    if (m >= n)
    {
      for (int m1 = Math.Min(n, qcolumns); m1 >= 1; --m1)
      {
        int num1 = m - m1 + 1;
        int num2 = m1 - 1;
        for (int index = 1; index <= num1; ++index)
          v[index] = qp[index + num2, m1];
        v[1] = 1.0;
        reflections.applyreflectionfromtheleft(ref q, tauq[m1], ref v, m1, m, 1, qcolumns, ref work);
      }
    }
    else
    {
      for (int index1 = Math.Min(m - 1, qcolumns - 1); index1 >= 1; --index1)
      {
        int num1 = m - index1;
        int num2 = index1 + 1 - 1;
        for (int index2 = 1; index2 <= num1; ++index2)
          v[index2] = qp[index2 + num2, index1];
        v[1] = 1.0;
        reflections.applyreflectionfromtheleft(ref q, tauq[index1], ref v, index1 + 1, m, 1, qcolumns, ref work);
      }
    }
  }

  public static void multiplybyqfrombidiagonal(ref double[,] qp, int m, int n, ref double[] tauq, ref double[,] z, int zrows, int zcolumns, bool fromtheright, bool dotranspose)
  {
    double[] numArray1 = new double[0];
    double[] numArray2 = new double[0];
    if (m <= 0 | n <= 0 | zrows <= 0 | zcolumns <= 0)
      return;
    int num1 = Math.Max(Math.Max(Math.Max(m, n), zrows), zcolumns);
    double[] v = new double[num1 + 1];
    double[] work = new double[num1 + 1];
    if (m >= n)
    {
      int num2;
      int num3;
      int num4;
      if (fromtheright)
      {
        num2 = 1;
        num3 = n;
        num4 = 1;
      }
      else
      {
        num2 = n;
        num3 = 1;
        num4 = -1;
      }
      if (dotranspose)
      {
        int num5 = num2;
        num2 = num3;
        num3 = num5;
        num4 = -num4;
      }
      int index1 = num2;
      do
      {
        int num5 = m - index1 + 1;
        int num6 = index1 - 1;
        for (int index2 = 1; index2 <= num5; ++index2)
          v[index2] = qp[index2 + num6, index1];
        v[1] = 1.0;
        if (fromtheright)
          reflections.applyreflectionfromtheright(ref z, tauq[index1], ref v, 1, zrows, index1, m, ref work);
        else
          reflections.applyreflectionfromtheleft(ref z, tauq[index1], ref v, index1, m, 1, zcolumns, ref work);
        index1 += num4;
      }
      while (index1 != num3 + num4);
    }
    else
    {
      int num2;
      int num3;
      int num4;
      if (fromtheright)
      {
        num2 = 1;
        num3 = m - 1;
        num4 = 1;
      }
      else
      {
        num2 = m - 1;
        num3 = 1;
        num4 = -1;
      }
      if (dotranspose)
      {
        int num5 = num2;
        num2 = num3;
        num3 = num5;
        num4 = -num4;
      }
      if (m - 1 <= 0)
        return;
      int index1 = num2;
      do
      {
        int num5 = m - index1;
        int num6 = index1 + 1 - 1;
        for (int index2 = 1; index2 <= num5; ++index2)
          v[index2] = qp[index2 + num6, index1];
        v[1] = 1.0;
        if (fromtheright)
          reflections.applyreflectionfromtheright(ref z, tauq[index1], ref v, 1, zrows, index1 + 1, m, ref work);
        else
          reflections.applyreflectionfromtheleft(ref z, tauq[index1], ref v, index1 + 1, m, 1, zcolumns, ref work);
        index1 += num4;
      }
      while (index1 != num3 + num4);
    }
  }

  public static void unpackptfrombidiagonal(ref double[,] qp, int m, int n, ref double[] taup, int ptrows, ref double[,] pt)
  {
    double[] numArray1 = new double[0];
    double[] numArray2 = new double[0];
    if (m == 0 | n == 0 | ptrows == 0)
      return;
    pt = new double[ptrows + 1, n + 1];
    double[] v = new double[n + 1];
    double[] work = new double[ptrows + 1];
    for (int index1 = 1; index1 <= ptrows; ++index1)
    {
      for (int index2 = 1; index2 <= n; ++index2)
        pt[index1, index2] = index1 != index2 ? 0.0 : 1.0;
    }
    if (m >= n)
    {
      for (int index1 = Math.Min(n - 1, ptrows - 1); index1 >= 1; --index1)
      {
        int num1 = n - index1;
        int num2 = index1 + 1 - 1;
        for (int index2 = 1; index2 <= num1; ++index2)
          v[index2] = qp[index1, index2 + num2];
        v[1] = 1.0;
        reflections.applyreflectionfromtheright(ref pt, taup[index1], ref v, 1, ptrows, index1 + 1, n, ref work);
      }
    }
    else
    {
      for (int n1 = Math.Min(m, ptrows); n1 >= 1; --n1)
      {
        int num1 = n - n1 + 1;
        int num2 = n1 - 1;
        for (int index = 1; index <= num1; ++index)
          v[index] = qp[n1, index + num2];
        v[1] = 1.0;
        reflections.applyreflectionfromtheright(ref pt, taup[n1], ref v, 1, ptrows, n1, n, ref work);
      }
    }
  }

  public static void multiplybypfrombidiagonal(ref double[,] qp, int m, int n, ref double[] taup, ref double[,] z, int zrows, int zcolumns, bool fromtheright, bool dotranspose)
  {
    double[] numArray1 = new double[0];
    double[] numArray2 = new double[0];
    if (m <= 0 | n <= 0 | zrows <= 0 | zcolumns <= 0)
      return;
    int num1 = Math.Max(Math.Max(Math.Max(m, n), zrows), zcolumns);
    numArray1 = new double[num1 + 1];
    numArray2 = new double[num1 + 1];
    double[] v = new double[num1 + 1];
    double[] work = new double[num1 + 1];
    if (m >= n)
    {
      int num2;
      int num3;
      int num4;
      if (fromtheright)
      {
        num2 = n - 1;
        num3 = 1;
        num4 = -1;
      }
      else
      {
        num2 = 1;
        num3 = n - 1;
        num4 = 1;
      }
      if (!dotranspose)
      {
        int num5 = num2;
        num2 = num3;
        num3 = num5;
        num4 = -num4;
      }
      if (n - 1 <= 0)
        return;
      int index1 = num2;
      do
      {
        int num5 = n - index1;
        int num6 = index1 + 1 - 1;
        for (int index2 = 1; index2 <= num5; ++index2)
          v[index2] = qp[index1, index2 + num6];
        v[1] = 1.0;
        if (fromtheright)
          reflections.applyreflectionfromtheright(ref z, taup[index1], ref v, 1, zrows, index1 + 1, n, ref work);
        else
          reflections.applyreflectionfromtheleft(ref z, taup[index1], ref v, index1 + 1, n, 1, zcolumns, ref work);
        index1 += num4;
      }
      while (index1 != num3 + num4);
    }
    else
    {
      int num2;
      int num3;
      int num4;
      if (fromtheright)
      {
        num2 = m;
        num3 = 1;
        num4 = -1;
      }
      else
      {
        num2 = 1;
        num3 = m;
        num4 = 1;
      }
      if (!dotranspose)
      {
        int num5 = num2;
        num2 = num3;
        num3 = num5;
        num4 = -num4;
      }
      int index1 = num2;
      do
      {
        int num5 = n - index1 + 1;
        int num6 = index1 - 1;
        for (int index2 = 1; index2 <= num5; ++index2)
          v[index2] = qp[index1, index2 + num6];
        v[1] = 1.0;
        if (fromtheright)
          reflections.applyreflectionfromtheright(ref z, taup[index1], ref v, 1, zrows, index1, n, ref work);
        else
          reflections.applyreflectionfromtheleft(ref z, taup[index1], ref v, index1, n, 1, zcolumns, ref work);
        index1 += num4;
      }
      while (index1 != num3 + num4);
    }
  }

  public static void unpackdiagonalsfrombidiagonal(ref double[,] b, int m, int n, ref bool isupper, ref double[] d, ref double[] e)
  {
    isupper = m >= n;
    if (m == 0 | n == 0)
      return;
    if (isupper)
    {
      d = new double[n + 1];
      e = new double[n + 1];
      for (int index = 1; index <= n - 1; ++index)
      {
        d[index] = b[index, index];
        e[index] = b[index, index + 1];
      }
      d[n] = b[n, n];
    }
    else
    {
      d = new double[m + 1];
      e = new double[m + 1];
      for (int index = 1; index <= m - 1; ++index)
      {
        d[index] = b[index, index];
        e[index] = b[index + 1, index];
      }
      d[m] = b[m, m];
    }
  }
}
