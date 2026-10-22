using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the Visio file path as the first command‑line argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as a command-line argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Verify that the diagram contains at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Retrieve the first page.
            Page page = diagram.Pages[0];

            // Find the first shape on the page.
            Shape shape = null;
            foreach (Shape s in page.Shapes)
            {
                shape = s;
                break;
            }

            if (shape == null)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Prepare a list to hold hyperlink information.
            List<object> hyperlinkData = new List<object>();

            // Check whether the shape has any hyperlinks.
            if (shape.Hyperlinks != null)
            {
                // Iterate through each hyperlink in the collection.
                foreach (Hyperlink link in shape.Hyperlinks)
                {
                    // Build an anonymous object with the desired hyperlink properties.
                    var linkInfo = new
                    {
                        Name = link.Name,
                        Description = link.Description != null ? link.Description.Value : null,
                        Address = link.Address != null ? link.Address.Value : null,
                        SubAddress = link.SubAddress != null ? link.SubAddress.Value : null,
                        NewWindow = link.NewWindow != null ? (link.NewWindow.Value == BOOL.True) : (bool?)null
                    };

                    // Add the object to the result list.
                    hyperlinkData.Add(linkInfo);
                }
            }

            // Serialize the list to formatted JSON.
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string jsonOutput = JsonSerializer.Serialize(hyperlinkData, jsonOptions);

            // Output the JSON to the console.
            Console.WriteLine(jsonOutput);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}