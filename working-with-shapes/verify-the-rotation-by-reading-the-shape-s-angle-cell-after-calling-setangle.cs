using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the input Visio file path (first command‑line argument or default).
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found -> {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram contains at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: Diagram contains no pages.");
                return;
            }

            // Access the first page.
            Page page = diagram.Pages[0];

            // Ensure the page contains at least one shape.
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("Error: First page contains no shapes.");
                return;
            }

            // Retrieve the first shape on the page.
            Shape shape = page.Shapes[0];

            // Define the desired rotation angle in degrees.
            double desiredAngle = 45.0;

            // Rotate the shape using the SetAngle method.
            shape.SetAngle(desiredAngle);

            // Read back the Angle cell value from the shape's XForm section.
            double actualAngle = shape.XForm.Angle.Value;

            // Verify that the angle was set correctly (allowing a tiny tolerance).
            const double tolerance = 0.001;
            if (Math.Abs(actualAngle - desiredAngle) <= tolerance)
            {
                Console.WriteLine($"Success: Shape rotation verified. Angle = {actualAngle} degrees.");
            }
            else
            {
                Console.WriteLine($"Failure: Expected angle {desiredAngle}°, but found {actualAngle}°.");
            }

            // Optional: Save the modified diagram to a new file for visual confirmation.
            string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "output_modified.vsdx");
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Modified diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"Exception: {ex.Message}");
        }
    }
}