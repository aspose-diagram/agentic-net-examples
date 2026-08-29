using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (use the provided load rule)
            Diagram diagram = new Diagram("{InputFilePath}");

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Create a timestamp string
                    string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    // Add the timestamp as a comment to the shape (uses the AddComment rule)
                    page.AddComment(shape.ID, timestamp);
                }
            }

            // Save the modified diagram (use the provided save rule)
            diagram.Save("{OutputFilePath}", SaveFileFormat.Vdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
