using Fargowiltas.NPCs;
using CalamityMod.NPCs.TownNPCs;
using System.Collections.Generic;
using ssm.Systems;
using ssm.Content.NPCs.Shtuxibus;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

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
			NPCID.Sets.DangerDetectRange[Type] = 10000; // The amount of pixels away from the center of the NPC that it tries to attack enemies.
			NPCID.Sets.AttackType[Type] = 1; // The type of attack the Town NPC performs. 0 = throwing, 1 = shooting, 2 = magic, 3 = melee
			NPCID.Sets.AttackTime[Type] = 10; 	// The amount of time it takes for the NPC's attack animation to be over once it starts.
			NPCID.Sets.AttackAverageChance[Type] = 5; // The denominator for the chance for a Town NPC to attack. Lower numbers make the Town NPC appear more aggressive.
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
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
			new FlavorTextBestiaryInfoElement("Shtuxibus is being came from other world called Shtundex. The shard of omnipotent Chtux'Lag'or and embodiment of energy concept.")});}
		public override ITownNPCProfile TownNPCProfile() {return NPCProfile;}
		public override List<string> SetNPCNameList() {return new List<string>() {"Shtuxibus"};}
		public override string GetChat() {
			WeightedRandom<string> chat = new WeightedRandom<string>();
			chat.Add(Language.GetTextValue("Hm. It's interesting to observe your progress. But you will never reach my level of power."));
			chat.Add(Language.GetTextValue("Can i jump? Jeah. I have that thing you call 'space'."));
			chat.Add(Language.GetTextValue("Have you ever seen shtuxian before? I bet no!"));
			chat.Add(Language.GetTextValue("Who is Yharim? How many years you waiting for him to be added in game?"));
			chat.Add(Language.GetTextValue("Heh. Gods of this world so pathetic. Kill dragon and consume his soul? Don't make me laugh!"));
			chat.Add(Language.GetTextValue("Do you know about 'Сhtux'Lag'Or'? Hm. I thought that people study about highest."));
			chat.Add(Language.GetTextValue("Hey, do you know about 3d dimension? No? Disappointing. My home world in 3d dimension."));
			chat.Add(Language.GetTextValue("Sometimes I feel that I'm too different from everyone else here."), 2.0);
			chat.Add(Language.GetTextValue("Do you think that Calamitas will reciprocate my feelings? She look nice..."), 0.01);
		string chosenChat = chat;
		return chosenChat;}

		public override bool PreAI(){
      	if (!NPC.AnyNPCs(ModContent.NPCType<Shtuxibus.Shtuxibus>()))
      	return true;
      	((Entity) this.NPC).active = false;
      	this.NPC.netUpdate = true;
      	return false;}
		public override void SetChatButtons(ref string button, ref string button2){button = Language.GetTextValue("LegacyInterface.28");}
		public override void OnChatButtonClicked(bool firstButton, ref string shop){if (firstButton){shop = ShopName;}}
        public override bool CanGoToStatue(bool toKingStatue) => toKingStatue;
		public override void TownNPCAttackStrength(ref int damage, ref float knockback) {damage = 745000;knockback = 74f;}
		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown) {cooldown = 7;randExtraCooldown = 4;}
		public override void TownNPCAttackProj(ref int projType, ref int attackDelay) {projType = ProjectileID.Meowmere;attackDelay = 1;}
		public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset) {multiplier = 20f;randomOffset = 2f;}
	}
}
