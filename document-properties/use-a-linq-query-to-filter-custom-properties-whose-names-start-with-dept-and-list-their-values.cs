using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        var diagramPath = "input.vsdx";
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            Diagram diagram = new Diagram(diagramPath);
            var customProps = diagram.DocumentProps.CustomProps;

            var deptProperties = customProps
                .Cast<CustomProp>()
                .Where(p => !string.IsNullOrEmpty(p.Name) && p.Name.StartsWith("Dept", StringComparison.OrdinalIgnoreCase));

            foreach (var prop in deptProperties)
            {
                string value = prop.CustomValue?.ValueString ?? string.Empty;
                Console.WriteLine($"{prop.Name}: {value}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}