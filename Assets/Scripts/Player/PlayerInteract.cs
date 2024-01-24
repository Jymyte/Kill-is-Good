using EasyCharacterMovement;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
  private Camera cam;
  [SerializeField]
  private float distance = 3f;
  [SerializeField]
  private LayerMask mask;
  private PlayerUI playerUI;
  private KillIsGoodCharacter character;
  private InputAction interactInputAction;

  private void Start() {
    cam = GetComponent<FirstPersonCharacter>().camera;
    playerUI = GetComponent<PlayerUI>();
    character = GetComponent<KillIsGoodCharacter>();
    interactInputAction = character.interactInputAction;
  }

  private void Update() {
    playerUI.UpdateText(string.Empty);

    //A ray to look for interactables. Shot from player pov camera.
    Ray ray = new Ray(cam.transform.position, cam.transform.forward);
    Debug.DrawRay(ray.origin, ray.direction * distance);
    RaycastHit hitInfo;
    if (Physics.Raycast(ray, out hitInfo, distance, mask)) {
      if (hitInfo.collider.GetComponent<Interactable>() != null) {
        Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
        playerUI.UpdateText(interactable.promptMessage);

        if (interactInputAction.WasPressedThisFrame()) {
          interactable.BaseInteract();
        }
      }
    }
  }
}
