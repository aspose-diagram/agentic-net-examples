using System;
using System.IO;
using Aspose.Diagram;

namespace DiagramProcessing
{
    public static class DiagramLoader
    {
        /// <summary>
        /// Loads a Visio diagram from a file. Supports both VDX (XML) and VSDX (Open Packaging) formats.
        /// The method automatically detects the format based on the file content.
        /// </summary>
        /// <param name="filePath">Full path to the Visio file.</param>
        /// <returns>An instance of <see cref="Diagram"/> representing the loaded diagram.</returns>
        public static Diagram LoadDiagram(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("File path must be provided.", nameof(filePath));

            if (!File.Exists(filePath))
                throw new FileNotFoundException("The specified Visio file does not exist.", filePath);

            // Aspose.Diagram automatically detects the format (VDX, VSDX, VDX, VDX, etc.)
            // when using the Diagram constructor that accepts a file name.
            Diagram diagram = new Diagram(filePath);

            // Optional: verify that the diagram was loaded successfully.
            if (diagram == null)
                throw new InvalidOperationException("Failed to load the Visio diagram.");

            return diagram;
        }
    }

    // Example usage:
    class Program
    {
        static void Main()
        {
            try
            {
                string pathToVisio = @"C:\Diagrams\sample.vsdx"; // can be .vdx or .vsdx
                Diagram diagram = DiagramLoader.LoadDiagram(pathToVisio);

                // Perform further processing on the diagram here.
                Console.WriteLine($"Diagram loaded. Pages count: {diagram.Pages.Count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}