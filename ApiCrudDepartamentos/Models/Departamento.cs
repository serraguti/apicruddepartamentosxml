using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ApiCrudDepartamentos.Models
{
    [Table("DEPT")]
    [XmlRoot("Departamento")]
    public class Departamento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("DEPT_NO")]
        [XmlElement("IdDepartamento")]
        public int IdDepartamento { get; set; }
        [Column("DNOMBRE")]
        [XmlElement("Nombre")]
        public String Nombre { get; set; }
        [Column("LOC")]
        [XmlElement("Localidad")]
        public String Localidad { get; set; }
    }
}
