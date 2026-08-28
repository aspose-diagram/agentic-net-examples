using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

public class PageInfo
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Width { get; set; }
    public double Height { get; set; }
}

public class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be processed
            string inputPath = "input.vsdx";

            // Path where the JSON output will be written
            string outputPath = "pages.json";

            // Load the diagram using Aspose.Diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                var pagesInfo = new List<PageInfo>();

                // Iterate through each page and collect size information
                foreach (Page page in diagram.Pages)
                {
                    var info = new PageInfo
                    {
                        Id = page.ID,
                        Name = page.Name,
                        Width = page.PageSheet.PageProps.PageWidth.Value,
                        Height = page.PageSheet.PageProps.PageHeight.Value
                    };
                    pagesInfo.Add(info);
                }

                // Serialize the list of page information to JSON
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(pagesInfo, jsonOptions);

                // Write the JSON string to the output file
                File.WriteAllText(outputPath, json);
                Console.WriteLine($"Page size information has been saved to '{outputPath}'.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}