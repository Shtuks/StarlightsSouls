using Terraria;
using Terraria.ModLoader;
using Fargowiltas.NPCs;
using System.Collections.Generic;
using MonoMod.RuntimeDetour;
using System.Reflection;
using Fargowiltas.Items.Summons.Deviantt;
using Fargowiltas.Items.Tiles;
using Fargowiltas;
using static Terraria.ModLoader.ModContent;
using ThoriumMod.Items.BossMini;
using ssm.Thorium;

namespace ssm.Core;
public class DeviGlobalNPC : GlobalNPC
{
    public override bool AppliesToEntity(NPC entity, bool lateInstantiation) => entity.type == ModContent.NPCType<Deviantt>();
    public override bool InstancePerEntity => true;
    internal static int currentShop;
    internal static readonly List<NPCShop> ModShops = [];

    public static void CycleShop()
    {
        currentShop++;
        currentShop %= ModShops.Count + 1;
    }

    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public static void AddThoriumShop()
    {
        if (!ModCompatibility.Thorium.Loaded)
            return;
        NPCShop shop = new(ModContent.NPCType<Deviantt>(), "Thorium");

        Condition killedPatch = new Condition("After killing a Patch Wreck", () => ThoriumWorldSave.downedPatchWrek);
        Condition killedBloom = new Condition("After killing a Corpse Bloop", () => ThoriumWorldSave.downedCorpseBloom);
        Condition killedIllusioner = new Condition("After killing an Illusioner", () => ThoriumWorldSave.downedIllusionist);
        
        shop.Add(new Item(ModContent.ItemType<FreshBrain>()) { shopCustomPrice = Item.buyPrice(gold: 5) }, killedPatch);
        shop.Add(new Item(ModContent.ItemType<RottingSpore>()) { shopCustomPrice = Item.buyPrice(gold: 5) }, killedBloom);
        shop.Add(new Item(ModContent.ItemType<IllusionaryGlass>()) { shopCustomPrice = Item.buyPrice(gold: 5) }, killedIllusioner);
        
        shop.Register();
        ModShops.Add(shop);
    }

    #region Hook Stuff
    public override void Load()
    {
        DeviChatButtonHook = new(DevianttOnChatButtonClickedMethod, DevianttOnChatButtonClickedDetour);
        DeviChatButtonHook.Apply();

        DeviShopHook = new(DevianttAddShopsMethod, DevianttAddShopsDetour);
        DeviShopHook.Apply();
    }
    public override void Unload()
    {
        DeviChatButtonHook.Undo();
        DeviShopHook.Undo();
    }
    public delegate void Orig_DevianttOnChatButtonClicked(Deviantt self, bool firstButton, ref string shopName);
    public delegate void Orig_DevianttAddShops(Deviantt self);

    private static readonly MethodInfo DevianttOnChatButtonClickedMethod = typeof(Deviantt).GetMethod("OnChatButtonClicked", LumUtils.UniversalBindingFlags);
    private static readonly MethodInfo DevianttAddShopsMethod = typeof(Deviantt).GetMethod("AddShops", LumUtils.UniversalBindingFlags);

    Hook DeviChatButtonHook;
    Hook DeviShopHook;

    internal static void DevianttOnChatButtonClickedDetour(Orig_DevianttOnChatButtonClicked orig, Deviantt self, bool firstButton, ref string shopName)
    {
        orig(self, firstButton, ref shopName);

        if (firstButton)
        {
            if (currentShop == 0)
            {
                shopName = Deviantt.ShopName;
            }
            else
            {
                shopName = ModShops[currentShop - 1].Name;
            }
        }
    }
    internal static void DevianttAddShopsDetour(Orig_DevianttAddShops orig, Deviantt self)
    {
        AddVanillaShop();
        if (ModCompatibility.Thorium.Loaded)
            AddThoriumShop();
    }

    public static Dictionary<string, bool> FargoWorldDownedBools => typeof(FargoWorld).GetField("DownedBools", LumUtils.UniversalBindingFlags).GetValue(null) as Dictionary<string, bool>;

