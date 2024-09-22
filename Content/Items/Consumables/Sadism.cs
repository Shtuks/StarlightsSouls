using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ssm.Content.Buffs;

namespace ssm.Content.Items.Consumables
{
  public class Sadism : ModItem
  {
    public override void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.maxStack = 30;
      this.Item.rare = 11;
      this.Item.useStyle = 2;
      this.Item.useAnimation = 17;
      this.Item.useTime = 17;
      this.Item.consumable = true;
      this.Item.buffType = ModContent.BuffType<SadismEX>();
      this.Item.buffTime = 25200;
      this.Item.UseSound = SoundID.Item3;
      this.Item.value = Item.sellPrice(10, 0, 0, 0);
    }
  }
}
