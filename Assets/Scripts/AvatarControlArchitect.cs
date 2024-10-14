using UnityEngine;

public class AvatarControlArchitect : MonoBehaviour
{
    private float motionSmoothing = 0.1f;
    private Vector3 touchCoordinates;
    private bool dragActiveFlag = false;
    
    private Camera primaryCamera;
    
    private void Start()
    {
        primaryCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragActiveFlag = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            dragActiveFlag = false;
        }

        if (dragActiveFlag)
        {
            touchCoordinates = primaryCamera.ScreenToWorldPoint(Input.mousePosition);
            touchCoordinates.z = 0;
            
            var restrictedPosition = touchCoordinates;
            restrictedPosition.x = Mathf.Clamp(restrictedPosition.x, -primaryCamera.orthographicSize * primaryCamera.aspect, primaryCamera.orthographicSize * primaryCamera.aspect);
            restrictedPosition.y = Mathf.Clamp(restrictedPosition.y, -primaryCamera.orthographicSize, primaryCamera.orthographicSize);
            
            transform.position = Vector3.Lerp(transform.position, restrictedPosition, motionSmoothing);
        }
    }
}