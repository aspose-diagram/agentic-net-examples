using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input file path, output file path, and the regex pattern to validate the right footer.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx> <footerPattern>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string pattern = args[2];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the API.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Modify the right footer text.
                diagram.HeaderFooter.FooterRight = "Generated Footer: 2023-10-20";

                // Retrieve the current footer text (null-safe).
                string footerText = diagram.HeaderFooter.FooterRight ?? string.Empty;

                // Validate the footer text against the provided regex pattern.
                if (!Regex.IsMatch(footerText, pattern))
                {
                    // Throw an exception if the pattern does not match.
                    throw new Exception($"Footer text does not match the expected pattern. Footer: '{footerText}', Pattern: '{pattern}'.");
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}