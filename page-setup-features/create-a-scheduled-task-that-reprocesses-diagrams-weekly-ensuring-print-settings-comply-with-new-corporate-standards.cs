using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Printing;

class Program
    {
        // Corporate print standards
        private const PrintPageOrientationValue DesiredOrientation = PrintPageOrientationValue.Landscape;
        private const double DesiredScale = 0.75; // 75%
        private const double MarginInInches = 0.5; // 0.5 inch margins

        static void Main(string[] args)
        {
            // Folder containing diagrams to process; can be passed as first argument
            string diagramsFolder = args.Length > 0 ? args[0] : @"C:\Diagrams";

            if (!Directory.Exists(diagramsFolder))
            {
                Console.WriteLine($"Folder not found: {diagramsFolder}");
                return;
            }

            // Process all Visio files (VSDX, VSD, VDX, etc.) in the folder
            string[] diagramFiles = Directory.GetFiles(diagramsFolder, "*.*", SearchOption.AllDirectories);
            foreach (string filePath in diagramFiles)
            {
                // Filter supported extensions based on SaveFileFormat enum
                string ext = Path.GetExtension(filePath).ToLowerInvariant();
                if (ext == ".vsdx" || ext == ".vsd" || ext == ".vdx")
                {
                    try
                    {
                        ProcessDiagram(filePath);
                        Console.WriteLine($"Processed: {filePath}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing {filePath}: {ex.Message}");
                    }
                }
            }

            Console.WriteLine("Weekly diagram reprocessing completed.");
        }

        private static void ProcessDiagram(string filePath)
        {
            // Load the diagram
            Diagram diagram = new Diagram(filePath);

            // Iterate through each page and apply corporate print settings
            foreach (Page page in diagram.Pages)
            {
                // Access the print properties for the page
                PrintProps printProps = page.PageSheet.PrintProps;

                // Set orientation
                printProps.PrintPageOrientation.Value = DesiredOrientation;

                // Set scaling factors
                printProps.ScaleX.Value = DesiredScale;
                printProps.ScaleY.Value = DesiredScale;

                // Enable fit-to-sheet (print on a single page)
                printProps.OnPage.Value = BOOL.True;
                printProps.PagesX.Value = 1;
                printProps.PagesY.Value = 1;

                // Set uniform margins (values are in inches)
                printProps.PageTopMargin.Value = MarginInInches;
                printProps.PageBottomMargin.Value = MarginInInches;
                printProps.PageLeftMargin.Value = MarginInInches;
                printProps.PageRightMargin.Value = MarginInInches;
            }

            // Save the updated diagram back to the original file
            diagram.Save(filePath, SaveFileFormat.Vsdx);
        }
    }