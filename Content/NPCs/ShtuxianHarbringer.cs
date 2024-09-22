using Fargowiltas.NPCs;
using FargowiltasSouls.Core.Systems;
using CalamityMod.NPCs.TownNPCs;
using System.Collections.Generic;
using ssm.Systems;
using ssm.Content.NPCs.Shtuxibus;
using ssm.Content.Items.Consumables;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Events;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using FargowiltasSouls.Content.Items.Weapons.FinalUpgrades;

namespace ssm.Content.NPCs
{
	[AutoloadHead]
	public class ShtuxianHarbringer : ModNPC
	{
		public const string ShopName = "Shop";
		public int NumberOfTimesTalkedTo = 0;
		private static Profiles.StackedNPCProfile NPCProfile;

		public override void SetStaticDefaults() {
			Main.npcFrameCount[Type] = 25; // The total amount of frames the NPC has
			NPCID.Sets.ExtraFramesCount[Type] = 9; // Generally for Town NPCs, but this is how the NPC does extra things such as sitting in a chair and talking to other NPCs. This is the remaining frames after the walking frames.
			NPCID.Sets.AttackFrameCount[Type] = 4; // The amount of frames in the attacking animation.
			NPCID.Sets.DangerDetectRange[Type] = 100000; // The amount of pixels away from the center of the NPC that it tries to attack enemies.
			NPCID.Sets.AttackType[Type] = 1; // The type of attack the Town NPC performs. 0 = throwing, 1 = shooting, 2 = magic, 3 = melee
			NPCID.Sets.AttackTime[Type] = 10; 	// The amount of time it takes for the NPC's attack animation to be over once it starts.
			NPCID.Sets.AttackAverageChance[Type] = 1; // The denominator for the chance for a Town NPC to attack. Lower numbers make the Town NPC appear more aggressive.
			NPCID.Sets.HatOffsetY[Type] = 4;
			NPCID.Sets.ShimmerTownTransform[NPC.type] = false;
			NPCID.Sets.ShimmerTownTransform[Type] = false;
			NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers() {Velocity = 1f,Direction = 1};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
			NPC.Happiness
				.SetNPCAffection<WITCH>(AffectionLevel.Love)
				.SetNPCAffection<Mutant>(AffectionLevel.Like)
				.SetBiomeAffection<SnowBiome>(AffectionLevel.Like)
				.SetBiomeAffection<DesertBiome>(AffectionLevel.Dislike)
				.SetBiomeAffection<JungleBiome>(AffectionLevel.Hate)
				.SetBiomeAffection<HallowBiome>(AffectionLevel.Hate);}

		public override void SetDefaults() {
			NPC.townNPC = true;
			NPC.friendly = true;
			NPC.width = 18;
			NPC.height = 40;
			NPC.aiStyle = 7;
			NPC.damage = 745000;
			NPC.defense = 745745;
			NPC.lifeMax = 745000000;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			AnimationType = NPCID.Guide;}
		public override bool CanTownNPCSpawn(int numTownNPCs){
      	return WorldSaveSystem.downedShtuxibus && !NPC.AnyNPCs(ModContent.NPCType<Shtuxibus.Shtuxibus>());}
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) {
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
			new FlavorTextBestiaryInfoElement("Mods.ssm.Bestiary.Shtuxibus")});}
		public override ITownNPCProfile TownNPCProfile() {return NPCProfile;}
		public override List<string> SetNPCNameList() {return new List<string>() {"Shtuxibus"};}
		public override string GetChat() {
			WeightedRandom<string> chat = new WeightedRandom<string>();
			chat.Add(Language.GetTextValue("Mods.ssm.NPCs.ShtuxianHarbringer.Chat.Normal0"));
			chat.Add(Language.GetTextValue("Mods.ssm.NPCs.ShtuxianHarbringer.Chat.Normal1"));
			chat.Add(Language.GetTextValue("Mods.ssm.NPCs.ShtuxianHarbringer.Chat.Normal2"));
			chat.Add(Language.GetTextValue("Mods.ssm.NPCs.ShtuxianHarbringer.Chat.Normal3"));
			chat.Add(Language.GetTextValue("Mods.ssm.NPCs.ShtuxianHarbringer.Chat.Normal4"));
			chat.Add(Language.GetTextValue("Mods.ssm.NPCs.ShtuxianHarbringer.Chat.Normal5"));
			chat.Add(Language.GetTextValue("Mods.ssm.NPCs.ShtuxianHarbringer.Chat.Normal6"));
			chat.Add(Language.GetTextValue("Mods.ssm.NPCs.ShtuxianHarbringer.Chat.Normal7"));
			chat.Add(Language.GetTextValue("Mods.ssm.NPCs.ShtuxianHarbringer.Chat.Normal8"));
			chat.Add(Language.GetTextValue("Mods.ssm.NPCs.ShtuxianHarbringer.Chat.Normal9"));
			chat.Add(Language.GetTextValue("Mods.ssm.NPCs.ShtuxianHarbringer.Chat.Normal10"));
			chat.Add(Language.GetTextValue("Mods.ssm.NPCs.ShtuxianHarbringer.Chat.Normal11"));
			chat.Add(Language.GetTextValue("Mods.ssm.NPCs.ShtuxianHarbringer.Chat.Normal12"));
			chat.Add(Language.GetTextValue("Mods.ssm.NPCs.ShtuxianHarbringer.Chat.Normal13"));
			chat.Add(Language.GetTextValue("Mods.ssm.NPCs.ShtuxianHarbringer.Chat.Normal14"));

			if (NPC.AnyNPCs(NPCType<WITCH>()))
                chat.Add(Language.GetTextValue("Mods.ssm.NPCs.ShtuxianHarbringer.Chat.Calamitas"), 1.45);
			if (NPC.AnyNPCs(NPCType<Mutant>()))
                chat.Add(Language.GetTextValue("Mods.ssm.NPCs.ShtuxianHarbringer.Chat.Mutant"), 2);

            if (WorldSavingSystem.MasochistModeReal)
                chat.Add(Language.GetTextValue("Mods.ssm.NPCs.ShtuxianHarbringer.Chat.MasoMode"), 1);
			if (Main.zenithWorld)
                chat.Add(Language.GetTextValue("Mods.ssm.NPCs.ShtuxianHarbringer.Chat.ZenithSeed"), 1.45);

			if (Main.rand.NextBool(745))return this.GetLocalizedValue("Mods.ssm.NPCs.ShtuxianHarbringer.Chat.Easter");

		string chosenChat = chat;
		return chosenChat;}

		public override bool PreAI(){
      	if (!NPC.AnyNPCs(ModContent.NPCType<Shtuxibus.Shtuxibus>()))
      	return true;
      	((Entity) this.NPC).active = false;
      	this.NPC.netUpdate = true;
      	return false;}

		public override void AddShops()
        {
            var npcShop = new NPCShop(Type, ShopName)
                .Add(new Item(ItemType<StarlightVodka>()) { shopCustomPrice = Item.buyPrice(copper: 1000000) })
				.Add(new Item(ItemType<ShtuxianCurse>()) { shopCustomPrice = Item.buyPrice(copper: 1000000) })
            ;
            npcShop.Register();
        }
		public override void SetChatButtons(ref string button, ref string button2){button = Language.GetTextValue("LegacyInterface.28");}
		public override void OnChatButtonClicked(bool firstButton, ref string shop){if (firstButton){shop = ShopName;}}
        public override bool CanGoToStatue(bool toKingStatue) => toKingStatue;
		public override void TownNPCAttackStrength(ref int damage, ref float knockback) {damage = 745000;knockback = 74f;}
		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown) {cooldown = 7;randExtraCooldown = 4;}
		public override void TownNPCAttackProj(ref int projType, ref int attackDelay) {projType = ProjectileID.Meowmere;attackDelay = 1;}
		public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset) {multiplier = 20f;randomOffset = 2f;}
	}
}
