using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Example: Access a custom event cell named EventCalc.
                        // The actual cell name may vary; adjust as needed.
                        // Event cells are accessed via the Event property and the specific cell name.
                        // Here we use Event.EventXFMod as a placeholder for EventCalc.
                        string eventFormula = shape.Event.EventXFMod.Ufe.F;

                        // Simple condition: if the formula contains the word "TRUE"
                        // (replace with actual evaluation logic as required)
                        bool conditionMet = !string.IsNullOrEmpty(eventFormula) && eventFormula.Contains("TRUE", StringComparison.OrdinalIgnoreCase);

                        // Apply conditional formatting based on the condition
                        if (conditionMet)
                        {
                            // Set fill foreground color to red
                            shape.Fill.FillForegnd.Value = "#FF0000";
                        }
                        else
                        {
                            // Set fill foreground color to green
                            shape.Fill.FillForegnd.Value = "#00FF00";
                        }
                    }
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }