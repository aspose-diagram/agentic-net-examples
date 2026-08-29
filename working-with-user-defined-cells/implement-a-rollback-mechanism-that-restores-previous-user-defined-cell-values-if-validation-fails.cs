using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <program> <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Load the diagram
        Diagram diagram = new Diagram(inputPath);

        // Store original user-defined cell values for rollback
        // Key: shape ID, Value: dictionary of cell name -> original value
        var originalValues = new Dictionary<long, Dictionary<string, string>>();

        // Capture current values
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                var userValues = new Dictionary<string, string>();
                foreach (User userCell in shape.Users)
                {
                    // Store by universal name (NameU) if available, otherwise Name
                    string cellName = !string.IsNullOrEmpty(userCell.NameU) ? userCell.NameU : userCell.Name;
                    userValues[cellName] = userCell.Value.Val;
                }

                if (userValues.Count > 0)
                {
                    originalValues[shape.ID] = userValues;
                }
            }
        }

        // Example modification: set a user-defined cell "Width" to an invalid value for demonstration
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                foreach (User userCell in shape.Users)
                {
                    string cellName = !string.IsNullOrEmpty(userCell.NameU) ? userCell.NameU : userCell.Name;
                    if (cellName.Equals("Width", StringComparison.OrdinalIgnoreCase))
                    {
                        // Intentionally set an invalid value
                        userCell.Value.Val = "-10";
                    }
                }
            }
        }

        // Perform validation
        bool validationFailed = false;
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                foreach (User userCell in shape.Users)
                {
                    string cellName = !string.IsNullOrEmpty(userCell.NameU) ? userCell.NameU : userCell.Name;
                    string cellValue = userCell.Value.Val;

                    // Example rule: "Width" must be a positive number
                    if (cellName.Equals("Width", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!double.TryParse(cellValue, out double width) || width <= 0)
                        {
                            Console.WriteLine($"Validation error on shape ID {shape.ID}: Width must be a positive number. Current value: {cellValue}");
                            validationFailed = true;
                        }
                    }
                }
            }
        }

        // Rollback if validation failed
        if (validationFailed)
        {
            Console.WriteLine("Validation failed. Rolling back changes to user-defined cells.");

            foreach (var kvp in originalValues)
            {
                long shapeId = kvp.Key;
                var savedCells = kvp.Value;

                // Find the shape by ID (search all pages)
                Shape shape = null;
                foreach (Page page in diagram.Pages)
                {
                    shape = page.Shapes.GetShape(shapeId);
                    if (shape != null) break;
                }

                if (shape == null) continue;

                // Restore each saved cell value
                foreach (User userCell in shape.Users)
                {
                    string cellName = !string.IsNullOrEmpty(userCell.NameU) ? userCell.NameU : userCell.Name;
                    if (savedCells.TryGetValue(cellName, out string originalVal))
                    {
                        userCell.Value.Val = originalVal;
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("Validation succeeded. No rollback needed.");
        }

        // Save the diagram (using SaveFileFormat.Vsdx as an example)
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
        Console.WriteLine($"Diagram saved to {outputPath}");
    }
}
