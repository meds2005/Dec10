using UnityEngine;

public class PipesDrag : MonoBehaviour
{
    private Vector3 offset;
    private Camera cam;
    private bool dragging = false;

    void Start()
    {
        cam = Camera.main;
    }

    void OnMouseDown()
    {
        dragging = true;

        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = transform.position.z;

        offset = transform.position - mouseWorld;
    }

    void OnMouseUp()
    {
        dragging = false;
    }

    void Update()
    {
        if (dragging)
        {
            // Get mouse position in world space
            Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
            mouseWorld.z = transform.position.z;

            // Calculate new vertical position only
            float newY = mouseWorld.y + offset.y;

            // Apply ONLY the Y movement (vertical)
            transform.position = new Vector3(
                transform.position.x,    // keep original X
                newY,                   // update Y
                transform.position.z    // keep original Z
            );
        }
    }
}
