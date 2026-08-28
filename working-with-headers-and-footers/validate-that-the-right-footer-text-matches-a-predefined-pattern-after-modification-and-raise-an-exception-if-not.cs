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
            Diagram diagram = new Diagram("input.vsdx");

            // Define the new footer right text
            string newFooterRight = "Report generated on 2026-08-21";

            // Apply the new text to the right portion of the footer
            diagram.HeaderFooter.FooterRight = newFooterRight;

            // Define the expected pattern (example: must contain a date in YYYY-MM-DD format)
            string pattern = @"\d{4}-\d{2}-\d{2}";

            // Validate the footer right text against the pattern
            if (!Regex.IsMatch(diagram.HeaderFooter.FooterRight, pattern))
            {
                throw new InvalidOperationException("FooterRight does not match the required pattern.");
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
