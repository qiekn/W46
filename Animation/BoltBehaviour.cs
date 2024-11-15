using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
	// Bolt Action Behaviour. Makes sure that the weapon's animator also matches the bolt action animation.
	public class BoltBehaviour : StateMachineBehaviour
	{
		#region FIELDS

		// Player Character.
		private CharacterBehaviour playerCharacter;

		// Player Inventory.
		private InventoryBehaviour playerInventoryBehaviour;

		#endregion

		#region UNITY

		// On State Enter.
		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			//We need to get the character component.
			playerCharacter ??= ServiceLocator.Current.Get<IGameModeService>().GetPlayerCharacter();

			//Get Inventory.
			playerInventoryBehaviour ??= playerCharacter.GetInventory();

			//Try to get the equipped weapon's Weapon component.
			if (!(playerInventoryBehaviour.GetEquipped() is { } weaponBehaviour))
				return;
			
			//Get the weapon animator.
			var weaponAnimator = weaponBehaviour.gameObject.GetComponent<Animator>();
			//Play Bolt Action Animation.
			weaponAnimator.Play("Bolt Action");
		}

		#endregion
	}
}
