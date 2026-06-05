using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Collect shapes that need to be removed to avoid modifying the collection during iteration
                    List<Shape> shapesToRemove = new List<Shape>();

                    // Examine each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape is a foreign (OLE) object and has foreign data
                        if (shape.Type == TypeValue.Foreign && shape.ForeignData != null)
                        {
                            // Verify the foreign type is an OLE object
                            if (shape.ForeignData.ForeignType == ForeignType.Object)
                            {
                                // Check that the OLE object actually contains binary data
                                if (shape.ForeignData.ObjectData != null && shape.ForeignData.ObjectData.Length > 0)
                                {
                                    // Identify Excel OLE objects by examining the source name (file extension or application name)
                                    string sourceName = shape.ForeignData.ObjectSourceFullName ?? string.Empty;
                                    string sourceNameLower = sourceName.ToLowerInvariant();

                                    bool isExcel = sourceNameLower.EndsWith(".xls") ||
                                                   sourceNameLower.EndsWith(".xlsx") ||
                                                   sourceNameLower.Contains("excel");

                                    if (isExcel)
                                    {
                                        // Mark this shape for removal
                                        shapesToRemove.Add(shape);
                                    }
                                }
                            }
                        }
                    }

                    // Remove the identified OLE shapes from the page
                    foreach (Shape shape in shapesToRemove)
                    {
                        page.Shapes.Remove(shape);
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