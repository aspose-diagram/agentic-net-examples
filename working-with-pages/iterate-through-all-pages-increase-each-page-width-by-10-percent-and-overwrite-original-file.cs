using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Expect the path to the Visio file as the first argument.
                if (args.Length == 0)
                {
                    Console.WriteLine("Usage: DiagramPageResize <path-to-visio-file>");
                    return;
                }

                string filePath = args[0];

                // Load the diagram, modify page widths, and overwrite the original file.
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Iterate through each page in the diagram.
                    foreach (Page page in diagram.Pages)
                    {
                        // Current width in inches.
                        double currentWidth = page.PageSheet.PageProps.PageWidth.Value;

                        // Increase width by 10 percent.
                        page.PageSheet.PageProps.PageWidth.Value = currentWidth * 1.10;
                    }

                    // Save back to the same file using the VSDX format.
                    diagram.Save(filePath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Page widths increased by 10% and file saved.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }