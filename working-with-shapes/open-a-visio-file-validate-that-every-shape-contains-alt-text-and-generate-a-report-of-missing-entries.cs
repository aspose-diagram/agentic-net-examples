using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

public class Program
{
    public static void Main(string[] args)
    {
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            Diagram diagram = new Diagram(inputPath);
            List<string> missingEntries = new List<string>();

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Del == BOOL.True)
                        continue;

                    string altText = shape.Misc.Comment?.Value;

                    if (string.IsNullOrWhiteSpace(altText))
                    {
                        missingEntries.Add(
                            $"Page: {page.Name} (ID {page.ID}), Shape ID: {shape.ID}, Name: {shape.Name}");
                    }
                }
            }

            Console.WriteLine("Alt Text Validation Report");
            if (missingEntries.Count == 0)
            {
                Console.WriteLine("All shapes contain Alt text.");
            }
            else
            {
                foreach (string line in missingEntries)
                {
                    Console.WriteLine(line);
                }

                string reportPath = "AltTextReport.txt";
                File.WriteAllLines(reportPath, missingEntries);
                Console.WriteLine($"Report written to {reportPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}