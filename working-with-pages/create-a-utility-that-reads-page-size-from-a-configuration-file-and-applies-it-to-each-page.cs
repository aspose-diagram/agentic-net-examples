using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Path to the configuration file that contains page width and height (in inches)
            // Expected format:
            // line 1: width (e.g., 8.27)
            // line 2: height (e.g., 11.69)
            string configPath = "pagesize.config";

            if (!File.Exists(configPath))
            {
                Console.WriteLine($"Configuration file not found: {configPath}");
                return;
            }

            double pageWidth;
            double pageHeight;

            try
            {
                string[] lines = File.ReadAllLines(configPath);
                if (lines.Length < 2)
                {
                    Console.WriteLine("Configuration file must contain at least two lines: width and height.");
                    return;
                }

                pageWidth = double.Parse(lines[0]);
                pageHeight = double.Parse(lines[1]);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading configuration: {ex.Message}");
                return;
            }

            // Path to the input Visio diagram
            string inputDiagramPath = "input.vsdx";

            if (!File.Exists(inputDiagramPath))
            {
                Console.WriteLine($"Input diagram file not found: {inputDiagramPath}");
                return;
            }

            // Load the diagram
            Diagram diagram = null;
            try
            {
                diagram = new Diagram(inputDiagramPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Apply the page size to every page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Page dimensions are stored in inches
                page.PageSheet.PageProps.PageWidth.Value = pageWidth;
                page.PageSheet.PageProps.PageHeight.Value = pageHeight;
            }

            // Save the modified diagram
            string outputDiagramPath = "output.vsdx";
            try
            {
                diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved with updated page size to: {outputDiagramPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save diagram: {ex.Message}");
            }
            finally
            {
                // Ensure resources are released
                diagram?.Dispose();
            }
        }
    }