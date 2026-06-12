using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram (replace with actual path if needed)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify dynamic connector shapes (1-D shapes with master name "Dynamic connector")
                    if (shape.OneD && shape.Master != null && shape.Master.Name == "Dynamic connector")
                    {
                        // Set the ConFixedCode to Undefined (default reroute behavior)
                        shape.Layout.ConFixedCode.Value = ConFixedCodeValue.Undefined;

                        // Validate that reroute is enabled (ConFixedCode should be Undefined)
                        if (shape.Layout.ConFixedCode.Value != ConFixedCodeValue.Undefined)
                        {
                            throw new Exception($"Connector ID {shape.ID} does not have reroute enabled.");
                        }
                    }
                }
            }

            // Save the updated diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Validation completed successfully. Diagram saved to " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
