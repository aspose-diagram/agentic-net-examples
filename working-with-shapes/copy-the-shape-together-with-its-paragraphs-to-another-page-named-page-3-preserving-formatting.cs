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
                Diagram diagram = new Diagram("input.vsdx");

                // -------------------------------------------------
                // 1. Locate the shape to copy (first shape on first page)
                // -------------------------------------------------
                Page sourcePage = diagram.Pages[0];
                Shape sourceShape = null;
                foreach (Shape shp in sourcePage.Shapes)
                {
                    sourceShape = shp;
                    break; // take the first shape found
                }

                if (sourceShape == null)
                {
                    throw new Exception("No shape found on the source page.");
                }

                // -------------------------------------------------
                // 2. Ensure a target page named "Page-3" exists
                // -------------------------------------------------
                Page targetPage = diagram.Pages.GetPage("Page-3");
                if (targetPage == null)
                {
                    // Determine a new unique page ID
                    int maxId = 0;
                    foreach (Page pg in diagram.Pages)
                    {
                        if (pg.ID > maxId)
                            maxId = pg.ID;
                    }

                    // Create and add the new page
                    targetPage = new Page();
                    targetPage.ID = maxId + 1;
                    targetPage.Name = "Page-3";
                    diagram.Pages.Add(targetPage);
                }

                // -------------------------------------------------
                // 3. Add a new shape on the target page using the same master as the source shape
                // -------------------------------------------------
                string masterName = sourceShape.Master?.Name ?? throw new Exception("Source shape has no master.");
                long newShapeId = targetPage.AddShape(
                    sourceShape.XForm.PinX.Value,   // preserve X position
                    sourceShape.XForm.PinY.Value,   // preserve Y position
                    masterName);                    // master name

                // Retrieve the newly added shape instance
                Shape newShape = targetPage.Shapes.GetShape(newShapeId);

                // -------------------------------------------------
                // 4. Copy all shape data (including text, paragraphs, formatting) from source to target
                // -------------------------------------------------
                sourceShape.Copy(newShape);

                // -------------------------------------------------
                // 5. Save the modified diagram
                // -------------------------------------------------
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }