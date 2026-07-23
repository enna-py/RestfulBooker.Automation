namespace RestfulBooker.Data;

public sealed class ExecutionContext
{
    public string TestName { get; init; } = string.Empty;

    public DateTime StartedAt { get; init; }

    public Guid CorrelationId { get; init; }
}
