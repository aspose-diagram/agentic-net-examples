using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram from a file.
                // Replace "input.vsdx" with the actual path to your diagram.
                using (Diagram diagram = new Diagram("input.vsdx"))
                {
                    // Iterate through each page in the diagram.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through each shape on the current page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Identify connector shapes: they are 1‑D shapes (OneD == true).
                            if (shape.OneD)
                            {
                                // Mark the connector shape as deleted.
                                shape.Del = BOOL.True;
                            }
                        }
                    }

                    // Save the modified diagram to a new file.
                    // The SaveFileFormat enum uses PascalCase.
                    diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }