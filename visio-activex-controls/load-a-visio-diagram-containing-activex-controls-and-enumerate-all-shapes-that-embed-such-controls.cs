using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                Console.WriteLine($"Loaded diagram: {inputPath}");
                Console.WriteLine("Enumerating shapes that contain ActiveX controls...");

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Check if the shape embeds an ActiveX control.
                        if (shape.ActiveXControl != null)
                        {
                            // Retrieve the concrete control type name.
                            string controlTypeName = shape.ActiveXControl.GetType().Name;

                            // Output shape identification and control type.
                            Console.WriteLine($"Page ID: {page.ID}, Shape ID: {shape.ID}, Control Type: {controlTypeName}");
                        }
                    }
                }

                Console.WriteLine("Enumeration completed.");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}