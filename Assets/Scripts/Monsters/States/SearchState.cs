using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : BaseState
{
  private float searchTimer = 10;
  private float timeSearched;

    public override void Enter() {
      timeSearched = 0;
      enemy.character.MoveToLocation(enemy.LastKnownPos);
    }

    public override void Perform() {
      if (enemy.CanSeePlayer()) stateMachine.ChangeState(new AttackState());
      
      if (enemy.Agent.remainingDistance < enemy.Agent.stoppingDistance) {
        timeSearched += Time.deltaTime;
        enemy.debugSphereSearchPos.transform.position = enemy.LastKnownPos + enemy.LastKnownDir * 5;
        enemy.character.MoveToLocation(enemy.LastKnownPos + enemy.LastKnownDir * 5);
        if (timeSearched > searchTimer) stateMachine.ChangeState(new PatrolState());
      }
    }

    public override void Exit() {
      
    }
}
