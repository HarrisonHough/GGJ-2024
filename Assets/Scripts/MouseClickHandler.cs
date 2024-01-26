using System;
using UnityEngine;

public class MouseClickHandler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;
    private float raycastDistance = 100f;
    
    public static Action<Vector3> OnDestinationSet;
    
    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, raycastDistance, layerMask))
            {
                var interactionPoint = hit.collider.gameObject.GetComponent<InteractionPoint>();
                if(interactionPoint != null)
                {
                    OnDestinationSet?.Invoke(interactionPoint.GetViewPont().position);
                    return;
                }

                Vector3 clickPosition = hit.point;
                OnDestinationSet?.Invoke(clickPosition);
            }
        }
    }
}