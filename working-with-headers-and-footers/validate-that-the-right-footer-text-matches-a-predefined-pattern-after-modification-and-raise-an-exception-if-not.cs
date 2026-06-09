using System.IO;
using System;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class FooterValidator
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            var diagram = new Diagram("input.vsdx");

            // Set the right footer text to a new value
            string newFooterText = "2023-09-15";
            diagram.HeaderFooter.FooterRight = newFooterText;

            // Define the required pattern (e.g., a date in YYYY-MM-DD format)
            string pattern = @"^\d{4}-\d{2}-\d{2}$";

            // Validate that the FooterRight matches the pattern
            if (!Regex.IsMatch(diagram.HeaderFooter.FooterRight, pattern))
            {
                throw new InvalidOperationException(
                    $"FooterRight '{diagram.HeaderFooter.FooterRight}' does not match the required pattern.");
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
