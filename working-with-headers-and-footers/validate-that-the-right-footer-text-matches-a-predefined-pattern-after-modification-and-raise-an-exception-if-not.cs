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

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Define the new footer text
            string newFooterText = "Report generated on 2026-07-22";

            // Set the right footer text
            diagram.HeaderFooter.FooterRight = newFooterText;

            // Define the pattern the footer must match (example: starts with "Report generated on" followed by a date)
            string pattern = @"^Report generated on \d{4}-\d{2}-\d{2}$";

            // Validate the footer text against the pattern
            if (!Regex.IsMatch(diagram.HeaderFooter.FooterRight, pattern))
            {
                throw new InvalidOperationException(
                    $"FooterRight text \"{diagram.HeaderFooter.FooterRight}\" does not match the required pattern \"{pattern}\".");
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
