using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private SenseSwitcher senseScript;
    
    public float zoomSpeed;
    public float minZoom;
    public float maxZoom;
    
    private Vector3 offset = new Vector3(0f, 0f, -10f);
    public float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;

    private void Update()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        
        if (!senseScript._smell)
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                Camera.main.orthographicSize -= zoomSpeed * Time.deltaTime;
            }
            if (Input.mouseScrollDelta.y < 0)
            {
                Camera.main.orthographicSize += zoomSpeed * Time.deltaTime;
            }

            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);
        }
        else
        {
            Camera.main.orthographicSize = minZoom;
        }
    }
}