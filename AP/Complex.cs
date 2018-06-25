namespace AP
{
  public struct Complex
  {
    public double x;
    public double y;

    public Complex(double _x)
    {
      this.x = _x;
      this.y = 0.0;
    }

    public Complex(double _x, double _y)
    {
      this.x = _x;
      this.y = _y;
    }

    public static implicit operator Complex(double _x)
    {
      return new Complex(_x);
    }

    public static bool operator ==(Complex lhs, Complex rhs)
    {
      return lhs.x == rhs.x & lhs.y == rhs.y;
    }

    public static bool operator !=(Complex lhs, Complex rhs)
    {
      return lhs.x != rhs.x | lhs.y != rhs.y;
    }

    public static Complex operator +(Complex lhs)
    {
      return lhs;
    }

    public static Complex operator -(Complex lhs)
    {
      return new Complex(-lhs.x, -lhs.y);
    }

    public static Complex operator +(Complex lhs, Complex rhs)
    {
      return new Complex(lhs.x + rhs.x, lhs.y + rhs.y);
    }

    public static Complex operator -(Complex lhs, Complex rhs)
    {
      return new Complex(lhs.x - rhs.x, lhs.y - rhs.y);
    }

    public static Complex operator *(Complex lhs, Complex rhs)
    {
      return new Complex(lhs.x * rhs.x - lhs.y * rhs.y, lhs.x * rhs.y + lhs.y * rhs.x);
    }

    public static Complex operator /(Complex lhs, Complex rhs)
    {
      Complex complex;
      if (System.Math.Abs(rhs.y) < System.Math.Abs(rhs.x))
      {
        double num1 = rhs.y / rhs.x;
        double num2 = rhs.x + rhs.y * num1;
        complex.x = (lhs.x + lhs.y * num1) / num2;
        complex.y = (lhs.y - lhs.x * num1) / num2;
      }
      else
      {
        double num1 = rhs.x / rhs.y;
        double num2 = rhs.y + rhs.x * num1;
        complex.x = (lhs.y + lhs.x * num1) / num2;
        complex.y = (-lhs.x + lhs.y * num1) / num2;
      }
      return complex;
    }
  }
}
