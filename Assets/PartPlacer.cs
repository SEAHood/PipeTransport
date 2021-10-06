using UnityEngine;

public class PartPlacer : MonoBehaviour
{
    public GameObject ProducerPrefab;
    public GameObject ConsumerPrefab;
    public GameObject PipePrefab;

    private GameObject _currentPart;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _currentPart = PipePrefab;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _currentPart = ProducerPrefab;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _currentPart = ConsumerPrefab;
        }

        // Left click
        if (Input.GetMouseButtonDown(0))
        {
            var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                var cell = hit.collider.GetComponent<CellBehaviour>();
                if (cell != null && !cell.IsOccupied())
                {
                    var part = Instantiate(_currentPart, cell.transform);
                    cell.SetOccupied(part.GetComponent<BasePart>());
                }
            }
        }
    }
}
