using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (VSDX, VDX, etc.)
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
                // Output text file path
                string outputPath = args.Length > 1 ? args[1] : "cleaned_text.txt";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                StringBuilder cleanedText = new StringBuilder();

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve plain text from the shape
                        string text = shape.Text.Value.Text;

                        if (string.IsNullOrWhiteSpace(text))
                            continue;

                        // Split text into words and filter out numeric strings
                        string[] words = text.Split(new char[] { ' ', '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string word in words)
                        {
                            // Skip if the word consists only of digits
                            if (Regex.IsMatch(word, @"^\d+$"))
                                continue;

                            cleanedText.Append(word);
                            cleanedText.Append(' ');
                        }
                    }
                }

                // Write the cleaned content to the output file
                File.WriteAllText(outputPath, cleanedText.ToString().Trim());

                Console.WriteLine($"Extracted text saved to: {outputPath}");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }