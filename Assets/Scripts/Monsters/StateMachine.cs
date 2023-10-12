using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
  public BaseState activeState;
  public PatrolState patrolState;
 
  public void Initialize() {
    patrolState = new PatrolState();
    ChangeState(patrolState);
  } 

  // Update is called once per frame
  void Update()
  {
      if (activeState != null) {
        activeState.Perform();
      }
  }
  
  public void ChangeState(BaseState newState) {
    if (activeState != null) {
      activeState.Exit();
    }
    activeState = newState;

    if (activeState != null) {
      activeState.stateMachine = this;
      activeState.monster = GetComponent<Monster>();
      activeState.Enter();
    }
  }
}
