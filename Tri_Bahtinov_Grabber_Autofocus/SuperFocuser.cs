using ASCOM.DriverAccess;

namespace Tri_Bahtinov_Grabber_Autofocus
{
  internal class SuperFocuser : Focuser
    {
    public int RelativePosition;

    public SuperFocuser(string focuserId) : base(focuserId)
    {
      this.RelativePosition = 0;
    }

    public new void Move(int ms)
    {
      if (this.Absolute)
      {
        if (this.Position + ms < 0)
          return;
        base.Move(this.Position + ms);
      }
      else
      {
        base.Move(ms);
        this.RelativePosition += ms;
      }
    }

    public void Reset()
    {
      this.RelativePosition = 0;
    }
  }
}
