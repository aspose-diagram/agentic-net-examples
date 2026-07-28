using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output Visio file path (protected)
                string outputPath = "output_protected.vsdx";

                try
                {
                    // Load the diagram from file
                    using (Diagram diagram = new Diagram(inputPath))
                    {
                        // Iterate through all pages and shapes
                        foreach (Page page in diagram.Pages)
                        {
                            foreach (Shape shape in page.Shapes)
                            {
                                // Prevent deletion of the shape
                                shape.Protection.LockDelete.Value = BOOL.True;
                            }
                        }

                        // Save the modified diagram
                        diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    }

                    Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                    throw;
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }