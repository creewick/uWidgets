namespace uWidgets.Core.Models.Attributes;

/// <summary>
/// Attribute to register a widget inside an assembly.
/// </summary>
/// <param name="viewType">Type of UserControl to render.</param>
/// <param name="modelType">Type of Model, that will be stored in <c>layout.json</c> and provided via DI.</param>
/// <param name="editModelViewType">Type of UserControl for editing the model.</param>
/// <param name="title">Resource key of your widget's name</param>
/// <param name="subtitle">Resource key of your widget's description</param>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public class WidgetInfoAttribute(
    Type viewType, 
    Type? modelType = null, 
    Type? editModelViewType = null, 
    string? title = null, 
    string? subtitle = null) 
    : Attribute
{
    /// <summary>
    /// Type of UserControl to render.
    /// </summary>
    public Type ViewType { get; } = viewType;
    
    /// <summary>
    /// Type of Model, that will be stored in <c>layout.json</c> and provided via DI.
    /// </summary>
    public Type? ModelType { get; } = modelType;
    
    /// <summary>
    /// Type of UserControl for editing the model.
    /// <para>Will be rendered in a separate window on Right-click, "Edit Widget"</para>
    /// </summary>
    public Type? EditModelViewType { get; } = editModelViewType;
    
    /// <summary>
    /// Resource key of your widget's name.
    /// </summary>
    public string? Title { get; } = title;
    
    /// <summary>
    /// Resource key of your widget's description.
    /// </summary>
    public string? Subtitle { get; } = subtitle;
}