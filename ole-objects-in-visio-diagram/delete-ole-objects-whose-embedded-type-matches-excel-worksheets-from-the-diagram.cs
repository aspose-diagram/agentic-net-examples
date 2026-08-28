using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio diagram
                string inputPath = "input.vsdx";
                // Path for the resulting diagram after OLE removal
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Collect shapes that need to be removed to avoid modifying the collection during iteration
                    List<Shape> shapesToRemove = new List<Shape>();

                    // Examine each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape is a foreign (OLE) object and has embedded data
                        if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.ObjectData != null)
                        {
                            // The source file name (or extension) indicates the embedded object's type
                            string sourceName = shape.ForeignData.ObjectSourceFullName;

                            if (!string.IsNullOrEmpty(sourceName) &&
                                (sourceName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase) ||
                                 sourceName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) ||
                                 sourceName.EndsWith(".xlsm", StringComparison.OrdinalIgnoreCase)))
                            {
                                // This OLE object is an Excel worksheet – mark it for removal
                                shapesToRemove.Add(shape);
                            }
                        }
                    }

                    // Remove the identified OLE shapes from the page
                    foreach (Shape oleShape in shapesToRemove)
                    {
                        page.Shapes.Remove(oleShape);
                    }
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