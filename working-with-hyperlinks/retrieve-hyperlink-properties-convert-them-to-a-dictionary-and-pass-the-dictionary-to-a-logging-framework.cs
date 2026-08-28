using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram
                string filePath = "sample.vsdx";
                Diagram diagram = new Diagram(filePath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape has any hyperlinks
                        if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                        {
                            int linkIndex = 0;
                            foreach (Hyperlink link in shape.Hyperlinks)
                            {
                                // Convert hyperlink properties to a dictionary
                                var hyperlinkDict = new Dictionary<string, string>
                                {
                                    { "ShapeId", shape.ID.ToString() },
                                    { "ShapeName", shape.Name ?? string.Empty },
                                    { "LinkIndex", linkIndex.ToString() },
                                    { "Name", link.Name ?? string.Empty },
                                    { "Address", link.Address?.Value ?? string.Empty },
                                    { "SubAddress", link.SubAddress?.Value ?? string.Empty },
                                    { "Description", link.Description?.Value ?? string.Empty }
                                };

                                // Pass the dictionary to the logging framework (console in this example)
                                LogHyperlink(hyperlinkDict);

                                linkIndex++;
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

        // Simple logger that writes dictionary contents to the console
        static void LogHyperlink(Dictionary<string, string> dict)
        {
            Console.WriteLine("Hyperlink Details:");
            foreach (var kvp in dict)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
            Console.WriteLine();
        }
    }