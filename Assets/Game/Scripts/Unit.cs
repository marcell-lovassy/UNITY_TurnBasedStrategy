using System.Drawing;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float stoppingDistance;
    [SerializeField]
    private Animator animator;

    private Vector3 targetPosition;
    private Quaternion lookRotation;

    private void Start()
    {
        targetPosition = transform.position;
        lookRotation = Quaternion.identity;
    }

    private void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance) 
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            transform.position += moveDirection * Time.deltaTime * moveSpeed;

            //this is the look rotation too, but the other is more consistent
            //transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);
           
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

        if (Input.GetMouseButtonDown(0)) 
        {
            Move(MouseController.GetPosition());
            var lookDirection = (targetPosition - transform.position).normalized;
            lookRotation = Quaternion.LookRotation(lookDirection);
        }
    }

    private void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}
