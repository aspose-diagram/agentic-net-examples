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
                    // Process only connector shapes (1‑D objects)
                    if (shape.OneD)
                    {
                        // Simple heuristic: count geometry elements as bends
                        int bendCount = shape.Geoms.Count;

                        // Apply reroute option when there are more than three bends
                        if (bendCount > 3)
                        {
                            // Set the connector to never reroute (example option)
                            shape.Layout.ConFixedCode.Value = ConFixedCodeValue.NeverReroute;
                        }
                    }
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
