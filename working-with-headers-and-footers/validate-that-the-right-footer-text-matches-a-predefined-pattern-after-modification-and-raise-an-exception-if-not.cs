using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> [newFooterText]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string newFooterText = args.Length >= 3 ? args[2] : "Page 1";

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Predefined pattern that the footer must match.
        const string pattern = @"^Page \d+$";

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Modify the right footer text.
                diagram.HeaderFooter.FooterRight = newFooterText;

                // Validate the footer text against the pattern.
                if (!Regex.IsMatch(diagram.HeaderFooter.FooterRight, pattern))
                {
                    throw new Exception($"Footer text \"{diagram.HeaderFooter.FooterRight}\" does not match the required pattern \"{pattern}\".");
                }

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Footer text validated and diagram saved successfully.");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}