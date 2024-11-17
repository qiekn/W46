using UnityEngine;

public class PatrolState : BaseState
{
    private float timer;

    // The time you stay on way point
    public float delay;

    // The first way point to patrol
    private int index;

    public override void Enter() { }

    public override void Perform() {
        PatrolCycle();
    }

    public override void Exit() { }

    public void PatrolCycle() {
        if (enemy.Agent.remainingDistance < 0.2f) {
            timer += Time.deltaTime;
            if (timer > delay) {
                if (index < enemy.path.wayPoints.Count - 1) {
                    index++;
                } else {
                    index = 0;
                }
                timer = 0;
            }
            enemy.Agent.SetDestination(enemy.path.wayPoints[index].position);
        }
    }
}
