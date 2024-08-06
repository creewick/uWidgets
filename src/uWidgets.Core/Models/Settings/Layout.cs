namespace uWidgets.Core.Models.Settings;

/// <summary>
/// Widget sizing and positioning settings.
/// </summary>
/// <param name="SnapSize">Should widget's size be snapped to the grid</param>
/// <param name="LockSize">Disable resizing a widget</param>
/// <param name="SnapPosition">Should widget's position be snapped to the grid</param>
/// <param name="LockPosition">Disable moving a widget</param>
public record Layout(bool SnapSize, bool LockSize, bool SnapPosition, bool LockPosition);