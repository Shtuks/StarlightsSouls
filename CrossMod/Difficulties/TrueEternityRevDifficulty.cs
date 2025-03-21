using CalamityMod.Systems;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ssm.SoA;
using ssm.Systems;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static CalamityMod.Systems.DifficultyModeSystem;
using FargowiltasSouls.Core.Systems;
using FargowiltasCrossmod.Core.Calamity.Systems;
using ssm.Core;

namespace ssm.CrossMod.Difficulties
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name, ModCompatibility.Crossmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name, ModCompatibility.Crossmod.Name)]
    public class TrueEternityRevDifficulty : DifficultyMode
    {
        public override bool Enabled
        {
            get => WorldSaveSystem.trueRevEternity;
            set
            {
                WorldSaveSystem.trueRevEternity = value;

                if (value)
                {
                    CalamityWorld.revenge = true;
                    CalamityWorld.death = false;
                    TrueModeManager.setTrueMode(value);
                    WorldSavingSystem.EternityMode = true;
                    WorldSavingSystem.ShouldBeEternityMode = true;
                }

                if (Main.netMode != NetmodeID.SinglePlayer)
                    PacketManager.SendPacket<DifficultyPackets.TrueEternityRevPacket>();
            }
        }

        private Asset<Texture2D> _texture;
        public override Asset<Texture2D> Texture
        {
            get
            {
                _texture ??= ModContent.Request<Texture2D>("ssm/Assets/EternityRevIcon");

                return _texture;
            }
        }

        public override LocalizedText ExpandedDescription => Language.GetText("Mods.ssm.TrueEternityRev.ExpandedDescription");

        public TrueEternityRevDifficulty()
        {
            DifficultyScale = 1f;
            Name = Language.GetText("Mods.ssm.TrueEternityRev.Name");
            ShortDescription = Language.GetText("Mods.ssm.TrueEternityRev.ShortDescription");

            ActivationTextKey = "Mods.ssm.TrueEternityRev.Activation";
            DeactivationTextKey = "Mods.ssm.TrueEternityRev.Deactivation";

            ActivationSound = SoundID.Roar with { Pitch = -0.3f };
            ChatTextColor = Color.Pink;
        }

        public override int FavoredDifficultyAtTier(int tier)
        {
            DifficultyMode[] tierList = DifficultyTiers[tier];

            for (int i = 0; i < tierList.Length; i++)
            {
                if (tierList[i].Name.Value == "Death")
                    return i;
            }

            return 0;
        }
    }
}