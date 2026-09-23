using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file
            string diagramPath = "sample.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Access custom document properties
            var customProps = diagram.DocumentProps.CustomProps;

            // Enumerate and output each name‑value pair
            for (int i = 0; i < customProps.Count; i++)
            {
                var prop = customProps[i];
                string name = prop.Name;
                string value = prop.CustomValue.ValueString;
                Console.WriteLine($"{name}: {value}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
