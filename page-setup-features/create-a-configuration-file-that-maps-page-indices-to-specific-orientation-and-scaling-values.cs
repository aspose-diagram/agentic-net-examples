using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Define mapping: page index -> (orientation, scaleX, scaleY)
                var pageSettings = new Dictionary<int, (PrintPageOrientationValue orientation, double scaleX, double scaleY)>
                {
                    // Page 0: Landscape orientation, 75% scaling
                    { 0, (PrintPageOrientationValue.Landscape, 0.75, 0.75) },

                    // Page 1: Portrait orientation, 100% scaling
                    { 1, (PrintPageOrientationValue.Portrait, 1.0, 1.0) },

                    // Page 2: Landscape orientation, 50% scaling
                    { 2, (PrintPageOrientationValue.Landscape, 0.5, 0.5) }
                };

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate over pages and apply settings if a mapping exists
                    foreach (Page page in diagram.Pages)
                    {
                        int pageIndex = page.ID; // Page.ID corresponds to its index in the collection

                        if (pageSettings.TryGetValue(pageIndex, out var settings))
                        {
                            // Set print orientation
                            page.PageSheet.PrintProps.PrintPageOrientation.Value = settings.orientation;

                            // Set scaling factors
                            page.PageSheet.PrintProps.ScaleX.Value = settings.scaleX;
                            page.PageSheet.PrintProps.ScaleY.Value = settings.scaleY;
                        }
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Diagram processing completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }