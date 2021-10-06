using System;

public class QueueItem
{
    public Guid Id { get; set; }
    public BasePart Sender { get; set; }
    public float TimeReceived { get; set; }
}