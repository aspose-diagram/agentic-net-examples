using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Diagram;

public class PageDimension
{
    // Width and Height are stored in inches (same units as Visio page properties)
    public double Width { get; set; }
    public double Height { get; set; }
    // Optional identifier to match a page; can be the page name or any custom key
    public string Name { get; set; }
}

public class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio diagram file
            string diagramPath = "input.vsdx";

            // Path to the JSON file that contains previously saved page dimensions
            string jsonPath = "pageDimensions.json";

            // Load the diagram from file
            Diagram diagram = new Diagram(diagramPath);

            // Deserialize the JSON file into a list of PageDimension objects
            List<PageDimension> savedDimensions;
            using (FileStream jsonStream = File.OpenRead(jsonPath))
            {
                savedDimensions = JsonSerializer.Deserialize<List<PageDimension>>(jsonStream);
            }

            if (savedDimensions == null)
            {
                throw new Exception("Failed to deserialize page dimensions from JSON.");
            }

            // Iterate through each page in the diagram and compare dimensions
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                Page page = diagram.Pages[i];

                // Try to locate a saved dimension entry that matches the page name
                PageDimension saved = savedDimensions.Find(p => p.Name == page.Name);

                // If no match by name, fall back to matching by index order
                if (saved == null && i < savedDimensions.Count)
                {
                    saved = savedDimensions[i];
                }

                if (saved == null)
                {
                    Console.WriteLine($"No saved dimensions found for page '{page.Name}'. Skipping comparison.");
                    continue;
                }

                // Retrieve current page width and height (values are in inches)
                double currentWidth = page.PageSheet.PageProps.PageWidth.Value;
                double currentHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Use a small tolerance to avoid false mismatches due to floating‑point rounding
                const double tolerance = 0.001;
                bool widthMatches = Math.Abs(currentWidth - saved.Width) <= tolerance;
                bool heightMatches = Math.Abs(currentHeight - saved.Height) <= tolerance;

                if (widthMatches && heightMatches)
                {
                    Console.WriteLine($"Page '{page.Name}' dimensions match: Width={currentWidth}, Height={currentHeight}");
                }
                else
                {
                    string error = $"Dimension mismatch on page '{page.Name}'. " +
                                   $"Current (W:{currentWidth}, H:{currentHeight}) vs Saved (W:{saved.Width}, H:{saved.Height})";
                    // Throw an exception to indicate the validation failure
                    throw new Exception(error);
                }
            }

            // No further actions required; the program ends after validation.

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}