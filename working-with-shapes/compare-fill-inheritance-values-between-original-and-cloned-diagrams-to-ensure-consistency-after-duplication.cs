using System;
using System.IO;
using Aspose.Diagram;

class FillInheritanceComparer
{
    static void Main()
    {
        try
        {

            // Load the original diagram from file
            Diagram originalDiagram = new Diagram("original.vsdx");

            // Clone the diagram by saving to a memory stream and loading it back
            using (MemoryStream ms = new MemoryStream())
            {
                // Save the original diagram into the memory stream
                originalDiagram.Save(ms, SaveFileFormat.Vsdx);
                ms.Position = 0; // Reset stream position for reading

                // Load the cloned diagram from the memory stream
                Diagram clonedDiagram = new Diagram(ms);

                // Compare InheritFill values page by page and shape by shape
                for (int pageIndex = 0; pageIndex < originalDiagram.Pages.Count; pageIndex++)
                {
                    Page originalPage = originalDiagram.Pages[pageIndex];
                    Page clonedPage = clonedDiagram.Pages[pageIndex];

                    for (int shapeIndex = 0; shapeIndex < originalPage.Shapes.Count; shapeIndex++)
                    {
                        Shape originalShape = originalPage.Shapes[shapeIndex];
                        Shape clonedShape = clonedPage.Shapes[shapeIndex];

                        Fill originalFill = originalShape.InheritFill;
                        Fill clonedFill = clonedShape.InheritFill;

                        // Compare each relevant property
                        if (originalFill.FillForegnd != clonedFill.FillForegnd ||
                            originalFill.FillForegndTrans != clonedFill.FillForegndTrans ||
                            originalFill.FillBkgnd != clonedFill.FillBkgnd ||
                            originalFill.FillBkgndTrans != clonedFill.FillBkgndTrans ||
                            originalFill.FillPattern != clonedFill.FillPattern)
                        {
                            Console.WriteLine($"Mismatch found in Page {pageIndex + 1}, Shape {shapeIndex + 1}:");
                            Console.WriteLine($"  Original FillForegnd: {originalFill.FillForegnd}, Clone: {clonedFill.FillForegnd}");
                            Console.WriteLine($"  Original FillForegndTrans: {originalFill.FillForegndTrans}, Clone: {clonedFill.FillForegndTrans}");
                            Console.WriteLine($"  Original FillBkgnd: {originalFill.FillBkgnd}, Clone: {clonedFill.FillBkgnd}");
                            Console.WriteLine($"  Original FillBkgndTrans: {originalFill.FillBkgndTrans}, Clone: {clonedFill.FillBkgndTrans}");
                            Console.WriteLine($"  Original FillPattern: {originalFill.FillPattern}, Clone: {clonedFill.FillPattern}");
                        }
                    }
                }

                Console.WriteLine("Fill inheritance comparison completed.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
