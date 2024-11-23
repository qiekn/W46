using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    public abstract class WeaponBehaviour : MonoBehaviour
    {
        #region UNITY

        protected virtual void Awake(){}
        protected virtual void Start(){}
        protected virtual void Update(){}
        protected virtual void LateUpdate(){}

        #endregion

        #region GETTERS

        // Returns the sprite to use when displaying the weapon's body.
        public abstract Sprite GetSpriteBody(); // UI Icon

        public abstract float GetRateOfFire();                      // RPM
        public abstract float GetMultiplierMovementSpeed();         // Movement Speed fines or bouns
        public abstract float GetFieldOfViewMultiplierAim();        // For just aimming
        public abstract float GetFieldOfViewMultiplierAimWeapon();  // For weapon scope

        public abstract Animator GetAnimator();
        public abstract RuntimeAnimatorController GetAnimatorController();
        public abstract WeaponAttachmentManagerBehaviour GetAttachmentManager();
        

        //NOTE: Audio
        public abstract AudioClip GetAudioClipHolster();
        public abstract AudioClip GetAudioClipUnholster();

        public abstract AudioClip GetAudioClipFire();
        public abstract AudioClip GetAudioClipFireEmpty();
        public abstract AudioClip GetAudioClipBoltAction();

        public abstract AudioClip GetAudioClipReload();
        public abstract AudioClip GetAudioClipReloadEmpty();
        
        // Reload Cycled
        public abstract AudioClip GetAudioClipReloadOpen();
        public abstract AudioClip GetAudioClipReloadInsert();
        public abstract AudioClip GetAudioClipReloadClose();

        
        //NOTE: Ammo
        public abstract int GetAmmunitionCurrent();  // Current Ammo
        public abstract int GetAmmunitionTotal();    // Total Ammo
        public abstract bool HasAmmunition();
        public abstract bool IsFull();

        
        //NOTE: Specific
        public abstract bool IsAutomatic();      // IsAutomatic weapon?
        public abstract bool IsBoltAction();     // Is bolt-action sniper rifle?
        public abstract bool HasCycledReload();  // Is shortgun?

        public abstract bool CanReloadAimed();
        public abstract bool CanReloadWhenFull();
        public abstract bool GetAutomaticallyReloadOnEmpty();
        public abstract float GetAutomaticallyReloadOnEmptyDelay();

        #endregion

        #region METHODS

        public abstract void Fire(float spreadMultiplier = 1.0f); // Multiplay to the weapon's spread.
        public abstract void Reload();

        // Fills the character's equipped weapon's ammunition by a certain amount, or fully if set to -1.
        public abstract void FillAmmunition(int amount);
        // Sets the slide back pose.
        public abstract void SetSlideBack(int back);

        // Ejects a casing from the weapon. This is commonly called from animation events, but can be called from anywhere.
        public abstract void EjectCasing();

        #endregion
    }
}
