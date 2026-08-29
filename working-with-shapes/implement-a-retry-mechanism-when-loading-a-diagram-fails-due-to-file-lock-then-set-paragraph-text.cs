using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                const string inputPath = "input.vsdx";
                const string outputPath = "output.vsdx";
                const int maxRetries = 3;
                const int delayMilliseconds = 500;

                Diagram diagram = null;
                int attempt = 0;
                bool loaded = false;

                // Retry loading the diagram in case the file is locked
                while (attempt < maxRetries && !loaded)
                {
                    try
                    {
                        attempt++;
                        diagram = new Diagram(inputPath);
                        loaded = true;
                    }
                    catch (IOException ex) when (ex.HResult == -2147024864) // ERROR_SHARING_VIOLATION
                    {
                        if (attempt >= maxRetries)
                        {
                            Console.WriteLine($"Failed to load diagram after {maxRetries} attempts: {ex.Message}");
                            throw;
                        }

                        Console.WriteLine($"Attempt {attempt} failed due to file lock. Retrying in {delayMilliseconds} ms...");
                        Thread.Sleep(delayMilliseconds);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Unexpected error while loading diagram: {ex.Message}");
                        throw;
                    }
                }

                // Ensure diagram was loaded
                if (diagram == null)
                {
                    Console.WriteLine("Diagram could not be loaded.");
                    return;
                }

                // Example: find a shape named "MyShape" on the first page and set its paragraph text
                Page page = diagram.Pages[0];
                Shape targetShape = null;

                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Name == "MyShape")
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.WriteLine("Shape 'MyShape' not found.");
                }
                else
                {
                    // Clear existing text and add new paragraph text
                    targetShape.Text.Value.Clear();
                    targetShape.Text.Value.Add(new Txt("This is the new paragraph text."));

                    // Optionally modify paragraph formatting (e.g., left alignment)
                    if (targetShape.Paras.Count > 0)
                    {
                        targetShape.Paras[0].HorzAlign.Value = HorzAlignValue.LeftAlign;
                    }

                    Console.WriteLine("Paragraph text updated successfully.");
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }