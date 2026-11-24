using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputVisioFilePath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(
            Path.GetDirectoryName(inputPath) ?? "",
            Path.GetFileNameWithoutExtension(inputPath) + "_rotated.vsdx");

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                bool ovalFound = false;

                // Iterate through all pages and shapes to locate the first oval shape
                foreach (Page page in diagram.Pages)
                {
                    // Shapes collection is indexed; use for loop to avoid LINQ
                    for (int i = 0; i < page.Shapes.Count; i++)
                    {
                        Shape shape = page.Shapes[i];

                        // Ensure the shape has a master and check its name
                        if (shape.Master != null && shape.Master.NameU == "Oval")
                        {
                            // Set rotation angle to 30 degrees using the SetAngle method
                            shape.SetAngle(30.0);
                            ovalFound = true;
                            break; // Exit inner loop after rotating the first oval
                        }
                    }

                    if (ovalFound)
                        break; // Exit outer loop once the oval has been processed
                }

                if (!ovalFound)
                {
                    Console.Error.WriteLine("No oval shape was found in the diagram.");
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}