using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Agenda
{
    public class AppSettings
    {
        public static string DatabaseName = "Agenda.db3";

        public static Dictionary<string, Color> nameToColor = new Dictionary<string, Color>
        {
            { "Aqua", Color.Aqua }, { "Black", Color.Black },
            { "Blue", Color.Blue }, { "Fucshia", Color.Fuchsia },
            { "Gray", Color.Gray }, { "Green", Color.Green },
            { "Lime", Color.Lime }, { "Maroon", Color.Maroon },
            { "Navy", Color.Navy }, { "Olive", Color.Olive },
            { "Purple", Color.Purple }, { "Red", Color.Red },
            { "Silver", Color.Silver }, { "Teal", Color.Teal },
            { "White", Color.White }, { "Yellow", Color.Yellow }
        };

        public static Color BackgroundColor
        {
            get
            {
                var color = Preferences.Get("BackgroundColor", "White");
                return nameToColor[color];
            }
        }
    }
}
