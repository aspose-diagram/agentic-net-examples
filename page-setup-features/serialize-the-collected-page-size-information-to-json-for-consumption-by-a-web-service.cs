using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputJsonFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Prepare a list to hold page size information
            var pageInfos = new List<PageInfo>();

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page identifier and name
                int pageId = page.ID;
                string pageName = page.Name;

                // Retrieve page width and height (values are in inches)
                double width = page.PageSheet.PageProps.PageWidth.Value;
                double height = page.PageSheet.PageProps.PageHeight.Value;

                // Add the collected information to the list
                pageInfos.Add(new PageInfo
                {
                    PageId = pageId,
                    Name = pageName,
                    WidthInches = width,
                    HeightInches = height
                });
            }

            // Serialize the list to JSON with indentation for readability
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(pageInfos, jsonOptions);

            // Ensure the output directory exists
            string outputDir = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Write JSON to the specified output file
            File.WriteAllText(outputPath, json);
            Console.WriteLine($"Page size information successfully written to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    // Helper class to represent page size information
    private class PageInfo
    {
        public int PageId { get; set; }
        public string Name { get; set; }
        public double WidthInches { get; set; }
        public double HeightInches { get; set; }
    }
}