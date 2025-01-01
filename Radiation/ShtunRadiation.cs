using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace ssm
{
    public class ShtunRadiation : ModPlayer
    {
        //Changable variables
        public bool irradiated; //Have radioactive items in inventory
        public float irradiationSpeed; //How many RAD you get per second
        public float radiationResistance; //0 - 100% value to determine how fast you will take RAD
        public int radiation; //Current RAD
        public float antiradRegen; //Speed of removing radiation

        //Specific variables
        internal int lethalRad = 600; //How many RAD you can have before death
        internal int regenTimer = 0; //Timer for values regeneration

        //Variables for stat sheet
        internal int statRad;
        internal float statRes;

        public override void PostUpdateMiscEffects() { UpdateRad(); }
        public override void ResetEffects() { ResetVariables(); }
        public override void UpdateDead() { ResetVariables(); }

        private void ResetVariables()
        {
            irradiationSpeed = 0f;
            radiationResistance = 0;
            radiation = 0;
            statRad = 0;
            statRes = 0;
            antiradRegen = 0.1f;
            irradiated = false;
        }

        public void Add(int? rad = null)
        {
            //Manualy add RAD
            if (!rad.HasValue)
            {
                rad = lethalRad;
            }
            else
            {
                if (ssm.debug) { ShtunUtils.DisplayLocalizedText("Added " + rad + " RAD", Color.White); }
                radiation += rad.Value;
            }
        }

        public void Remove(int? rad = null)
        {
            //Manualy remove RAD
            if (!rad.HasValue)
            {
                rad = radiation;
            }
            else
            {
                if (ssm.debug) { ShtunUtils.DisplayLocalizedText("Removed " + rad+ " RAD", Color.White); }
                radiation -= rad.Value;
            }
        }

        public void UpdateRad()
        {
            regenTimer++; //increace 1 per tick.

            //If don't have RAD items, regen
            if (!irradiated) 
            {
                //Remove RAD
                if (regenTimer > 60 / antiradRegen)
                {
                    radiation -= 1;
                    regenTimer = 0;
                }
            }

            //If have RAD items, add RAD
            if (irradiated)
            {
                //add RAD
                if (regenTimer > 60 / irradiationSpeed)
                {
                    radiation += 1;
                    regenTimer = 0;
                }
            }

            if (Player.Shtun().geiger)
            {
                statRad = radiation;
                statRes = radiationResistance;
            }
        }
    }
}