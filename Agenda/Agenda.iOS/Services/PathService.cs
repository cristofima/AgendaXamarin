using Agenda.Interfaces;
using Agenda.iOS.Services;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(PathService))]

namespace Agenda.iOS.Services
{
    public class PathService : IPathService
    {
        public string GetDatabasePath()
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, AppSettings.DatabaseName);
        }
    }
}