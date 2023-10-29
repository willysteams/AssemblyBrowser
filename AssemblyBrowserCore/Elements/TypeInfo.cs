using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AssemblyBrowserCore.Elements;

public class TypeInfo : IElementInfo
{
    public string Name { get; }
    public ObservableCollection<IElementInfo> Elements { get; }

    private readonly Type _type;

    public TypeInfo(Assembly assembly, Type type)
    {
        _type = type;
        
        Name = type.Name;
        Elements = new ObservableCollection<IElementInfo>();

        GetFields();
        GetProperties();
        GetMethods();
        GetExtensionMethods(assembly, type);
    }

    private void GetFields()
    {
        var fieldsInfo = _type.GetFields();

        foreach (var field in fieldsInfo)
        {
            Elements.Add(new FieldInfo(field));
        }
    }

    private void GetProperties()
    {
        var propertiesInfo = _type.GetProperties();

        foreach (var property in propertiesInfo)
        {
            Elements.Add(new PropertyInfo(property));
        }
    }
    
    private void GetMethods()
    {
        var methodsInfo = _type.GetMethods();

        foreach (var methodInfo in methodsInfo)
        {
            if (!methodInfo.IsDefined(typeof(ExtensionAttribute), false))
                Elements.Add(new MethodInfo(methodInfo));
        }
    }
    
    private void GetExtensionMethods(Assembly assembly, Type extendedType)
    {
        var extensionMethods = from t in assembly.GetTypes()
            where t.IsDefined(typeof(ExtensionAttribute), false)
            from mi in t.GetMethods()
            where mi.IsDefined(typeof(ExtensionAttribute), false)
            where mi.GetParameters()[0].ParameterType == extendedType
            select mi;
        
        foreach (var methodInfo in extensionMethods)
        {
            Elements.Add(new ExtensionMethodInfo(methodInfo));
        }
        
    }
}