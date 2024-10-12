using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Chat;
using Terraria.GameContent.Events;
using Terraria.GameContent.ItemDropRules;
using ssm.Content.Buffs;
using ssm.Content.Items;
using ssm.Content.Items.Accessories;
using ssm.Content.Items.Consumables;
using ssm.Content.Items.Materials;
using ssm.Content.Items.Singularities;
using ssm.Content.NPCs;
using ssm.Content.NPCs.Shtuxibus;
using Fargowiltas;
using Terraria.ModLoader;

namespace ssm
{
    public partial class ShtunNpcs : GlobalNPC
    {
        private readonly Mod fargosouls = ModLoader.GetMod("FargowiltasSouls");
        public override bool InstancePerEntity => true;
        public bool isWaterEnemy;
        public static int Shtuxibus = -1;
        public static int DukeEX = -1;
        public static int boss = -1;
        public static int championBoss = -1;
        public bool CelestialPower;

        public override void ModifyShop(NPCShop shop)
        {
            void AddItem(int ItemID, int customPrice = -1, Condition condition = null, Condition[] conditions = null)
            {
                if (condition != (Condition)null)
                {
                    conditions = (Condition[])(object)new Condition[1] { condition };
                }
                if (conditions != null)
                {
                    if (customPrice != -1)
                    {
                        shop.Add(new Item(ItemID, 1, 0)
                        {
                            shopCustomPrice = customPrice
                        }, conditions);
                    }
                    else
                    {
                        shop.Add(ItemID, conditions);
                    }
                }
                else if (customPrice != -1)
                {
                    shop.Add(new Item(ItemID, 1, 0)
                    {
                        shopCustomPrice = customPrice
                    }, Array.Empty<Condition>());
                }
                else
                {
                    shop.Add(ItemID, Array.Empty<Condition>());
                }
            }
        }
    }
}
