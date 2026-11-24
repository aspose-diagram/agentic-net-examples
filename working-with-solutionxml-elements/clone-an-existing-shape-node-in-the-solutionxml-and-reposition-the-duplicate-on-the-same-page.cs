using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check if input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Get the first page
            Page page = diagram.Pages[0];

            // Ensure the page has at least one shape
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes to clone.");
                return;
            }

            // Retrieve the first shape on the page
            Shape originalShape = page.Shapes[0];

            // TODO: Clone the original shape.
            // The Aspose.Diagram API provides a way to duplicate a shape.
            // Replace the following placeholder with the correct cloning logic,
            // then add the cloned shape to the page and reposition it.
            Shape clonedShape = null; // = originalShape.Clone(); // Example placeholder

            if (clonedShape != null)
            {
                // Add the cloned shape to the same page
                // TODO: Use the appropriate method to add the cloned shape to the page.
                // Example (may vary): long newShapeId = page.Shapes.Add(clonedShape);
                // Retrieve the shape instance if needed:
                // Shape addedShape = page.Shapes.GetShape(newShapeId);

                // Reposition the duplicated shape (offset by 2 inches on both axes)
                clonedShape.XForm.PinX.Value += 2.0;
                clonedShape.XForm.PinY.Value += 2.0;
            }
            else
            {
                Console.Error.WriteLine("Cloning of the shape is not implemented. Please replace the placeholder with actual cloning code.");
            }

            // Clone the first SolutionXML element, if any
            if (diagram.SolutionXMLs.Count > 0)
            {
                SolutionXML originalXml = diagram.SolutionXMLs[0];
                SolutionXML clonedXml = new SolutionXML();
                clonedXml.Name = originalXml.Name + "_Copy";
                clonedXml.XmlValue = originalXml.XmlValue;
                diagram.SolutionXMLs.Add(clonedXml);
            }
            else
            {
                Console.WriteLine("No SolutionXML elements found to clone.");
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}