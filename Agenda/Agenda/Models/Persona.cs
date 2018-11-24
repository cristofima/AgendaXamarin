using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Models
{
    [Table("Personas")]
    public class Persona
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Celular { get; set; }
        public DateTime DOB { get; set; }
    }
}
