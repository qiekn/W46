﻿using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    // Weapon Attachment Manager. Handles equipping and storing a Weapon's Attachments.
    // Attachmeents: Muzzle, Laser, Grip, Scope, Magazine
    public class WeaponAttachmentManager : WeaponAttachmentManagerBehaviour {
        #region FIELDS SERIALIZED

        //NOTE: Muzzle
        [Title(label: "Muzzle")]

        [Tooltip("Selected Muzzle Index.")]
        [SerializeField]
        private int muzzleIndex = 0;

        [Tooltip("All possible Muzzle Attachments that this Weapon can use!")]
        [SerializeField]
        private MuzzleBehaviour[] muzzleArray;
        
        //NOTE: Laser
        [Title(label: "Laser")]

        [Tooltip("Selected Laser Index.")]
        [SerializeField]
        private int laserIndex = -1;

        [Tooltip("All possible Laser Attachments that this Weapon can use!")]
        [SerializeField]
        private LaserBehaviour[] laserArray;
        
        //NOTE: Grip
        [Title(label: "Grip")]

        [Tooltip("Selected Grip Index.")]
        [SerializeField]
        private int gripIndex = -1;

        [Tooltip("All possible Grip Attachments that this Weapon can use!")]
        [SerializeField]
        private GripBehaviour[] gripArray;
        
        //NOTE: Scope
        [Title(label: "Scope")]

        [Tooltip("Determines if the ironsights should be shown on the weapon model.")]
        [SerializeField]
        private bool scopeDefaultShow = true;
        
        [Tooltip("Default Ironsight Scope!")]
        [SerializeField]
        private ScopeBehaviour scopeDefaultBehaviour;

        [SerializeField] private int scopeIndex = -1;
        [SerializeField] private ScopeBehaviour[] scopeArray;
        
        //NOTE: Magazine
        [Title(label: "Magazine")]

        [Tooltip("Selected Magazine Index.")]
        [SerializeField]
        private int magazineIndex = 0;

        [Tooltip("All possible Magazine Attachments that this Weapon can use!")]
        [SerializeField]
        private Magazine[] magazineArray;

        #endregion

        private MuzzleBehaviour muzzleBehaviour;      // Equipped Muzzle.
        private LaserBehaviour laserBehaviour;        // Equipped Laser.
        private GripBehaviour gripBehaviour;          // Equipped Grip.
        private ScopeBehaviour scopeBehaviour;        // Equipped Scope.
        private MagazineBehaviour magazineBehaviour;  // Equipped Magazine.

        // Awake.
        protected override void Awake() {
            muzzleBehaviour = muzzleArray.SelectAndSetActive(muzzleIndex);       // Select Muzzle!
            laserBehaviour  = laserArray.SelectAndSetActive(laserIndex);         // Select Laser!
            gripBehaviour   = gripArray.SelectAndSetActive(gripIndex);           // Select Grip!
            scopeBehaviour  = scopeArray.SelectAndSetActive(scopeIndex);         // Select Scope!
            scopeBehaviour.gameObject.SetActive(scopeDefaultShow);               // Toogle IronSight!
            magazineBehaviour = magazineArray.SelectAndSetActive(magazineIndex); // Select Magazine!
            //Check if we have no scope. This could happen if we have an incorrect index.
            if (scopeBehaviour == null) {
                scopeBehaviour = scopeDefaultBehaviour;
            }
        }        

        public override MuzzleBehaviour GetEquippedMuzzle() => muzzleBehaviour;
        public override LaserBehaviour GetEquippedLaser() => laserBehaviour;
        public override GripBehaviour GetEquippedGrip() => gripBehaviour;
        public override ScopeBehaviour GetEquippedScope() => scopeBehaviour;
        public override ScopeBehaviour GetEquippedScopeDefault() => scopeDefaultBehaviour;
        public override MagazineBehaviour GetEquippedMagazine() => magazineBehaviour;
    }
}
