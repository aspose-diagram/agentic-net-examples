using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // TODO: replace with the actual path to your Visio file
                const string diagramPath = "input.vsdx";

                // Load the diagram
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Iterate through all pages
                    for (int i = 0; i < diagram.Pages.Count; i++)
                    {
                        // Retrieve the page by index
                        Page page = diagram.Pages[i];

                        // Page orientation (Landscape, Portrait, SameAsPrinter)
                        PrintPageOrientationValue orientation = page.PageSheet.PrintProps.PrintPageOrientation.Value;

                        // ScaleX value (double)
                        double scaleX = page.PageSheet.PrintProps.ScaleX.Value;

                        // Output the information to the console
                        Console.WriteLine($"Page Index: {i}");
                        Console.WriteLine($"Orientation: {orientation}");
                        Console.WriteLine($"ScaleX: {scaleX}");
                        Console.WriteLine(new string('-', 30));
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }