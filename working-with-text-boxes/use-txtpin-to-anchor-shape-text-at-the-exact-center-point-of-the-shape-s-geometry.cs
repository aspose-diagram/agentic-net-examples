using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram
                // Replace "input.vsdx" with the path to your source file
                Diagram diagram = new Diagram("input.vsdx");

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Anchor the text block to the shape's geometric center
                        // TxtPinX/Y define the pin point of the text block (center of rotation)
                        // Setting them to the shape's PinX/Y aligns the text block with the shape's center
                        shape.TextXForm.TxtPinX.Value = shape.XForm.PinX.Value;
                        shape.TextXForm.TxtPinY.Value = shape.XForm.PinY.Value;

                        // Optional: ensure the local pin of the text block is at its center
                        // This makes the text block expand equally in all directions from the pin
                        shape.TextXForm.TxtLocPinX.Value = shape.TextXForm.TxtWidth.Value / 2.0;
                        shape.TextXForm.TxtLocPinY.Value = shape.TextXForm.TxtHeight.Value / 2.0;
                    }
                }

                // Save the modified diagram
                // Replace "output.vsdx" with the desired output path
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }