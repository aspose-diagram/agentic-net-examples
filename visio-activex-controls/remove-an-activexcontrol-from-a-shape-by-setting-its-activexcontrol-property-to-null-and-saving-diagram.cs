using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFilePath> <outputFilePath>");
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
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                bool anyControlFound = false;

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape contains an ActiveX control.
                        if (shape.ActiveXControl != null)
                        {
                            anyControlFound = true;
                            // The ActiveXControl property is read‑only; direct removal by assignment is not supported.
                            // Log the presence of the control; removal cannot be performed via the current API.
                            Console.WriteLine($"ActiveX control detected on shape ID {shape.ID} on page \"{page.NameU}\". Removal not supported by API.");
                        }
                    }
                }

                if (!anyControlFound)
                {
                    Console.WriteLine("No ActiveX controls were found in the diagram.");
                }

                // Save the (potentially unchanged) diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}