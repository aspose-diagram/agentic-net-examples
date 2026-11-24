using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // ----- Attempt to modify Height -----
                        // Check if the shape's height is locked.
                        if (shape.Protection.LockHeight.Value == BOOL.True)
                        {
                            // Log the attempt to modify a locked height.
                            Console.WriteLine($"[Locked] Attempted to modify Height of shape ID {shape.ID} on page '{page.Name}' but it is locked.");
                        }
                        else
                        {
                            // Perform the height modification.
                            double currentHeight = shape.XForm.Height.Value;
                            shape.XForm.Height.Value = currentHeight + 1.0;
                            Console.WriteLine($"[Modified] Height of shape ID {shape.ID} increased to {shape.XForm.Height.Value}.");
                        }

                        // ----- Attempt to modify Rotation -----
                        // Check if the shape's rotation is locked.
                        if (shape.Protection.LockRotate.Value == BOOL.True)
                        {
                            // Log the attempt to modify a locked rotation.
                            Console.WriteLine($"[Locked] Attempted to modify Rotation of shape ID {shape.ID} on page '{page.Name}' but it is locked.");
                        }
                        else
                        {
                            // Perform the rotation modification (set to 10 degrees for demonstration).
                            shape.SetAngle(10.0);
                            Console.WriteLine($"[Modified] Rotation of shape ID {shape.ID} set to 10 degrees.");
                        }
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Log any unexpected errors.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}