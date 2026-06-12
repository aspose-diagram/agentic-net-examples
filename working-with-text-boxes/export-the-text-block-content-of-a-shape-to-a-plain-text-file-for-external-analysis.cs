using System;
using System.IO;
using Aspose.Diagram;

class ExportShapeTexts
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Prepare the output text file
            using (StreamWriter writer = new StreamWriter("shape_texts.txt"))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Get the plain text of the shape
                        string pureText = shape.GetPureText();

                        // Write only if the shape contains text
                        if (!string.IsNullOrWhiteSpace(pureText))
                        {
                            writer.WriteLine($"Page: {page.Name}, Shape ID: {shape.ID}");
                            writer.WriteLine(pureText);
                            writer.WriteLine(new string('-', 40));
                        }
                    }
                }
            }

            // Optional: inform the user
            Console.WriteLine("Shape texts have been exported to shape_texts.txt");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
