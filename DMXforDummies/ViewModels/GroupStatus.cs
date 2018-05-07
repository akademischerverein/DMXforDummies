using System.Windows.Media;

namespace DMXforDummies.ViewModels
{
    public struct GroupStatus
    {
        public Color[] Identifiers;

        public static GroupStatus Create(int ids)
        {
            GroupStatus s;
            s.Identifiers = new Color[ids];

            for (int i = 0; i < ids; ++i)
            {
                s.Identifiers[i] = Color.FromRgb(0, 0, 0);
            }

            return s;
        }
    }
}