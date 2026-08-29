using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class DiagramPlaceholderReplacer
{
    /// <summary>
    /// Loads a Visio diagram, replaces placeholders in shape texts with values from the dictionary,
    /// and saves the updated diagram.
    /// </summary>
    /// <param name="inputPath">Path to the source Visio file.</param>
    /// <param name="outputPath">Path where the updated Visio file will be saved.</param>
    /// <param name="placeholders">Dictionary where key is the placeholder name (without braces) and value is the replacement text.</param>
    public void ReplacePlaceholders(string inputPath, string outputPath, Dictionary<string, string> placeholders)
    {
        // Load the diagram from the file (uses Diagram(string) constructor)
        Diagram diagram = new Diagram(inputPath);

        // Iterate through all pages in the diagram
        foreach (Page page in diagram.Pages)
        {
            // Iterate through all shapes on the current page
            foreach (Shape shape in page.Shapes)
            {
                // For each placeholder, replace occurrences in the shape's text
                foreach (KeyValuePair<string, string> kvp in placeholders)
                {
                    // Define the placeholder pattern, e.g., {{Name}}
                    string placeholderPattern = $"{{{{{kvp.Key}}}}}";
                    // Replace the placeholder with the actual value
                    shape.ReplaceText(placeholderPattern, kvp.Value);
                }

                // Refresh shape data after text changes (optional but recommended)
                shape.RefreshData();
            }
        }

        // Save the updated diagram to the specified output file in VDX format
        diagram.Save(outputPath, SaveFileFormat.Vdx);
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {

            var obj = new DiagramPlaceholderReplacer();
            obj.ReplacePlaceholders("", "", null);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
