using System;
using UnityEngine;

public class Producer : BasePart
{
    public float EmitRate;
    private float _nextActionTime;

    void Start()
    {
        base.Start();
        Type = PartType.Producer;
    }

    // Create new QueueItems and push them from the output queue at the EmitRate interval
    internal override void DoTick()
    {
        base.DoTick();

        if (Time.time > _nextActionTime)
        {
            OutputQueue.Enqueue(new QueueItem {Id = Guid.NewGuid(), Sender = this});
            PushItem();
            _nextActionTime += EmitRate;
        }

    }
}
