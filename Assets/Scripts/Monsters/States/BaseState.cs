using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
  public Monster monster;
  public StateMachine stateMachine;

  public abstract void Enter();
  public abstract void Perform();
  public abstract void Exit();

}
