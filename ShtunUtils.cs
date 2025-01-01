using Terraria.ModLoader;
using System.Reflection;
using Terraria;
using Terraria.Chat;
using Terraria.GameContent.Creative;
using Terraria.Graphics.Shaders;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using FargowiltasSouls.Content.Projectiles;
using Terraria.ID;
using ReLogic.Content;
using System.Linq;
using Terraria.DataStructures;
using Terraria.Localization;
using ssm.Electricity;

namespace ssm
{
    public static class ShtunUtils
    {
        public static bool HostCheck => Main.netMode != NetmodeID.MultiplayerClient;
        public static bool EternityMode = true;
        public static Vector2 RandomRotate => Vector2.UnitX.RotatedByRandom(MathHelper.TwoPi);
        public static Color GetAdditiveColor(Color Color) => new Color(Color.R, Color.G, Color.B, 0);
        public static int NewSummonProjectile(IEntitySource source, Vector2 spawn, Vector2 velocity, int type, int rawBaseDamage, float knockback, int owner = 255, float ai0 = 0, float ai1 = 0)
        {
            int p = Projectile.NewProjectile(source, spawn, velocity, type, rawBaseDamage, knockback, owner, ai0, ai1);
            if (p != Main.maxProjectiles)
            {
                Main.projectile[p].originalDamage = rawBaseDamage;
                Main.projectile[p].ContinuouslyUpdateDamageStats = true;
            }
            return p;
        }

        public static void HomeInOnNPC(Projectile projectile, bool ignoreTiles, float distanceRequired, float homingVelocity, float inertia)
        {
            if (!projectile.friendly)
                return;

            Vector2 destination = projectile.Center;
            float maxDistance = distanceRequired;
            bool locatedTarget = false;

            float npcDistCompare = 25000f;
            int index = -1;
            foreach (NPC n in Main.ActiveNPCs)
            {
                float extraDistance = (n.width / 2) + (n.height / 2);
                if (!n.CanBeChasedBy(projectile, false) || !projectile.WithinRange(n.Center, maxDistance + extraDistance))
                    continue;

                float currentNPCDist = Vector2.Distance(n.Center, projectile.Center);
                if ((currentNPCDist < npcDistCompare) && (ignoreTiles || Collision.CanHit(projectile.Center, 1, 1, n.Center, 1, 1)))
                {
                    npcDistCompare = currentNPCDist;
                    index = n.whoAmI;
                }
            }
            if (index != -1)
            {
                destination = Main.npc[index].Center;
                locatedTarget = true;
            }

            if (locatedTarget)
            {
                Vector2 homeDirection = (destination - projectile.Center).SafeNormalize(Vector2.UnitY);
                projectile.velocity = (projectile.velocity * inertia + homeDirection * homingVelocity) / (inertia + 1f);
            }
        }


