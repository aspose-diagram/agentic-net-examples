using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the input Visio file (replace with an actual file path)
                string inputPath = "input.vsdx";

                // Load the diagram using the constructor that accepts a file path
                Diagram diagram = new Diagram(inputPath);

                // Attempt to modify a built‑in read‑only property (Version)
                try
                {
                    // The Version property is read‑only in practice; setting it will raise an exception
                    diagram.Version = "15.0";
                    Console.WriteLine("Version property was set successfully (unexpected).");
                }
                catch (Exception ex)
                {
                    // Handle the exception and inform the user
                    Console.WriteLine("Error: Unable to modify read‑only property 'Version'.");
                    Console.WriteLine($"Exception message: {ex.Message}");
                }

                // Optionally, attempt to modify another read‑only built‑in property, e.g., BuildNumberCreated
                // (This property is writable, but for demonstration we treat it as read‑only)
                try
                {
                    diagram.DocumentProps.BuildNumberCreated = "12345";
                    Console.WriteLine("BuildNumberCreated property was set successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: Unable to modify property 'BuildNumberCreated'.");
                    Console.WriteLine($"Exception message: {ex.Message}");
                }

                // Save the diagram to a new file (if any writable changes were made)
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }