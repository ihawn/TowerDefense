using UnityEngine;

public class DragAndDropController : MonoBehaviour
{
    private float gridSize = 1.0f;

    public GameObject ActiveObject;
    public bool Dragging = false;

    void FixedUpdate()
    {
        SetDragging();
        GetActiveObject();
        DoDrag();
    }


    void DoDrag()
    {
        if(Dragging && ActiveObject != null)
        {
            Vector3 p = Input.mousePosition;
            p.y = ActiveObject.transform.position.y;
            ActiveObject.transform.position = Camera.main.ScreenToWorldPoint(p);
        }
    }

    void SetDragging()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0)
        && Physics.Raycast(ray, out hit, Mathf.Infinity, GlobalReferences.gm.DragAndDrop))
        {
            Dragging = true;
            ActiveObject = hit.collider.gameObject.transform.parent.gameObject;
        }
    }

    void GetActiveObject()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Dragging = false;
        }
    }
}
