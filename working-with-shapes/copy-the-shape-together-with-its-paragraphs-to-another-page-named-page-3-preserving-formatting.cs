using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the source Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // -------------------------------------------------
                // 1. Ensure the target page "Page-3" exists
                // -------------------------------------------------
                string targetPageName = "Page-3";
                Page targetPage = diagram.Pages.GetPage(targetPageName);

                if (targetPage == null)
                {
                    // Determine a new unique page ID
                    int maxId = 0;
                    foreach (Page p in diagram.Pages)
                    {
                        if (p.ID > maxId)
                            maxId = p.ID;
                    }

                    // Create and add the new page
                    targetPage = new Page(maxId + 1);
                    targetPage.Name = targetPageName;
                    diagram.Pages.Add(targetPage);
                }

                // -------------------------------------------------
                // 2. Locate the shape to copy (example: first shape on the first page)
                // -------------------------------------------------
                Page sourcePage = diagram.Pages[0]; // assuming at least one page exists
                if (sourcePage.Shapes.Count == 0)
                {
                    Console.WriteLine("No shapes found on the source page.");
                    return;
                }

                // Get the first shape as the source shape
                Shape sourceShape = sourcePage.Shapes.GetShape(0);

                // -------------------------------------------------
                // 3. Add a new shape on the target page using the same master and position
                // -------------------------------------------------
                // Retrieve master name (must exist)
                if (sourceShape.Master == null)
                {
                    Console.WriteLine("Source shape does not have an associated master.");
                    return;
                }

                string masterName = sourceShape.Master.Name;

                // Add shape on target page at the same PinX/PinY coordinates
                long newShapeId = targetPage.AddShape(
                    sourceShape.XForm.PinX.Value,
                    sourceShape.XForm.PinY.Value,
                    masterName);

                // Retrieve the newly added shape instance
                Shape newShape = targetPage.Shapes.GetShape(newShapeId);

                // -------------------------------------------------
                // 4. Copy formatting, text, and paragraphs from source to new shape
                // -------------------------------------------------
                // The Copy method copies most of the shape's properties, including paragraphs.
                sourceShape.Copy(newShape);

                // -------------------------------------------------
                // 5. Save the modified diagram
                // -------------------------------------------------
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Clean up
                diagram.Dispose();

                Console.WriteLine("Shape copied to Page-3 and diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }