// it just don't work




//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Mono.Cecil.Cil;
//using MonoMod.Cil;
//using System;
//using System.Reflection;
//using Terraria;
//using Terraria.ModLoader;
//using Terraria.UI;

//namespace ssm
//{
//    public class UIModIconSystem : ModSystem
//    {
//        private delegate void UIModDelegate(object instance, SpriteBatch spriteBatch);

//        public override void Load()
//        {
//            IL_UIElement.Draw += ModifyUIModILPatch;
//        }

//        public override void Unload()
//        {
//            IL_UIElement.Draw -= ModifyUIModILPatch;
//        }

//        private void ModifyUIModILPatch(ILContext il)
//        {
//            ILCursor c = new ILCursor(il);
//            MethodInfo baseDrawMethod = typeof(UIElement).GetMethod("Draw", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

//            if (c.TryGotoNext(MoveType.After, i => i.MatchCallvirt(baseDrawMethod)))
//            {
//                c.Emit(OpCodes.Ldarg_0);
//                c.Emit(OpCodes.Ldarg_1);
//                c.EmitDelegate<UIModDelegate>(UIDrawMethod);
//            }
//        }

//        private void UIDrawMethod(object instance, SpriteBatch spriteBatch)
//        {
//            if (instance is not UIElement uiElement) return;

//            Type type = instance.GetType();
//            PropertyInfo modNameProp = type.GetProperty("ModName", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
//            MethodInfo getInnerDimensions = type.GetMethod("GetInnerDimensions", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

//            if (modNameProp == null || getInnerDimensions == null) return;

//            string modName = (string)modNameProp.GetValue(instance);
//            CalculatedStyle style = (CalculatedStyle)getInnerDimensions.Invoke(instance, null);

//            Texture2D bgTexture = ModContent.Request<Texture2D>("ssm/Assets/ModIcon/MutantSkyTest").Value;
//            Texture2D modIcon = ModContent.Request<Texture2D>("ssm/Assets/ModIcon/ModIconMutant").Value;
//            Rectangle frameIcon = new Rectangle(((int)(Main.GlobalTimeWrappedHourly * 10f) % 1) * 40, 0, 40, 40);

//            //style.Position()

//            spriteBatch.Draw(bgTexture, new Vector2(100, 100), null, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
//            spriteBatch.Draw(modIcon, new Vector2(100, 100), frameIcon, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
//        }
//    }
//}