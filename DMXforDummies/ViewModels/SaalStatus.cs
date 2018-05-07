using System.Windows.Media;

namespace DMXforDummies.ViewModels
{
    public struct SaalStatus
    {
        public Color[] Identifiers;

        public static SaalStatus Create(int ids)
        {
            SaalStatus s;
            s.Identifiers = new Color[ids];

            for (int i = 0; i < ids; ++i)
            {
                s.Identifiers[i] = Color.FromRgb(0, 0, 0);
            }

            return s;
        }
    }
}