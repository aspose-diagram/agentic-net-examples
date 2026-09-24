using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file to be analyzed
        string inputPath = "input.vsdx";

        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Define the style guide: expected horizontal alignment per shape name (NameU)
            var styleGuide = new Dictionary<string, HorzAlignValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "Title", HorzAlignValue.Center },      // Center alignment
                { "Subtitle", HorzAlignValue.Center },   // Center alignment
                { "Header", HorzAlignValue.LeftAlign },  // Left alignment
                { "Footer", HorzAlignValue.RightAlign }  // Right alignment
                // Add more shape name → expected alignment mappings as needed
            };

            // Iterate through all pages and shapes in the diagram
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes without any paragraph (text) entries
                    if (shape.Paras == null || shape.Paras.Count == 0)
                        continue;

                    // Use the first paragraph's horizontal alignment as the shape's alignment
                    HorzAlignValue currentAlignment = shape.Paras[0].HorzAlign.Value;

                    // Determine expected alignment from the style guide (if defined)
                    if (styleGuide.TryGetValue(shape.NameU, out HorzAlignValue expectedAlignment))
                    {
                        // Log mismatch when current alignment differs from the expected one
                        if (currentAlignment != expectedAlignment)
                        {
                            Console.WriteLine($"Mismatch on page '{page.NameU}', shape '{shape.NameU}' (ID: {shape.ID}): " +
                                              $"Current alignment = {currentAlignment}, Expected = {expectedAlignment}");
                        }
                    }
                    else
                    {
                        // No rule defined for this shape; optionally report or ignore
                        Console.WriteLine($"No style guide entry for shape '{shape.NameU}' (ID: {shape.ID}).");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}