using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Input Visio file path (provide via command line or use default)
            string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

            Diagram diagram;
            try
            {
                // Load the diagram; this may throw if the file is missing or corrupted
                diagram = new Diagram(inputPath);
            }
            catch (Exception loadEx)
            {
                Console.WriteLine($"Failed to load diagram '{inputPath}'. Error: {loadEx.Message}");
                return;
            }

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    try
                    {
                        // Attempt to read an inherited fill property.
                        // Older Visio files may not contain the InheritFill section,
                        // which would cause a NullReferenceException.
                        string inheritedFillColor = shape.InheritFill.FillForegnd.Value;
                        Console.WriteLine($"Shape ID {shape.ID} inherited fill color: {inheritedFillColor}");
                    }
                    catch (Exception ex)
                    {
                        // Gracefully handle missing InheritFill or any other access issue.
                        Console.WriteLine($"Shape ID {shape.ID} does not have InheritFill information. Details: {ex.Message}");
                    }
                }
            }

            // Optionally, save the diagram after processing (no changes made here)
            try
            {
                string outputPath = "processed_output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");
            }
            catch (Exception saveEx)
            {
                Console.WriteLine($"Failed to save diagram. Error: {saveEx.Message}");
            }
        }
    }