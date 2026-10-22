using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (provide via command‑line or use defaults)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define shape‑to‑page navigation mapping (shape name -> target page name)
        var navigationMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            // Example mappings – adjust as needed
            { "HomeButton", "HomePage" },
            { "DetailsButton", "DetailsPage" },
            { "SettingsButton", "SettingsPage" }
        };

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the shape's universal name (NameU)
                        string shapeName = shape.NameU;
                        if (string.IsNullOrEmpty(shapeName))
                            continue;

                        // Check if this shape should receive a hyperlink
                        if (navigationMap.TryGetValue(shapeName, out string targetPageName))
                        {
                            // Create a new hyperlink object
                            Hyperlink hyperlink = new Hyperlink();

                            // Set a description (optional)
                            hyperlink.Description.Value = $"Navigate to page \"{targetPageName}\"";

                            // Set the sub‑address to the target page name (page‑link)
                            hyperlink.SubAddress.Value = targetPageName;

                            // Ensure the address is empty for an internal page link
                            hyperlink.Address.Value = string.Empty;

                            // Add the hyperlink to the shape's collection
                            shape.Hyperlinks.Add(hyperlink);
                        }
                    }
                }

                // Save the modified diagram to the output file in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}