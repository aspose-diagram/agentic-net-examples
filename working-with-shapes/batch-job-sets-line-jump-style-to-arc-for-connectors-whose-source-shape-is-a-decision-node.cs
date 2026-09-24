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

            // Input Visio file path
            string inputPath = "input.vsdx";
            // Output Visio file path
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Identify connector shapes (1‑D shapes)
                    if (shape.OneD)
                    {
                        long connectorId = shape.ID;
                        long sourceShapeId = -1;

                        // Find the connection that links the source shape to this connector
                        foreach (Connect conn in page.Connects)
                        {
                            if (conn.ToSheet == connectorId)
                            {
                                sourceShapeId = conn.FromSheet;
                                break;
                            }
                        }

                        // If a source shape was found, check if it is a decision node
                        if (sourceShapeId != -1)
                        {
                            Shape sourceShape = page.Shapes.GetShape(sourceShapeId);
                            if (sourceShape != null && sourceShape.Master != null && sourceShape.Master.Name == "Decision")
                            {
                                // Set the connector's line jump style to Arc
                                shape.Layout.ConLineJumpStyle.Value = ConLineJumpStyleValue.Arc;
                            }
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
