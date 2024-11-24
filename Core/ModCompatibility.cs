using System;
using Terraria.ModLoader;

namespace ssm.Core;

public static class ModCompatibility
{
    public static class HEROSMod
    {
        public const string Name = "HEROsMod";
        public static bool Loaded => ModLoader.HasMod(Name);
        public static Mod Mod => ModLoader.GetMod(Name);
    }
    public static class CheatSheet
    {
        public const string Name = "CheatSheet";
        public static bool Loaded => ModLoader.HasMod(Name);
        public static Mod Mod => ModLoader.GetMod(Name);
    }
    public static class QualityOfCheatingChinese
    {
        public const string Name = "ImproveGame";
        public static bool Loaded => ModLoader.HasMod(Name);
        public static Mod Mod => ModLoader.GetMod(Name);
    }
    public static class QualityOfCheating
    {
        public const string Name = "miningcracks_take_on_luiafk";
        public static bool Loaded => ModLoader.HasMod(Name);
        public static Mod Mod => ModLoader.GetMod(Name);
    }
    public static class Dragonlens
    {
        public const string Name = "Dragonlens";
        public static bool Loaded => ModLoader.HasMod(Name);
        public static Mod Mod => ModLoader.GetMod(Name);
    }
    public static class MutantMod
    {
        public const string Name = "Fargowiltas";
        public static bool Loaded => ModLoader.HasMod(Name);
        public static Mod Mod => ModLoader.GetMod(Name);
    }
    public static class Crossmod
    {
        public const string Name = "FargowiltasCrossmod";
        public static bool Loaded => ModLoader.HasMod(Name);
        public static Mod Mod => ModLoader.GetMod(Name);
    }
    public static class MagicStorage
    {
        public const string Name = "MagicStorage";
        public static bool Loaded => ModLoader.HasMod(Name);
        public static Mod Mod => ModLoader.GetMod(Name);
    }
    public static class SoulsMod
    {
        public const string Name = "FargowiltasSouls";
        public static bool Loaded => ModLoader.HasMod(Name);
        public static FargowiltasSouls.FargowiltasSouls Mod => ModLoader.GetMod(Name) as FargowiltasSouls.FargowiltasSouls;
    }
    public static class Thorium
    {
        public const string Name = "ThoriumMod";
        public static bool Loaded => ModLoader.HasMod(Name);
        public static Mod Mod => ModLoader.GetMod(Name);
    }
    public static class Spirit
    {
        public const string Name = "SpititMod";
        public static bool Loaded => ModLoader.HasMod(Name);
        public static Mod Mod => ModLoader.GetMod(Name);
    }
    public static class Homeward
    {
        public const string Name = "ContinentOfJourney";
        public static bool Loaded => ModLoader.HasMod(Name);
        public static Mod Mod => ModLoader.GetMod(Name);
    }
    public static class Calamity
    {
        public const string Name = "CalamityMod";
        public static bool Loaded => ModLoader.HasMod(Name);
        public static Mod Mod => ModLoader.GetMod(Name);
    }
    public static class Goozma
    {
        public const string Name = "CalamityHunt";
        public static bool Loaded => ModLoader.HasMod(Name);
        public static Mod Mod => ModLoader.GetMod(Name);
    }
    public static class Redemption
    {
        public const string Name = "Redemption";
        public static bool Loaded => ModLoader.HasMod(Name);
        public static Mod Mod => ModLoader.GetMod(Name);
    }
    public static class SacredTools
    {
        public const string Name = "SacredTools";
        public static bool Loaded => ModLoader.HasMod(Name);
        public static Mod Mod => ModLoader.GetMod(Name);
    }
    public static class Catalyst
    {
        public const string Name = "CatalystMod";
        public static bool Loaded => ModLoader.HasMod(Name);
        public static Mod Mod => ModLoader.GetMod(Name);
    }
    public static class CalValEX
    {
        public const string Name = "CalValEX";
        public static bool Loaded => ModLoader.HasMod(Name);
        public static Mod Mod => ModLoader.GetMod(Name);
    }
    public static class InfernumMode
    {
        public const string Name = "InfernumMode";
        public static bool Loaded => ModLoader.HasMod(Name);
        public static Mod Mod => ModLoader.GetMod(Name);
        public static bool InfernumDifficulty => Loaded && (bool)Mod.Call("GetInfernumActive");
    }
    public static class WrathoftheGods
    {
        public const string Name = "NoxusBoss";
        public static bool Loaded => ModLoader.HasMod(Name);
        public static Mod Mod => ModLoader.GetMod(Name);
        public static ModNPC NoxusBoss1 = Mod.Find<ModNPC>(Mod.Version >= new Version(1, 2, 0) ? "AvatarRift" : "NoxusEgg");
        public static ModNPC NoxusBoss2 = Mod.Find<ModNPC>(Mod.Version >= new Version(1, 2, 0) ? "AvatarOfEmptiness" : "EntropicGod");
        public static ModNPC NamelessDeityBoss = Mod.Find<ModNPC>("NamelessDeityBoss");
    }
}