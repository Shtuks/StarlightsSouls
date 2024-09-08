using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent;
using FargowiltasSouls.Content.Items.Materials;
using Terraria.ID;
using FargowiltasSouls.Content.Items;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using Fargowiltas.Items.Tiles;
using Terraria.Localization;
using Terraria.DataStructures;
using FargowiltasSouls.Core.Toggler;
using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Armor;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using ssm.Content.Items.Accessories;
using ssm.Core;
using ssm.Content.Buffs.Minions;
using FargowiltasSouls.Content.Items.Accessories.Forces;

namespace ssm.Content.Items.Accessories
{
  public class EternityForce : BaseForce
  {
    private readonly Mod FargoSoul = Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls");

    public override void SetStaticDefaults() {
      ItemID.Sets.ItemNoGravity[this.Type] = true;
      Enchants[Type] = new int[]
        {
          ModContent.ItemType<StyxEnchant>(),
          ModContent.ItemType<PhantaplazmalEnchant>(),
          ModContent.ItemType<NekomiEnchant>(),
          ModContent.ItemType<EridanusEnchant>(),
          ModContent.ItemType<GaiaEnchant>(),
        };}

    public override void SetDefaults()
    {
      this.Item.value = Item.buyPrice(10, 0, 0, 0);
      this.Item.rare = 10;
      this.Item.accessory = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
      if(player.AddEffect<MutantSoulEffect>(Item)){
        player.AddBuff(ModContent.BuffType<MutantSoulBuff>(), 2);
      }
      ModContent.Find<ModItem>(((ModType) this).Mod.Name, "StyxEnchant").UpdateAccessory(player, false);
      ModContent.Find<ModItem>(((ModType) this).Mod.Name, "PhantaplazmalEnchant").UpdateAccessory(player, false);
      ModContent.Find<ModItem>(((ModType) this).Mod.Name, "NekomiEnchant").UpdateAccessory(player, false);
      ModContent.Find<ModItem>(((ModType) this).Mod.Name, "EridanusEnchant").UpdateAccessory(player, false);
      ModContent.Find<ModItem>(((ModType) this).Mod.Name, "GaiaEnchant").UpdateAccessory(player, false);
    }

    public override void AddRecipes()
    {
      Recipe recipe = this.CreateRecipe(1);
      recipe.AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"));
      recipe.Register();
    }

    public abstract class EternityForceEffect : AccessoryEffect
    {
      public override Header ToggleHeader => Header.GetHeader<EternityForceHeader>();
    }

    public class MutantSoulEffect : AccessoryEffect
    {
      public override Header ToggleHeader => Header.GetHeader<EternityForceHeader>();
      public override int ToggleItemType => ModContent.ItemType<EternityForce>();
      public override bool MinionEffect => true;
    }
  }
}