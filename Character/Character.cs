﻿using System;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace InfimaGames.LowPolyShooterPack {
	// Main Character Component. This component handles the most important functions of the character, and interfaces
	// with basically every part of the asset, it is the hub where it all converges.
	[RequireComponent(typeof(CharacterKinematics))]
	public sealed class Character : CharacterBehaviour {
		#region FIELDS SERIALIZED

		[Title(label: "References")]

		[Tooltip("The character's LowerWeapon component.")]
		[SerializeField]
		private LowerWeapon lowerWeapon;

		[Title(label: "Inventory")]

		[Tooltip("Determines the index of the weapon to equip when the game starts.")]
		[SerializeField]
		private int weaponIndexEquippedAtStart;

		[Tooltip("Inventory.")]
		[SerializeField]
		private InventoryBehaviour inventory;

		[Title(label: "Grenade")]

		[Tooltip("If true, the character's grenades will never run out.")]
		[SerializeField]
		private bool grenadesUnlimited;

		[Tooltip("Total amount of grenades at start.")]
		[SerializeField]
		private int grenadeTotal = 10;

		[Tooltip("Grenade spawn offset from the character's camera.")]
		[SerializeField]
		private float grenadeSpawnOffset = 1.0f;

		[Tooltip("Grenade Prefab. Spawned when throwing a grenade.")]
		[SerializeField]
		private GameObject grenadePrefab;

		[Title(label: "Knife")]

		[Tooltip("Knife GameObject.")]
		[SerializeField]
		private GameObject knife;

		[Title(label: "Cameras")]

		[Tooltip("Normal Camera.")]
		[SerializeField]
		private Camera cameraWorld;

		[Tooltip("Weapon-Only Camera. Depth.")]
		[SerializeField]
		private Camera cameraDepth;

		[Title(label: "Animation")]

		[Tooltip("Determines how smooth the turning animation is.")]
		[SerializeField]
		private float dampTimeTurning = 0.4f;

		[Tooltip("Determines how smooth the locomotion blendspace is.")]
		[SerializeField]
		private float dampTimeLocomotion = 0.15f;

		[Tooltip("How smoothly we play aiming transitions. Beware that this affects lots of things!")]
		[SerializeField]
		private float dampTimeAiming = 0.3f;

		[Tooltip("Interpolation speed for the running offsets.")]
		[SerializeField]
		private float runningInterpolationSpeed = 12.0f;

		[Tooltip("Determines how fast the character's weapons are aimed.")]
		[SerializeField]
		private float aimingSpeedMultiplier = 1.0f;

		[Title(label: "Animation Procedural")]

		[Tooltip("Character Animator.")]
		[SerializeField]
		private Animator characterAnimator;

		[Title(label: "Field Of View")]

		[Tooltip("Normal world field of view.")]
		[SerializeField]
		private float fieldOfView = 100.0f;

		[Tooltip("Multiplier for the field of view while running.")]
		[SerializeField]
		private float fieldOfViewRunningMultiplier = 1.05f;

		[Tooltip("Weapon-specific field of view.")]
		[SerializeField]
		private float fieldOfViewWeapon = 55.0f;

		[Title(label: "Audio Clips")]

		[Tooltip("Melee Audio Clips.")]
		[SerializeField]
		private AudioClip[] audioClipsMelee;

		[Tooltip("Grenade Throw Audio Clips.")]
		[SerializeField]
		private AudioClip[] audioClipsGrenadeThrow;

		[Title(label: "Input Options")]

		[Tooltip("If true, the running input has to be held to be active.")]
		[SerializeField]
		private bool holdToRun = true;

		[Tooltip("If true, the aiming input has to be held to be active.")]
		[SerializeField]
		private bool holdToAim = true;

		#endregion

		#region FIELDS

		// True if the character is aiming.
		private bool aiming;
		// Last Frame's Aiming Value.
		private bool wasAiming;
		// True if the character is running.
		private bool running;
		// True if the character has its weapon holstered.
		private bool holstered;

		// Last Time.time at which we shot.
		private float lastShotTime;

		// Overlay Layer Index. Useful for playing things like firing animations.
		private int layerOverlay;
		// Holster Layer Index. Used to play holster animations.
		private int layerHolster;
		// Actions Layer Index. Used to play actions like reloading.
		private int layerActions;

		// Cached Movement Component. Used in order to access some of the movement-related properties.
		private MovementBehaviour movementBehaviour;

		// The currently equipped weapon.
		private WeaponBehaviour equippedWeapon;
		// The equipped weapon's attachment manager.
		private WeaponAttachmentManagerBehaviour weaponAttachmentManager;

		// The scope equipped on the character's weapon.
		private ScopeBehaviour equippedWeaponScope;
		// The magazine equipped on the character's weapon.
		private MagazineBehaviour equippedWeaponMagazine;

		// True if the character is reloading.
		private bool reloading;

		// True if the character is inspecting its weapon.
		private bool inspecting;
		// True if the character is throwing a grenade.
		private bool throwingGrenade;

		// True if the character is meleeing.
		private bool meleeing;

		// True if the character is in the middle of holstering a weapon.
		private bool holstering;
		// Alpha Aiming Value. Zero to one value representing aiming. Zero if we're not aiming, and one if we are
		// fully aiming.
		private float aimingAlpha;

		// Crouching Alpha. This value dictates how visible the crouching state is at any given time.
		private float crouchingAlpha;
		// Running Alpha. This value dictates how visible the running state is at any given time.
		private float runningAlpha;

		// Look Axis Values.
		private Vector2 axisLook;

		// Look Axis Values.
		private Vector2 axisMovement;

		// True if the character is playing the bolt-action animation.
		private bool bolting;

		// Current grenades left.
		private int grenadeCount;

		// True if the player is holding the aiming button.
		private bool holdingButtonAim;
		// True if the player is holding the running button.
		private bool holdingButtonRun;
		// True if the player is holding the firing button.
		private bool holdingButtonFire;

		// If true, the tutorial text should be visible on screen.
		private bool tutorialTextVisible;

		// True if the game cursor is locked! Used when pressing "Escape" to allow developers to more easily access the editor.
		private bool cursorLocked;
		// Amount of shots fired in succession. We use this value to increase the spread, and also to apply recoil
		private int shotsFired;

		#endregion

		#region UNITY

		protected override void Awake() {
			#region Lock Cursor

			// Always make sure that our cursor is locked when the game starts!
			cursorLocked = true;
			// Update the cursor's state.
			UpdateCursorState();

			#endregion

			// Cache the movement behaviour.
			movementBehaviour = GetComponent<MovementBehaviour>();

			// Initialize Inventory.
			inventory.Init(weaponIndexEquippedAtStart);

			// Refresh!
			RefreshWeaponSetup();
		}

		protected override void Start() {
			// Max out the grenades.
			grenadeCount = grenadeTotal;

			// Hide knife. We do this so we don't see a giant knife stabbing through the character's hands all the time!
			if (knife != null)
				knife.SetActive(false);

			layerHolster = characterAnimator.GetLayerIndex("Layer Holster");
			layerActions = characterAnimator.GetLayerIndex("Layer Actions");
			layerOverlay = characterAnimator.GetLayerIndex("Layer Overlay");
		}

		protected override void Update() {
			// Match Aim.
			aiming = holdingButtonAim && CanAim();
			// Match Run.
			running = holdingButtonRun && CanRun();

			// Check if we're aiming & Update aimming state
			switch (aiming) {
				// Just Started.
				case true when !wasAiming:
					equippedWeaponScope.OnAim();
					break;
				// Just Stopped.
				case false when wasAiming:
					equippedWeaponScope.OnAimStop();
					break;
			}

			// Holding the firing button.
			if (holdingButtonFire) {
				// Check.
				if (CanPlayAnimationFire() && equippedWeapon.HasAmmunition() && equippedWeapon.IsAutomatic()) {
					// Has fire rate passed.
					if (Time.time - lastShotTime > 60.0f / equippedWeapon.GetRateOfFire())
						Fire();
				} else {
					// Reset fired shots, so recoil/spread does not just stay at max when we've run out
					// of ammo already!
					shotsFired = 0;
				}
			}

			//Update Animator.
			UpdateAnimator();

			//Update Aiming Alpha. We need to get this here because we're using the Animator to interpolate the aiming value.
			aimingAlpha = characterAnimator.GetFloat(AHashes.AimingAlpha);

			//Interpolate the crouching alpha. We do this here as a quick and dirty shortcut, but there's definitely better ways to do this.
			crouchingAlpha = Mathf.Lerp(crouchingAlpha, movementBehaviour.IsCrouching() ? 1.0f : 0.0f, Time.deltaTime * 12.0f);
			//Interpolate the running alpha. We do this here as a quick and dirty shortcut, but there's definitely better ways to do this.
			runningAlpha = Mathf.Lerp(runningAlpha, running ? 1.0f : 0.0f, Time.deltaTime * runningInterpolationSpeed);

			//Running Field Of View Multiplier.
			float runningFieldOfView = Mathf.Lerp(1.0f, fieldOfViewRunningMultiplier, runningAlpha);

			//Interpolate the world camera's field of view based on whether we are aiming or not.
			cameraWorld.fieldOfView = Mathf.Lerp(fieldOfView, fieldOfView * equippedWeapon.GetFieldOfViewMultiplierAim(), aimingAlpha) * runningFieldOfView;
			//Interpolate the depth camera's field of view based on whether we are aiming or not.
			cameraDepth.fieldOfView = Mathf.Lerp(fieldOfViewWeapon, fieldOfViewWeapon * equippedWeapon.GetFieldOfViewMultiplierAimWeapon(), aimingAlpha);

			//Save Aiming Value.
			wasAiming = aiming;
		}

		#endregion

		#region GETTERS

		// GetShotsFired.
		public override int GetShotsFired() => shotsFired;

		// IsLowered.
		public override bool IsLowered() {
			//Weapons are never lowered if we don't even have a LowerWeapon component.
			if (lowerWeapon == null)
				return false;

			return lowerWeapon.IsLowered();
		}

		// GetCameraWorld.
		public override Camera GetCameraWorld() => cameraWorld;
		// GetCameraDepth.
		public override Camera GetCameraDepth() => cameraDepth;

		// GetInventory.
		public override InventoryBehaviour GetInventory() => inventory;

		// GetGrenadesCurrent.
		public override int GetGrenadesCurrent() => grenadeCount;
		// GetGrenadesTotal.
		public override int GetGrenadesTotal() => grenadeTotal;

		// IsRunning.
		public override bool IsRunning() => running;
		// IsHolstered.
		public override bool IsHolstered() => holstered;

		// Is Crouching.
		public override bool IsCrouching() => movementBehaviour.IsCrouching();

		// IsReloading.
		public override bool IsReloading() => reloading;

		// IsThrowingGrenade.
		public override bool IsThrowingGrenade() => throwingGrenade;

		// IsMeleeing.
		public override bool IsMeleeing() => meleeing;

		// IsAiming.
		public override bool IsAiming() => aiming;
		// IsCursorLocked.
		public override bool IsCursorLocked() => cursorLocked;

		// IsTutorialTextVisible.
		public override bool IsTutorialTextVisible() => tutorialTextVisible;

		// GetInputMovement.
		public override Vector2 GetInputMovement() => axisMovement;
		// GetInputLook.
		public override Vector2 GetInputLook() => axisLook;

		// GetAudioClipsGrenadeThrow.
		public override AudioClip[] GetAudioClipsGrenadeThrow() => audioClipsGrenadeThrow;
		// GetAudioClipsMelee.
		public override AudioClip[] GetAudioClipsMelee() => audioClipsMelee;

		// IsInspecting.
		public override bool IsInspecting() => inspecting;
		// IsHoldingButtonFire. 
		public override bool IsHoldingButtonFire() => holdingButtonFire;

		#endregion

		#region METHODS

		// Updates all the animator properties for this frame.
		// i.e. Updates Animator Controller Parameters
		private void UpdateAnimator() {
			#region Reload Stop

			//Check if we're currently reloading cycled.
			const string boolNameReloading = "Reloading";
			if (characterAnimator.GetBool(boolNameReloading)) {
				//If we only have one more bullet to reload, then we can change the boolean already.
				if (equippedWeapon.GetAmmunitionTotal() - equippedWeapon.GetAmmunitionCurrent() < 1) {
					//Update the character animator.
					characterAnimator.SetBool(boolNameReloading, false);
					//Update the weapon animator.
					equippedWeapon.GetAnimator().SetBool(boolNameReloading, false);
				}
			}

			#endregion

			//Leaning. Affects how much the character should apply of the leaning additive animation.
			float leaningValue = Mathf.Clamp01(axisMovement.y);
			characterAnimator.SetFloat(AHashes.LeaningForward, leaningValue, 0.5f, Time.deltaTime);

			//Movement Value. This value affects absolute movement. Aiming movement uses this, as opposed to per-axis movement.
			float movementValue = Mathf.Clamp01(Mathf.Abs(axisMovement.x) + Mathf.Abs(axisMovement.y));
			characterAnimator.SetFloat(AHashes.Movement, movementValue, dampTimeLocomotion, Time.deltaTime);

			//Aiming Speed Multiplier.
			characterAnimator.SetFloat(AHashes.AimingSpeedMultiplier, aimingSpeedMultiplier);

			//Turning Value. This determines how much of the turning animation to play based on our current look rotation.
			characterAnimator.SetFloat(AHashes.Turning, Mathf.Abs(axisLook.x), dampTimeTurning, Time.deltaTime);

			//Horizontal Movement Float.
			characterAnimator.SetFloat(AHashes.Horizontal, axisMovement.x, dampTimeLocomotion, Time.deltaTime);
			//Vertical Movement Float.
			characterAnimator.SetFloat(AHashes.Vertical, axisMovement.y, dampTimeLocomotion, Time.deltaTime);

			//Update the aiming value, but use interpolation. This makes sure that things like firing can transition properly.
			characterAnimator.SetFloat(AHashes.AimingAlpha, Convert.ToSingle(aiming), dampTimeAiming, Time.deltaTime);

			//Set the locomotion play rate. This basically stops movement from happening while in the air.
			const string playRateLocomotionBool = "Play Rate Locomotion";
			characterAnimator.SetFloat(playRateLocomotionBool, movementBehaviour.IsGrounded() ? 1.0f : 0.0f, 0.2f, Time.deltaTime);

			#region Movement Play Rates

			//Update Forward Multiplier. This allows us to change the play rate of our animations based on our movement multipliers.
			characterAnimator.SetFloat(AHashes.PlayRateLocomotionForward, movementBehaviour.GetMultiplierForward(), 0.2f, Time.deltaTime);
			//Update Sideways Multiplier. This allows us to change the play rate of our animations based on our movement multipliers.
			characterAnimator.SetFloat(AHashes.PlayRateLocomotionSideways, movementBehaviour.GetMultiplierSideways(), 0.2f, Time.deltaTime);
			//Update Backwards Multiplier. This allows us to change the play rate of our animations based on our movement multipliers.
			characterAnimator.SetFloat(AHashes.PlayRateLocomotionBackwards, movementBehaviour.GetMultiplierBackwards(), 0.2f, Time.deltaTime);

			#endregion

			//Update Animator Aiming.
			characterAnimator.SetBool(AHashes.Aim, aiming);
			//Update Animator Running.
			characterAnimator.SetBool(AHashes.Running, running);
			//Update Animator Crouching.
			characterAnimator.SetBool(AHashes.Crouching, movementBehaviour.IsCrouching());
		}

		// Plays the inspect animation.
		private void Inspect() {
			//State.
			inspecting = true;
			//Play.
			characterAnimator.CrossFade("Inspect", 0.0f, layerActions, 0);
		}

		// Fires the character's weapon.
		private void Fire() {
			// Increase shots fired. We use this value to increase the spread, and also to apply recoil, so
			// it is very important that we keep it up to date.
			shotsFired++;

			// Save the shot time, so we can calculate the fire rate correctly.
			lastShotTime = Time.time;
			// Fire the weapon! Make sure that we also pass the scope's spread multiplier if we're aiming.
			equippedWeapon.Fire(aiming ? equippedWeaponScope.GetMultiplierSpread() : 1.0f);

			// Play firing animation.
			const string stateName = "Fire";
			characterAnimator.CrossFade(stateName, 0.05f, layerOverlay, 0);

			// Play bolt actioning animation if needed, and if we have ammunition. We don't play this for the last shot.
			if (equippedWeapon.IsBoltAction() && equippedWeapon.HasAmmunition())
				UpdateBolt(true);

			//Automatically reload the weapon if we need to. This is very helpful for things like grenade launchers or rocket launchers.
			if (!equippedWeapon.HasAmmunition() && equippedWeapon.GetAutomaticallyReloadOnEmpty())
				StartCoroutine(nameof(TryReloadAutomatic));
		}

		// Plays the reload animation.
		private void PlayReloadAnimation() {
			#region Animation

			// Get the name of the animation state to play, which depends on weapon settings, and ammunition!
			string stateName = equippedWeapon.HasCycledReload() ? "Reload Open" :
				(equippedWeapon.HasAmmunition() ? "Reload" : "Reload Empty");

			// Play the animation state!
			characterAnimator.Play(stateName, layerActions, 0.0f);

			#endregion

			// Set Reloading Bool. This helps cycled reloads know when they need to stop cycling.
			characterAnimator.SetBool(AHashes.Reloading, reloading = true);

			// Reload.
			equippedWeapon.Reload();
		}

		// Plays The Reload Animation After A Delay. Helpful to reload automatically after running out of ammunition.
		private IEnumerator TryReloadAutomatic() {
			//Yield.
			yield return new WaitForSeconds(equippedWeapon.GetAutomaticallyReloadOnEmptyDelay());

			//Play Reload Animation.
			PlayReloadAnimation();
		}

		// Equip Weapon Coroutine.
		// i.e. 武器切换
		// holster
		private IEnumerator Equip(int index = 0) {
			//Only if we're not holstered, holster. If we are already, we don't need to wait.
			if (!holstered) {
				//Holster.
				SetHolstered(holstering = true);
				//Wait.
				yield return new WaitUntil(() => holstering == false);
			}
			// Unholster. We do this just in case we were holstered.
			// i.e. 有可能我们已经按 H 收枪了
			SetHolstered(false);
			//Play Unholster Animation.
			characterAnimator.Play("Unholster", layerHolster, 0);

			//Equip The New Weapon.
			inventory.Equip(index);
			//Refresh.
			RefreshWeaponSetup();
		}
		// Refresh all weapon things to make sure we're all set up!
		private void RefreshWeaponSetup() {
			//Make sure we have a weapon. We don't want errors!
			if ((equippedWeapon = inventory.GetEquipped()) == null)
				return;

			//Update Animator Controller. We do this to update all animations to a specific weapon's set.
			characterAnimator.runtimeAnimatorController = equippedWeapon.GetAnimatorController();

			//Get the attachment manager so we can use it to get all the attachments!
			weaponAttachmentManager = equippedWeapon.GetAttachmentManager();
			if (weaponAttachmentManager == null)
				return;

			//Get equipped scope. We need this one for its settings!
			equippedWeaponScope = weaponAttachmentManager.GetEquippedScope();
			//Get equipped magazine. We need this one for its settings!
			equippedWeaponMagazine = weaponAttachmentManager.GetEquippedMagazine();
		}

		private void FireEmpty() {
			/*
			 * Save Time. Even though we're not actually firing, we still need this for the fire rate between
			 * empty shots.
			 */
			lastShotTime = Time.time;
			//Play.
			characterAnimator.CrossFade("Fire Empty", 0.05f, layerOverlay, 0);
		}

		// Updates the cursor state based on the value of the cursorLocked variable.
		private void UpdateCursorState() {
			// Update cursor visibility.
			Cursor.visible = !cursorLocked;
			// Update cursor lock state.
			Cursor.lockState = cursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
		}

		// Plays The Grenade Throwing Animation.
		private void PlayGrenadeThrow() {
			//Start State.
			throwingGrenade = true;

			//Play Normal.
			characterAnimator.CrossFade("Grenade Throw", 0.15f,
				characterAnimator.GetLayerIndex("Layer Actions Arm Left"), 0.0f);

			//Play Additive.
			characterAnimator.CrossFade("Grenade Throw", 0.05f,
				characterAnimator.GetLayerIndex("Layer Actions Arm Right"), 0.0f);
		}
		// Play The Melee Animation.
		private void PlayMelee() {
			//Start State.
			meleeing = true;

			//Play Normal.
			characterAnimator.CrossFade("Knife Attack", 0.05f,
				characterAnimator.GetLayerIndex("Layer Actions Arm Left"), 0.0f);

			//Play Additive.
			characterAnimator.CrossFade("Knife Attack", 0.05f,
				characterAnimator.GetLayerIndex("Layer Actions Arm Right"), 0.0f);
		}

		// Changes the value of bolting, and updates the animator.
		private void UpdateBolt(bool value) {
			//Update.
			characterAnimator.SetBool(AHashes.Bolt, bolting = value);
		}
		// Updates the "Holstered" variable, along with the Character's Animator value.
		private void SetHolstered(bool value = true) {
			//Update value.
			holstered = value;

			//Update Animator.
			const string boolName = "Holstered";
			characterAnimator.SetBool(boolName, holstered);
		}

		#region ACTION CHECKS

		// Can Fire.
		private bool CanPlayAnimationFire() {
			//Block.
			if (holstered || holstering)
				return false;

			//Block.
			if (meleeing || throwingGrenade)
				return false;

			//Block.
			if (reloading || bolting)
				return false;

			//Block.
			if (inspecting)
				return false;

			//Return.
			return true;
		}

		// Determines if we can play the reload animation.
		private bool CanPlayAnimationReload() {
			//No reloading!
			if (reloading)
				return false;

			//No meleeing!
			if (meleeing)
				return false;

			//Not actioning a bolt.
			if (bolting)
				return false;

			//Can't reload while throwing a grenade.
			if (throwingGrenade)
				return false;

			//Block while inspecting.
			if (inspecting)
				return false;

			//Block Full Reloading if needed.
			if (!equippedWeapon.CanReloadWhenFull() && equippedWeapon.IsFull())
				return false;

			//Return.
			return true;
		}

		// Returns true if the character is able to throw a grenade.
		private bool CanPlayAnimationGrenadeThrow() {
			//Block.
			if (holstered || holstering)
				return false;

			//Block.
			if (meleeing || throwingGrenade)
				return false;

			//Block.
			if (reloading || bolting)
				return false;

			//Block.
			if (inspecting)
				return false;

			//We need to have grenades!
			if (!grenadesUnlimited && grenadeCount == 0)
				return false;

			//Return.
			return true;
		}

		// Returns true if the Character is able to melee attack.
		private bool CanPlayAnimationMelee() {
			//Block.
			if (holstered || holstering)
				return false;

			//Block.
			if (meleeing || throwingGrenade)
				return false;

			//Block.
			if (reloading || bolting)
				return false;

			//Block.
			if (inspecting)
				return false;

			//Return.
			return true;
		}

		// Returns true if the character is able to holster their weapon.
		private bool CanPlayAnimationHolster() {
			//Block.
			if (meleeing || throwingGrenade)
				return false;

			//Block.
			if (reloading || bolting)
				return false;

			//Block.
			if (inspecting)
				return false;

			//Return.
			return true;
		}

		// Returns true if the Character can change their Weapon.
		private bool CanChangeWeapon() {
			//Block.
			if (holstering)
				return false;

			//Block.
			if (meleeing || throwingGrenade)
				return false;

			//Block.
			if (reloading || bolting)
				return false;

			//Block.
			if (inspecting)
				return false;

			//Return.
			return true;
		}

		// Returns true if the Character can play the Inspect animation.
		private bool CanPlayAnimationInspect() {
			//Block.
			if (holstered || holstering)
				return false;

			//Block.
			if (meleeing || throwingGrenade)
				return false;

			//Block.
			if (reloading || bolting)
				return false;

			//Block.
			if (inspecting)
				return false;

			//Return.
			return true;
		}

		// Returns true if the Character can Aim.
		private bool CanAim() {
			//Block.
			if (holstered || inspecting)
				return false;

			//Block.
			if (meleeing || throwingGrenade)
				return false;

			//Block.
			if ((!equippedWeapon.CanReloadAimed() && reloading) || holstering)
				return false;

			//Return.
			return true;
		}

		// Returns true if the character can run.
		private bool CanRun() {
			//Block.
			if (inspecting || bolting)
				return false;

			//No running while crouching.
			if (movementBehaviour.IsCrouching())
				return false;

			//Block.
			if (meleeing || throwingGrenade)
				return false;

			//Block.
			if (reloading || aiming)
				return false;

			//While trying to fire, we don't want to run. We do this just in case we do fire.
			if (holdingButtonFire && equippedWeapon.HasAmmunition())
				return false;

			//This blocks running backwards, or while fully moving sideways.
			if (axisMovement.y <= 0 || Math.Abs(Mathf.Abs(axisMovement.x) - 1) < 0.01f)
				return false;

			//Return.
			return true;
		}

		#endregion

		#region INPUT

		// Fire.
		public void OnTryFire(InputAction.CallbackContext context) {
			//Block while the cursor is unlocked.
			if (!cursorLocked)
				return;

			//Switch.
			switch (context) {
				//Started.
				case { phase: InputActionPhase.Started }:
					//Hold.
					holdingButtonFire = true;

					//Restart the shots.
					shotsFired = 0;
					break;
				//Performed.
				case { phase: InputActionPhase.Performed }:
					//Ignore if we're not allowed to actually fire.
					if (!CanPlayAnimationFire())
						break;

					//Check.
					if (equippedWeapon.HasAmmunition()) {
						//Check.
						if (equippedWeapon.IsAutomatic()) {
							//Reset fired shots, so recoil/spread does not just stay at max when we've run out
							//of ammo already!
							shotsFired = 0;

							//Break.
							break;
						}

						//Has fire rate passed.
						if (Time.time - lastShotTime > 60.0f / equippedWeapon.GetRateOfFire())
							Fire();
					}
					//Fire Empty.
					else
						FireEmpty();
					break;
				//Canceled.
				case { phase: InputActionPhase.Canceled }:
					//Stop Hold.
					holdingButtonFire = false;

					//Reset shotsFired.
					shotsFired = 0;
					break;
			}
		}

		// Reload.
		// 处理玩家尝试重新装填武器的输入
		public void OnTryPlayReload(InputAction.CallbackContext context) {
			//Block while the cursor is unlocked.
			if (!cursorLocked)
				return;

			//Block.
			if (!CanPlayAnimationReload())
				return;

			//Switch.
			switch (context) {
				//Performed.
				case { phase: InputActionPhase.Performed }:
					//Play Animation.
					PlayReloadAnimation();
					break;
			}
		}

		// Inspect.
		public void OnTryInspect(InputAction.CallbackContext context) {
			//Block while the cursor is unlocked.
			if (!cursorLocked)
				return;

			//Block.
			if (!CanPlayAnimationInspect())
				return;

			//Switch.
			switch (context) {
				//Performed.
				case { phase: InputActionPhase.Performed }:
					//Play Animation.
					Inspect();
					break;
			}
		}

		// Aiming.
		public void OnTryAiming(InputAction.CallbackContext context) {
			// Block while the cursor is unlocked.
			// 确保在特定条件下（如游戏进行中）才能进行瞄准。
			if (!cursorLocked)
				return;

			//Switch.
			switch (context.phase) {
				//Started.
				case InputActionPhase.Started: // InputActionPhase 是一个枚举类型
					if (holdToAim)
						holdingButtonAim = true;
					break;
				//Performed.
				case InputActionPhase.Performed:
					if (!holdToAim)
						holdingButtonAim = !holdingButtonAim;
					break;
				//Canceled.
				case InputActionPhase.Canceled:
					if (holdToAim)
						holdingButtonAim = false;
					break;
			}
		}

		// Holster.
		public void OnTryHolster(InputAction.CallbackContext context) {
			//Block while the cursor is unlocked.
			if (!cursorLocked)
				return;

			//Go back if we cannot even play the holster animation.
			if (!CanPlayAnimationHolster())
				return;

			//Switch.
			switch (context.phase) {
				//Started. This is here so we unholster with a tap, instead of a hold.
				case InputActionPhase.Started:
					//Only if holstered.
					if (holstered) {
						//Unholster.
						SetHolstered(false);
						//Holstering.
						holstering = true;
					}
					break;
				//Performed.
				case InputActionPhase.Performed:
					//Set.
					SetHolstered(!holstered);
					//Holstering.
					holstering = true;
					break;
			}
		}

		// Throw Grenade. 
		public void OnTryThrowGrenade(InputAction.CallbackContext context) {
			//Block while the cursor is unlocked.
			if (!cursorLocked)
				return;

			//Switch.
			switch (context.phase) {
				//Performed.
				case InputActionPhase.Performed:
					//Try Play.
					if (CanPlayAnimationGrenadeThrow())
						PlayGrenadeThrow();
					break;
			}
		}

		// Melee.
		public void OnTryMelee(InputAction.CallbackContext context) {
			//Block while the cursor is unlocked.
			if (!cursorLocked)
				return;

			//Switch.
			switch (context.phase) {
				//Performed.
				case InputActionPhase.Performed:
					//Try Play.
					if (CanPlayAnimationMelee())
						PlayMelee();
					break;
			}
		}
		// Run. 
		public void OnTryRun(InputAction.CallbackContext context) {
			//Block while the cursor is unlocked.
			if (!cursorLocked)
				return;

			//Switch.
			switch (context.phase) {
				//Performed.
				case InputActionPhase.Performed:
					//Use this if we're using run toggle.
					if (!holdToRun)
						holdingButtonRun = !holdingButtonRun;
					break;
				//Started.
				case InputActionPhase.Started:
					//Start.
					if (holdToRun)
						holdingButtonRun = true;
					break;
				//Canceled.
				case InputActionPhase.Canceled:
					//Stop.
					if (holdToRun)
						holdingButtonRun = false;
					break;
			}
		}

		// Jump. 
		public void OnTryJump(InputAction.CallbackContext context) {
			//Block while the cursor is unlocked.
			if (!cursorLocked)
				return;

			//Switch.
			switch (context.phase) {
				//Performed.
				case InputActionPhase.Performed:
					//Jump.
					movementBehaviour.Jump();
					break;
			}
		}
		// Next Inventory Weapon.
		public void OnTryInventoryNext(InputAction.CallbackContext context) {
			//Block while the cursor is unlocked.
			if (!cursorLocked)
				return;

			//Null Check.
			if (inventory == null)
				return;

			//Switch.
			switch (context) {
				//Performed.
				case { phase: InputActionPhase.Performed }:
					//Get the index increment direction for our inventory using the scroll wheel direction. If we're not
					//actually using one, then just increment by one.
					float scrollValue = context.valueType.IsEquivalentTo(typeof(Vector2)) ? Mathf.Sign(context.ReadValue<Vector2>().y) : 1.0f;

					//Get the next index to switch to.
					int indexNext = scrollValue > 0 ? inventory.GetNextIndex() : inventory.GetLastIndex();
					//Get the current weapon's index.
					int indexCurrent = inventory.GetEquippedIndex();

					//Make sure we're allowed to change, and also that we're not using the same index, otherwise weird things happen!
					if (CanChangeWeapon() && (indexCurrent != indexNext))
						StartCoroutine(nameof(Equip), indexNext);
					break;
			}
		}

		public void OnLockCursor(InputAction.CallbackContext context) {
			//Switch.
			switch (context) {
				//Performed.
				case { phase: InputActionPhase.Performed }:
					//Toggle the cursor locked value.
					cursorLocked = !cursorLocked;
					//Update the cursor's state.
					UpdateCursorState();
					break;
			}
		}

		// Movement.
		public void OnMove(InputAction.CallbackContext context) {
			//Read.
			axisMovement = cursorLocked ? context.ReadValue<Vector2>() : default;
		}
		// Look.
		public void OnLook(InputAction.CallbackContext context) {
			//Read.
			axisLook = cursorLocked ? context.ReadValue<Vector2>() : default;

			//Make sure that we have a weapon.
			if (equippedWeapon == null)
				return;

			//Make sure that we have a scope.
			if (equippedWeaponScope == null)
				return;

			//If we're aiming, multiply by the mouse sensitivity multiplier of the equipped weapon's scope!
			axisLook *= aiming ? equippedWeaponScope.GetMultiplierMouseSensitivity() : 1.0f;
		}

		// Called in order to update the tutorial text value.
		public void OnUpdateTutorial(InputAction.CallbackContext context) {
			//Switch.
			tutorialTextVisible = context switch {
				//Started. Show the tutorial.
				{ phase: InputActionPhase.Started } => true,
				//Canceled. Hide the tutorial.
				{ phase: InputActionPhase.Canceled } => false,
				//Default.
				_ => tutorialTextVisible
			};
		}

		#endregion

		#region ANIMATION EVENTS

		// EjectCasing.
		public override void EjectCasing() {
			//Notify the weapon.
			if (equippedWeapon != null)
				equippedWeapon.EjectCasing();
		}
		// FillAmmunition.
		public override void FillAmmunition(int amount) {
			//Notify the weapon to fill the ammunition by the amount.
			if (equippedWeapon != null)
				equippedWeapon.FillAmmunition(amount);
		}
		// Grenade.
		public override void Grenade() {
			//Make sure that the grenade is valid, otherwise we'll get errors.
			if (grenadePrefab == null)
				return;

			//Make sure we have a camera!
			if (cameraWorld == null)
				return;

			//Remove Grenade.
			if (!grenadesUnlimited)
				grenadeCount--;

			//Get Camera Transform.
			Transform cTransform = cameraWorld.transform;
			//Calculate the throwing location.
			Vector3 position = cTransform.position;
			position += cTransform.forward * grenadeSpawnOffset;
			//Throw.
			Instantiate(grenadePrefab, position, cTransform.rotation);
		}
		// SetActiveMagazine.
		public override void SetActiveMagazine(int active) {
			//Set magazine gameObject active.
			equippedWeaponMagazine.gameObject.SetActive(active != 0);
		}

		// AnimationEndedBolt.
		public override void AnimationEndedBolt() {
			//Update.
			UpdateBolt(false);
		}
		// AnimationEndedReload.
		public override void AnimationEndedReload() {
			//Stop reloading!
			reloading = false;
		}

		// AnimationEndedGrenadeThrow.
		public override void AnimationEndedGrenadeThrow() {
			//Stop Grenade Throw.
			throwingGrenade = false;
		}
		// AnimationEndedMelee.
		public override void AnimationEndedMelee() {
			//Stop Melee.
			meleeing = false;
		}

		// AnimationEndedInspect.
		public override void AnimationEndedInspect() {
			//Stop Inspecting.
			inspecting = false;
		}
		// AnimationEndedHolster.
		public override void AnimationEndedHolster() {
			//Stop Holstering.
			holstering = false;
		}

		// SetSlideBack.
		public override void SetSlideBack(int back) {
			//Set slide back.
			if (equippedWeapon != null)
				equippedWeapon.SetSlideBack(back);
		}

		// SetActiveKnife.
		public override void SetActiveKnife(int active) {
			//Set Active.
			knife.SetActive(active != 0);
		}

		#endregion

		#endregion
	}
}
