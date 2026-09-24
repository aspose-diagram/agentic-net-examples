using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        // Simple translation stub – in real scenarios replace with actual translation service
        static string Translate(string text, string targetLanguage)
        {
            // Example: append language code to simulate translation
            return $"{text}_{targetLanguage}";
        }

        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: DiagramLocalization <inputPath> <outputPath> <targetLanguageCode>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];
            string targetLanguage = args[2];

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Process each user-defined cell (custom property)
                    foreach (User userCell in shape.Users)
                    {
                        string originalValue = userCell.Value.Val;
                        if (!string.IsNullOrEmpty(originalValue))
                        {
                            string translatedValue = Translate(originalValue, targetLanguage);
                            userCell.Value.Val = translatedValue;
                        }
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}' with localized user-defined cells.");
        }
    }