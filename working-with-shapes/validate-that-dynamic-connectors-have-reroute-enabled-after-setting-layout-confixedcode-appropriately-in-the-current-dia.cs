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
            // Path to the output Visio file after processing
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Identify dynamic connectors: 1-D shape with master name "Dynamic connector"
                    if (shape.OneD && shape.Master != null && shape.Master.Name == "Dynamic connector")
                    {
                        // Set ConFixedCode to Undefined (default) to enable rerouting
                        shape.Layout.ConFixedCode.Value = ConFixedCodeValue.Undefined;

                        // Validate that the setting was applied
                        if (shape.Layout.ConFixedCode.Value != ConFixedCodeValue.Undefined)
                        {
                            throw new Exception($"Reroute not enabled for connector ID {shape.ID}");
                        }
                    }
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
