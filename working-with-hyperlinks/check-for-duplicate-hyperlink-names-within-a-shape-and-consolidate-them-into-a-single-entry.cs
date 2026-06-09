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

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Dictionary to track first occurrence of each hyperlink name
                    var seenNames = new Dictionary<string, Hyperlink>(StringComparer.OrdinalIgnoreCase);

                    // Iterate backwards so that removal does not affect the index
                    for (int i = shape.Hyperlinks.Count - 1; i >= 0; i--)
                    {
                        Hyperlink hl = shape.Hyperlinks[i];

                        // Use Name property as the identifier; fall back to empty string if null
                        string name = hl.Name ?? string.Empty;

                        if (seenNames.ContainsKey(name))
                        {
                            // Duplicate found – remove this hyperlink
                            shape.Hyperlinks.Remove(hl);
                        }
                        else
                        {
                            // First time we see this name – keep it
                            seenNames[name] = hl;
                        }
                    }
                }
            }

            // Save the modified diagram (replace with your desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
