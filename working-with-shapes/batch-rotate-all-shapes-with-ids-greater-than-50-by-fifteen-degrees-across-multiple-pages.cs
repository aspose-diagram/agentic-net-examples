using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load the Visio diagram from a file.
                // Replace "input.vsdx" with the actual path to your diagram.
                using (Diagram diagram = new Diagram("input.vsdx"))
                {
                    // Iterate through all pages in the diagram.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the current page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Shape IDs are of type long. Rotate only shapes with ID > 50.
                            if (shape.ID > 50)
                            {
                                // Add 15 degrees to the existing rotation angle.
                                // The Angle property is used for rotation.
                                shape.XForm.Angle.Value = shape.XForm.Angle.Value + 15;
                            }
                        }
                    }

                    // Save the modified diagram.
                    // Replace "output.vsdx" with the desired output path.
                    diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }