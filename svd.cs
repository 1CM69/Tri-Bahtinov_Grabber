using System;

internal class svd
{
  public static bool rmatrixsvd(double[,] a, int m, int n, int uneeded, int vtneeded, int additionalmemory, ref double[] w, ref double[,] u, ref double[,] vt)
  {
    double[] tauq = new double[0];
    double[] taup = new double[0];
    double[] tau = new double[0];
    double[] e = new double[0];
    double[] numArray1 = new double[0];
    double[,] numArray2 = new double[0, 0];
    bool isupper = false;
    a = (double[,]) a.Clone();
    bool flag1 = true;
    if (m == 0 | n == 0)
      return flag1;
    int n1 = Math.Min(m, n);
    w = new double[n1 + 1];
    int qcolumns = 0;
    int num1 = 0;
    if (uneeded == 1)
    {
      num1 = m;
      qcolumns = n1;
      u = new double[num1 - 1 + 1, qcolumns - 1 + 1];
    }
    if (uneeded == 2)
    {
      num1 = m;
      qcolumns = m;
      u = new double[num1 - 1 + 1, qcolumns - 1 + 1];
    }
    int num2 = 0;
    int ncvt = 0;
    if (vtneeded == 1)
    {
      num2 = n1;
      ncvt = n;
      vt = new double[num2 - 1 + 1, ncvt - 1 + 1];
    }
    if (vtneeded == 2)
    {
      num2 = n;
      ncvt = n;
      vt = new double[num2 - 1 + 1, ncvt - 1 + 1];
    }
    if ((double) m > 1.6 * (double) n)
    {
      if (uneeded == 0)
      {
        qr.rmatrixqr(ref a, m, n, ref tau);
        for (int index1 = 0; index1 <= n - 1; ++index1)
        {
          for (int index2 = 0; index2 <= index1 - 1; ++index2)
            a[index1, index2] = 0.0;
        }
        bidiagonal.rmatrixbd(ref a, n, n, ref tauq, ref taup);
        bidiagonal.rmatrixbdunpackpt(ref a, n, n, ref taup, num2, ref vt);
        bidiagonal.rmatrixbdunpackdiagonals(ref a, n, n, ref isupper, ref w, ref e);
        return bdsvd.rmatrixbdsvd(ref w, e, n, isupper, false, ref u, 0, ref a, 0, ref vt, ncvt);
      }
      qr.rmatrixqr(ref a, m, n, ref tau);
      qr.rmatrixqrunpackq(ref a, m, n, ref tau, qcolumns, ref u);
      for (int index1 = 0; index1 <= n - 1; ++index1)
      {
        for (int index2 = 0; index2 <= index1 - 1; ++index2)
          a[index1, index2] = 0.0;
      }
      bidiagonal.rmatrixbd(ref a, n, n, ref tauq, ref taup);
      bidiagonal.rmatrixbdunpackpt(ref a, n, n, ref taup, num2, ref vt);
      bidiagonal.rmatrixbdunpackdiagonals(ref a, n, n, ref isupper, ref w, ref e);
      bool flag2;
      if (additionalmemory < 1)
      {
        bidiagonal.rmatrixbdmultiplybyq(ref a, n, n, ref tauq, ref u, m, n, true, false);
        flag2 = bdsvd.rmatrixbdsvd(ref w, e, n, isupper, false, ref u, m, ref a, 0, ref vt, ncvt);
      }
      else
      {
        double[] work = new double[Math.Max(m, n) + 1];
        bidiagonal.rmatrixbdunpackq(ref a, n, n, ref tauq, n, ref numArray2);
        blas.copymatrix(ref u, 0, m - 1, 0, n - 1, ref a, 0, m - 1, 0, n - 1);
        blas.inplacetranspose(ref numArray2, 0, n - 1, 0, n - 1, ref work);
        flag2 = bdsvd.rmatrixbdsvd(ref w, e, n, isupper, false, ref u, 0, ref numArray2, n, ref vt, ncvt);
        blas.matrixmatrixmultiply(ref a, 0, m - 1, 0, n - 1, false, ref numArray2, 0, n - 1, 0, n - 1, true, 1.0, ref u, 0, m - 1, 0, n - 1, 0.0, ref work);
      }
      return flag2;
    }
    if ((double) n > 1.6 * (double) m)
    {
      if (vtneeded == 0)
      {
        lq.rmatrixlq(ref a, m, n, ref tau);
        for (int index1 = 0; index1 <= m - 1; ++index1)
        {
          for (int index2 = index1 + 1; index2 <= m - 1; ++index2)
            a[index1, index2] = 0.0;
        }
        bidiagonal.rmatrixbd(ref a, m, m, ref tauq, ref taup);
        bidiagonal.rmatrixbdunpackq(ref a, m, m, ref tauq, qcolumns, ref u);
        bidiagonal.rmatrixbdunpackdiagonals(ref a, m, m, ref isupper, ref w, ref e);
        double[] work = new double[m + 1];
        blas.inplacetranspose(ref u, 0, num1 - 1, 0, qcolumns - 1, ref work);
        bool flag2 = bdsvd.rmatrixbdsvd(ref w, e, m, isupper, false, ref a, 0, ref u, num1, ref vt, 0);
        blas.inplacetranspose(ref u, 0, num1 - 1, 0, qcolumns - 1, ref work);
        return flag2;
      }
      lq.rmatrixlq(ref a, m, n, ref tau);
      lq.rmatrixlqunpackq(ref a, m, n, ref tau, num2, ref vt);
      for (int index1 = 0; index1 <= m - 1; ++index1)
      {
        for (int index2 = index1 + 1; index2 <= m - 1; ++index2)
          a[index1, index2] = 0.0;
      }
      bidiagonal.rmatrixbd(ref a, m, m, ref tauq, ref taup);
      bidiagonal.rmatrixbdunpackq(ref a, m, m, ref tauq, qcolumns, ref u);
      bidiagonal.rmatrixbdunpackdiagonals(ref a, m, m, ref isupper, ref w, ref e);
      double[] work1 = new double[Math.Max(m, n) + 1];
      blas.inplacetranspose(ref u, 0, num1 - 1, 0, qcolumns - 1, ref work1);
      bool flag3;
      if (additionalmemory < 1)
      {
        bidiagonal.rmatrixbdmultiplybyp(ref a, m, m, ref taup, ref vt, m, n, false, true);
        flag3 = bdsvd.rmatrixbdsvd(ref w, e, m, isupper, false, ref a, 0, ref u, num1, ref vt, n);
      }
      else
      {
        bidiagonal.rmatrixbdunpackpt(ref a, m, m, ref taup, m, ref numArray2);
        flag3 = bdsvd.rmatrixbdsvd(ref w, e, m, isupper, false, ref a, 0, ref u, num1, ref numArray2, m);
        blas.copymatrix(ref vt, 0, m - 1, 0, n - 1, ref a, 0, m - 1, 0, n - 1);
        blas.matrixmatrixmultiply(ref numArray2, 0, m - 1, 0, m - 1, false, ref a, 0, m - 1, 0, n - 1, false, 1.0, ref vt, 0, m - 1, 0, n - 1, 0.0, ref work1);
      }
      blas.inplacetranspose(ref u, 0, num1 - 1, 0, qcolumns - 1, ref work1);
      return flag3;
    }
    if (m <= n)
    {
      bidiagonal.rmatrixbd(ref a, m, n, ref tauq, ref taup);
      bidiagonal.rmatrixbdunpackq(ref a, m, n, ref tauq, qcolumns, ref u);
      bidiagonal.rmatrixbdunpackpt(ref a, m, n, ref taup, num2, ref vt);
      bidiagonal.rmatrixbdunpackdiagonals(ref a, m, n, ref isupper, ref w, ref e);
      double[] work = new double[m + 1];
      blas.inplacetranspose(ref u, 0, num1 - 1, 0, qcolumns - 1, ref work);
      bool flag2 = bdsvd.rmatrixbdsvd(ref w, e, n1, isupper, false, ref a, 0, ref u, num1, ref vt, ncvt);
      blas.inplacetranspose(ref u, 0, num1 - 1, 0, qcolumns - 1, ref work);
      return flag2;
    }
    bidiagonal.rmatrixbd(ref a, m, n, ref tauq, ref taup);
    bidiagonal.rmatrixbdunpackq(ref a, m, n, ref tauq, qcolumns, ref u);
    bidiagonal.rmatrixbdunpackpt(ref a, m, n, ref taup, num2, ref vt);
    bidiagonal.rmatrixbdunpackdiagonals(ref a, m, n, ref isupper, ref w, ref e);
    bool flag4;
    if (additionalmemory < 2 | uneeded == 0)
    {
      flag4 = bdsvd.rmatrixbdsvd(ref w, e, n1, isupper, false, ref u, num1, ref a, 0, ref vt, ncvt);
    }
    else
    {
      double[,] numArray3 = new double[n1 - 1 + 1, m - 1 + 1];
      blas.copyandtranspose(ref u, 0, m - 1, 0, n1 - 1, ref numArray3, 0, n1 - 1, 0, m - 1);
      flag4 = bdsvd.rmatrixbdsvd(ref w, e, n1, isupper, false, ref u, 0, ref numArray3, m, ref vt, ncvt);
      blas.copyandtranspose(ref numArray3, 0, n1 - 1, 0, m - 1, ref u, 0, m - 1, 0, n1 - 1);
    }
    return flag4;
  }

  public static bool svddecomposition(double[,] a, int m, int n, int uneeded, int vtneeded, int additionalmemory, ref double[] w, ref double[,] u, ref double[,] vt)
  {
    double[] tauq = new double[0];
    double[] taup = new double[0];
    double[] tau = new double[0];
    double[] e = new double[0];
    double[] numArray1 = new double[0];
    double[,] numArray2 = new double[0, 0];
    bool isupper = false;
    a = (double[,]) a.Clone();
    bool flag1 = true;
    if (m == 0 | n == 0)
      return flag1;
    int num1 = Math.Min(m, n);
    w = new double[num1 + 1];
    int num2 = 0;
    int num3 = 0;
    if (uneeded == 1)
    {
      num3 = m;
      num2 = num1;
      u = new double[num3 + 1, num2 + 1];
    }
    if (uneeded == 2)
    {
      num3 = m;
      num2 = m;
      u = new double[num3 + 1, num2 + 1];
    }
    int num4 = 0;
    int ncvt = 0;
    if (vtneeded == 1)
    {
      num4 = num1;
      ncvt = n;
      vt = new double[num4 + 1, ncvt + 1];
    }
    if (vtneeded == 2)
    {
      num4 = n;
      ncvt = n;
      vt = new double[num4 + 1, ncvt + 1];
    }
    if ((double) m > 1.6 * (double) n)
    {
      if (uneeded == 0)
      {
        qr.qrdecomposition(ref a, m, n, ref tau);
        for (int index1 = 2; index1 <= n; ++index1)
        {
          for (int index2 = 1; index2 <= index1 - 1; ++index2)
            a[index1, index2] = 0.0;
        }
        bidiagonal.tobidiagonal(ref a, n, n, ref tauq, ref taup);
        bidiagonal.unpackptfrombidiagonal(ref a, n, n, ref taup, num4, ref vt);
        bidiagonal.unpackdiagonalsfrombidiagonal(ref a, n, n, ref isupper, ref w, ref e);
        return bdsvd.bidiagonalsvddecomposition(ref w, e, n, isupper, false, ref u, 0, ref a, 0, ref vt, ncvt);
      }
      qr.qrdecomposition(ref a, m, n, ref tau);
      qr.unpackqfromqr(ref a, m, n, ref tau, num2, ref u);
      for (int index1 = 2; index1 <= n; ++index1)
      {
        for (int index2 = 1; index2 <= index1 - 1; ++index2)
          a[index1, index2] = 0.0;
      }
      bidiagonal.tobidiagonal(ref a, n, n, ref tauq, ref taup);
      bidiagonal.unpackptfrombidiagonal(ref a, n, n, ref taup, num4, ref vt);
      bidiagonal.unpackdiagonalsfrombidiagonal(ref a, n, n, ref isupper, ref w, ref e);
      bool flag2;
      if (additionalmemory < 1)
      {
        bidiagonal.multiplybyqfrombidiagonal(ref a, n, n, ref tauq, ref u, m, n, true, false);
        flag2 = bdsvd.bidiagonalsvddecomposition(ref w, e, n, isupper, false, ref u, m, ref a, 0, ref vt, ncvt);
      }
      else
      {
        double[] work = new double[Math.Max(m, n) + 1];
        bidiagonal.unpackqfrombidiagonal(ref a, n, n, ref tauq, n, ref numArray2);
        blas.copymatrix(ref u, 1, m, 1, n, ref a, 1, m, 1, n);
        blas.inplacetranspose(ref numArray2, 1, n, 1, n, ref work);
        flag2 = bdsvd.bidiagonalsvddecomposition(ref w, e, n, isupper, false, ref u, 0, ref numArray2, n, ref vt, ncvt);
        blas.matrixmatrixmultiply(ref a, 1, m, 1, n, false, ref numArray2, 1, n, 1, n, true, 1.0, ref u, 1, m, 1, n, 0.0, ref work);
      }
      return flag2;
    }
    if ((double) n > 1.6 * (double) m)
    {
      if (vtneeded == 0)
      {
        lq.lqdecomposition(ref a, m, n, ref tau);
        for (int index1 = 1; index1 <= m - 1; ++index1)
        {
          for (int index2 = index1 + 1; index2 <= m; ++index2)
            a[index1, index2] = 0.0;
        }
        bidiagonal.tobidiagonal(ref a, m, m, ref tauq, ref taup);
        bidiagonal.unpackqfrombidiagonal(ref a, m, m, ref tauq, num2, ref u);
        bidiagonal.unpackdiagonalsfrombidiagonal(ref a, m, m, ref isupper, ref w, ref e);
        double[] work = new double[m + 1];
        blas.inplacetranspose(ref u, 1, num3, 1, num2, ref work);
        bool flag2 = bdsvd.bidiagonalsvddecomposition(ref w, e, m, isupper, false, ref a, 0, ref u, num3, ref vt, 0);
        blas.inplacetranspose(ref u, 1, num3, 1, num2, ref work);
        return flag2;
      }
      lq.lqdecomposition(ref a, m, n, ref tau);
      lq.unpackqfromlq(ref a, m, n, ref tau, num4, ref vt);
      for (int index1 = 1; index1 <= m - 1; ++index1)
      {
        for (int index2 = index1 + 1; index2 <= m; ++index2)
          a[index1, index2] = 0.0;
      }
      bidiagonal.tobidiagonal(ref a, m, m, ref tauq, ref taup);
      bidiagonal.unpackqfrombidiagonal(ref a, m, m, ref tauq, num2, ref u);
      bidiagonal.unpackdiagonalsfrombidiagonal(ref a, m, m, ref isupper, ref w, ref e);
      double[] work1 = new double[Math.Max(m, n) + 1];
      blas.inplacetranspose(ref u, 1, num3, 1, num2, ref work1);
      bool flag3;
      if (additionalmemory < 1)
      {
        bidiagonal.multiplybypfrombidiagonal(ref a, m, m, ref taup, ref vt, m, n, false, true);
        flag3 = bdsvd.bidiagonalsvddecomposition(ref w, e, m, isupper, false, ref a, 0, ref u, num3, ref vt, n);
      }
      else
      {
        bidiagonal.unpackptfrombidiagonal(ref a, m, m, ref taup, m, ref numArray2);
        flag3 = bdsvd.bidiagonalsvddecomposition(ref w, e, m, isupper, false, ref a, 0, ref u, num3, ref numArray2, m);
        blas.copymatrix(ref vt, 1, m, 1, n, ref a, 1, m, 1, n);
        blas.matrixmatrixmultiply(ref numArray2, 1, m, 1, m, false, ref a, 1, m, 1, n, false, 1.0, ref vt, 1, m, 1, n, 0.0, ref work1);
      }
      blas.inplacetranspose(ref u, 1, num3, 1, num2, ref work1);
      return flag3;
    }
    if (m <= n)
    {
      bidiagonal.tobidiagonal(ref a, m, n, ref tauq, ref taup);
      bidiagonal.unpackqfrombidiagonal(ref a, m, n, ref tauq, num2, ref u);
      bidiagonal.unpackptfrombidiagonal(ref a, m, n, ref taup, num4, ref vt);
      bidiagonal.unpackdiagonalsfrombidiagonal(ref a, m, n, ref isupper, ref w, ref e);
      double[] work = new double[m + 1];
      blas.inplacetranspose(ref u, 1, num3, 1, num2, ref work);
      bool flag2 = bdsvd.bidiagonalsvddecomposition(ref w, e, num1, isupper, false, ref a, 0, ref u, num3, ref vt, ncvt);
      blas.inplacetranspose(ref u, 1, num3, 1, num2, ref work);
      return flag2;
    }
    bidiagonal.tobidiagonal(ref a, m, n, ref tauq, ref taup);
    bidiagonal.unpackqfrombidiagonal(ref a, m, n, ref tauq, num2, ref u);
    bidiagonal.unpackptfrombidiagonal(ref a, m, n, ref taup, num4, ref vt);
    bidiagonal.unpackdiagonalsfrombidiagonal(ref a, m, n, ref isupper, ref w, ref e);
    bool flag4;
    if (additionalmemory < 2 | uneeded == 0)
    {
      flag4 = bdsvd.bidiagonalsvddecomposition(ref w, e, num1, isupper, false, ref u, num3, ref a, 0, ref vt, ncvt);
    }
    else
    {
      double[,] numArray3 = new double[num1 + 1, m + 1];
      blas.copyandtranspose(ref u, 1, m, 1, num1, ref numArray3, 1, num1, 1, m);
      flag4 = bdsvd.bidiagonalsvddecomposition(ref w, e, num1, isupper, false, ref u, 0, ref numArray3, m, ref vt, ncvt);
      blas.copyandtranspose(ref numArray3, 1, num1, 1, m, ref u, 1, m, 1, num1);
    }
    return flag4;
  }
}
