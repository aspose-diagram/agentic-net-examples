using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the input Visio file path as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Optional output path; if not provided, create a default one
        string outputPath = args.Length > 1 ? args[1] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "output_modified.vsdx");

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the header/footer font object
            HeaderFooterFont headerFont = diagram.HeaderFooter.HeaderFooterFont;

            // Retrieve and display current header font properties
            Console.WriteLine("Current Header Font Settings:");
            Console.WriteLine($"FaceName : {headerFont.FaceName}");
            Console.WriteLine($"Height   : {headerFont.Height}");
            Console.WriteLine($"Weight   : {headerFont.Weight}");
            Console.WriteLine($"Italic   : {(headerFont.Italic == BOOL.True ? "True" : "False")}");
            Console.WriteLine($"Underline: {(headerFont.Underline == BOOL.True ? "True" : "False")}");

            // Change the Italic property to true
            headerFont.Italic = BOOL.True;

            // Verify the change by reading back the property
            if (headerFont.Italic != BOOL.True)
            {
                throw new Exception("Failed to set header font to italic.");
            }

            Console.WriteLine("\nHeader font italic property set to TRUE successfully.");

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"\nModified diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}