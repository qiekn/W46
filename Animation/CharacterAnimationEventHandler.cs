using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
	// Handles all the animation events that come from the character in the asset.
	public class CharacterAnimationEventHandler : MonoBehaviour
	{
		#region FIELDS

        // Character Component Reference.
        private CharacterBehaviour playerCharacter;

		#endregion

		#region UNITY

		private void Awake()
		{
			//Grab a reference to the character component.
			playerCharacter = ServiceLocator.Current.Get<IGameModeService>().GetPlayerCharacter();
		}

		#endregion

		#region ANIMATION

		// Ejects a casing from the character's equipped weapon. This function is called from an Animation Event.
		private void OnEjectCasing()
		{
			//Notify the character.
			if(playerCharacter != null)
				playerCharacter.EjectCasing();
		}

		// Fills the character's equipped weapon's ammunition by a certain amount, or fully if set to 0. This function is called
		// from a Animation Event.
		private void OnAmmunitionFill(int amount = 0)
		{
			//Notify the character.
			if(playerCharacter != null)
				playerCharacter.FillAmmunition(amount);
		}
		// Sets the character's knife active value. This function is called from an Animation Event.
		private void OnSetActiveKnife(int active)
		{
			//Notify the character.
			if(playerCharacter != null)
				playerCharacter.SetActiveKnife(active);
		}
		
		// Spawns a grenade at the correct location. This function is called from an Animation Event.
		private void OnGrenade()
		{
			//Notify the character.
			if(playerCharacter != null)
				playerCharacter.Grenade();
		}
		// Sets the equipped weapon's magazine to be active or inactive! This function is called from an Animation Event.
		private void OnSetActiveMagazine(int active)
		{
			//Notify the character.
			if(playerCharacter != null)
				playerCharacter.SetActiveMagazine(active);
		}

		// Bolt Animation Ended. This function is called from an Animation Event.
		private void OnAnimationEndedBolt()
		{
			//Notify the character.
			if(playerCharacter != null)
				playerCharacter.AnimationEndedBolt();
		}
		// Reload Animation Ended. This function is called from an Animation Event.
		private void OnAnimationEndedReload()
		{
			//Notify the character.
			if(playerCharacter != null)
				playerCharacter.AnimationEndedReload();
		}

		// Grenade Throw Animation Ended. This function is called from an Animation Event.
		private void OnAnimationEndedGrenadeThrow()
		{
			//Notify the character.
			if(playerCharacter != null)
				playerCharacter.AnimationEndedGrenadeThrow();
		}
		// Melee Animation Ended. This function is called from an Animation Event.
		private void OnAnimationEndedMelee()
		{
			//Notify the character.
			if(playerCharacter != null)
				playerCharacter.AnimationEndedMelee();
		}

		// Inspect Animation Ended. This function is called from an Animation Event.
		private void OnAnimationEndedInspect()
		{
			//Notify the character.
			if(playerCharacter != null)
				playerCharacter.AnimationEndedInspect();
		}
		// Holster Animation Ended. This function is called from an Animation Event.
		private void OnAnimationEndedHolster()
		{
			//Notify the character.
			if(playerCharacter != null)
				playerCharacter.AnimationEndedHolster();
		}

		// Sets the character's equipped weapon's slide back pose. This function is called from an Animation Event.
		private void OnSlideBack(int back)
		{
			//Notify the character.
			if(playerCharacter != null)
				playerCharacter.SetSlideBack(back);
		}

		#endregion
	}   
}
