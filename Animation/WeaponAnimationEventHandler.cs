using UnityEngine;

namespace InfimaGames.LowPolyShooterPack {
    // Handles all the animation events that come from the weapon in the asset.
    public class WeaponAnimationEventHandler : MonoBehaviour {

        private WeaponBehaviour weapon;

        private void Awake() {
            weapon = GetComponent<WeaponBehaviour>();
        }

        // Ejects a casing from this weapon. This function is called from an Animation Event.
        private void OnEjectCasing() {
            if (weapon != null)
                weapon.EjectCasing();
        }
    }
}
