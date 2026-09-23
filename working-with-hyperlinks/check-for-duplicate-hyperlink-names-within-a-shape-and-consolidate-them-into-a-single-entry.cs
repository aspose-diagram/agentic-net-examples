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

            // Load the diagram (replace with your actual file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has hyperlinks
                    if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                    {
                        // Track first occurrence of each hyperlink name
                        var firstByName = new Dictionary<string, Hyperlink>(StringComparer.OrdinalIgnoreCase);
                        // Collect duplicates to remove after iteration
                        var duplicates = new List<Hyperlink>();

                        foreach (Hyperlink hl in shape.Hyperlinks)
                        {
                            // Skip hyperlinks without a name
                            if (string.IsNullOrEmpty(hl.Name))
                                continue;

                            if (firstByName.ContainsKey(hl.Name))
                            {
                                // Duplicate name found – mark for removal
                                duplicates.Add(hl);
                            }
                            else
                            {
                                // First time we see this name – keep it
                                firstByName[hl.Name] = hl;
                            }
                        }

                        // Remove duplicate hyperlink entries from the shape
                        foreach (Hyperlink dup in duplicates)
                        {
                            shape.Hyperlinks.Remove(dup);
                        }
                    }
                }
            }

            // Save the modified diagram (replace with your desired output path)
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
