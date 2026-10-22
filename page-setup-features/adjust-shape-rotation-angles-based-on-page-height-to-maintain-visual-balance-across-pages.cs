using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (provided via command‑line arguments or defaults)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the source Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Reference page height (in inches) used to compute a scaling factor.
            // Here we use 10 inches as a baseline; adjust as needed for your scenario.
            const double referenceHeight = 10.0;

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Retrieve the page height (in inches)
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Compute a rotation scaling factor based on page height
                double heightFactor = pageHeight / referenceHeight;

                // Adjust rotation angle for every shape on the current page
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shape = page.Shapes[i];

                    // Get the current rotation angle (degrees)
                    double originalAngle = shape.XForm.Angle.Value;

                    // Calculate the new angle using the height factor
                    double newAngle = originalAngle * heightFactor;

                    // Normalize the angle to the range [0, 360)
                    newAngle = newAngle % 360.0;
                    if (newAngle < 0) newAngle += 360.0;

                    // Apply the new rotation angle
                    shape.XForm.Angle.Value = newAngle;
                }
            }

            // Save the modified diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}