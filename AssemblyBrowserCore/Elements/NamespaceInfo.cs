using System.Collections.ObjectModel;
using System.Reflection;

namespace AssemblyBrowserCore.Elements;

internal class NamespaceInfo : IElementInfo
{
    public string Name { get; }
    public ObservableCollection<IElementInfo> Elements { get; }

    public NamespaceInfo(string name)
    {
        Name = name;
        Elements = new ObservableCollection<IElementInfo>();
    }
    
    public void AddType(Assembly assembly, Type type)
    {
        Elements.Add(new TypeInfo(assembly, type));
    }
}