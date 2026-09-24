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

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Store original user-defined cell values for rollback
            // Key: shape ID, Value: dictionary of user cell name -> original value
            var originalValues = new Dictionary<long, Dictionary<string, string>>();

            // Flag to indicate validation failure
            bool validationFailed = false;

            // Iterate through all pages and shapes to capture original values
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    long shapeId = shape.ID;
                    var userValues = new Dictionary<string, string>();

                    foreach (User userCell in shape.Users)
                    {
                        // Store the current value
                        userValues[userCell.Name] = userCell.Value.Val;
                    }

                    originalValues[shapeId] = userValues;
                }
            }

            // Example validation: ensure that any user-defined cell named "Width" contains a positive number
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    foreach (User userCell in shape.Users)
                    {
                        if (string.Equals(userCell.Name, "Width", StringComparison.OrdinalIgnoreCase))
                        {
                            if (!double.TryParse(userCell.Value.Val, out double width) || width <= 0)
                            {
                                Console.WriteLine($"Validation failed for shape ID {shape.ID}: Width must be a positive number.");
                                validationFailed = true;
                                break;
                            }
                        }
                    }

                    if (validationFailed)
                        break;
                }

                if (validationFailed)
                    break;
            }

            // If validation failed, rollback to original values
            if (validationFailed)
            {
                Console.WriteLine("Rolling back changes to original user-defined cell values.");

                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        long shapeId = shape.ID;
                        if (originalValues.TryGetValue(shapeId, out var savedUserValues))
                        {
                            foreach (User userCell in shape.Users)
                            {
                                if (savedUserValues.TryGetValue(userCell.Name, out string originalVal))
                                {
                                    userCell.Value.Val = originalVal;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("All validations passed. Proceeding with further processing if needed.");
            }

            // Save the diagram (overwrites the original file or saves to a new file)
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
