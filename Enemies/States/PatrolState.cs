using UnityEngine;

public class PatrolState : BaseState
{
    private float timer = 0;
    private int   index = 0;     // The first way point to patrol
    private float delay = 1.5f;  // The time you stay on way point

    // Constructors
    public PatrolState() {}
    public PatrolState(float _delay) {
        delay = _delay;
    }

    public override void Enter() { }

    public override void Perform() {
        PatrolCycle();
        if (enemy.CanSeePlayer) {
            stateMachine.ChangeState(new AttackState());
        } else {

        }
    }

    public override void Exit() { }

    public void PatrolCycle() {
        if (enemy.Agent.remainingDistance < 0.2f) {
            timer += Time.deltaTime;
            if (timer > delay) {
                if (index < enemy.Path.wayPoints.Count - 1) {
                    index++;
                } else {
                    index = 0;
                }
                timer = 0;
            }
            enemy.Agent.SetDestination(enemy.Path.wayPoints[index].position);
        }
    }
}
