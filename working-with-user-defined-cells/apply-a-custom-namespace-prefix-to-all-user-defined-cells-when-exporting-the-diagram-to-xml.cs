using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputXmlPath>");
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

        // Define the custom namespace prefix to apply to user‑defined cells.
        const string prefix = "MyNS_";

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Iterate through all user‑defined cells of the shape.
                        foreach (User user in shape.Users)
                        {
                            // Prepend the custom namespace prefix to the universal name of the cell.
                            // The NameU property is writable in Aspose.Diagram.
                            user.NameU = prefix + user.NameU;
                        }
                    }
                }

                // Export the modified diagram to Visio XML (VDX) format.
                diagram.Save(outputPath, SaveFileFormat.Vdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}