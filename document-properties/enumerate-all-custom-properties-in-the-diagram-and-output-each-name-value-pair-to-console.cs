using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the Visio file (can be passed as a command‑line argument)
            string filePath = args.Length > 0 ? args[0] : "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(filePath);

            // Enumerate all custom properties defined at the document level
            foreach (CustomProp prop in diagram.DocumentProps.CustomProps)
            {
                // Output name‑value pair to the console
                Console.WriteLine($"{prop.Name} = {prop.CustomValue}");
            }

            // Release resources
            diagram.Dispose();

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
