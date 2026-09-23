using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Path to the Visio file to load
            string filePath = "input.vsdx";

            try
            {
                // Load the diagram. If the file contains corrupted master definitions,
                // an exception will be thrown and caught below.
                Diagram diagram = new Diagram(filePath);
                Console.WriteLine("Diagram loaded successfully.");
                // Further processing of the diagram can be added here.
            }
            catch (Exception ex)
            {
                // Handle loading errors (e.g., corrupted masters, missing file, etc.)
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                // Additional error handling or logging can be performed here.
            }
        }
    }