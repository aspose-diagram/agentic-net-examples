using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Path to the Visio file (adjust as needed)
            string filePath = "input.vsdx";

            // Load the diagram
            Diagram diagram = null;
            try
            {
                diagram = new Diagram(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // List to hold page widths (in inches)
            List<double> pageWidths = new List<double>();

            // Iterate over each page and collect its width
            foreach (Page page in diagram.Pages)
            {
                double width = page.PageSheet.PageProps.PageWidth.Value;
                pageWidths.Add(width);
                Console.WriteLine($"Page ID {page.ID} width: {width} inches");
            }

            // Example analysis: compute average width
            if (pageWidths.Count > 0)
            {
                double sum = 0;
                foreach (double w in pageWidths)
                {
                    sum += w;
                }
                double average = sum / pageWidths.Count;
                Console.WriteLine($"Average page width: {average} inches");
            }

            // Clean up
            diagram.Dispose();
        }
    }