using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard to ensure the input file exists
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
                // Access the first page of the diagram
                Page page = diagram.Pages[0];

                // Locate the first non-deleted shape to duplicate
                Shape originalShape = null;
                foreach (Shape s in page.Shapes)
                {
                    if (s.Del == BOOL.False)
                    {
                        originalShape = s;
                        break;
                    }
                }

                if (originalShape == null)
                {
                    Console.Error.WriteLine("No shape found to duplicate.");
                    return;
                }

                // Retrieve the master name associated with the original shape
                string masterName = originalShape.Master != null ? originalShape.Master.NameU : null;
                if (string.IsNullOrEmpty(masterName))
                {
                    Console.Error.WriteLine("Original shape does not have an associated master.");
                    return;
                }

                // Calculate new position (move 10 units to the right)
                double newPinX = originalShape.XForm.PinX.Value + 10;
                double newPinY = originalShape.XForm.PinY.Value;

                // Add a new shape based on the same master; AddShape returns a long ID
                long newShapeId = diagram.AddShape(
                    newPinX,
                    newPinY,
                    originalShape.XForm.Width.Value,
                    originalShape.XForm.Height.Value,
                    masterName,
                    0); // page index 0 for the first page

                // Retrieve the newly added shape using its ID
                Shape newShape = page.Shapes.GetShape((int)newShapeId);

                // Output the IDs to confirm uniqueness
                Console.WriteLine($"Original Shape ID: {originalShape.ID}");
                Console.WriteLine($"Duplicated Shape ID: {newShape.ID}");

                // Save the modified diagram to the output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}