using System;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (can be passed as command‑line arguments)
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
                string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

                try
                {
                    // Load the Visio diagram
                    using (Diagram diagram = new Diagram(inputPath))
                    {
                        // Iterate through all pages in the diagram
                        foreach (Page page in diagram.Pages)
                        {
                            // Identify background pages (Background flag is BOOL.True)
                            if (page.Background == BOOL.True)
                            {
                                // Set the print orientation of the background page to Portrait
                                page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;
                            }
                        }

                        // Save the modified diagram (preserve original format)
                        diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    }

                    Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
                }
                catch (Exception ex)
                {
                    // Report any errors that occur during processing
                    Console.WriteLine("An error occurred: " + ex.Message);
                    throw;
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }