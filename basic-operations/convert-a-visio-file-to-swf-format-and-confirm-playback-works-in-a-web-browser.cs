using System;
using System.Diagnostics;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (change as needed)
                string inputPath = "input.vsdx";

                // Output SWF file path
                string outputPath = "output.swf";

                try
                {
                    // Load the Visio diagram
                    Diagram diagram = new Diagram(inputPath);

                    // Configure SWF save options (optional: set default font)
                    SWFSaveOptions swfOptions = new SWFSaveOptions();
                    swfOptions.DefaultFont = "Arial";

                    // Save the diagram as SWF
                    diagram.Save(outputPath, swfOptions);
                    Console.WriteLine($"SWF file saved to: {outputPath}");

                    // Attempt to open the SWF file in the default web browser
                    ProcessStartInfo startInfo = new ProcessStartInfo(outputPath)
                    {
                        UseShellExecute = true
                    };
                    Process.Start(startInfo);
                    Console.WriteLine("Opened SWF file in the default browser for playback verification.");
                }
                catch (Exception ex)
                {
                    // Report any errors
                    Console.WriteLine("An error occurred during conversion:");
                    Console.WriteLine(ex.Message);
                    throw;
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }