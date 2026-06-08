using System;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths for input Visio file and output PDF
                string inputPath = "input.vsdx";
                string outputPath = "output.pdf";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Create and assign an InterruptMonitor to the diagram
                InterruptMonitor monitor = new InterruptMonitor();
                diagram.InterruptMonitor = monitor;

                // Start a background thread that will interrupt after 5 seconds
                Thread interruptThread = new Thread(() =>
                {
                    Thread.Sleep(5000); // 5 seconds
                    monitor.Interrupt();
                });
                interruptThread.Start();

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    DefaultFont = "Arial"
                };

                try
                {
                    // Save the diagram as PDF
                    diagram.Save(outputPath, pdfOptions);
                    Console.WriteLine("PDF saved successfully.");
                }
                catch (Exception ex)
                {
                    // Handle interruption or other errors
                    Console.WriteLine("Operation was interrupted or failed: " + ex.Message);
                }
                finally
                {
                    // Ensure the interrupt thread has finished
                    interruptThread.Join();
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }