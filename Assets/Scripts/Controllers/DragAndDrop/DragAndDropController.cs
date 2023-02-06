using UnityEngine;

public class DragAndDropController : MonoBehaviour
{
    private float gridSize = 1.0f;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Vector3 cursorWorldPoint = hit.point;
                transform.position = new Vector3(Mathf.Round(cursorWorldPoint.x / gridSize) * gridSize, cursorWorldPoint.y, Mathf.Round(cursorWorldPoint.z / gridSize) * gridSize);
            }
        }
    }
}
