using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasePart : MonoBehaviour
{
    [HideInInspector]
    public string Id;

    private UiController _ui;
    private float _holdTime = 0.2f;

    internal PartType Type;
    internal List<BasePart> ConnectedParts = new List<BasePart>();

    internal Queue<QueueItem> InputQueue = new Queue<QueueItem>();
    internal Queue<QueueItem> OutputQueue = new Queue<QueueItem>();

    internal void Start()
    {
        _ui = FindObjectOfType<UiController>();
        _ui.AddPartId(this);
    }

    public PartType GetPartType()
    {
        return Type;
    }

    public void ConnectPart(BasePart part)
    {
        Debug.Log($"Connecting {Id} to {part.Id}, current connections: {ConnectedParts.Count}");
        foreach (var connectedPart in ConnectedParts)
        {
            Debug.Log($"{connectedPart.Id}");
        }

        if (ConnectedParts.Contains(part))
            return;

        ConnectedParts.Add(part);
        part.ConnectPart(this);
        HandleConnect(part);
    }

    // Not used currently
    public void DisconnectPart(BasePart part)
    {
        ConnectedParts.Remove(part);
        OnDisconnectPart(part);
    }

    // Not used currently
    public void DisconnectAll()
    {
        var toDisconnect = ConnectedParts.ToList();
        foreach (var connectedPart in toDisconnect)
        {
            DisconnectPart(connectedPart);
        }
    }

    public bool ReceiveItem(Guid id, BasePart sender)
    {
        if (InputQueue.Count + 1 > GetInputQueueLimit())
            return false;

        InputQueue.Enqueue(new QueueItem { Id = id, Sender = sender, TimeReceived = Time.time });

        return true;
    }
    
    internal void PushItem()
    {
        if (OutputQueue.Count > 0)
        {
            var item = OutputQueue.Peek();
            // Makes it easier to see what's going on if items are held in the pipe for a moment
            if (Time.time > item.TimeReceived + _holdTime)
            {
                foreach (var part in ConnectedParts)
                {
                    if (part != item.Sender)
                    {
                        var partReceived = part.ReceiveItem(item.Id, this);
                        if (partReceived)
                            OutputQueue.Dequeue();
                    }
                }
            }
        }
    }

    private void HandleConnect(BasePart part)
    {
        DrawLine(transform.position, part.transform.position, Color.cyan);
    }

    void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        // Line helps to see what's connected
        var myLine = new GameObject {name = "PipeLine"};
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        var lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default")) {color = Color.white};
        lr.sortingOrder = 1;
        lr.startColor = color;
        lr.endColor = color;
        lr.startWidth = 0.5f;
        lr.endWidth = 0.3f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }

    internal virtual void OnConnectPart(BasePart part) { }
    internal virtual void OnDisconnectPart(BasePart part) { }
    internal virtual void DoTick() { }

    internal virtual int GetInputQueueLimit()
    {
        return 50000;
    }

    internal virtual int GetOutputQueueLimit()
    {
        return 50000;
    }

    public int PipeConnections()
    {
        return ConnectedParts.Count(p => p.GetPartType() == PartType.Pipe);
    }

    public int TotalConnections()
    {
        return ConnectedParts.Count;
    }
}
