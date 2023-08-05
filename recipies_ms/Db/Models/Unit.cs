using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using recipies_ms.Db.Models.Enums;

namespace recipies_ms.Db.Models
{
    [Table("unit")]
    public class Unit
    {
        [Key]
        [Column("si_unit")]
        public SiUnit SiUnit { get; set; }
    }
}