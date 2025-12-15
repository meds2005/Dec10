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
            Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
            mouseWorld.z = transform.position.z;

            float newY = mouseWorld.y + offset.y;

            transform.position = new Vector3(
                transform.position.x,    
                newY,                
                transform.position.z    
            );
        }
    }

    public void BeginDragFromChild()
    {
        dragging = true;

        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = transform.position.z;

        offset = transform.position - mouseWorld;
    }

    public void EndDrag()
    {
        dragging = false;
    }
}
