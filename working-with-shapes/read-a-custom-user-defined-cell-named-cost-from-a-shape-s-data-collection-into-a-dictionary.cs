using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Dictionary to hold Shape ID -> Cost value
                Dictionary<long, string> shapeCostMap = new Dictionary<long, string>();

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Look for a user‑defined cell named "Cost"
                        foreach (User userCell in shape.Users)
                        {
                            if (userCell.Name == "Cost")
                            {
                                // Store the value in the dictionary (using Shape ID as the key)
                                shapeCostMap[shape.ID] = userCell.Value.Val;
                                break; // Cost cell found, no need to continue inner loop
                            }
                        }
                    }
                }

                // Output the collected values
                foreach (KeyValuePair<long, string> entry in shapeCostMap)
                {
                    Console.WriteLine($"Shape ID {entry.Key}: Cost = {entry.Value}");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }