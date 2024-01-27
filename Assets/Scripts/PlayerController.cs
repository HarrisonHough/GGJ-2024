using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.0f; 
    [SerializeField] private float rotationSpeed = 5.0f; 
    [SerializeField] private InteractionPoint interactionPoint; 
    
    private CharacterController characterController;
    private NavMeshAgent navMeshAgent;
    private Vector3 destination;
    private Animator animator;
    
    private static readonly int AnimMoveSpeed = Animator.StringToHash("MoveSpeed");

    private bool isFacingTarget;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (navMeshAgent.remainingDistance > characterController.radius)
        {
            animator.SetFloat(AnimMoveSpeed, navMeshAgent.velocity.magnitude);
            navMeshAgent.isStopped = false;
            characterController.SimpleMove(navMeshAgent.velocity * Time.deltaTime * moveSpeed);
            Vector3 targetDirection = (navMeshAgent.destination - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            return;
        }
        animator.SetFloat(AnimMoveSpeed, 0f);
        navMeshAgent.isStopped = true;
        if(!navMeshAgent.pathPending && interactionPoint != null)
        {
            RotateTowardsTarget();
        }
    }

    private void RotateTowardsTarget()
    { 
        var targetRotation = interactionPoint.GetDestinationRotation();
        isFacingTarget = Quaternion.Angle(transform.rotation, targetRotation) < 1.0f;
        if (!isFacingTarget)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 1.0f)
            {
                isFacingTarget = true;
                interactionPoint.ShowPrompt();
                interactionPoint.SwitchCamera();
            }
        }
    }

    public void SetDestination(InteractionPoint target)
    {
        interactionPoint = target;
        navMeshAgent.SetDestination(interactionPoint.GetDestinationPosition());
    }

    public void SetDestination(Vector3 destination)
    {
        navMeshAgent.SetDestination(destination);
    }
    
    public void ClearInteractionPoint()
    {
        interactionPoint = null;
    }
}