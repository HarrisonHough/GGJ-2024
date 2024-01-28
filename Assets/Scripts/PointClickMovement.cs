using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PointClickMovement : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private MouseCursor mouseCursor;
    private float raycastDistance = 100f;
    private PlayerController playerController;
    
    
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance, layerMask))
        {
            var interactionPoint = hit.collider.gameObject.GetComponent<InteractionPoint>();
            if(interactionPoint != null && GameManager.Instance.GameState == GameState.Playing)
            {
                mouseCursor.SetIconToSpeech();
            }
            

            if (Input.GetMouseButtonDown(0) && GameManager.Instance.GameState == GameState.Playing)
            {
                if (interactionPoint != null)
                {
                    playerController.SetDestination(interactionPoint);
                    
                }
                else
                {
                    Vector3 clickPosition = hit.point;
                    playerController.ClearInteractionPoint();
                    playerController.SetDestination(clickPosition);
                    mouseCursor.SetIconToArrow();
                }
            }
            return;
            
        }
        mouseCursor.SetIconToArrow();
    }
}