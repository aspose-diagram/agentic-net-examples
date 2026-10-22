using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input file, old domain, new domain
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <oldDomain> <newDomain>");
            return;
        }

        string inputPath = args[0];
        string oldDomain = args[1];
        string newDomain = args[2];

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Prepare output path (same folder, suffix "_updated.vsdx")
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_updated.vsdx");

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has a Hyperlinks collection
                    if (shape.Hyperlinks == null)
                        continue;

                    // Iterate through each hyperlink of the shape
                    foreach (Hyperlink link in shape.Hyperlinks)
                    {
                        // Guard against null Address
                        if (link.Address == null)
                            continue;

                        string address = link.Address.Value ?? string.Empty;

                        // If the address contains the old domain, replace it
                        if (address.Contains(oldDomain, StringComparison.OrdinalIgnoreCase))
                        {
                            string updatedAddress = address.Replace(oldDomain, newDomain, StringComparison.OrdinalIgnoreCase);
                            link.Address.Value = updatedAddress;
                        }
                    }
                }
            }

            // Save the modified diagram to the output file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Hyperlinks updated successfully. Saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}