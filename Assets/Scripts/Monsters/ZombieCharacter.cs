using System.Collections;
using System.Collections.Generic;
using EasyCharacterMovement;
using UnityEngine;

public class ZombieCharacter : AgentCharacter
{

  [SerializeField] Transform targetGoal;
  [SerializeField] bool pursuing;



  protected override void Update()
  {
    base.Update();
    if (pursuing)
    {
      MoveToLocation(targetGoal.position);
    }
  }
}
