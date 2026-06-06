using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Prompt user for desired orientation
            Console.WriteLine("Enter orientation (P for Portrait, L for Landscape):");
            string input = Console.ReadLine();

            // Determine orientation based on input
            PrintPageOrientationValue orientation = PrintPageOrientationValue.Portrait;
            if (!string.IsNullOrEmpty(input) && input.Equals("L", StringComparison.OrdinalIgnoreCase))
            {
                orientation = PrintPageOrientationValue.Landscape;
            }
            else if (!string.IsNullOrEmpty(input) && input.Equals("P", StringComparison.OrdinalIgnoreCase))
            {
                orientation = PrintPageOrientationValue.Portrait;
            }
            else
            {
                Console.WriteLine("Invalid input. Defaulting to Portrait orientation.");
            }

            // Create a new diagram (contains a default page)
            using (Diagram diagram = new Diagram())
            {
                // Apply the chosen orientation to every page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = orientation;
                }

                // Save the diagram to a VSDX file
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}' with {orientation} orientation.");
            }
        }
    }