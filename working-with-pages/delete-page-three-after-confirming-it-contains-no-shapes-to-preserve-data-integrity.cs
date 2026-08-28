using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vdx");

            // Verify that the document has at least three pages
            if (diagram.Pages.Count >= 3)
            {
                // Access the third page (zero‑based index)
                Page pageThree = diagram.Pages[2];

                // Confirm the page contains no shapes
                if (pageThree.Shapes.Count == 0)
                {
                    // Remove the empty page from the document
                    diagram.Pages.Remove(pageThree);

                    // Release unmanaged resources held by the page
                    pageThree.Dispose();
                }
            }

            // Save the updated diagram
            diagram.Save("output.vdx", SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
