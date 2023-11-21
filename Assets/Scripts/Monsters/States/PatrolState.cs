using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
  public int waypointIndex;
  public float waitTimer;

  //private Monster monster;
  public override void Enter() {

  }

  public override void Perform() {
    PatrolCycle();
  }

  public override void Exit() {
    
  }

  public void PatrolCycle() {
    if(monster.Agent.remainingDistance < 0.2f) {
      waitTimer += Time.deltaTime;
      if (waitTimer > 1.5 ) {
        if (waypointIndex < monster.path.waypoints.Count -1 )
          waypointIndex++;
        else
          waypointIndex = 0;
        monster.character.MoveToLocation(monster.path.waypoints[waypointIndex].position);
        waitTimer = 0;
      }
    }
  }
}
