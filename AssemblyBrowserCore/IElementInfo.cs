using System.Collections.ObjectModel;

namespace AssemblyBrowserCore;

/// <summary>
/// Interface which describe classes for storage
/// info about assembly elements (e.g. namespaces, types, methods)
/// </summary>
public interface IElementInfo
{
    public string Name { get; }
    public ObservableCollection<IElementInfo> Elements { get; }
}