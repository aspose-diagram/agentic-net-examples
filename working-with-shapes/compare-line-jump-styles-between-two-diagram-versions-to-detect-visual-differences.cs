using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two file paths: first diagram version, second diagram version
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramComparison <DiagramPath1> <DiagramPath2>");
                return;
            }

            string diagramPath1 = args[0];
            string diagramPath2 = args[1];

            try
            {
                // Load the two diagram versions
                Diagram diagram1 = new Diagram(diagramPath1);
                Diagram diagram2 = new Diagram(diagramPath2);

                // Simple validation: ensure both diagrams have the same number of pages
                if (diagram1.Pages.Count != diagram2.Pages.Count)
                {
                    Console.WriteLine("The diagrams have a different number of pages.");
                    return;
                }

                // Iterate through each page
                for (int pageIndex = 0; pageIndex < diagram1.Pages.Count; pageIndex++)
                {
                    Page page1 = diagram1.Pages[pageIndex];
                    Page page2 = diagram2.Pages[pageIndex];

                    // Iterate through shapes on the page and focus on connectors (OneD == true)
                    foreach (Shape shape1 in page1.Shapes)
                    {
                        if (!shape1.OneD) continue; // Skip non‑connector shapes

                        // Attempt to find the corresponding shape in the second diagram by ID
                        Shape shape2 = null;
                        try
                        {
                            shape2 = page2.Shapes.GetShape(shape1.ID);
                        }
                        catch
                        {
                            // Shape with this ID does not exist in the second diagram
                            Console.WriteLine($"Connector ID {shape1.ID} exists in diagram 1 but not in diagram 2 (page {pageIndex}).");
                            continue;
                        }

                        // Retrieve line‑jump style values
                        var jumpStyle1 = shape1.Layout.ConLineJumpStyle.Value;
                        var jumpStyle2 = shape2.Layout.ConLineJumpStyle.Value;

                        // Compare the styles
                        if (jumpStyle1 != jumpStyle2)
                        {
                            Console.WriteLine($"Page {pageIndex}, Connector ID {shape1.ID}:");
                            Console.WriteLine($"  Diagram 1 Jump Style = {jumpStyle1}");
                            Console.WriteLine($"  Diagram 2 Jump Style = {jumpStyle2}");
                        }

                        // Optionally compare the jump code (routing behavior)
                        var jumpCode1 = shape1.Layout.ConLineJumpCode.Value;
                        var jumpCode2 = shape2.Layout.ConLineJumpCode.Value;

                        if (jumpCode1 != jumpCode2)
                        {
                            Console.WriteLine($"Page {pageIndex}, Connector ID {shape1.ID}:");
                            Console.WriteLine($"  Diagram 1 Jump Code = {jumpCode1}");
                            Console.WriteLine($"  Diagram 2 Jump Code = {jumpCode2}");
                        }
                    }
                }

                Console.WriteLine("Comparison completed.");
            }
            catch (Exception ex)
            {
                // Any unexpected error is reported and re‑thrown to signal failure
                Console.WriteLine($"Error during comparison: {ex.Message}");
                throw;
            }
        }
    }