using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output Visio file path
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // List to hold shapes that need to be removed
                List<Shape> shapesToRemove = new List<Shape>();

                // Iterate through all pages
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Verify the shape is a foreign (OLE) object and has foreign data
                        if (shape.Type == TypeValue.Foreign && shape.ForeignData != null)
                        {
                            // Get the source name of the embedded object
                            string sourceName = shape.ForeignData.ObjectSourceFullName;

                            // Check if the embedded object is an Excel worksheet (xls or xlsx)
                            if (!string.IsNullOrEmpty(sourceName) &&
                                (sourceName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase) ||
                                 sourceName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase)))
                            {
                                // Mark this shape for removal
                                shapesToRemove.Add(shape);
                            }
                        }
                    }

                    // Remove the marked shapes from the current page
                    foreach (Aspose.Diagram.Shape s in shapesToRemove)
                    {
                        page.Shapes.Remove(s);
                    }
                    shapesToRemove.Clear();
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }