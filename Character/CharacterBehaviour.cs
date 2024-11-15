using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    // Character Abstract Behaviour.
    public abstract class CharacterBehaviour : MonoBehaviour
    {
        #region UNITY

        protected virtual void Awake(){}
        protected virtual void Start(){}
        protected virtual void Update(){}
        protected virtual void LateUpdate(){}

        #endregion
        
        #region GETTERS
        
        // This function should return the amount of shots that the character has fired in succession.
        // Using this value for applying recoil, and for modifying spread is what we have this function for.
        public abstract int GetShotsFired();
        // Returns true when the character's weapons are lowered.
        public abstract bool IsLowered();

        // Returns the player character's main camera.
        public abstract Camera GetCameraWorld();
        // Returns the player character's weapon camera.
        public abstract Camera GetCameraDepth();
        
        // Returns a reference to the Inventory component.
        public abstract InventoryBehaviour GetInventory();

        // Returns the current amount of grenades left.
        public abstract int GetGrenadesCurrent();
        // Returns the total amount of grenades left.
        public abstract int GetGrenadesTotal();

        // Returns true if the character is running.
        public abstract bool IsRunning();
        // Returns true if the character has a weapon that is holstered in their hands.
        public abstract bool IsHolstered();

        // Returns true if the character is crouching.
        public abstract bool IsCrouching();
        // Returns true if the character is reloading.
        public abstract bool IsReloading();

        // Returns true if the character is throwing a grenade.
        public abstract bool IsThrowingGrenade();
        // Returns true if the character is meleeing.
        public abstract bool IsMeleeing();
        
        // Returns true if the character is aiming.
        public abstract bool IsAiming();
        // Returns true if the game cursor is locked.
        public abstract bool IsCursorLocked();

        // Returns true if the tutorial text should be visible on the screen.
        public abstract bool IsTutorialTextVisible();

        // Returns the Movement Input.
        public abstract Vector2 GetInputMovement();
        // Returns the Look Input.
        public abstract Vector2 GetInputLook();

        // Returns the audio clip played when the character throws a grenade.
        public abstract AudioClip[] GetAudioClipsGrenadeThrow();
        // Returns the audio clip played when the character melees.
        public abstract AudioClip[] GetAudioClipsMelee();
        
        // Returns true if the character is inspecting.
        public abstract bool IsInspecting();
        // Returns true if the player is holding the fire button.
        public abstract bool IsHoldingButtonFire();
        
        #endregion

        #region ANIMATION

        // 弹出一个弹壳
        // Ejects a casing from the equipped weapon.
        public abstract void EjectCasing();
        // 补充弹药
        // Fills the character's equipped weapon's ammunition by a certain amount, or fully if set to -1.
        public abstract void FillAmmunition(int amount);

        // Throws a grenade.
        public abstract void Grenade();
        // Sets the equipped weapon's magazine to be active or inactive!
        public abstract void SetActiveMagazine(int active);
        
        // Bolt Animation Ended.
        public abstract void AnimationEndedBolt();
        // Reload Animation Ended.
        public abstract void AnimationEndedReload();

        // Grenade Throw Animation Ended.
        public abstract void AnimationEndedGrenadeThrow();
        // Melee Animation Ended.
        public abstract void AnimationEndedMelee();

        // Inspect Animation Ended.
        public abstract void AnimationEndedInspect();
        // 收枪动画结束
        // Holster Animation Ended.
        public abstract void AnimationEndedHolster();

        // Sets the equipped weapon's slide back pose.
        public abstract void SetSlideBack(int back);

        public abstract void SetActiveKnife(int active);

        #endregion
    }
}
