using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the first argument to be the path to the Visio file.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No diagram file path provided.");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists before attempting to load.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve the current rotation angle from the shape's XForm.Angle cell.
                    double actualAngle = shape.XForm.Angle.Value;

                    // Attempt to find a custom property named "IntendedAngle" that holds the expected rotation.
                    double intendedAngle = double.NaN;
                    bool hasIntendedAngle = false;

                    for (int i = 0; i < shape.Props.Count; i++)
                    {
                        Prop prop = shape.Props[i];
                        if (string.Equals(prop.Name, "IntendedAngle", StringComparison.OrdinalIgnoreCase))
                        {
                            // Parse the intended angle value from the property.
                            if (double.TryParse(prop.Value.Val, out double parsed))
                            {
                                intendedAngle = parsed;
                                hasIntendedAngle = true;
                            }
                            else
                            {
                                Console.Error.WriteLine($"Warning: Shape ID {shape.ID} has an invalid IntendedAngle value '{prop.Value.Val}'.");
                            }
                            break;
                        }
                    }

                    // If there is no intended angle defined, skip verification for this shape.
                    if (!hasIntendedAngle)
                        continue;

                    // Compare the actual angle with the intended angle using a small tolerance.
                    const double tolerance = 0.001;
                    if (Math.Abs(actualAngle - intendedAngle) > tolerance)
                    {
                        Console.WriteLine($"Mismatch detected on Page {page.ID}, Shape ID {shape.ID}: " +
                                          $"Actual Angle = {actualAngle}, Intended Angle = {intendedAngle}");
                    }
                    else
                    {
                        // Optional: report successful verification.
                        Console.WriteLine($"Shape ID {shape.ID} on Page {page.ID} matches intended rotation ({actualAngle}°).");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors and report them.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}