using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new blank diagram
        using (Diagram diagram = new Diagram())
        {
            // Access the first (default) page
            Page page = diagram.Pages[0];

            // Ask the user for the desired orientation
            Console.WriteLine("Select page orientation: P for Portrait, L for Landscape");
            string input = Console.ReadLine();

            // Apply the chosen orientation
            if (!string.IsNullOrEmpty(input) && input.Equals("L", StringComparison.OrdinalIgnoreCase))
            {
                page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                Console.WriteLine("Orientation set to Landscape.");
            }
            else
            {
                page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;
                Console.WriteLine("Orientation set to Portrait.");
            }

            // Save the diagram to a PDF file
            string outputPath = "OrientationDemo.pdf";
            diagram.Save(outputPath, SaveFileFormat.Pdf);
            Console.WriteLine($"Diagram saved to {outputPath}");
        }
    }
}
