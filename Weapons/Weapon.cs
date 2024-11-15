﻿using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    // Weapon. This class handles most of the things that weapons need.
    public class Weapon : WeaponBehaviour
    {
        #region FIELDS SERIALIZED
        
        [Title(label: "Settings")]
        
        [Tooltip("Weapon Name. Currently not used for anything, but in the future, we will use this for pickups!")]
        [SerializeField] 
        private string weaponName;

        [Tooltip("How much the character's movement speed is multiplied by when wielding this weapon.")]
        [SerializeField]
        private float multiplierMovementSpeed = 1.0f;
        
        [Title(label: "Firing")]

        [Tooltip("Is this weapon automatic? If yes, then holding down the firing button will continuously fire.")]
        [SerializeField] 
        private bool automatic;
        
        [Tooltip("Is this weapon bolt-action? If yes, then a bolt-action animation will play after every shot.")]
        [SerializeField]
        private bool boltAction;

        [Tooltip("Amount of shots fired at once. Helpful for things like shotguns, where there are multiple projectiles fired at once.")]
        [SerializeField]
        private int shotCount = 1;
        
        [Tooltip("How far the weapon can fire from the center of the screen.")]
        [SerializeField]
        private float spread = 0.25f;
        
        [Tooltip("How fast the projectiles are.")]
        [SerializeField]
        private float projectileImpulse = 400.0f;

        [Tooltip("Amount of shots this weapon can shoot in a minute. It determines how fast the weapon shoots.")]
        [SerializeField] 
        private int roundsPerMinutes = 200;

        [Title(label: "Reloading")]
        
        [Tooltip("Determines if this weapon reloads in cycles, meaning that it inserts one bullet at a time, or not.")]
        [SerializeField]
        private bool cycledReload;
        
        [Tooltip("Determines if the player can reload this weapon when it is full of ammunition.")]
        [SerializeField]
        private bool canReloadWhenFull = true;

        [Tooltip("Should this weapon be reloaded automatically after firing its last shot?")]
        [SerializeField]
        private bool automaticReloadOnEmpty;

        [Tooltip("Time after the last shot at which a reload will automatically start.")]
        [SerializeField]
        private float automaticReloadOnEmptyDelay = 0.25f;

        [Title(label: "Animation")]

        [Tooltip("Transform that represents the weapon's ejection port, meaning the part of the weapon that casings shoot from.")]
        [SerializeField]
        private Transform socketEjection;

        [Tooltip("Settings this to false will stop the weapon from being reloaded while the character is aiming it.")]
        [SerializeField]
        private bool canReloadAimed = true;

        [Title(label: "Resources")]

        [Tooltip("Casing Prefab.")]
        [SerializeField]
        private GameObject prefabCasing;
        
        [Tooltip("Projectile Prefab. This is the prefab spawned when the weapon shoots.")]
        [SerializeField]
        private GameObject prefabProjectile;
        
        [Tooltip("The AnimatorController a player character needs to use while wielding this weapon.")]
        [SerializeField] 
        public RuntimeAnimatorController controller;

        [Tooltip("Weapon Body Texture.")]
        [SerializeField]
        private Sprite spriteBody;
        
        [Title(label: "Audio Clips Holster")]

        [Tooltip("Holster Audio Clip.")]
        [SerializeField]
        private AudioClip audioClipHolster;

        [Tooltip("Unholster Audio Clip.")]
        [SerializeField]
        private AudioClip audioClipUnholster;
        
        [Title(label: "Audio Clips Reloads")]

        [Tooltip("Reload Audio Clip.")]
        [SerializeField]
        private AudioClip audioClipReload;
        
        [Tooltip("Reload Empty Audio Clip.")]
        [SerializeField]
        private AudioClip audioClipReloadEmpty;
        
        [Title(label: "Audio Clips Reloads Cycled")]
        
        [Tooltip("Reload Open Audio Clip.")]
        [SerializeField]
        private AudioClip audioClipReloadOpen;
        
        [Tooltip("Reload Insert Audio Clip.")]
        [SerializeField]
        private AudioClip audioClipReloadInsert;
        
        [Tooltip("Reload Close Audio Clip.")]
        [SerializeField]
        private AudioClip audioClipReloadClose;
        
        [Title(label: "Audio Clips Other")]

        [Tooltip("AudioClip played when this weapon is fired without any ammunition.")]
        [SerializeField]
        private AudioClip audioClipFireEmpty;
        
        [Tooltip("")]
        [SerializeField]
        private AudioClip audioClipBoltAction;

        #endregion

        #region FIELDS

        // Weapon Animator.
        private Animator animator;
        // Attachment Manager.
        private WeaponAttachmentManagerBehaviour attachmentManager;

        // Amount of ammunition left.
        private int ammunitionCurrent;

        #region Attachment Behaviours
        
        // Equipped scope Reference.
        private ScopeBehaviour scopeBehaviour;
        
        // Equipped Magazine Reference.
        private MagazineBehaviour magazineBehaviour;
        // Equipped Muzzle Reference.
        private MuzzleBehaviour muzzleBehaviour;

        // Equipped Laser Reference.
        private LaserBehaviour laserBehaviour;
        // Equipped Grip Reference.
        private GripBehaviour gripBehaviour;

        #endregion

        // The GameModeService used in this game!
        private IGameModeService gameModeService;
        // The main player character behaviour component.
        private CharacterBehaviour characterBehaviour;

        // The player character's camera.
        private Transform playerCamera;
        
        #endregion

        #region UNITY
        
        protected override void Awake()
        {
            //Get Animator.
            animator = GetComponent<Animator>();
            //Get Attachment Manager.
            attachmentManager = GetComponent<WeaponAttachmentManagerBehaviour>();

            //Cache the game mode service. We only need this right here, but we'll cache it in case we ever need it again.
            gameModeService = ServiceLocator.Current.Get<IGameModeService>();
            //Cache the player character.
            characterBehaviour = gameModeService.GetPlayerCharacter();
            //Cache the world camera. We use this in line traces.
            playerCamera = characterBehaviour.GetCameraWorld().transform;
        }
        protected override void Start()
        {
            #region Cache Attachment References

            //Get Scope.
            scopeBehaviour = attachmentManager.GetEquippedScope();
            
            //Get Magazine.
            magazineBehaviour = attachmentManager.GetEquippedMagazine();
            //Get Muzzle.
            muzzleBehaviour = attachmentManager.GetEquippedMuzzle();

            //Get Laser.
            laserBehaviour = attachmentManager.GetEquippedLaser();
            //Get Grip.
            gripBehaviour = attachmentManager.GetEquippedGrip();

            #endregion

            //Max Out Ammo.
            ammunitionCurrent = magazineBehaviour.GetAmmunitionTotal();
        }

        #endregion

        #region GETTERS

        // GetFieldOfViewMultiplierAim.
        public override float GetFieldOfViewMultiplierAim()
        {
            //Make sure we don't have any issues even with a broken setup!
            if (scopeBehaviour != null) 
                return scopeBehaviour.GetFieldOfViewMultiplierAim();
            
            //Error.
            Debug.LogError("Weapon has no scope equipped!");
  
            //Return.
            return 1.0f;
        }
        // GetFieldOfViewMultiplierAimWeapon.
        public override float GetFieldOfViewMultiplierAimWeapon()
        {
            //Make sure we don't have any issues even with a broken setup!
            if (scopeBehaviour != null) 
                return scopeBehaviour.GetFieldOfViewMultiplierAimWeapon();
            
            //Error.
            Debug.LogError("Weapon has no scope equipped!");
  
            //Return.
            return 1.0f;
        }
        
        // GetAnimator.
        public override Animator GetAnimator() => animator;
        // CanReloadAimed.
        public override bool CanReloadAimed() => canReloadAimed;

        // GetSpriteBody.
        public override Sprite GetSpriteBody() => spriteBody;
        // GetMultiplierMovementSpeed.
        public override float GetMultiplierMovementSpeed() => multiplierMovementSpeed;

        // GetAudioClipHolster.
        public override AudioClip GetAudioClipHolster() => audioClipHolster;
        // GetAudioClipUnholster.
        public override AudioClip GetAudioClipUnholster() => audioClipUnholster;

        // GetAudioClipReload.
        public override AudioClip GetAudioClipReload() => audioClipReload;
        // GetAudioClipReloadEmpty.
        public override AudioClip GetAudioClipReloadEmpty() => audioClipReloadEmpty;
        
        // GetAudioClipReloadOpen.
        public override AudioClip GetAudioClipReloadOpen() => audioClipReloadOpen;
        // GetAudioClipReloadInsert.
        public override AudioClip GetAudioClipReloadInsert() => audioClipReloadInsert;
        // GetAudioClipReloadClose.
        public override AudioClip GetAudioClipReloadClose() => audioClipReloadClose;

        // GetAudioClipFireEmpty.
        public override AudioClip GetAudioClipFireEmpty() => audioClipFireEmpty;
        // GetAudioClipBoltAction.
        public override AudioClip GetAudioClipBoltAction() => audioClipBoltAction;
        
        // GetAudioClipFire.
        public override AudioClip GetAudioClipFire() => muzzleBehaviour.GetAudioClipFire();
        // GetAmmunitionCurrent.
        public override int GetAmmunitionCurrent() => ammunitionCurrent;

        // GetAmmunitionTotal.
        public override int GetAmmunitionTotal() => magazineBehaviour.GetAmmunitionTotal();
        // HasCycledReload.
        public override bool HasCycledReload() => cycledReload;

        // IsAutomatic.
        public override bool IsAutomatic() => automatic;
        // IsBoltAction.
        public override bool IsBoltAction() => boltAction;

        // GetAutomaticallyReloadOnEmpty.
        public override bool GetAutomaticallyReloadOnEmpty() => automaticReloadOnEmpty;
        // GetAutomaticallyReloadOnEmptyDelay.
        public override float GetAutomaticallyReloadOnEmptyDelay() => automaticReloadOnEmptyDelay;

        // CanReloadWhenFull.
        public override bool CanReloadWhenFull() => canReloadWhenFull;
        // GetRateOfFire.
        public override float GetRateOfFire() => roundsPerMinutes;
        
        // IsFull.
        public override bool IsFull() => ammunitionCurrent == magazineBehaviour.GetAmmunitionTotal();
        // HasAmmunition.
        public override bool HasAmmunition() => ammunitionCurrent > 0;

        // GetAnimatorController.
        public override RuntimeAnimatorController GetAnimatorController() => controller;
        // GetAttachmentManager.
        public override WeaponAttachmentManagerBehaviour GetAttachmentManager() => attachmentManager;

        #endregion

        #region METHODS

        // Reload.
        public override void Reload()
        {
            //Set Reloading Bool. This helps cycled reloads know when they need to stop cycling.
            const string boolName = "Reloading";
            animator.SetBool(boolName, true);
            
            //Try Play Reload Sound.
            ServiceLocator.Current.Get<IAudioManagerService>().PlayOneShot(HasAmmunition() ? audioClipReload : audioClipReloadEmpty, new AudioSettings(1.0f, 0.0f, false));
            
            //Play Reload Animation.
            animator.Play(cycledReload ? "Reload Open" : (HasAmmunition() ? "Reload" : "Reload Empty"), 0, 0.0f);
        }
        // Fire.
        public override void Fire(float spreadMultiplier = 1.0f)
        {
            //We need a muzzle in order to fire this weapon!
            if (muzzleBehaviour == null)
                return;
            
            //Make sure that we have a camera cached, otherwise we don't really have the ability to perform traces.
            if (playerCamera == null)
                return;

            //Play the firing animation.
            const string stateName = "Fire";
            animator.Play(stateName, 0, 0.0f);
            //Reduce ammunition! We just shot, so we need to get rid of one!
            ammunitionCurrent = Mathf.Clamp(ammunitionCurrent - 1, 0, magazineBehaviour.GetAmmunitionTotal());

            //Set the slide back if we just ran out of ammunition.
            if (ammunitionCurrent == 0)
                SetSlideBack(1);
            
            //Play all muzzle effects.
            muzzleBehaviour.Effect();

            //Spawn as many projectiles as we need.
            for (var i = 0; i < shotCount; i++)
            {
                //Determine a random spread value using all of our multipliers.
                Vector3 spreadValue = Random.insideUnitSphere * (spread * spreadMultiplier);
                //Remove the forward spread component, since locally this would go inside the object we're shooting!
                spreadValue.z = 0;
                //Convert to world space.
                spreadValue = playerCamera.TransformDirection(spreadValue);

                //Spawn projectile from the projectile spawn point.
                GameObject projectile = Instantiate(prefabProjectile, playerCamera.position, Quaternion.Euler(playerCamera.eulerAngles + spreadValue));
                //Add velocity to the projectile.
                projectile.GetComponent<Rigidbody>().linearVelocity = projectile.transform.forward * projectileImpulse;
            }
        }

        // FillAmmunition.
        public override void FillAmmunition(int amount)
        {
            //Update the value by a certain amount.
            ammunitionCurrent = amount != 0 ? Mathf.Clamp(ammunitionCurrent + amount, 
                0, GetAmmunitionTotal()) : magazineBehaviour.GetAmmunitionTotal();
        }
        // SetSlideBack.
        public override void SetSlideBack(int back)
        {
            //Set the slide back bool.
            const string boolName = "Slide Back";
            animator.SetBool(boolName, back != 0);
        }

        // EjectCasing.
        public override void EjectCasing()
        {
            //Spawn casing prefab at spawn point.
            if(prefabCasing != null && socketEjection != null)
                Instantiate(prefabCasing, socketEjection.position, socketEjection.rotation);
        }

        #endregion
    }
}
