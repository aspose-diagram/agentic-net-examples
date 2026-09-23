using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        // Output DOCX file path (Aspose.Diagram does not support DOCX directly; using PDF as an alternative)
        string outputPath = "output.pdf";
        if (string.IsNullOrWhiteSpace(outputPath)) { Console.Error.WriteLine("Output path is empty."); return; }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Font to apply to all text runs
            const string targetFont = "Calibri";

            // Desired line spacing (in inches)
            const double lineSpacing = 0.2; // adjust as needed

            // Iterate through all pages, shapes, paragraphs and character runs
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Apply the font to every character run in the shape
                    if (shape.Chars != null && shape.Chars.Count > 0)
                    {
                        foreach (Aspose.Diagram.Char ch in shape.Chars)
                        {
                            // Set the font name for the character
                            ch.FontName.Value = targetFont;
                        }
                    }

                    // Set line spacing for each paragraph in the shape
                    if (shape.Paras != null && shape.Paras.Count > 0)
                    {
                        foreach (Para para in shape.Paras)
                        {
                            // SpLine controls line spacing; value is in inches
                            para.SpLine.Value = lineSpacing;
                        }
                    }
                }
            }

            // Save the modified diagram as PDF (DOCX not supported by Aspose.Diagram)
            diagram.Save(outputPath, new PdfSaveOptions());
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}