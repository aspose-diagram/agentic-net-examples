using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                using (Diagram diagram = new Diagram("input.vsdx"))
                {
                    // Iterate through all pages in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Apply a 90‑degree rotation to the shape's text.
                            // TxtAngle expects radians, so convert 90 degrees to radians.
                            shape.TextXForm.TxtAngle.Value = Math.PI / 2;

                            // Reposition the text block to the left side of the shape.
                            // TxtLocPinX is set to the text block width, and TxtPinX is set to 0.
                            shape.TextXForm.TxtLocPinX.Value = shape.TextXForm.TxtWidth.Value;
                            shape.TextXForm.TxtPinX.Value = 0;
                        }
                    }

                    // Save the modified diagram to a new file.
                    diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }