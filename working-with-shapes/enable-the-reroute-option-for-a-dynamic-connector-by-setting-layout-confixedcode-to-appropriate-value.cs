using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path to the output Visio file
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through shapes on the first page to find a dynamic connector
            Page page = diagram.Pages[0];
            foreach (Shape shape in page.Shapes)
            {
                // Identify dynamic connectors: 1‑D shape with master name "Dynamic connector"
                if (shape.OneD && shape.Master != null && shape.Master.Name == "Dynamic connector")
                {
                    // Enable reroute by setting ConFixedCode to its default (Undefined) value
                    shape.Layout.ConFixedCode.Value = ConFixedCodeValue.Undefined;
                    // Optionally break after the first connector is processed
                    break;
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
