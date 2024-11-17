using UnityEngine;

public class StateMachine : MonoBehaviour {

  public BaseState activeState;
  public PatrolState patrolState;

  [Tooltip("The time you stay on way point")]
  [SerializeField]
  private float patrolDelay = 3;

  //NOTE: Unity
  private void Start() { }

  private void Update() {
    if (activeState != null) {
      activeState.Perform();
    }
  }

  //NOTE: Methods
  public void Initialise() {
    patrolState = new PatrolState();
    patrolState.delay = patrolDelay;
    ChangeState(patrolState);
  }

  public void ChangeState(BaseState newState) {
    // Check activeState
    if (activeState != null) {
      // Run cleanup on activestate
      activeState.Exit();
    }

    // Change to a new state
    activeState = newState;

    // Fail-safe null check to make sure new state wasn't null
    if (activeState != null) {
      // Setup new state
      activeState.stateMachine = this;
      activeState.enemy = GetComponent<Enemy>();
      activeState.Enter();
    }
  }
}