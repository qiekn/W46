using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    #region Fields

    [SerializeField]
    private Path path;

    [Header("Sight Values")]
    [SerializeField]
    private float sightDistance = 15f;
    private float fieldOfView = 120f;

    // Just for debugging purposes
    [SerializeField]
    private string currentState;

    private GameObject player;
    private StateMachine stateMachine;
    private NavMeshAgent agent;

    private bool canSeePlayer;


    #endregion

    #region Unity

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise();
    }

    private void Update() {
        if (See()) {
            canSeePlayer = true;
        } else {
            canSeePlayer = false;
        }
        currentState = stateMachine.activeState.ToString();
    }

    #endregion

    #region Getters

    public NavMeshAgent Agent { get => agent; }
    public GameObject Player { get => player; }
    public float SightDistance { get => sightDistance; }
    public float FieldOfView { get => fieldOfView; }
    public bool CanSeePlayer { get => canSeePlayer; }
    public Path Path { get => path; }

    #endregion

    #region Methods
    
    public bool See() {
        if (player != null) {
            var A = transform.position; // The position of the enemy
            var B = player.transform.position; // The position of the player
            A.y += 1.6f;
            B.y += 1.6f;
            // Is the player close enough to be seen?
            if (Vector3.Distance(A, B) < sightDistance) {
                Vector3 directionToPlayer = (B - A).normalized;
                float angleToPlayer = Vector3.Angle(directionToPlayer, transform.forward);
                // Is the player in the field of view?
                if (angleToPlayer >= -fieldOfView / 2 && angleToPlayer <= fieldOfView / 2) {
                    Ray ray = new Ray(A, directionToPlayer);
                    Debug.DrawRay(ray.origin, ray.direction * sightDistance); // Draw the ray
                    // Final Check: Did the ray hit the player?
                    RaycastHit hitInfo = new RaycastHit();
                    if (Physics.Raycast(ray, out hitInfo, sightDistance)) {
                        if (hitInfo.transform.gameObject == player) {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    #endregion
}
