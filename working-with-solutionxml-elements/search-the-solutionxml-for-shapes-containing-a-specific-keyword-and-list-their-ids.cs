using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Keyword to search for
            string keyword = "YourKeyword";

            // List to hold IDs of matching shapes
            List<long> matchingShapeIds = new List<long>();

            // Iterate through all pages and their shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the plain text of the shape
                    string shapeText = shape.GetPureText();

                    // Check if the text contains the keyword (case‑insensitive)
                    if (!string.IsNullOrEmpty(shapeText) &&
                        shapeText.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    {
                        matchingShapeIds.Add(shape.ID);
                    }
                }
            }

            // Output the IDs of shapes that contain the keyword
            Console.WriteLine($"Shapes containing keyword \"{keyword}\":");
            foreach (long id in matchingShapeIds)
            {
                Console.WriteLine($"Shape ID: {id}");
            }

            // Optional: save the diagram if modifications were made
            // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
