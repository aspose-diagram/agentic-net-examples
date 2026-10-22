using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, data feed file, output Visio file
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <dataFeedPath> <outputVisioPath>");
            return;
        }

        string inputVisioPath = args[0];
        string dataFeedPath = args[1];
        string outputVisioPath = args[2];

        // Guard: verify input Visio file exists
        if (!File.Exists(inputVisioPath))
        {
            Console.Error.WriteLine($"Input Visio file not found: {inputVisioPath}");
            return;
        }

        // Guard: verify data feed file exists
        if (!File.Exists(dataFeedPath))
        {
            Console.Error.WriteLine($"Data feed file not found: {dataFeedPath}");
            return;
        }

        // Load external data feed (CSV format: ShapeName,Url)
        Dictionary<string, string> urlMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        try
        {
            foreach (string line in File.ReadAllLines(dataFeedPath))
            {
                // Skip empty lines
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] parts = line.Split(new char[] { ',' }, 2);
                if (parts.Length == 2)
                {
                    string shapeName = parts[0].Trim();
                    string url = parts[1].Trim();
                    if (!string.IsNullOrEmpty(shapeName) && !string.IsNullOrEmpty(url))
                    {
                        // Store mapping; later shapes with matching NameU will receive this URL
                        urlMap[shapeName] = url;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading data feed: {ex.Message}");
            return;
        }

        // Process the Visio diagram and populate hyperlinks
        try
        {
            // Load diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputVisioPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a NameU and that a URL exists for this name
                        if (!string.IsNullOrEmpty(shape.NameU) && urlMap.TryGetValue(shape.NameU, out string url))
                        {
                            // Create a new Hyperlink instance
                            Hyperlink link = new Hyperlink();

                            // Set the address of the hyperlink to the external URL
                            link.Address.Value = url;

                            // Optionally, set a description (visible on hover in Visio)
                            link.Description.Value = "External link";

                            // Add the hyperlink to the shape's collection
                            shape.Hyperlinks.Add(link);
                        }
                    }
                }

                // Refresh the diagram to apply changes
                diagram.Refresh();

                // Save the modified diagram to the specified output path in VSDX format
                diagram.Save(outputVisioPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}