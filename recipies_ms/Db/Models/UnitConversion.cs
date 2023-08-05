using System.ComponentModel.DataAnnotations.Schema;
using recipies_ms.Db.Models.Enums;

namespace recipies_ms.Db.Models
{
    [Table("unit_conversion_rel")]
    public class UnitConversion
    {
        [Column("si_unit_source")]
        public SiUnit SiUnitSource { get; set; }
        
        [Column("amount_source")]
        public float AmountSource { get; set; }
        
        [Column("si_unit_target")]
        public SiUnit SiUnitTarget { get; set; }
        
        [Column("amount_target")]
        public float AmountTarget { get; set; }
    }
}