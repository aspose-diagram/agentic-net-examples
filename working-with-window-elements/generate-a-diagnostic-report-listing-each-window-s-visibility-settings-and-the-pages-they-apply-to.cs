using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (modify the path as needed)
            string filePath = "input.vsdx";
            Diagram diagram = new Diagram(filePath);

            Console.WriteLine("=== Window Visibility Diagnostic Report ===");

            // Check if any windows are defined
            if (diagram.Windows.Count == 0)
            {
                Console.WriteLine("No windows found in the diagram.");
            }
            else
            {
                // Iterate through each window
                foreach (Window window in diagram.Windows)
                {
                    Console.WriteLine($"Window ID: {window.ID}");
                    Console.WriteLine($"  Type: {window.WindowType}");
                    Console.WriteLine($"  State: {window.WindowState}");
                    Console.WriteLine($"  ShowGrid: {window.ShowGrid}");
                    Console.WriteLine($"  ShowGuides: {window.ShowGuides}");
                    Console.WriteLine($"  ShowRulers: {window.ShowRulers}");
                    Console.WriteLine($"  ShowPageBreaks: {window.ShowPageBreaks}");
                    Console.WriteLine($"  DynamicGridEnabled: {window.DynamicGridEnabled}");
                    Console.WriteLine($"  ShowConnectionPoints: {window.ShowConnectionPoints}");

                    // List pages that the window settings affect (global to all pages)
                    Console.WriteLine("  Applies to Pages:");
                    foreach (Page page in diagram.Pages)
                    {
                        Console.WriteLine($"    Page ID: {page.ID}, Name: {page.Name}");
                    }

                    Console.WriteLine();
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
