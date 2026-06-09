using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file
                string filePath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a Hyperlinks collection
                        if (shape.Hyperlinks != null)
                        {
                            // Iterate each hyperlink in the shape
                            foreach (Hyperlink link in shape.Hyperlinks)
                            {
                                // Create a dictionary with hyperlink properties
                                var hyperlinkData = new Dictionary<string, string>
                                {
                                    { "Address", link.Address?.Value },
                                    { "SubAddress", link.SubAddress?.Value },
                                    { "Description", link.Description?.Value }
                                };

                                // Log the dictionary (using Console as a simple logger)
                                Console.WriteLine($"Shape ID: {shape.ID}");
                                foreach (KeyValuePair<string, string> kvp in hyperlinkData)
                                {
                                    Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
                                }
                            }
                        }
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }