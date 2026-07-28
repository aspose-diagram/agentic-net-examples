using System;
using System.Collections.Generic;
using Aspose.Diagram;

namespace HyperlinkExtractor
{
    // Simple logger that writes dictionary contents to the console
    public static class Logger
    {
        public static void LogHyperlinks(Dictionary<string, string> hyperlinks)
        {
            Console.WriteLine("=== Hyperlink Information ===");
            foreach (KeyValuePair<string, string> entry in hyperlinks)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value}");
            }
            Console.WriteLine("=== End of Hyperlink Information ===");
        }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Load the Visio diagram (replace with your actual file path)
                string diagramPath = "input.vsdx";
                Diagram diagram = new Diagram(diagramPath);

                // Dictionary to hold hyperlink data
                Dictionary<string, string> hyperlinkData = new Dictionary<string, string>();

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a Hyperlinks collection
                        if (shape.Hyperlinks != null)
                        {
                            int linkIndex = 0;
                            foreach (Hyperlink link in shape.Hyperlinks)
                            {
                                // Build a unique key for each hyperlink
                                string key = $"Page{page.ID}_Shape{shape.ID}_Link{linkIndex}";

                                // Retrieve hyperlink properties using .Value
                                string address = link.Address?.Value ?? string.Empty;
                                string subAddress = link.SubAddress?.Value ?? string.Empty;
                                string description = link.Description?.Value ?? string.Empty;
                                string name = link.Name ?? string.Empty;

                                // Combine properties into a readable string
                                string value = $"Name={name}; Address={address}; SubAddress={subAddress}; Description={description}";

                                hyperlinkData[key] = value;
                                linkIndex++;
                            }
                        }
                    }
                }

                // Pass the dictionary to the logging framework
                Logger.LogHyperlinks(hyperlinkData);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}