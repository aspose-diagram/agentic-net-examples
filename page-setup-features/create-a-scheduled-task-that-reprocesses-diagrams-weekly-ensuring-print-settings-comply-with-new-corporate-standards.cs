using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Printing;

class Program
    {
        // Folder containing diagrams to process
        private static readonly string InputFolder = @"C:\Diagrams\Input";
        // Folder to save processed diagrams
        private static readonly string OutputFolder = @"C:\Diagrams\Output";

        // Corporate print standards
        private const double ScaleFactor = 0.75;          // 75% scaling
        private const double MarginInches = 0.5;          // 0.5 inch margins
        private const PrintPageOrientationValue Orientation = PrintPageOrientationValue.Landscape;

        static void Main(string[] args)
        {
            // Ensure output folder exists
            Directory.CreateDirectory(OutputFolder);

            // Run immediately and then every 7 days
            Timer timer = new Timer(_ => RunBatch(), null, TimeSpan.Zero, TimeSpan.FromDays(7));

            // Keep the application running
            using (ManualResetEvent waitHandle = new ManualResetEvent(false))
            {
                waitHandle.WaitOne();
            }
        }

        private static void RunBatch()
        {
            try
            {
                string[] diagramFiles = Directory.GetFiles(InputFolder, "*.vsdx", SearchOption.TopDirectoryOnly);
                foreach (string filePath in diagramFiles)
                {
                    ProcessDiagram(filePath);
                }

                Console.WriteLine($"Batch processing completed at {DateTime.Now}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during batch processing: {ex.Message}");
            }
        }

        private static void ProcessDiagram(string filePath)
        {
            // Load the diagram
            Diagram diagram = new Diagram(filePath);

            // Update print settings for each page
            foreach (Page page in diagram.Pages)
            {
                // Access print properties
                PrintProps printProps = page.PageSheet.PrintProps;

                // Set orientation
                printProps.PrintPageOrientation.Value = Orientation;

                // Set scaling
                printProps.ScaleX.Value = ScaleFactor;
                printProps.ScaleY.Value = ScaleFactor;

                // Enable fit to sheet (print on a single sheet)
                printProps.OnPage.Value = BOOL.True;
                printProps.PagesX.Value = 1;
                printProps.PagesY.Value = 1;

                // Set margins (values are in inches)
                printProps.PageTopMargin.Value = MarginInches;
                printProps.PageBottomMargin.Value = MarginInches;
                printProps.PageLeftMargin.Value = MarginInches;
                printProps.PageRightMargin.Value = MarginInches;
            }

            // Determine output path (overwrite original or save to output folder)
            string fileName = Path.GetFileName(filePath);
            string outputPath = Path.Combine(OutputFolder, fileName);

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Processed diagram: {fileName}");
        }
    }