namespace IT_maraphon.Domain.Models.Enums;

public enum CanvasJobStatuses
{
    Enqueued,
    Processing,
    Succeeded,
    Failed = -1,
}