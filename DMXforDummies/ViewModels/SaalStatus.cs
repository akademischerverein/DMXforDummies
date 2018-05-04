using System.Windows.Media;

namespace DMXforDummies.ViewModels
{
    public struct SaalStatus
    {
        public Color Schattenfuge;
        public Color BarOben;
        public Color BarUnten;
        public byte BarWeiß;

        public static SaalStatus Create()
        {
            SaalStatus s;
            s.Schattenfuge = Color.FromRgb(0, 0, 0);
            s.BarOben = Color.FromRgb(0, 0, 0);
            s.BarUnten = Color.FromRgb(0, 0, 0);
            s.BarWeiß = 0;
            return s;
        }
    }
}