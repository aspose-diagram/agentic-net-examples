using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expecting four arguments: input diagram path, output diagram path, old domain, new domain
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <inputPath> <outputPath> <oldDomain> <newDomain>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string oldDomain = args[2];
        string newDomain = args[3];

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

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
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has a Hyperlinks collection
                    if (shape.Hyperlinks == null)
                        continue;

                    // Iterate through each hyperlink in the shape
                    foreach (Hyperlink link in shape.Hyperlinks)
                    {
                        // Guard against null Address
                        if (link.Address == null || link.Address.Value == null)
                            continue;

                        string address = link.Address.Value;
                        // Perform domain replacement if the old domain is present
                        if (address.Contains(oldDomain, StringComparison.OrdinalIgnoreCase))
                        {
                            string updatedAddress = address.Replace(oldDomain, newDomain, StringComparison.OrdinalIgnoreCase);
                            link.Address.Value = updatedAddress;
                        }
                    }
                }
            }

            // Save the updated diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Hyperlinks updated and diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}