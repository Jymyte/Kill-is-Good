using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Interactable
{
  [SerializeField]
  private GameObject door;
  private bool isOpen;

  protected override void Interact() {
    Debug.Log("interacted with: " + gameObject.name);
    isOpen = !isOpen;
    
    door.GetComponent<Animator>().SetBool("IsOpen", isOpen);
  }

}
