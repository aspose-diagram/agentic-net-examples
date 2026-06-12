using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source and target Visio files
                string sourcePath = "source.vsdx";
                string targetPath = "target.vsdx";
                string outputPath = "merged.vsdx";

                // Load the source and target diagrams
                Diagram sourceDiagram = new Diagram(sourcePath);
                Diagram targetDiagram = new Diagram(targetPath);

                // Assume both diagrams have the same number of pages and corresponding pages are in the same order
                int pageCount = sourceDiagram.Pages.Count;
                for (int i = 0; i < pageCount; i++)
                {
                    // Retrieve pages by index
                    Page srcPage = sourceDiagram.Pages[i];
                    Page tgtPage = targetDiagram.Pages[i];

                    // Iterate all shapes on the source page
                    foreach (Shape srcShape in srcPage.Shapes)
                    {
                        // Find the matching shape on the target page by universal name (NameU)
                        Shape tgtShape = FindShapeByNameU(tgtPage, srcShape.NameU);
                        if (tgtShape == null)
                        {
                            // No matching shape found; skip to next shape
                            continue;
                        }

                        // Clear existing user-defined cells on the target shape
                        tgtShape.Users.Clear();

                        // Copy each user-defined cell from source shape to target shape
                        foreach (User srcUser in srcShape.Users)
                        {
                            User tgtUser = new User();
                            tgtUser.Name = srcUser.Name;
                            tgtUser.NameU = srcUser.NameU;
                            tgtUser.Value.Val = srcUser.Value.Val;
                            tgtUser.Prompt.Value = srcUser.Prompt.Value;
                            tgtShape.Users.Add(tgtUser);
                        }
                    }
                }

                // Save the updated target diagram to the output file
                targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Helper method to locate a shape on a page by its universal name (NameU)
        private static Shape FindShapeByNameU(Page page, string nameU)
        {
            foreach (Shape shape in page.Shapes)
            {
                if (shape.NameU == nameU)
                {
                    return shape;
                }
            }
            return null;
        }
    }