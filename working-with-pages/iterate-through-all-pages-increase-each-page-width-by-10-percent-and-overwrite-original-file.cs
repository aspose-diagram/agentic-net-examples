using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect the first argument to be the path of the Visio file to process.
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: DiagramPageResize <path-to-visio-file>");
                return;
            }

            string filePath = args[0];

            // Load the diagram from the specified file.
            Diagram diagram;
            try
            {
                diagram = new Diagram(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Iterate through each page and increase its width by 10%.
            foreach (Page page in diagram.Pages)
            {
                double currentWidth = page.PageSheet.PageProps.PageWidth.Value;
                double newWidth = currentWidth * 1.10; // Increase by 10%
                page.PageSheet.PageProps.PageWidth.Value = newWidth;
            }

            // Save the modified diagram, overwriting the original file.
            try
            {
                // Use the Vsdx format for saving. Adjust if a different format is required.
                diagram.Save(filePath, SaveFileFormat.Vsdx);
                Console.WriteLine("Diagram saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save diagram: {ex.Message}");
            }
            finally
            {
                // Ensure resources are released.
                diagram.Dispose();
            }
        }
    }