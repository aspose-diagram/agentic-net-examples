using System.IO;
using System;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Filter custom properties whose names start with "Dept"
            var deptProps = diagram.DocumentProps.CustomProps
                .Where(p => p.Name != null && p.Name.StartsWith("Dept", StringComparison.OrdinalIgnoreCase));

            // List the names and values of the filtered custom properties
            foreach (var prop in deptProps)
            {
                string value = prop.CustomValue?.ValueString ?? string.Empty;
                Console.WriteLine($"{prop.Name}: {value}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
