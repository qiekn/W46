using UnityEngine;

public class AttackState : BaseState {
    private float moveTimer;
    private float shootTimer;
    private float losePlayerTimer;

    public override void Enter() { }

    public override void Perform() {
        if (enemy.CanSeePlayer) {
            losePlayerTimer = 0;
            Shoot();
            Move();
        } else { // Lose sight of player
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer > 3) {
                stateMachine.ChangeState(new PatrolState(1.5f)); // Change to the serach state
            }
        }
    }

    public override void Exit() { }

    private void Shoot() {
        // enemy.transform.LookAt(enemy.Player.transform); // Lock to player immediately
        // Lock to player with slerp
        Quaternion targetRotation = Quaternion.LookRotation(enemy.Player.transform.position - enemy.transform.position);
        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, targetRotation, Time.deltaTime * 5f); // Adjust the speed as needed

        shootTimer += Time.deltaTime;
    }

    private void Move() {
        moveTimer += Time.deltaTime;
        // Move the enemy to a random position after a random time
        if (moveTimer > Random.Range(3, 7)) {
            enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));
            moveTimer = 0;
        }
    }
}
