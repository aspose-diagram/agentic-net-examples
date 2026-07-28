using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your file path)
                var diagram = new Diagram("input.vsdx");

                // Dictionary to store Cost values keyed by shape ID
                var costValues = new Dictionary<long, string>();

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Look for a user‑defined cell named "Cost"
                        foreach (User userCell in shape.Users)
                        {
                            if (userCell.Name == "Cost" || userCell.NameU == "Cost")
                            {
                                // Store the value (userCell.Value.Val) in the dictionary
                                costValues[shape.ID] = userCell.Value.Val;
                                break; // Assuming only one "Cost" cell per shape
                            }
                        }
                    }
                }

                // Output the collected Cost values
                foreach (var kvp in costValues)
                {
                    Console.WriteLine($"Shape ID {kvp.Key}: Cost = {kvp.Value}");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }