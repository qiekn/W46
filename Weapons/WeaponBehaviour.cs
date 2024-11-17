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
        public abstract Sprite GetSpriteBody();
        // Returns the value of multiplierMovementSpeed;
        public abstract float GetMultiplierMovementSpeed();

        // 收枪、取枪声音
        public abstract AudioClip GetAudioClipHolster();
        public abstract AudioClip GetAudioClipUnholster();

        // Returns the reload audio clip.
        public abstract AudioClip GetAudioClipReload();
        // Returns the reload empty audio clip.
        public abstract AudioClip GetAudioClipReloadEmpty();
        
        // Returns the reload open audio clip.
        public abstract AudioClip GetAudioClipReloadOpen();
        // Returns the reload insert audio clip.
        public abstract AudioClip GetAudioClipReloadInsert();
        // Returns the reload close audio clip.
        public abstract AudioClip GetAudioClipReloadClose();

        // Returns the fire empty audio clip.
        public abstract AudioClip GetAudioClipFireEmpty();
        // Returns the bolt action audio clip.
        public abstract AudioClip GetAudioClipBoltAction();

        // Returns the fire audio clip.
        public abstract AudioClip GetAudioClipFire();
        
        // Returns Current Ammunition. 
        public abstract int GetAmmunitionCurrent();
        // Returns Total Ammunition.
        public abstract int GetAmmunitionTotal();

        // Determines if this Weapon reloads in cycles.
        public abstract bool HasCycledReload();

        // Returns the Weapon's Animator component.
        public abstract Animator GetAnimator();

        // Returns the value of canReloadAimed.
        public abstract bool CanReloadAimed();
        
        // Returns true if this weapon shoots in automatic.
        public abstract bool IsAutomatic();
        // Returns true if the weapon has any ammunition left.
        public abstract bool HasAmmunition();

        // Returns true if the weapon is full of ammunition.
        public abstract bool IsFull();
        // Returns true if this is a bolt-action weapon.
        public abstract bool IsBoltAction();

        // Returns true if the weapon should be automatically reload when empty.
        public abstract bool GetAutomaticallyReloadOnEmpty();
        // Returns the delay after firing the last shot when the weapon should start automatically reloading.
        public abstract float GetAutomaticallyReloadOnEmptyDelay();

        // Can this weapon be reloaded when it is full?
        public abstract bool CanReloadWhenFull();
        // Returns the weapon's rate of fire.
        public abstract float GetRateOfFire();

        // Returns the field of view multiplier when aiming.
        public abstract float GetFieldOfViewMultiplierAim();
        // Returns the field of view multiplier when aiming for the weapon camera.
        public abstract float GetFieldOfViewMultiplierAimWeapon();

        // Returns the RuntimeAnimationController the Character needs to use when this Weapon is equipped!
        public abstract RuntimeAnimatorController GetAnimatorController();
        // Returns the weapon's attachment manager component.
        public abstract WeaponAttachmentManagerBehaviour GetAttachmentManager();
        
        #endregion

        #region METHODS

        // Fires the weapon.
        // <param name="spreadMultiplier">Value to multiply the weapon's spread by. Very helpful to account for aimed spread multipliers.</param>
        public abstract void Fire(float spreadMultiplier = 1.0f);
        // Reloads the weapon.
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
