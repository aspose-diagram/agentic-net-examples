using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Verify the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Remove hidden shapes and masters (Pages flag does not exist)
            diagram.RemoveHiddenInformation(
                (int)(RemoveHiddenInfoItem.Shapes | RemoveHiddenInfoItem.Masters));

            // Validate that no hidden comments (annotations) remain on any page
            foreach (Page page in diagram.Pages)
            {
                // Annotations collection may be null; check count if present
                if (page.PageSheet.Annotations != null && page.PageSheet.Annotations.Count > 0)
                {
                    throw new Exception(
                        $"Hidden comments detected on page '{page.Name}'. Count: {page.PageSheet.Annotations.Count}");
                }
            }

            Console.WriteLine("Validation passed: No hidden comments remain.");

            // Path for the cleaned output diagram
            string outputPath = "output_cleaned.vsdx";

            // Save the cleaned diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream for visibility
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}