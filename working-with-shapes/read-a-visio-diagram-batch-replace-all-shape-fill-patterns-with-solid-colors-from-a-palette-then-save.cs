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

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Define a palette of solid colors (hex strings)
                string[] palette = new string[]
                {
                    "#FF0000", // Red
                    "#00FF00", // Green
                    "#0000FF", // Blue
                    "#FFFF00", // Yellow
                    "#FF00FF", // Magenta
                    "#00FFFF"  // Cyan
                };

                try
                {
                    // Load the Visio diagram
                    Diagram diagram = new Diagram(inputPath);

                    // Iterate through all pages and shapes
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip shapes that are marked as deleted
                            if (shape.Del == BOOL.True)
                                continue;

                            // Set fill pattern to solid (value 1)
                            shape.Fill.FillPattern.Value = 1;

                            // Choose a color from the palette based on shape ID
                            int colorIndex = (int)(shape.ID % palette.Length);
                            shape.Fill.FillForegnd.Value = palette[colorIndex];
                        }
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }
                catch (Exception ex)
                {
                    // Simple error handling
                    Console.WriteLine("Error: " + ex.Message);
                    throw;
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }