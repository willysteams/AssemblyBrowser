using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO.Enumeration;
using System.Reflection;
using AssemblyBrowserCore.Elements;

namespace AssemblyBrowserCore;

/// <summary>
/// Class for extract information from assembly
/// </summary>
public class AssemblyLoader
{
    /// <summary>
    /// Create and return list with namespaces
    /// and types from its namespaces
    /// </summary>
    /// <param name="assemblyPath">assembly which contains namespaces</param>
    public static ObservableCollection<IElementInfo> GetNamespaces(string assemblyPath)
    {
        var namespaces = new ObservableCollection<IElementInfo>();
        
        var assembly = Assembly.LoadFrom(assemblyPath);
        
        var types = assembly.GetTypes();

        foreach (var type in types)
        {
            AddType(assembly, namespaces, type);
        }

        return namespaces;
    }


    private static void AddType(Assembly assembly, ICollection<IElementInfo> namespaces, Type type)
    {
        var typeNamespace = type.Namespace;
        if (typeNamespace is null) return;

        var namespaceInfo = namespaces.FirstOrDefault(value => value.Name == typeNamespace) as NamespaceInfo;
        if (namespaceInfo is null)
        {
            namespaceInfo = new NamespaceInfo(typeNamespace);
            namespaces.Add(namespaceInfo);
        }
        
        namespaceInfo.AddType(assembly, type);
    }
}