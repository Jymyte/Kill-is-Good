using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
  public int waypointIndex;
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
      if (waypointIndex < monster.path.waypoints.Count -1 )
        waypointIndex++;
      else
        waypointIndex = 0;
      monster.Agent.SetDestination(monster.path.waypoints[waypointIndex].position);
    }
  }
}
