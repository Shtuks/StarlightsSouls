using System.Collections.Generic;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria;
using FargowiltasSouls;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

// WHY THIS SHIT IS NOT WORKING
namespace ssm.Content.UI
{
    public class BossSummonUI : UIState
    {
        private UIPanel panel;
        private UIList list;
        private UIScrollbar scrollbar;

        public BossSummonUI()
        {
            panel = new UIPanel();
            panel.Width.Set(400, 0);
            panel.Height.Set(600, 0);
            panel.HAlign = 0.5f;
            panel.VAlign = 0.5f;
            Append(panel);

            list = new UIList();
            list.Width.Set(-25, 1f);
            list.Height.Set(0, 1f);
            panel.Append(list);

            scrollbar = new UIScrollbar();
            scrollbar.SetView(100f, 1000f);
            scrollbar.Height.Set(0, 1f);
            scrollbar.HAlign = 1f;
            panel.Append(scrollbar);
            list.SetScrollbar(scrollbar);

            List<int> bossTypes = new List<int>();
            for (int type = 0; type < NPCLoader.NPCCount; type++)
            {
                NPC npc = new NPC();
                npc.SetDefaults(type);
                if (npc.boss)
                {
                    bossTypes.Add(type);
                }
            }

            foreach (int type in bossTypes)
            {
                string name = Lang.GetNPCNameValue(type);
                UITextPanel<string> button = new(name);
                button.Width.Set(0, 1f);
                button.Height.Set(25, 0);
                button.OnLeftClick += (evt, element) => SpawnBoss(type);
                list.Add(button);
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Main.keyState.IsKeyDown(Keys.Escape))
            {
                ssm.Instance._showBossSummonUI = false;
            }
        }
        private void SpawnBoss(int type)
        {
            Player player = Main.LocalPlayer;
            FargoSoulsUtil.SpawnBossNetcoded(player, type);
        }
    }
}