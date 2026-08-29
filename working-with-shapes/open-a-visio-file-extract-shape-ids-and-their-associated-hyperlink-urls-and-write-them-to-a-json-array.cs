using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file
                string visioPath = "input.vsdx";

                // Path for the output JSON file
                string jsonOutputPath = "hyperlinks.json";

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(visioPath))
                {
                    // List to hold shape‑id and hyperlink pairs
                    var hyperlinkEntries = new List<object>();

                    // Iterate through all pages in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Each shape may contain zero or more hyperlinks
                            foreach (Hyperlink link in shape.Hyperlinks)
                            {
                                // Add an entry with the shape ID and the hyperlink address
                                hyperlinkEntries.Add(new
                                {
                                    ShapeId = shape.ID,
                                    Url = link.Address
                                });
                            }
                        }
                    }

                    // Serialize the list to a formatted JSON string
                    string json = JsonSerializer.Serialize(
                        hyperlinkEntries,
                        new JsonSerializerOptions { WriteIndented = true });

                    // Write the JSON to the output file
                    File.WriteAllText(jsonOutputPath, json);
                }

                Console.WriteLine("Hyperlink extraction completed. Output written to " + jsonOutputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }