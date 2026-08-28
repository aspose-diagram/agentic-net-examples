using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class HyperlinkConsolidator
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Get the collection of hyperlinks for the current shape
                    HyperlinkCollection hyperlinks = shape.Hyperlinks;

                    // Dictionary to keep track of the first hyperlink encountered for each name
                    Dictionary<string, Hyperlink> firstByName = new Dictionary<string, Hyperlink>(StringComparer.OrdinalIgnoreCase);

                    // List to collect duplicates that need to be removed
                    List<Hyperlink> duplicates = new List<Hyperlink>();

                    // Examine each hyperlink in the collection
                    foreach (Hyperlink hl in hyperlinks)
                    {
                        // Use empty string if Name is null to avoid null reference issues
                        string name = hl.Name ?? string.Empty;

                        if (firstByName.ContainsKey(name))
                        {
                            // Duplicate found – schedule for removal
                            duplicates.Add(hl);
                        }
                        else
                        {
                            // First occurrence of this name – store it
                            firstByName[name] = hl;
                        }
                    }

                    // Remove duplicate hyperlinks from the shape's collection
                    foreach (Hyperlink dup in duplicates)
                    {
                        hyperlinks.Remove(dup);
                    }
                }
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
