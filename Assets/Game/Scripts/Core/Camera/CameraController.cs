using Cinemachine;
using UnityEngine;

namespace Game.Core.Cameras
{
    public class CameraController : MonoBehaviour
    {
        private const float ZOOM_MIN = 3f;
        private const float ZOOM_MAX = 20f;

        [SerializeField]
        private CinemachineVirtualCamera vCam;
        [SerializeField]
        private float moveSpeed;
        [SerializeField]
        private float rotationSpeed;
        [SerializeField]
        private float zoomAmount;
        [SerializeField]
        private float zoomSpeed;

        private CinemachineTransposer cinemacineTransposer;
        private Vector3 zoomTarget;

        private void Start()
        {
            cinemacineTransposer = vCam.GetCinemachineComponent<CinemachineTransposer>();
            zoomTarget = cinemacineTransposer.m_FollowOffset;
        }

        void Update()
        {
            HandleMovement();
            HandleRotation();
            HandleZoom();
        }

        private void HandleZoom()
        {
            //DIFFERENT ZOOM STYLES
            //transposer.m_FollowOffset -= new Vector3(0, Input.mouseScrollDelta.y * zoomSpeed * Time.deltaTime, -Input.mouseScrollDelta.y * zoomSpeed * Time.deltaTime);
            //transposer.m_FollowOffset.y = Mathf.Clamp(transposer.m_FollowOffset.y, 3f, 20f);
            //transposer.m_FollowOffset.z = Mathf.Clamp(transposer.m_FollowOffset.z, -20f, -5f);

            if (Input.mouseScrollDelta.y != 0)
            {
                zoomTarget = cinemacineTransposer.m_FollowOffset;
                zoomTarget.y -= Input.mouseScrollDelta.y * zoomAmount;
                zoomTarget.y = Mathf.Clamp(zoomTarget.y, ZOOM_MIN, ZOOM_MAX);
            }

            if (Vector3.Distance(cinemacineTransposer.m_FollowOffset, zoomTarget) > 0.1)
            {
                cinemacineTransposer.m_FollowOffset = Vector3.Lerp(cinemacineTransposer.m_FollowOffset, zoomTarget, Time.deltaTime * zoomSpeed);
            }
        }

        private void HandleRotation()
        {
            Vector3 rotationVector = Vector3.zero;

            if (Input.GetKey(KeyCode.Q)) rotationVector.y = 1f;
            if (Input.GetKey(KeyCode.E)) rotationVector.y = -1f;

            transform.Rotate(rotationVector * rotationSpeed * Time.deltaTime);
            //same as the Rotate() function
            //transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;
        }

        private void HandleMovement()
        {
            Vector3 inputMoveDirection = Vector3.zero;

            if (Input.GetKey(KeyCode.W)) inputMoveDirection.z = 1f;
            if (Input.GetKey(KeyCode.S)) inputMoveDirection.z = -1f;
            if (Input.GetKey(KeyCode.D)) inputMoveDirection.x = 1f;
            if (Input.GetKey(KeyCode.A)) inputMoveDirection.x = -1f;

            transform.Translate(inputMoveDirection * moveSpeed * Time.deltaTime);
            //same as the Translate() function
            //Vector3 moveVector = transform.forward * inputMoveDirection.z + transform.right * inputMoveDirection.x;
            //transform.position += moveVector * moveSpeed * Time.deltaTime;
        }
    }
}