        public static bool CircularHitboxCollision(Vector2 centerCheckPosition, float radius, Rectangle targetHitbox)
        {
            // If the center intersects the hitbox, return true immediately
            Rectangle center = new Rectangle((int)centerCheckPosition.X, (int)centerCheckPosition.Y, 1, 1);
            if (center.Intersects(targetHitbox))
                return true;

            float topLeftDistance = Vector2.Distance(centerCheckPosition, targetHitbox.TopLeft());
            float topRightDistance = Vector2.Distance(centerCheckPosition, targetHitbox.TopRight());
            float bottomLeftDistance = Vector2.Distance(centerCheckPosition, targetHitbox.BottomLeft());
            float bottomRightDistance = Vector2.Distance(centerCheckPosition, targetHitbox.BottomRight());

            float distanceToClosestPoint = topLeftDistance;
            if (topRightDistance < distanceToClosestPoint)
                distanceToClosestPoint = topRightDistance;
            if (bottomLeftDistance < distanceToClosestPoint)
                distanceToClosestPoint = bottomLeftDistance;
            if (bottomRightDistance < distanceToClosestPoint)
                distanceToClosestPoint = bottomRightDistance;

            return distanceToClosestPoint <= radius;
        }
        public static Vector2 ClosestPointInHitbox(Rectangle hitboxOfTarget, Vector2 desiredLocation)
        {
            Vector2 offset = desiredLocation - hitboxOfTarget.Center.ToVector2();
            offset.X = Math.Min(Math.Abs(offset.X), hitboxOfTarget.Width / 2) * Math.Sign(offset.X);
            offset.Y = Math.Min(Math.Abs(offset.Y), hitboxOfTarget.Height / 2) * Math.Sign(offset.Y);
            return hitboxOfTarget.Center.ToVector2() + offset;
        }
        public static void DustCircle(Vector2 position, int amount, float speed, int dustID, float scale = 1, bool noGrav = true, int alpha = 0, Color newColor = default)
        {
            for (int i = 0; i < amount; i++)
            {
                var dust = Dust.NewDustDirect(position, 1, 1, dustID);
                dust.velocity = new Vector2(0, -speed).RotatedBy(MathHelper.ToRadians(i * (360 / amount)));
                if (noGrav)
                {
                    dust.noGravity = true;
                }
            }
        }
        public static void ExpandHitboxBy(this Projectile projectile, int width, int height)
        {
            projectile.position = projectile.Center;
            projectile.width = width;
            projectile.height = height;
            projectile.position -= projectile.Size * 0.5f;
        }
        public static void CirculateOldpos(this Projectile projectile)
        {
            for (int i = projectile.oldPos.Length - 1; i > 0; i--)
            {
                projectile.oldPos[i] = projectile.oldPos[i - 1];
                projectile.oldRot[i] = projectile.oldRot[i - 1];
            }
            projectile.oldPos[0] = projectile.Center;
            projectile.oldRot[0] = projectile.rotation;
        }
        public static Vector2 ToDegreesVector2(this float num, float mult = 1, float add = 0) => MathHelper.ToRadians(num * mult + add).ToRotationVector2();
        public static Vector2 DirectionToSafe(this Vector2 pos1, Vector2 pos2)
        {
            pos1 = Vector2.Normalize(-pos1 + pos2);
            if (pos1.HasNaNs()) pos1 = Vector2.Zero;
            return pos1;
        }
        public static Vector2 SafeDirectionTo(this Entity entity, Vector2 destination, Vector2? fallback = null)
        {
            if (!fallback.HasValue)
            {
                fallback = Vector2.Zero;
            }
            return Utils.SafeNormalize(destination - entity.Center, fallback.Value);
        }
        public static bool WithinBounds(this int index, int cap)
        {
            if (index >= 0)
            {
                return index < cap;
            }
            return false;
        }

        public static ShtunNpcs Shtun(this NPC npc)
            => npc.GetGlobalNPC<ShtunNpcs>();
        public static ShtunPlayer Shtun(this Player player)
            => player.GetModPlayer<ShtunPlayer>();
        public static ShtunShield Shield(this Player player)
            => player.GetModPlayer<ShtunShield>();
        public static ShtunRadiation Radiation(this Player player)
            => player.GetModPlayer<ShtunRadiation>();
        public static ElectricalItem Electricity(this Item item)
            => item.GetGlobalItem<ElectricalItem>();

        public static bool Stalin = ShtunConfig.Instance.Stalin;
        public static string TryStalinTexture => Stalin ? "_Stalin" : "";

        public static NPC ClosestNPCAt(this Vector2 origin, float maxDistanceToCheck, bool ignoreTiles = true, bool bossPriority = false)
        {
            NPC closestTarget = null;
            float distance = maxDistanceToCheck;
            if (bossPriority)
            {
                bool bossFound = false;
                for (int index = 0; index < Main.npc.Length; index++)
                {
                    // If we've found a valid boss target, ignore ALL targets which aren't bosses.
                    if (bossFound && !(Main.npc[index].boss || Main.npc[index].type == NPCID.WallofFleshEye))
                        continue;

                    if (Main.npc[index].CanBeChasedBy(null, false))
                    {
                        float extraDistance = (Main.npc[index].width / 2) + (Main.npc[index].height / 2);

                        bool canHit = true;
                        if (extraDistance < distance && !ignoreTiles)
                            canHit = Collision.CanHit(origin, 1, 1, Main.npc[index].Center, 1, 1);

                        if (Vector2.Distance(origin, Main.npc[index].Center) < distance && canHit)
                        {
                            if (Main.npc[index].boss || Main.npc[index].type == NPCID.WallofFleshEye)
                                bossFound = true;

                            distance = Vector2.Distance(origin, Main.npc[index].Center);
                            closestTarget = Main.npc[index];
                        }
                    }
                }
            }
            else
            {
                for (int index = 0; index < Main.npc.Length; index++)
                {
                    if (Main.npc[index].CanBeChasedBy(null, false))
                    {
                        float extraDistance = (Main.npc[index].width / 2) + (Main.npc[index].height / 2);

                        bool canHit = true;
                        if (extraDistance < distance && !ignoreTiles)
                            canHit = Collision.CanHit(origin, 1, 1, Main.npc[index].Center, 1, 1);

                        if (Vector2.Distance(origin, Main.npc[index].Center) < distance && canHit)
                        {
                            distance = Vector2.Distance(origin, Main.npc[index].Center);
                            closestTarget = Main.npc[index];
                        }
                    }
                }
            }
            return closestTarget;
        }

