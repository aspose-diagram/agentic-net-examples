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
                Diagram diagram = new Diagram("input.vsdx");

                // Dictionary to hold hyperlink information
                Dictionary<string, string> hyperlinkDict = new Dictionary<string, string>();

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a Hyperlinks collection
                        if (shape.Hyperlinks != null)
                        {
                            foreach (Hyperlink link in shape.Hyperlinks)
                            {
                                // Build a unique key for each hyperlink
                                string key = $"Shape{shape.ID}_Link{link.Name}";

                                // Concatenate hyperlink properties
                                string value = $"Address={link.Address.Value}; " +
                                               $"SubAddress={link.SubAddress.Value}; " +
                                               $"Description={link.Description.Value}";

                                // Add to the dictionary (overwrite if duplicate key)
                                hyperlinkDict[key] = value;
                            }
                        }
                    }
                }

                // Pass the dictionary to a logging routine
                LogHyperlinks(hyperlinkDict);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Simple logging method that writes each entry to the console
        static void LogHyperlinks(Dictionary<string, string> dict)
        {
            foreach (KeyValuePair<string, string> entry in dict)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value}");
            }
        }
    }