using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Check if auto‑expand (automatic drawing resize) is enabled
                        bool isAutoExpandEnabled = page.PageSheet.PageProps.DrawingResizeType.Value == DrawingResizeTypeValue.Automatically;

                        Console.WriteLine($"Page \"{page.Name}\" (ID: {page.ID}) auto‑expand enabled: {isAutoExpandEnabled}");

                        // If auto‑expand is enabled, disable it before applying custom size
                        if (isAutoExpandEnabled)
                        {
                            page.PageSheet.PageProps.DrawingResizeType.Value = DrawingResizeTypeValue.NotAutomatically;
                            Console.WriteLine($"Auto‑expand disabled for page \"{page.Name}\".");
                        }

                        // Apply custom page size (example: 8.5 x 11 inches)
                        page.PageSheet.PageProps.PageWidth.Value = 8.5;
                        page.PageSheet.PageProps.PageHeight.Value = 11.0;
                        Console.WriteLine($"Custom size applied to page \"{page.Name}\": 8.5\" x 11\".");
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved to \"{outputPath}\".");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }