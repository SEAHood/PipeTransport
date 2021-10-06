using UnityEngine;

public class Synchroniser : MonoBehaviour
{
    public float TickRate;
    private float _nextActionTime;

    void Update()
    {
        if (Time.time > _nextActionTime)
        {
            var parts = FindObjectsOfType<BasePart>();
            foreach (var basePart in parts)
            {
                basePart.DoTick();
            }
            _nextActionTime += TickRate;
        }
    }
}
