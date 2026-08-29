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
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Access the first page (adjust index if needed)
                Page page = diagram.Pages[0];

                // Find the first group shape on the page
                Shape groupShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Type == TypeValue.Group)
                    {
                        groupShape = shape;
                        break;
                    }
                }

                if (groupShape == null)
                {
                    throw new Exception("No group shape found on the page.");
                }

                // Retrieve a sub‑shape from the group.
                // Here we take the first sub‑shape encountered.
                Shape subShape = null;
                foreach (Shape inner in groupShape.Shapes)
                {
                    subShape = inner;
                    break;
                }

                if (subShape == null)
                {
                    throw new Exception("The group shape does not contain any sub‑shapes.");
                }

                // Adjust the width of the sub‑shape to 5.0 inches using SetWidth (double precision)
                subShape.SetWidth(5.0);

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }