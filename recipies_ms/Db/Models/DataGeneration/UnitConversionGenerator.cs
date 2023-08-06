using recipies_ms.Db.Models.Enums;

namespace recipies_ms.Db.Models.DataGeneration
{
    public static class UnitConversionGenerator
    {
        public static UnitConversion[] GenerateUnitConversionData()
        {
            return new UnitConversion[] {
                new() { SiUnitSource = SiUnit.Cup, AmountSource = 1, SiUnitTarget = SiUnit.TeaSpoon, AmountTarget = 50 },
                new() { SiUnitSource = SiUnit.Cup, AmountSource = 1, SiUnitTarget = SiUnit.TableSpoon, AmountTarget = 16.67f },
                new() { SiUnitSource = SiUnit.Cup, AmountSource = 1, SiUnitTarget = SiUnit.Milliliter, AmountTarget = 250 },
                new() { SiUnitSource = SiUnit.Cup, AmountSource = 1, SiUnitTarget = SiUnit.FlOz, AmountTarget = 8.8f },
                
                new() { SiUnitSource = SiUnit.Cup, AmountSource = 1, SiUnitTarget = SiUnit.Gram, AmountTarget = 250 },
                new() { SiUnitSource = SiUnit.Cup, AmountSource = 0.1134f, SiUnitTarget = SiUnit.Oz, AmountTarget = 1 },
                
                new() { SiUnitSource = SiUnit.Oz, AmountSource = 1, SiUnitTarget = SiUnit.Gram, AmountTarget = 28.35f },
                new() { SiUnitSource = SiUnit.Oz, AmountSource = 1, SiUnitTarget = SiUnit.Pound, AmountTarget = 0.0625f },
            };

        }
    }
}