using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Prompt user for desired orientation
            Console.WriteLine("Select page orientation:");
            Console.WriteLine("P - Portrait");
            Console.WriteLine("L - Landscape");
            Console.Write("Enter choice (P/L): ");
            string input = Console.ReadLine()?.Trim().ToUpperInvariant();

            // Determine the orientation value
            PrintPageOrientationValue orientation;
            if (input == "L")
            {
                orientation = PrintPageOrientationValue.Landscape;
            }
            else if (input == "P")
            {
                orientation = PrintPageOrientationValue.Portrait;
            }
            else
            {
                Console.WriteLine("Invalid input. Defaulting to Portrait.");
                orientation = PrintPageOrientationValue.Portrait;
            }

            // Create a new diagram (contains a default page)
            using (Diagram diagram = new Diagram())
            {
                // Apply the chosen orientation to each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = orientation;
                }

                // Save the diagram to a VSDX file
                string outputPath = "OrientationDemo.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}' with {orientation} orientation.");
            }
        }
    }