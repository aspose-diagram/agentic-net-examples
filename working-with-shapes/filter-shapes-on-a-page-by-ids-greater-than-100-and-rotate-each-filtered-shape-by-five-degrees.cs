using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram from a file
                using (Diagram diagram = new Diagram("input.vsdx"))
                {
                    // Access the first page of the diagram
                    Page page = diagram.Pages[0];

                    // Iterate over all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Process only shapes whose ID is greater than 100
                        if (shape.ID > 100)
                        {
                            // Increase the shape's rotation angle by 5 degrees
                            shape.XForm.Angle.Value += 5;
                        }
                    }

                    // Save the updated diagram to a new file
                    diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }