using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float stoppingDistance;

    private Vector3 targetPosition;

    private void Update()
    {
        if(Vector3.Distance(transform.position, targetPosition) > stoppingDistance) 
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            transform.position += moveDirection * Time.deltaTime * moveSpeed;
        }

        if (Input.GetMouseButtonDown(0)) 
        {
            Move(MouseController.GetPosition());
        }
    }

    private void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}
