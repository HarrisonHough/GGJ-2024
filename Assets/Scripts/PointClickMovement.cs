using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerController))]
public class PointClickMovement : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Sprite arrowIcon; // Assign the arrow icon sprite in the Inspector
    [SerializeField] private Sprite speechIcon;
    [SerializeField] private Image iconImage;
    private float raycastDistance = 100f;
    private PlayerController playerController;
    
    
    private void Start()
    {
        iconImage.sprite = arrowIcon;
        Cursor.visible = false; 
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        iconImage.transform.position = mousePosition;
        
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance, layerMask))
        {
            var interactionPoint = hit.collider.gameObject.GetComponent<InteractionPoint>();
            if(interactionPoint != null && GameManager.Instance.GameState == GameState.Playing)
            {
                SetIconToSpeech();
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
                }
            }
            return;
            
        }
        SetIconToArrow();
    }
    
    public void SetIcon(Sprite sprite)
    {
        iconImage.sprite = sprite;
    }
    
    public void SetIconToArrow()
    {
        if (iconImage.sprite == arrowIcon) return;
        iconImage.sprite = arrowIcon;
    }
    
    public void SetIconToSpeech()
    {
        iconImage.sprite = speechIcon;
    }
}