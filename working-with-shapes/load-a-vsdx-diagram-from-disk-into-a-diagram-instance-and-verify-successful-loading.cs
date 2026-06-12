using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the VSDX file. Adjust as needed or pass as a command‑line argument.
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Load the diagram from the specified file.
                Diagram diagram = new Diagram(inputPath);

                // Verify that the diagram was loaded successfully.
                if (diagram == null)
                {
                    throw new Exception("Failed to create Diagram instance.");
                }

                // At least one page should be present in a valid Visio file.
                if (diagram.Pages == null || diagram.Pages.Count == 0)
                {
                    throw new Exception($"Diagram loaded but contains no pages. File: {inputPath}");
                }

                Console.WriteLine($"Diagram loaded successfully. Page count: {diagram.Pages.Count}");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }