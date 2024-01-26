using UnityEngine;

public class InteractionPoint : MonoBehaviour
{
    [SerializeField] private Transform viewPoint;
    
    public Transform GetViewPont()
    {
        return viewPoint;
    }
}