        public static NPC MinionHoming(this Vector2 origin, float maxDistanceToCheck, Player owner, bool ignoreTiles = true, bool checksRange = false)
        {
            if (owner == null || !((Entity)owner).whoAmI.WithinBounds(255) || !owner.MinionAttackTargetNPC.WithinBounds(200))
            {
                return origin.ClosestNPCAt(maxDistanceToCheck, ignoreTiles);
            }
            NPC val = Main.npc[owner.MinionAttackTargetNPC];
            bool flag = true;
            if (!ignoreTiles)
            {
                flag = Collision.CanHit(origin, 1, 1, ((Entity)val).Center, 1, 1);
            }
            float num = ((Entity)val).width / 2 + ((Entity)val).height / 2;
            bool flag2 = Vector2.Distance(origin, ((Entity)val).Center) < maxDistanceToCheck + num || !checksRange;
            if (owner.HasMinionAttackTargetNPC && flag && flag2)
            {
                return val;
            }
            return origin.ClosestNPCAt(maxDistanceToCheck, ignoreTiles);
        }
        public static float AngleToSafe(this Vector2 pos1, Vector2 pos2) => DirectionToSafe(pos1, pos2).ToRotation();
        public static Vector2 DirectionFromSafe(this Entity entity, Vector2 pos)
        {
            pos = entity.DirectionFrom(pos);
            if (pos.HasNaNs()) pos = Vector2.Zero;
            return pos;
        }
        public static Vector2 DirectionToSafe(this Entity entity, Vector2 pos)
        {
            pos = entity.DirectionTo(pos);
            if (pos.HasNaNs()) pos = Vector2.Zero;
            return pos;
        }
        public static int ClosetNPC(this Projectile projectile, float maxDist) => projectile.Center.ClosetNPC(maxDist, !projectile.tileCollide);
        public static int ClosetNPC(this Vector2 pos, float maxDist, bool ignoreTile = true)
        {
            int index = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC n = Main.npc[i];
                if (n.active && !n.friendly && n.CanBeChasedBy() && (maxDist == -1 || n.Hitbox.Distance(pos) < maxDist))
                {
                    if (ignoreTile || Collision.CanHit(pos, 1, 1, n.position, n.width, n.height))
                    {
                        index = i;
                        maxDist = n.Hitbox.Distance(pos);
                    }
                }
            }
            return index;
        }
        public static int TransFloatToInt(this float num)
        {
            int low = (int)num;
            int chance = (int)((num - low) * 100);
            if (Main.rand.Next(100) < chance) low++;
            return low;
        }
        public static Projectile[] XWay(int num, IEntitySource spawnSource, Vector2 pos, int type, float speed, int damage, float knockback)
        {
            Projectile[] projs = new Projectile[num];
            double spread = 2 * Math.PI / num;
            for (int i = 0; i < num; i++)
                projs[i] = NewProjectileDirectSafe(spawnSource, pos, new Vector2(speed, speed).RotatedBy(spread * i), type, damage, knockback, Main.myPlayer);
            return projs;
        }
        public static Projectile NewProjectileDirectSafe(IEntitySource spawnSource, Vector2 pos, Vector2 vel, int type, int damage, float knockback, int owner = 255, float ai0 = 0f, float ai1 = 0f)
        {
            int p = Projectile.NewProjectile(spawnSource, pos, vel, type, damage, knockback, owner, ai0, ai1);
            return p < Main.maxProjectiles ? Main.projectile[p] : null;
        }
        public static int GetProjectileByIdentity(int player, float projectileIdentity, params int[] projectileType)
        {
            return GetProjectileByIdentity(player, (int)projectileIdentity, projectileType);
        }
        public static int GetProjectileByIdentity(int player, int projectileIdentity, params int[] projectileType)
        {
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].identity == projectileIdentity && Main.projectile[i].owner == player
                    && (projectileType.Length == 0 || projectileType.Contains(Main.projectile[i].type)))
                {
                    return i;
                }
            }
            return -1;
        }
        public static bool OtherBossAlive(int npcID)
        {
            if (npcID > -1 && npcID < Main.maxNPCs)
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    if (Main.npc[i].active && Main.npc[i].boss && i != npcID)
                        return true;
                }
            }
            return false;
        }
        public static void DisplayLocalizedText(string key, Color? textColor = null)
        {
            if (!textColor.HasValue)
            {
                textColor = Color.Green;
            }
            if (Main.netMode == 0)
            {
                Main.NewText((object)Language.GetTextValue(key), (Color?)textColor.Value);
            }
            else if (Main.netMode == 2 || Main.netMode == 1)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key, Array.Empty<object>()), textColor.Value, -1);
            }
        }
        public static void ClearFriendlyProjectiles(int deletionRank = 0, int bossNpc = -1, bool clearSummonProjs = false)
        {
            ClearProjectiles(false, true, deletionRank, bossNpc, clearSummonProjs);
        }
        public static void ClearHostileProjectiles(int deletionRank = 0, int bossNpc = -1)
        {
            ClearProjectiles(true, false, deletionRank, bossNpc);
        }
        public static void ClearAllProjectiles(int deletionRank = 0, int bossNpc = -1, bool clearSummonProjs = false)
        {
            ClearProjectiles(true, true, deletionRank, bossNpc, clearSummonProjs);
        }
        private static void ClearProjectiles(bool clearHostile, bool clearFriendly, int deletionRank = 0, int bossNpc = -1, bool clearSummonProjs = false)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
                return;

            if (OtherBossAlive(bossNpc))
                clearHostile = false;

            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile projectile = Main.projectile[i];
                    if (projectile.active && ((projectile.hostile && clearHostile) || (projectile.friendly && clearFriendly)) && CanDeleteProjectile(projectile, deletionRank, clearSummonProjs))
                    {
                        projectile.Kill();
                    }
                }
            }
        }
        public static string GetModItemText(int ItemID, string color = "", string Itemname = "")
        {
            Item val = new Item();
            val.SetDefaults(ItemID, false);
            string text = val.Name;
            if (Itemname != "")
            {
                text = Itemname;
            }
            if (color == "")
            {
                return "[i:" + ItemID + "]「" + text + "」";
            }
            return "[i:" + ItemID + "]「[c/" + color + ":" + text + "]」";
        }
        public static int ScaledProjectileDamage(int npcDamage, float modifier = 1, int npcDamageCalculationsOffset = 2)
        {
            const float inherentHostileProjMultiplier = 2;
            float worldDamage = ProjWorldDamage;
            return (int)(modifier * npcDamage / inherentHostileProjMultiplier / Math.Max(npcDamageCalculationsOffset, worldDamage));
        }
        public static bool IsSummonDamage(Projectile projectile, bool includeMinionShot = true, bool includeWhips = true)
        {
            if (!includeWhips && ProjectileID.Sets.IsAWhip[projectile.type])
                return false;

            if (!includeMinionShot && (ProjectileID.Sets.MinionShot[projectile.type] || ProjectileID.Sets.SentryShot[projectile.type]))
                return false;

            return projectile.CountsAsClass(DamageClass.Summon) || projectile.minion || projectile.sentry || projectile.minionSlots > 0 || ProjectileID.Sets.MinionSacrificable[projectile.type]
                || (includeMinionShot && (ProjectileID.Sets.MinionShot[projectile.type] || ProjectileID.Sets.SentryShot[projectile.type]))
                || (includeWhips && ProjectileID.Sets.IsAWhip[projectile.type]);
        }
        public static bool CanDeleteProjectile(Projectile projectile, int deletionRank = 0, bool clearSummonProjs = false)
        {
            if (!projectile.active)
                return false;
            if (projectile.damage <= 0)
                return false;
            if (projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().DeletionImmuneRank > deletionRank)
                return false;
            if (projectile.friendly)
            {
                if (projectile.whoAmI == Main.player[projectile.owner].heldProj)
                    return false;
                if (IsSummonDamage(projectile, false) && !clearSummonProjs)
                    return false;
            }
            return true;
        }
        public static Vector2 ClosestPointInHitbox(Entity entity, Vector2 desiredLocation)
        {
            return ClosestPointInHitbox(entity.Hitbox, desiredLocation);
        }
        public static float ProjWorldDamage => Main.GameModeInfo.IsJourneyMode
            ? CreativePowerManager.Instance.GetPower<CreativePowers.DifficultySliderPower>().StrengthMultiplierToGiveNPCs
            : Main.GameModeInfo.EnemyDamageMultiplier;
        public static Projectile ProjectileExists(int whoAmI, params int[] types)
        {
            return whoAmI > -1 && whoAmI < Main.maxProjectiles && Main.projectile[whoAmI].active && (types.Length == 0 || types.Contains(Main.projectile[whoAmI].type)) ? Main.projectile[whoAmI] : null;
        }
        public static Projectile ProjectileExists(float whoAmI, params int[] types)
        {
            return ProjectileExists((int)whoAmI, types);
        }
        public static int FindClosestHostileNPC(Vector2 location, float detectionRange, bool lineCheck = false)
        {
            NPC closestNpc = null;
            foreach (NPC n in Main.npc)
            {
                if (n.CanBeChasedBy() && n.Distance(location) < detectionRange && (!lineCheck || Collision.CanHitLine(location, 0, 0, n.Center, 0, 0)))
                {
                    detectionRange = n.Distance(location);
                    closestNpc = n;
                }
            }
            return closestNpc == null ? -1 : closestNpc.whoAmI;
        }
        public static bool BossIsAlive(ref int bossID, int bossType)
        {
            if (bossID != -1)
            {
                if (Main.npc[bossID].active && Main.npc[bossID].type == bossType)
                {
                    return true;
                }
                else
                {
                    bossID = -1;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public static bool AnyBossAlive()
        {
            if (ShtunNpcs.boss == -1)
                return false;
            if (Main.npc[ShtunNpcs.boss].active && (Main.npc[ShtunNpcs.boss].boss))
                return true;
            ShtunNpcs.boss = -1;
            return false;
        }
        public static int FindClosestHostileNPCPrioritizingMinionFocus(Projectile projectile, float detectionRange, bool lineCheck = false, Vector2 center = default)
        {
            if (center == default)
                center = projectile.Center;

            NPC minionAttackTargetNpc = projectile.OwnerMinionAttackTargetNPC;
            if (minionAttackTargetNpc != null && minionAttackTargetNpc.CanBeChasedBy() && minionAttackTargetNpc.Distance(center) < detectionRange
                && (!lineCheck || Collision.CanHitLine(center, 0, 0, minionAttackTargetNpc.Center, 0, 0)))
            {
                return minionAttackTargetNpc.whoAmI;
            }
            return FindClosestHostileNPC(center, detectionRange, lineCheck);
        }
        public static NPC NPCExists(int whoAmI, params int[] types)
        {
            return whoAmI > -1 && whoAmI < Main.maxNPCs && Main.npc[whoAmI].active && (types.Length == 0 || types.Contains(Main.npc[whoAmI].type)) ? Main.npc[whoAmI] : null;
        }
        public static int NewNPCEasy(IEntitySource source, Vector2 spawnPos, int type, int start = 0, float ai0 = 0, float ai1 = 0, float ai2 = 0, float ai3 = 0, int target = 255, Vector2 velocity = default)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
                return Main.maxNPCs;

            int n = NPC.NewNPC(source, (int)spawnPos.X, (int)spawnPos.Y, type, start, ai0, ai1, ai2, ai3, target);
            if (n != Main.maxNPCs)
            {
                if (velocity != default)
                {
                    Main.npc[n].velocity = velocity;
                }

                if (Main.netMode == NetmodeID.Server)
                    NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, n);
            }

            return n;
        }
        public static NPC NPCExists(float whoAmI, params int[] types)
        {
            return NPCExists((int)whoAmI, types);
        }

        #region Shader Utils

        private static readonly FieldInfo shaderTextureField = typeof(MiscShaderData).GetField("_uImage1", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo shaderTextureField2 = typeof(MiscShaderData).GetField("_uImage2", BindingFlags.NonPublic | BindingFlags.Instance);

        /// <summary>
        /// Uses reflection to set uImage1. Its underlying data is private and the only way to change it publicly
        /// is via a method that only accepts paths to vanilla textures.
        /// </summary>
        /// <param name="shader">The shader</param>
        /// <param name="texture">The texture to set</param>
        public static void SetShaderTexture(this MiscShaderData shader, Asset<Texture2D> texture) => shaderTextureField.SetValue(shader, texture);

        /// <summary>
        /// Uses reflection to set uImage2. Its underlying data is private and the only way to change it publicly
        /// is via a method that only accepts paths to vanilla textures.
        /// </summary>
        /// <param name="shader">The shader</param>
        /// <param name="texture">The texture to set</param>
        public static void SetShaderTexture2(this MiscShaderData shader, Asset<Texture2D> texture) => shaderTextureField2.SetValue(shader, texture);

        /// <summary>
        /// Prepares a <see cref="SpriteBatch"/> for shader-based drawing.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public static void EnterShaderRegion(this SpriteBatch spriteBatch)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
        }

        /// <summary>
        /// Ends changes to a <see cref="SpriteBatch"/> based on shader-based drawing in favor of typical draw begin states.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public static void ExitShaderRegion(this SpriteBatch spriteBatch)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
        }
        #endregion
        public static float ActualClassDamage(this Player player, DamageClass damageClass)
            => player.GetTotalDamage(damageClass).Additive * player.GetTotalDamage(damageClass).Multiplicative;
        public static int HighestDamageTypeScaling(Player player, int dmg)
        {
            List<float> types = new List<float> {
                player.ActualClassDamage(DamageClass.Melee),
                player.ActualClassDamage(DamageClass.Ranged),
                player.ActualClassDamage(DamageClass.Magic),
                player.ActualClassDamage(DamageClass.Summon)
            };
            return (int)(types.Max() * dmg);
        }
    }
}