using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source and destination Visio files
            string sourceFile = "input.vsdx";
            string destinationFile = "output.vsdx";

            // Load the diagram from the source file
            Diagram diagram = new Diagram(sourceFile);

            // Dictionary containing placeholder keys and their replacement values
            var replacements = new Dictionary<string, string>
            {
                { "Name", "John Doe" },
                { "Date", "2023-10-01" }
                // Add more key/value pairs as needed
            };

            // Iterate through every page and shape in the diagram
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a Text cell with a value
                    if (shape.Text != null && shape.Text.Value != null)
                    {
                        string originalText = shape.Text.Value.ToString();
                        string updatedText = originalText;

                        // Replace each placeholder found in the shape's text
                        foreach (var kvp in replacements)
                        {
                            string placeholder = $"{{{{{kvp.Key}}}}}";
                            if (updatedText.Contains(placeholder))
                            {
                                updatedText = updatedText.Replace(placeholder, kvp.Value);
                            }
                        }

                        // If any replacement occurred, apply it to the shape
                        if (!updatedText.Equals(originalText, StringComparison.Ordinal))
                        {
                            shape.ReplaceText(originalText, updatedText);
                            shape.RefreshData(); // Refresh shape geometry after text change
                        }
                    }
                }
            }

            // Save the modified diagram using the provided Save method
            diagram.Save(destinationFile, SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
