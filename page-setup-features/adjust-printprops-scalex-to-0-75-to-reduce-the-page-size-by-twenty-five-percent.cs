using System;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path for the modified Visio file
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                try
                {
                    // Iterate through all pages and set ScaleX to 0.75 (75% of original size)
                    foreach (Page page in diagram.Pages)
                    {
                        // Access the PrintProps via the PageSheet and assign the scaling factor
                        page.PageSheet.PrintProps.ScaleX.Value = 0.75;
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }
                catch (Exception ex)
                {
                    // Simple error handling
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }
                finally
                {
                    // Ensure resources are released
                    diagram.Dispose();
                }

                Console.WriteLine("Print scaling applied and diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }