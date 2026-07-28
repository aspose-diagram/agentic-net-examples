using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and collect OLE shapes that embed Excel worksheets
                List<Shape> shapesToDelete = new List<Shape>();

                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Verify the shape is an OLE object
                        if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.ObjectData != null)
                        {
                            // Identify the embedded object type via the source file name
                            string sourceName = shape.ForeignData.ObjectSourceFullName;
                            if (!string.IsNullOrEmpty(sourceName))
                            {
                                string lowerSource = sourceName.ToLowerInvariant();
                                // Check for Excel file extensions
                                if (lowerSource.EndsWith(".xls") || lowerSource.EndsWith(".xlsx"))
                                {
                                    shapesToDelete.Add(shape);
                                }
                            }
                        }
                    }
                }

                // Remove the identified shapes from their respective pages
                foreach (Shape shape in shapesToDelete)
                {
                    // The shape's parent page can be accessed via the Shape.Page property
                    Page parentPage = shape.Page;
                    if (parentPage != null)
                    {
                        parentPage.Shapes.Remove(shape);
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