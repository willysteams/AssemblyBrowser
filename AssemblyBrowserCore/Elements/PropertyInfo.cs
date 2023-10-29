using System.Collections.ObjectModel;

namespace AssemblyBrowserCore.Elements;

public class PropertyInfo : IElementInfo
{
    public string Name { get; }
    public ObservableCollection<IElementInfo> Elements { get; }
    
    public PropertyInfo(System.Reflection.PropertyInfo propertyInfo)
    {
        Name = $"PROPERTY: {propertyInfo.PropertyType.Name} {propertyInfo.Name}";
        Elements = new ObservableCollection<IElementInfo>();
    }
}