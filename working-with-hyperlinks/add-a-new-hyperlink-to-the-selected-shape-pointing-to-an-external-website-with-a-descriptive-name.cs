using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Prompt user for the input Visio file path.
        Console.Write("Enter the path to the Visio file (e.g., diagram.vsdx): ");
        string inputPath = Console.ReadLine();

        // Guard: ensure the file exists.
        if (string.IsNullOrWhiteSpace(inputPath) || !File.Exists(inputPath))
        {
            Console.Error.WriteLine("Error: The specified Visio file does not exist.");
            return;
        }

        // Prompt user for the shape name (NameU) to which the hyperlink will be added.
        Console.Write("Enter the NameU of the shape to modify: ");
        string targetShapeName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(targetShapeName))
        {
            Console.Error.WriteLine("Error: Shape name cannot be empty.");
            return;
        }

        // Prompt user for the hyperlink URL.
        Console.Write("Enter the hyperlink URL (e.g., https://example.com): ");
        string hyperlinkUrl = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(hyperlinkUrl))
        {
            Console.Error.WriteLine("Error: Hyperlink URL cannot be empty.");
            return;
        }

        // Prompt user for a descriptive name for the hyperlink.
        Console.Write("Enter a descriptive name for the hyperlink: ");
        string hyperlinkName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(hyperlinkName))
        {
            Console.Error.WriteLine("Error: Hyperlink name cannot be empty.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                Shape targetShape = null;

                // Iterate through all pages and shapes to find the shape with the specified NameU.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // NameU holds the universal name of the shape.
                        if (shape.NameU == targetShapeName)
                        {
                            targetShape = shape;
                            break;
                        }
                    }
                    if (targetShape != null)
                        break;
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Error: Shape with NameU '{targetShapeName}' not found.");
                    return;
                }

                // Create a new Hyperlink instance.
                Hyperlink link = new Hyperlink();

                // Set the descriptive name of the hyperlink.
                link.Name = hyperlinkName;

                // Set the external address (URL) of the hyperlink.
                link.Address.Value = hyperlinkUrl;

                // Add the hyperlink to the shape's Hyperlinks collection.
                targetShape.Hyperlinks.Add(link);

                // Define output file path (original name with "_modified" suffix).
                string directory = Path.GetDirectoryName(inputPath);
                string filenameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(directory, $"{filenameWithoutExt}_modified.vsdx");

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Hyperlink added successfully. Modified file saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}