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

            // Load an existing Visio diagram
            const string inputPath = "input.vsdx";
            const string outputPath = "output.vsdx";

            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there are at least two pages (source and target)
                Page sourcePage = diagram.Pages[0];

                // Create a target page if it does not exist
                Page targetPage;
                if (diagram.Pages.Count > 1)
                {
                    targetPage = diagram.Pages[1];
                }
                else
                {
                    // Determine a unique page ID
                    int maxPageId = 0;
                    foreach (Page p in diagram.Pages)
                    {
                        if (p.ID > maxPageId)
                            maxPageId = p.ID;
                    }

                    targetPage = new Page(maxPageId + 1);
                    diagram.Pages.Add(targetPage);
                }

                // Retrieve a shape to duplicate (first shape on the source page)
                // Adjust the index as needed for your specific diagram
                Shape sourceShape = sourcePage.Shapes.GetShape(0);
                if (sourceShape == null)
                    throw new Exception("No shape found on the source page to duplicate.");

                // Add a new shape on the target page using the same master as the source shape
                // Position it at the same coordinates as the source shape (you can offset if desired)
                long newShapeId = targetPage.AddShape(
                    sourceShape.XForm.PinX.Value,
                    sourceShape.XForm.PinY.Value,
                    sourceShape.Master.Name);

                // Retrieve the newly added shape instance
                Shape newShape = targetPage.Shapes.GetShape(newShapeId);
                if (newShape == null)
                    throw new Exception("Failed to retrieve the newly added shape.");

                // Copy all properties (geometry, text, formatting, etc.) from the source shape to the new shape
                sourceShape.Copy(newShape);

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Shape duplicated successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
