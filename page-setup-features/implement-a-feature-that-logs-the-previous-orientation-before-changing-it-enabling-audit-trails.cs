using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through all pages and change orientation to Landscape,
                    // while logging the previous orientation for audit purposes.
                    foreach (Page page in diagram.Pages)
                    {
                        // Retrieve the current orientation
                        PrintPageOrientationValue previousOrientation = page.PageSheet.PrintProps.PrintPageOrientation.Value;

                        // Log the previous orientation
                        Console.WriteLine($"Page ID {page.ID} ('{page.NameU}') previous orientation: {previousOrientation}");

                        // Change orientation to Landscape (example target orientation)
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                        // Log the new orientation
                        Console.WriteLine($"Page ID {page.ID} ('{page.NameU}') new orientation set to: {PrintPageOrientationValue.Landscape}");
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