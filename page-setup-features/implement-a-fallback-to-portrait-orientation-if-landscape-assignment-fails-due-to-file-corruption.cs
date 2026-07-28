using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths to the source and destination diagram files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram from file
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Work with the first page (adjust index as needed)
                    Page page = diagram.Pages[0];

                    try
                    {
                        // Attempt to set the page orientation to Landscape
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                    }
                    catch (Exception ex)
                    {
                        // If setting Landscape fails (e.g., due to file corruption), log the error
                        Console.WriteLine($"Failed to set Landscape orientation: {ex.Message}");

                        // Fallback: try to set the orientation to Portrait
                        try
                        {
                            page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;
                            Console.WriteLine("Fallback to Portrait orientation applied.");
                        }
                        catch (Exception innerEx)
                        {
                            // If Portrait also fails, report and rethrow
                            Console.WriteLine($"Failed to set Portrait orientation as well: {innerEx.Message}");
                            throw;
                        }
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }