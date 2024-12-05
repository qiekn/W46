using UnityEngine;
using System.Collections;

namespace InfimaGames.LowPolyShooterPack.Legacy {
	public class TargetScript : MonoBehaviour {

		#region Field

		private float randomTime;
		private bool routineStarted = false;

		public bool isHit = false;

		[Header("Customizable Options")]
		public float minTime; // Minimum time before the target goes back up
		public float maxTime; // Maximum time before the target goes back up

		[Header("Audio")]
		public AudioClip upSound;
		public AudioClip downSound;

		[Header("Animations")]
		public AnimationClip targetUp;
		public AnimationClip targetDown;
		public AudioSource audioSource;

		#endregion

		private void Update() {

			// Generate random time based on min and max time values
			randomTime = Random.Range(minTime, maxTime);

			if (isHit && !routineStarted) {
				// Animate the target "down"
				gameObject.GetComponent<Animation>().clip = targetDown;
				gameObject.GetComponent<Animation>().Play();

				// Set the downSound as current sound, and play it
				audioSource.GetComponent<AudioSource>().clip = downSound;
				audioSource.Play();

				// Start the timer
				StartCoroutine(DelayTimer());
				routineStarted = true;
			}
		}

		// Time before the target pops back up
		private IEnumerator DelayTimer() {
			yield return new WaitForSeconds(randomTime);
			// Animate the target "up" 
			gameObject.GetComponent<Animation>().clip = targetUp;
			gameObject.GetComponent<Animation>().Play();

			// Set the upSound as current sound, and play it
			audioSource.GetComponent<AudioSource>().clip = upSound;
			audioSource.Play();

			isHit = false;
			routineStarted = false;
		}
	}
}
