using System;
using System.IO;
using System.Diagnostics;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string inputPath = "input.vsdx";

                // Output SWF file path
                string outputPath = "output.swf";

                // Verify input file exists
                if (!File.Exists(inputPath))
                    throw new FileNotFoundException($"Input file not found: {inputPath}");

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Configure SWF save options (default settings are sufficient for basic conversion)
                SWFSaveOptions swfOptions = new SWFSaveOptions();

                // Save the diagram as SWF
                diagram.Save(outputPath, swfOptions);

                // Verify that the SWF file was created
                if (!File.Exists(outputPath))
                    throw new Exception("SWF conversion failed: output file was not created.");

                Console.WriteLine("SWF file saved successfully at: " + Path.GetFullPath(outputPath));

                // Attempt to open the SWF file with the default associated application (e.g., web browser)
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = outputPath,
                        UseShellExecute = true
                    };
                    Process.Start(startInfo);
                    Console.WriteLine("Opened SWF file for playback verification.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unable to open SWF file automatically. Please open it manually to verify playback.");
                    Console.WriteLine("Error: " + ex.Message);
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }