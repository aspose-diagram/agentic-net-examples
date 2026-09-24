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

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify dynamic connector shapes (1-D connectors with the proper master name)
                    if (shape.OneD && shape.Master != null && shape.Master.Name == "Dynamic connector")
                    {
                        // Set ConFixedCode to Undefined (default routing, i.e., reroute enabled)
                        shape.Layout.ConFixedCode.Value = ConFixedCodeValue.Undefined;

                        // Validate that the setting was applied
                        if (shape.Layout.ConFixedCode.Value != ConFixedCodeValue.Undefined)
                        {
                            throw new Exception($"Connector ID {shape.ID} failed to set ConFixedCode to Undefined.");
                        }

                        Console.WriteLine($"Connector ID {shape.ID} routing validated.");
                    }
                }
            }

            // Save the updated diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
