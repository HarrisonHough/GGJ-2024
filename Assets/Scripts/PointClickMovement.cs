using System;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PointClickMovement : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;
    private float raycastDistance = 100f;
    private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance, layerMask))
        {
            var interactionPoint = hit.collider.gameObject.GetComponent<InteractionPoint>();
            if(interactionPoint != null)
            {
                playerController.SetDestination(interactionPoint);
                return;
            }

            Vector3 clickPosition = hit.point;
            playerController.ClearInteractionPoint();
            playerController.SetDestination(clickPosition);
        }
    }
}