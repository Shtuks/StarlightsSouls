//using Fargowiltas.Items.Summons.Deviantt;
//using Fargowiltas.Items.Tiles;
//using Fargowiltas.NPCs;
//using Fargowiltas;
//using FargowiltasCrossmod.Content.Calamity.Items.Summons;
//using MonoMod.RuntimeDetour;
//using System.Collections.Generic;
//using System.Reflection;
//using Terraria.ModLoader;
//using Terraria;
//using ssm.Core;

//namespace ssm.Reworks
//{
//    public class DevianttShop
//    {
//        public class DevianttGlobalNPC : GlobalNPC
//        {
//            public override bool AppliesToEntity(NPC entity, bool lateInstantiation) => entity.type == ModContent.NPCType<Deviantt>();


//            [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
//            public static void AddRedemptionShop()
//            {
//                if (!ModCompatibility.Calamity.Loaded)
//                    return;
//                NPCShop shop = new(ModContent.NPCType<Deviantt>(), "Calamity");

//                Condition killedClam = new Condition("After killing a Giant Clam", () => CalDLCCompatibilityMisc.DownedClam);
//                Condition killedPlaguebringerMini = new Condition("After killing a Plaguebringer", () => CalDLCWorldSavingSystem.downedMiniPlaguebringer);
//                Condition killedReaperShark = new Condition("After killing a Reaper Shark", () => CalDLCWorldSavingSystem.downedReaperShark);
//                Condition killedColossalSquid = new Condition("After killing a Colossal Squid", () => CalDLCWorldSavingSystem.downedColossalSquid);
//                Condition killedEidolonWyrm = new Condition("After killing an Eidolon Wyrm", () => CalDLCWorldSavingSystem.downedEidolonWyrm);
//                Condition killedCloudElemental = new Condition("After killing a Cloud Elemental", () => CalDLCWorldSavingSystem.downedCloudElemental);
//                Condition killedEarthElemental = new Condition("After killing an Earth Elemental", () => CalDLCWorldSavingSystem.downedEarthElemental);
//                Condition killedArmoredDigger = new Condition("After killing an Armored Digger", () => CalDLCWorldSavingSystem.downedArmoredDigger);

//                shop.Add(new Item(ModContent.ItemType<ClamPearl>()) { shopCustomPrice = Item.buyPrice(gold: 5) }, killedClam);
//                shop.Add(new Item(ModContent.ItemType<AbandonedRemote>()) { shopCustomPrice = Item.buyPrice(gold: 10) }, killedArmoredDigger);
//                shop.Add(new Item(ModContent.ItemType<PlaguedWalkieTalkie>()) { shopCustomPrice = Item.buyPrice(gold: 10) }, killedPlaguebringerMini);
//                shop.Add(new Item(ModContent.ItemType<DeepseaProteinShake>()) { shopCustomPrice = Item.buyPrice(gold: 30) }, killedReaperShark);
//                shop.Add(new Item(ModContent.ItemType<ColossalTentacle>()) { shopCustomPrice = Item.buyPrice(gold: 30) }, killedColossalSquid);
//                shop.Add(new Item(ModContent.ItemType<WyrmTablet>()) { shopCustomPrice = Item.buyPrice(gold: 30) }, killedEidolonWyrm);
//                shop.Add(new Item(ModContent.ItemType<StormIdol>()) { shopCustomPrice = Item.buyPrice(gold: 7) }, killedCloudElemental);
//                shop.Add(new Item(ModContent.ItemType<QuakeIdol>()) { shopCustomPrice = Item.buyPrice(gold: 7) }, killedEarthElemental);

//                shop.Register();
//                ModShops.Add(shop);
//            }


//            internal static void DevianttAddShopsDetour(Orig_DevianttAddShops orig, Deviantt self)
//            {
//                if (ModCompatibility.Redemption.Loaded)
//                    AddRedemptionShop();
//            }
//        }
//    }
//}
