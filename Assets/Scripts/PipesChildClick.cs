using UnityEngine;

public class PipesChildClick : MonoBehaviour
{
    private PipesDrag parentDrag;

    void Start()
    {
        parentDrag = GetComponentInParent<PipesDrag>();
    }

    void OnMouseDown()
    {
        if (parentDrag != null)
        {
            parentDrag.BeginDragFromChild();
        }
    }

    void OnMouseUp()
    {
        if (parentDrag != null)
        {
            parentDrag.EndDrag();
        }
    }
}
