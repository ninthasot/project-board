namespace Labels.Application.Defaults;

internal static class DefaultLabelDefinitions
{
    public static readonly IReadOnlyList<LabelTemplate> Templates =
    [
        new("Priority: High", "#D32F2F"), // Red
        new("Priority: Medium", "#FF9800"), // Orange
        new("Priority: Low", "#4CAF50"), // Green
        new("Bug", "#F44336"), // Bright Red
        new("Feature", "#2196F3"), // Blue
        new("Enhancement", "#9C27B0"), // Purple
        new("Documentation", "#607D8B"), // Blue Grey
    ];

    public sealed record LabelTemplate(string Name, string HexColor);
}
