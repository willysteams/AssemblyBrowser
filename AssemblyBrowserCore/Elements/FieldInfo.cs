using System.Collections.ObjectModel;

namespace AssemblyBrowserCore.Elements;

public class FieldInfo : IElementInfo
{
    public string Name { get; }
    public ObservableCollection<IElementInfo> Elements { get; }
    
    public FieldInfo(System.Reflection.FieldInfo fieldInfo)
    {
        Name = $"FIELD: {fieldInfo.FieldType.Name} {fieldInfo.Name}";
        Elements = new ObservableCollection<IElementInfo>();
    }
}