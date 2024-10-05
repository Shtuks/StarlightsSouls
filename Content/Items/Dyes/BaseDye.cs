using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace ssm.Content.Items.Dyes
{
    public abstract class BaseDye : ModItem, ILocalizedModType, IModType
    {
        public string LocalizationCategory => "Items.Dyes";

        public abstract ArmorShaderData ShaderDataToBind { get; }

        public virtual void SetStaticDefaults()
        {
            if (!Main.dedServ)
                GameShaders.Armor.BindShader<ArmorShaderData>(this.Item.type, this.ShaderDataToBind);
            this.SafeSetStaticDefaults();
        }

        public virtual void SetDefaults()
        {
            int dye = this.Item.dye;
            this.Item.CloneDefaults(3561);
            this.Item.dye = dye;
            this.SafeSetDefaults();
        }

        public virtual void SafeSetDefaults()
        {
        }

        public virtual void SafeSetStaticDefaults()
        {
        }
    }
}