    public static void AddVanillaShop()
    {
        var npcShop = new NPCShop(ModContent.NPCType<Deviantt>(), Deviantt.ShopName);

        npcShop
            .Add(new Item(ItemType<WormSnack>()) { shopCustomPrice = Item.buyPrice(copper: 20000) }, new Condition("Mods.Fargowiltas.Conditions.WormDown", () => FargoWorldDownedBools["worm"]))
            .Add(new Item(ItemType<PinkSlimeCrown>()) { shopCustomPrice = Item.buyPrice(copper: 50000) }, new Condition("Mods.Fargowiltas.Conditions.PinkyDown", () => FargoWorldDownedBools["pinky"]))
            .Add(new Item(ItemType<GoblinScrap>()) { shopCustomPrice = Item.buyPrice(copper: 10000) }, new Condition("Mods.Fargowiltas.Conditions.ScoutDown", () => FargoWorldDownedBools["goblinScout"]))
            .Add(new Item(ItemType<Eggplant>()) { shopCustomPrice = Item.buyPrice(copper: 20000) }, new Condition("Mods.Fargowiltas.Conditions.DoctorDown", () => FargoWorldDownedBools["doctorBones"]))
            .Add(new Item(ItemType<AttractiveOre>()) { shopCustomPrice = Item.buyPrice(copper: 30000) }, new Condition("Mods.Fargowiltas.Conditions.MinerDown", () => FargoWorldDownedBools["undeadMiner"]))
            .Add(new Item(ItemType<HolyGrail>()) { shopCustomPrice = Item.buyPrice(copper: 50000) }, new Condition("Mods.Fargowiltas.Conditions.TimDown", () => FargoWorldDownedBools["tim"]))
            .Add(new Item(ItemType<GnomeHat>()) { shopCustomPrice = Item.buyPrice(copper: 50000) }, new Condition("Mods.Fargowiltas.Conditions.GnomeDown", () => FargoWorldDownedBools["gnome"]))
            .Add(new Item(ItemType<GoldenSlimeCrown>()) { shopCustomPrice = Item.buyPrice(copper: 600000) }, new Condition("Mods.Fargowiltas.Conditions.GoldSlimeDown", () => FargoWorldDownedBools["goldenSlime"]))
            .Add(new Item(ItemType<SlimyLockBox>()) { shopCustomPrice = Item.buyPrice(copper: 100000) }, new Condition("Mods.Fargowiltas.Conditions.DungeonSlimeDown", () => NPC.downedBoss3 && FargoWorldDownedBools["dungeonSlime"]))
            .Add(new Item(ItemType<AthenianIdol>()) { shopCustomPrice = Item.buyPrice(copper: 50000) }, new Condition("Mods.Fargowiltas.Conditions.MedusaDown", () => Main.hardMode && FargoWorldDownedBools["medusa"]))
            .Add(new Item(ItemType<ClownLicense>()) { shopCustomPrice = Item.buyPrice(copper: 50000) }, new Condition("Mods.Fargowiltas.Conditions.ClownDown", () => Main.hardMode && FargoWorldDownedBools["clown"]))
            .Add(new Item(ItemType<HeartChocolate>()) { shopCustomPrice = Item.buyPrice(copper: 100000) }, new Condition("Mods.Fargowiltas.Conditions.NymphDown", () => FargoWorldDownedBools["nymph"]))
            .Add(new Item(ItemType<MothLamp>()) { shopCustomPrice = Item.buyPrice(copper: 100000) }, new Condition("Mods.Fargowiltas.Conditions.MothDown", () => Main.hardMode && FargoWorldDownedBools["moth"]))
            .Add(new Item(ItemType<DilutedRainbowMatter>()) { shopCustomPrice = Item.buyPrice(copper: 100000) }, new Condition("Mods.Fargowiltas.Conditions.RainbowSlimeDown", () => Main.hardMode && FargoWorldDownedBools["rainbowSlime"]))
            .Add(new Item(ItemType<CloudSnack>()) { shopCustomPrice = Item.buyPrice(copper: 100000) }, new Condition("Mods.Fargowiltas.Conditions.WyvernDown", () => Main.hardMode && FargoWorldDownedBools["wyvern"]))
            .Add(new Item(ItemType<RuneOrb>()) { shopCustomPrice = Item.buyPrice(copper: 150000) }, new Condition("Mods.Fargowiltas.Conditions.RuneDown", () => Main.hardMode && FargoWorldDownedBools["runeWizard"]))
            .Add(new Item(ItemType<SuspiciousLookingChest>()) { shopCustomPrice = Item.buyPrice(copper: 300000) }, new Condition("Mods.Fargowiltas.Conditions.MimicDown", () => Main.hardMode && FargoWorldDownedBools["mimic"]))
            .Add(new Item(ItemType<HallowChest>()) { shopCustomPrice = Item.buyPrice(copper: 300000) }, new Condition("Mods.Fargowiltas.Conditions.MimicHallowDown", () => Main.hardMode && FargoWorldDownedBools["mimicHallow"]))
            .Add(new Item(ItemType<CorruptChest>()) { shopCustomPrice = Item.buyPrice(copper: 300000) }, new Condition("Mods.Fargowiltas.Conditions.MimicCorruptDown", () => Main.hardMode && (FargoWorldDownedBools["mimicCorrupt"] || FargoWorldDownedBools["mimicCrimson"])))
            .Add(new Item(ItemType<CrimsonChest>()) { shopCustomPrice = Item.buyPrice(copper: 300000) }, new Condition("Mods.Fargowiltas.Conditions.MimicCrimsonDown", () => Main.hardMode && (FargoWorldDownedBools["mimicCorrupt"] || FargoWorldDownedBools["mimicCrimson"])))
            .Add(new Item(ItemType<JungleChest>()) { shopCustomPrice = Item.buyPrice(copper: 300000) }, new Condition("Mods.Fargowiltas.Conditions.MimicJungleDown", () => Main.hardMode && FargoWorldDownedBools["mimicJungle"]))
            .Add(new Item(ItemType<CoreoftheFrostCore>()) { shopCustomPrice = Item.buyPrice(copper: 100000) }, new Condition("Mods.Fargowiltas.Conditions.IceGolemDown", () => Main.hardMode && FargoWorldDownedBools["iceGolem"]))
            .Add(new Item(ItemType<ForbiddenForbiddenFragment>()) { shopCustomPrice = Item.buyPrice(copper: 100000) }, new Condition("Mods.Fargowiltas.Conditions.SandDown", () => Main.hardMode && FargoWorldDownedBools["sandElemental"]))
            .Add(new Item(ItemType<DemonicPlushie>()) { shopCustomPrice = Item.buyPrice(copper: 100000) }, new Condition("Mods.Fargowiltas.Conditions.DevilDown", () => NPC.downedMechBossAny && FargoWorldDownedBools["redDevil"]))
            .Add(new Item(ItemType<SuspiciousLookingLure>()) { shopCustomPrice = Item.buyPrice(copper: 100000) }, new Condition("Mods.Fargowiltas.Conditions.BloodFishDown", () => FargoWorldDownedBools["eyeFish"] || FargoWorldDownedBools["zombieMerman"]))
            .Add(new Item(ItemType<BloodUrchin>()) { shopCustomPrice = Item.buyPrice(copper: 100000) }, new Condition("Mods.Fargowiltas.Conditions.BloodEelDown", () => Main.hardMode && FargoWorldDownedBools["bloodEel"]))
            .Add(new Item(ItemType<HemoclawCrab>()) { shopCustomPrice = Item.buyPrice(copper: 100000) }, new Condition("Mods.Fargowiltas.Conditions.BloodGoblinDown", () => Main.hardMode && FargoWorldDownedBools["goblinShark"]))
            .Add(new Item(ItemType<BloodSushiPlatter>()) { shopCustomPrice = Item.buyPrice(copper: 200000) }, new Condition("Mods.Fargowiltas.Conditions.BloodNautDown", () => Main.hardMode && FargoWorldDownedBools["dreadnautilus"]))
            .Add(new Item(ItemType<ShadowflameIcon>()) { shopCustomPrice = Item.buyPrice(copper: 100000) }, new Condition("Mods.Fargowiltas.Conditions.SummonerDown", () => Main.hardMode && NPC.downedGoblins && FargoWorldDownedBools["goblinSummoner"]))
            .Add(new Item(ItemType<PirateFlag>()) { shopCustomPrice = Item.buyPrice(copper: 150000) }, new Condition("Mods.Fargowiltas.Conditions.PirateDown", () => Main.hardMode && NPC.downedPirates && FargoWorldDownedBools["pirateCaptain"]))
            .Add(new Item(ItemType<Pincushion>()) { shopCustomPrice = Item.buyPrice(copper: 150000) }, new Condition("Mods.Fargowiltas.Conditions.NailheadDown", () => NPC.downedPlantBoss && FargoWorldDownedBools["nailhead"]))
            .Add(new Item(ItemType<MothronEgg>()) { shopCustomPrice = Item.buyPrice(copper: 150000) }, new Condition("Mods.Fargowiltas.Conditions.MothronDown", () => NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && FargoWorldDownedBools["mothron"]))
            .Add(new Item(ItemType<LeesHeadband>()) { shopCustomPrice = Item.buyPrice(copper: 150000) }, new Condition("Mods.Fargowiltas.Conditions.LeeDown", () => NPC.downedPlantBoss && FargoWorldDownedBools["boneLee"]))
            .Add(new Item(ItemType<GrandCross>()) { shopCustomPrice = Item.buyPrice(copper: 150000) }, new Condition("Mods.Fargowiltas.Conditions.PaladinDown", () => NPC.downedPlantBoss && FargoWorldDownedBools["paladin"]))
            .Add(new Item(ItemType<AmalgamatedSkull>()) { shopCustomPrice = Item.buyPrice(copper: 300000) }, new Condition("Mods.Fargowiltas.Conditions.SkeleGunDown", () => NPC.downedPlantBoss && FargoWorldDownedBools["skeletonGun"]))
            .Add(new Item(ItemType<AmalgamatedSpirit>()) { shopCustomPrice = Item.buyPrice(copper: 300000) }, new Condition("Mods.Fargowiltas.Conditions.SkeleGunDown", () => NPC.downedPlantBoss && FargoWorldDownedBools["skeletonMage"]))
            .Add(new Item(ItemType<SiblingPylon>()), Condition.HappyEnoughToSellPylons, Condition.NpcIsPresent(NPCType<Mutant>()), Condition.NpcIsPresent(NPCType<Abominationn>()))
        ;

        npcShop.Register();
    }

    #endregion


}