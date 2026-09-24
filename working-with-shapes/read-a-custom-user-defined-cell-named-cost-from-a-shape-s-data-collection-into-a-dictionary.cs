using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Path to the Visio file
        string inputPath = "input.vsdx";

        // Guard to ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Dictionary to hold shape identifier and its "Cost" value
        Dictionary<string, string> costDictionary = new Dictionary<string, string>();

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Iterate through the shape's Props collection (Shape Data section)
                    foreach (Prop prop in shape.Props)
                    {
                        // Look for the custom data field named "Cost"
                        if (prop.Name == "Cost")
                        {
                            // Use the shape's universal name (NameU) as the key
                            // Store the value of the "Cost" field
                            costDictionary[shape.NameU] = prop.Value.Val;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            return;
        }

        // Output the collected cost values
        Console.WriteLine("Collected Cost values:");
        foreach (KeyValuePair<string, string> entry in costDictionary)
        {
            Console.WriteLine($"Shape: {entry.Key}, Cost: {entry.Value}");
        }
    }
}