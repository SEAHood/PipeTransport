using UnityEngine;

public class Consumer : BasePart
{
    private int _lastInputCount;

    void Start()
    {
        base.Start();
        Type = PartType.Consumer;
    }

    // Just print out stats on what's been consumer
    internal override void DoTick()
    {
        base.DoTick();
        if (_lastInputCount != InputQueue.Count)
        {
            Debug.Log($"Consumer has consumed {InputQueue.Count} items");
            _lastInputCount = InputQueue.Count;
        }
    }
}
