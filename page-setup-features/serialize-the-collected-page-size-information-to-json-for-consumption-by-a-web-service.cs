using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    // Simple DTO to hold page size information
    private class PageInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double WidthInches { get; set; }
        public double HeightInches { get; set; }
    }

    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output JSON file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputJsonPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Collect page size information
            List<PageInfo> pagesInfo = new List<PageInfo>();

            // Iterate over each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Retrieve width and height from PageProps (values are in inches)
                double width = page.PageSheet.PageProps.PageWidth.Value;
                double height = page.PageSheet.PageProps.PageHeight.Value;

                // Page ID and Name
                int pageId = page.ID;
                string pageName = page.NameU;

                pagesInfo.Add(new PageInfo
                {
                    Id = pageId,
                    Name = pageName,
                    WidthInches = width,
                    HeightInches = height
                });
            }

            // Serialize the list to JSON with indentation for readability
            string json = JsonSerializer.Serialize(pagesInfo, new JsonSerializerOptions { WriteIndented = true });

            // Ensure the output directory exists
            string outputDir = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Write JSON to the specified file
            File.WriteAllText(outputPath, json);
            Console.WriteLine($"Page size information successfully written to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}