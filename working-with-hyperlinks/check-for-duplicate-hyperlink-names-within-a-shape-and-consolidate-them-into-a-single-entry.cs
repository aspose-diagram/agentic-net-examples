using System;
using System.IO;
using Aspose.Diagram;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    HyperlinkCollection hyperlinks = shape.Hyperlinks;
                    if (hyperlinks == null || hyperlinks.Count <= 1)
                        continue; // No possible duplicates

                    // Track first occurrence of each hyperlink name
                    var firstByName = new Dictionary<string, Hyperlink>();
                    var duplicates = new List<Hyperlink>();

                    foreach (Hyperlink link in hyperlinks)
                    {
                        string name = link.Name;
                        if (string.IsNullOrEmpty(name))
                            continue; // Skip unnamed hyperlinks

                        if (firstByName.ContainsKey(name))
                        {
                            // Duplicate name found – mark for removal
                            duplicates.Add(link);
                        }
                        else
                        {
                            firstByName[name] = link;
                        }
                    }

                    // Remove duplicate hyperlinks, keeping the first occurrence
                    foreach (Hyperlink dup in duplicates)
                    {
                        hyperlinks.Remove(dup);
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
