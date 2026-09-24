using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Paths to the two diagram versions to compare
            string diagramPath1 = "DiagramVersion1.vsdx";
            string diagramPath2 = "DiagramVersion2.vsdx";

            // Load both diagrams
            Diagram diagram1 = new Diagram(diagramPath1);
            Diagram diagram2 = new Diagram(diagramPath2);

            // Iterate through pages (assuming same page count and IDs)
            foreach (Page page1 in diagram1.Pages)
            {
                // Find the corresponding page in the second diagram by ID
                Page page2 = diagram2.Pages.GetPage(page1.ID);
                if (page2 == null)
                {
                    Console.WriteLine($"Page with ID {page1.ID} not found in second diagram.");
                    continue;
                }

                // Iterate through shapes on the first page
                foreach (Shape shape1 in page1.Shapes)
                {
                    // Process only connector shapes (1‑D shapes)
                    if (!shape1.OneD)
                        continue;

                    // Retrieve jump style and code from the first diagram
                    var jumpStyle1 = shape1.Layout.ConLineJumpStyle.Value;
                    var jumpCode1 = shape1.Layout.ConLineJumpCode.Value;

                    // Find the matching connector in the second diagram by shape ID
                    Shape shape2 = page2.Shapes.GetShape(shape1.ID);
                    if (shape2 == null)
                    {
                        Console.WriteLine($"Connector ID {shape1.ID} missing in second diagram (Page ID {page1.ID}).");
                        continue;
                    }

                    // Retrieve jump style and code from the second diagram
                    var jumpStyle2 = shape2.Layout.ConLineJumpStyle.Value;
                    var jumpCode2 = shape2.Layout.ConLineJumpCode.Value;

                    // Compare and report differences
                    if (jumpStyle1 != jumpStyle2 || jumpCode1 != jumpCode2)
                    {
                        Console.WriteLine($"Connector ID {shape1.ID} on Page ID {page1.ID} differs:");
                        Console.WriteLine($"  Jump Style - Diagram1: {jumpStyle1}, Diagram2: {jumpStyle2}");
                        Console.WriteLine($"  Jump Code  - Diagram1: {jumpCode1}, Diagram2: {jumpCode2}");
                    }
                }
            }

            // Optional: indicate completion
            Console.WriteLine("Comparison completed.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
