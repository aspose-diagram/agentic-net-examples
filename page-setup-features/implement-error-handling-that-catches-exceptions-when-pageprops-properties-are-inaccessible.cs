using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the input diagram file path (first argument or default).
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the API.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Use the page ID for identification (Page.Index does not exist).
                    long pageId = page.ID;

                    // Attempt to read page width and height; handle any access issues.
                    try
                    {
                        double width = page.PageSheet.PageProps.PageWidth.Value;
                        double height = page.PageSheet.PageProps.PageHeight.Value;
                        Console.WriteLine($"Page ID {pageId}: Width = {width} inches, Height = {height} inches");
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Error accessing size of page ID {pageId}: {ex.Message}");
                    }

                    // Attempt to read the auto‑expand setting; handle potential exceptions.
                    try
                    {
                        var autoExpand = page.PageSheet.PageProps.DrawingResizeType.Value;
                        Console.WriteLine($"Page ID {pageId}: AutoExpand = {autoExpand}");
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Error accessing auto‑expand of page ID {pageId}: {ex.Message}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Catch any exceptions thrown during diagram loading or processing.
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}