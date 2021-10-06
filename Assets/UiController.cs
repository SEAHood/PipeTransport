using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public GameObject PartIdPrefab;

    public void AddPartId(BasePart part)
    {
        var screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, part.transform.position);
        var idLabel = Instantiate(PartIdPrefab, screenPoint, Quaternion.identity, transform);
        idLabel.GetComponent<Text>().text = part.Id;
    }
}
