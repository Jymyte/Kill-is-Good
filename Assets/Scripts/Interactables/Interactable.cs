using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class Interactable : MonoBehaviour
{
  //Add or remove an InteractionEvent component on this gameobject.
  public bool  useEvents;
  [SerializeField]
  public string promptMessage;

  

  public void BaseInteract() {
    if (useEvents)
      GetComponent<InteractionEvent>().OnInteract.Invoke();
    Interact();
  }
  protected virtual void Interact() {
    
  }
}
