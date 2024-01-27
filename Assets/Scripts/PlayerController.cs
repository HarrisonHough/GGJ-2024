using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.0f; 
    [SerializeField] private float rotationSpeed = 5.0f; 
    
    private CharacterController characterController;
    private NavMeshAgent navMeshAgent;
    private Vector3 destination;
    private Animator animator;
    
    private static readonly int AnimMoveSpeed = Animator.StringToHash("MoveSpeed");

    private void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        MouseClickHandler.OnDestinationSet += SetTarget;
    }

    private void Update()
    {
        if (navMeshAgent.remainingDistance > characterController.radius)
        {
            animator.SetFloat(AnimMoveSpeed, navMeshAgent.velocity.magnitude);
            navMeshAgent.isStopped = false;
            characterController.SimpleMove(navMeshAgent.velocity);
            Vector3 targetDirection = (navMeshAgent.destination - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetFloat(AnimMoveSpeed, 0f);
            navMeshAgent.isStopped = true;
        }
    }

    public void SetTarget(Vector3 location)
    {
        navMeshAgent.SetDestination(location);
    }
}