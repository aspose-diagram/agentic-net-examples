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

            // Path to the Visio file
            string filePath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(filePath);

            // Filter custom properties whose names start with "Dept" and select their values
            var deptValues = diagram.DocumentProps.CustomProps
                .Where(cp => cp.Name != null && cp.Name.StartsWith("Dept"))
                .Select(cp => cp.CustomValue?.ValueString);

            // Output the values
            foreach (var value in deptValues)
            {
                Console.WriteLine(value);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
