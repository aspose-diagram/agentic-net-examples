using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the existing diagram
            Diagram diagram = new Diagram(inputPath);

            // Determine the current maximum page ID
            int maxPageId = 0;
            foreach (Page page in diagram.Pages)
            {
                if (page.ID > maxPageId)
                    maxPageId = page.ID;
            }

            // Insert three blank placeholder pages at the beginning.
            // Add them in reverse order and move each to index 0 to preserve the desired sequence.
            for (int i = 3; i >= 1; i--)
            {
                // Create a new page with a unique ID
                Page placeholder = new Page(++maxPageId);
                placeholder.Name = $"Placeholder{i}";

                // Add the page to the diagram
                diagram.Pages.Add(placeholder);

                // Move the newly added page to the first position
                placeholder.MoveTo(0);
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
