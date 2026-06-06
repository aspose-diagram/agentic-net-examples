using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path for the output Visio file after orientation change
                string outputPath = "output.vsdx";

                // Load the diagram inside a using block to ensure proper disposal
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Retrieve the current orientation
                        PrintPageOrientationValue currentOrientation = page.PageSheet.PrintProps.PrintPageOrientation.Value;

                        // Log the previous orientation
                        Console.WriteLine($"Page ID {page.ID} ('{page.Name}') current orientation: {currentOrientation}");

                        // Determine the new orientation (toggle between Landscape and Portrait for demonstration)
                        PrintPageOrientationValue newOrientation = currentOrientation == PrintPageOrientationValue.Landscape
                            ? PrintPageOrientationValue.Portrait
                            : PrintPageOrientationValue.Landscape;

                        // Apply the new orientation
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = newOrientation;

                        // Log the change
                        Console.WriteLine($"Page ID {page.ID} orientation changed to: {newOrientation}");
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved to '{outputPath}'.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }