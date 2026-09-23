using System.IO;
using System;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Modify the right footer text
            diagram.HeaderFooter.FooterRight = "Page: 1";

            // Define the expected pattern for the footer text
            string pattern = @"^Page: \d+$";

            // Validate that the footer text matches the pattern
            if (!Regex.IsMatch(diagram.HeaderFooter.FooterRight, pattern))
            {
                throw new Exception($"Footer text '{diagram.HeaderFooter.FooterRight}' does not match the required pattern.");
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Diagram saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
