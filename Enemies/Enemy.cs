using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Path path;
    public NavMeshAgent Agent { get => agent; }

    public float sightDistance  = 15f;
    public float fieldOfView = 120f;

    private GameObject player;
    private StateMachine stateMachine;
    private NavMeshAgent agent;

    void Start() {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update() {
        // Used to change the color of the field of view
        DrawViewArea drawViewArea = FindFirstObjectByType<DrawViewArea>();
        if (CanSeePlayer()) {
            if (drawViewArea != null)
                drawViewArea.SetState(true);
        } else {
            if (drawViewArea != null)
                drawViewArea.SetState(false);
        }
    }

    public bool CanSeePlayer() {
        if (player != null) {
            // Is the player close enough to be seen?
            if (Vector3.Distance(transform.position, player.transform.position) < sightDistance) {
                Vector3 targetDirection = player.transform.position - transform.position;
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                if (angleToPlayer >= -fieldOfView / 2 && angleToPlayer <= fieldOfView / 2) {
                    // Debug
                    Ray ray = new Ray(transform.position, targetDirection);
                    Debug.DrawRay(ray.origin, ray.direction * sightDistance, Color.red);
                    return true;
                }
            }
        }
        return false;
    }

}
