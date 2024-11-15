using System.Globalization;

namespace InfimaGames.LowPolyShooterPack.Interface
{
    // Total Ammunition Text.
    public class TextAmmunitionTotal : ElementText
    {
        #region METHODS
        
        // Tick.
        protected override void Tick()
        {
            //Total Ammunition.
            float ammunitionTotal = equippedWeaponBehaviour.GetAmmunitionTotal();
            
            //Update Text.
            textMesh.text = ammunitionTotal.ToString(CultureInfo.InvariantCulture);
        }
        
        #endregion
    }
}
