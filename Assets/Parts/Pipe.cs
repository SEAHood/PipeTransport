using UnityEngine;

public class Pipe : BasePart
{
    void Start()
    {
        base.Start();
        Type = PartType.Pipe;
    }

    void Update()
    {
        SetPipeColour();
    }
    
    internal override void DoTick()
    {
        base.DoTick();
        
        if (OutputQueue.Count < GetOutputQueueLimit())
        {
            for (var i = 0; i < InputQueue.Count; i++)
            {
                var item = InputQueue.Dequeue();
                OutputQueue.Enqueue(item);
            }
        }

        PushItem();
    }

    private void SetPipeColour()
    {
        GetComponent<SpriteRenderer>().color = (InputQueue.Count > 0 || OutputQueue.Count > 0) ? Color.green : Color.black;
    }

    internal override int GetInputQueueLimit()
    {
        return 1;
    }
    internal override int GetOutputQueueLimit()
    {
        return 1;
    }
}
