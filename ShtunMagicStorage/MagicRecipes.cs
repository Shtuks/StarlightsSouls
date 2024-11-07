using Terraria;
using Terraria.ModLoader;
using ssm.Core;
using MagicStorage.Items;
using ssm.ShtunMagicStorage;
using ssm.Content.Items.Materials;

namespace ssm.Calamity
{
    [ExtendsFromMod(ModCompatibility.MagicStorage.Name)]
    [JITWhenModsEnabled(ModCompatibility.MagicStorage.Name)]
    public class MagicRecipes : ModSystem
    {
        public override void AddRecipes()
        {
            if (ShtunConfig.Instance.ExtraContent)
            {
                Recipe.Create(ModContent.ItemType<CreativeStorageUnit>(), 1)
                    .AddIngredient<StorageUnitShtuxian>(10)
                    //.AddIngredient<ShtuxianSoul>()
                    .AddIngredient<ChtuxlagorShard>(10)
                    .AddIngredient<InfinityIngot>(10)
                    .AddTile<Content.Tiles.ShtuxibusForgeTile>()
                    .Register();
            }
        }

    }
}


