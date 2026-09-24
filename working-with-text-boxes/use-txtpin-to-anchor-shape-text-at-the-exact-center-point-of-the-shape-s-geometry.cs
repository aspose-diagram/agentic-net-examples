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

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Anchor the text block to the shape's geometric center
                        // TxtPinX/Y represent the absolute position of the text block.
                        // Setting them equal to the shape's PinX/Y places the text at the shape's center.
                        shape.TextXForm.TxtPinX.Value = shape.XForm.PinX.Value;
                        shape.TextXForm.TxtPinY.Value = shape.XForm.PinY.Value;
                    }
                }

                // Save the modified diagram
                diagram.Save("output_centered.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }