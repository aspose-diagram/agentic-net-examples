using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                Diagram diagram = new Diagram("input.vsdx");

                // Access the first page
                Page page = diagram.Pages[0];

                // Retrieve the first shape on the page
                Shape shape = null;
                foreach (Shape s in page.Shapes)
                {
                    shape = s;
                    break;
                }

                if (shape == null)
                {
                    Console.WriteLine("No shape found on the page.");
                    return;
                }

                // Custom width and height for the text block (in inches)
                double customWidth = 2.0;   // example width
                double customHeight = 1.0;  // example height

                // Set the text block dimensions
                shape.TextXForm.TxtWidth.Value = customWidth;
                shape.TextXForm.TxtHeight.Value = customHeight;

                // Calculate the bounding box of the text block
                double bboxWidth = shape.TextXForm.TxtWidth.Value;
                double bboxHeight = shape.TextXForm.TxtHeight.Value;

                // Output the results
                Console.WriteLine($"Text block bounding box width: {bboxWidth} inches");
                Console.WriteLine($"Text block bounding box height: {bboxHeight} inches");

                // Optionally save the diagram to verify changes
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }