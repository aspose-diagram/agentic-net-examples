using System.IO;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class FilterShapesByEventComment
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Define the keyword to search for (case‑insensitive)
            string keyword = "Important";
            // Build a regular expression that looks for the keyword anywhere in the comment
            Regex regex = new Regex(keyword, RegexOptions.IgnoreCase);

            // List to hold IDs of shapes whose comment matches the regex
            List<long> matchingShapeIds = new List<long>();

            // Iterate through all pages and their shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // The comment text is stored in the Misc.Comment property
                    string comment = shape.Misc?.Comment?.Value;
                    if (!string.IsNullOrEmpty(comment) && regex.IsMatch(comment))
                    {
                        matchingShapeIds.Add(shape.ID);
                    }
                }
            }

            // Output the matching shape IDs
            Console.WriteLine("Shapes with EventComment containing the keyword:");
            foreach (long id in matchingShapeIds)
            {
                Console.WriteLine($"Shape ID: {id}");
            }

            // (Optional) Save the diagram if any modifications were made
            // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
