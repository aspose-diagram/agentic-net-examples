using System;
using Aspose.Diagram;

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
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through all pages by index
                    for (int i = 0; i < diagram.Pages.Count; i++)
                    {
                        // Retrieve the page (0‑based index)
                        Page page = diagram.Pages[i];

                        // Determine if the page number (1‑based) is odd
                        bool isOdd = ((i + 1) % 2) == 1;

                        if (isOdd)
                        {
                            // Apply a custom horizontal scale (e.g., 50% of original size)
                            page.PageSheet.PrintProps.ScaleX.Value = 0.5;
                        }
                        else
                        {
                            // Ensure even pages retain the default scale (100%)
                            page.PageSheet.PrintProps.ScaleX.Value = 1.0;
                        }
                    }

                    // Save the modified diagram in VSDX format
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Diagram processing completed successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }