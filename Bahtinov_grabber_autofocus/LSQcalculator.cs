using System;

namespace Bahtinov_grabber_autofocus
{
  internal class LSQcalculator
  {
    private void calculatebasefuncs(ref double[,] basefunctions, ref float[] x, int funcs, int numpoints)
    {
      for (int index1 = 0; index1 < numpoints; ++index1)
      {
        for (int index2 = 0; index2 < funcs; ++index2)
          basefunctions[index1, index2] = Math.Pow((double) x[index1], (double) (funcs - 1 - index2));
      }
    }

    public float peakposition(float[] yvals, int estimatedPos, int halffitrange)
    {
      try
      {
        float[] x = new float[2 * halffitrange + 1];
        float[] numArray1 = new float[2 * halffitrange + 1];
        for (int index = -halffitrange; index <= halffitrange; ++index)
        {
          x[index + halffitrange] = (float) (index + estimatedPos);
          numArray1[index + halffitrange] = yvals[index + estimatedPos];
        }
        int length = x.Length;
        double[] w = new double[length];
        double[] c = new double[3];
        double[,] numArray2 = new double[length, 3];
        for (int index = 0; index < length; ++index)
          w[index] = 1.0;
        this.calculatebasefuncs(ref numArray2, ref x, 3, length);
        double[] y = new double[numArray1.Length];
        for (int index = 0; index < numArray1.Length; ++index)
          y[index] = (double) numArray1[index];
        leastsquares.buildgeneralleastsquares(ref y, ref w, ref numArray2, length, 3, ref c);
        float num = (float) (-c[1] / (2.0 * c[0]));
        float[] numArray3;
        float[] numArray4 = numArray3 = (float[]) null;
        double[] numArray5;
        double[] numArray6 = numArray5 = (double[]) null;
        double[] numArray7;
        if (c[0] < 0.0)
        {
          numArray7 = (double[]) null;
          return num;
        }
        numArray7 = (double[]) null;
        return num;
      }
      catch
      {
        return 0.0f;
      }
    }
  }
}
