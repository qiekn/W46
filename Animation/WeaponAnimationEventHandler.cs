﻿using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    // Handles all the animation events that come from the weapon in the asset.
    public class WeaponAnimationEventHandler : MonoBehaviour
    {
        #region FIELDS

        // Equipped Weapon.
        private WeaponBehaviour weapon;

        #endregion

        #region UNITY

        private void Awake()
        {
            //Cache. We use this one to call things on the weapon later.
            weapon = GetComponent<WeaponBehaviour>();
        }

        #endregion

        #region ANIMATION

        // Ejects a casing from this weapon. This function is called from an Animation Event.
        private void OnEjectCasing()
        {
            //Notify.
            if(weapon != null)
                weapon.EjectCasing();
        }

        #endregion
    }
}
