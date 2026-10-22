using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                Shape connectorShape = null;
                Page targetPage = null;

                // Find the first connector (1‑D shape) in the document
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.OneD) // 1‑D shapes are connectors
                        {
                            connectorShape = shape;
                            targetPage = page;
                            break;
                        }
                    }

                    if (connectorShape != null)
                        break;
                }

                if (connectorShape == null || targetPage == null)
                {
                    Console.Error.WriteLine("No connector shape found in the diagram.");
                    return;
                }

                // Determine a point on the connector where the comment should appear.
                // For demonstration we use the connector's central PinX/PinY.
                double commentX = connectorShape.XForm.PinX.Value;
                double commentY = connectorShape.XForm.PinY.Value;

                // Add a page‑level comment at the calculated coordinates.
                targetPage.AddComment(commentX, commentY, "Connector segment comment");

                // Retrieve the newly added annotation (assumed to be the last one).
                Annotation newAnnotation = null;
                int count = targetPage.PageSheet.Annotations.Count;
                if (count > 0)
                {
                    int index = 0;
                    foreach (Annotation ann in targetPage.PageSheet.Annotations)
                    {
                        if (index == count - 1)
                        {
                            newAnnotation = ann;
                            break;
                        }
                        index++;
                    }
                }

                if (newAnnotation == null)
                {
                    Console.Error.WriteLine("Failed to retrieve the newly added annotation.");
                    return;
                }

                // Associate the annotation with the connector shape.
                // ShapeID is an int, so cast the long shape ID explicitly.
                newAnnotation.ShapeID = (int)connectorShape.ID;

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}