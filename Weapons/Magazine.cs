using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    // Magazine.
    public class Magazine : MagazineBehaviour
    {
        #region FIELDS SERIALIZED

        [Title(label: "Settings")]
        
        [Tooltip("Total Ammunition.")]
        [SerializeField]
        private int ammunitionTotal = 10;

        [Title(label: "Interface")]

        [Tooltip("Interface Sprite.")]
        [SerializeField]
        private Sprite sprite;

        #endregion

        #region GETTERS

        // Ammunition Total.
        public override int GetAmmunitionTotal() => ammunitionTotal;
        // Sprite.
        public override Sprite GetSprite() => sprite;

        #endregion
    }
}
