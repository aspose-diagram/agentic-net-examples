using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Manipulation;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input MMD file path and output Visio file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: MmdToVisio <input.mmd> <output.vsdx>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Input file not found: {inputPath}");
                return;
            }

            // Read all lines from the MMD file
            string[] lines = File.ReadAllLines(inputPath);

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Dictionaries to keep track of node positions and shape IDs
            Dictionary<string, long> nodeShapeIds = new Dictionary<string, long>();
            Dictionary<string, (int col, int row)> nodePositions = new Dictionary<string, (int, int)>();

            int currentCol = 0;
            int currentRow = 0;

            // Simple parser for Mermaid flowchart connections (e.g., A --> B)
            foreach (string rawLine in lines)
            {
                string line = rawLine.Trim();

                // Skip empty lines and lines that are not connections
                if (string.IsNullOrEmpty(line) || !line.Contains("-->"))
                    continue;

                // Split the line into left and right parts
                string[] parts = line.Split(new string[] { "-->" }, StringSplitOptions.None);
                if (parts.Length != 2)
                    continue; // malformed line

                string leftNode = parts[0].Trim();
                string rightNode = parts[1].Trim();

                // Remove optional labels or brackets (e.g., A[Start] -> B)
                leftNode = CleanNodeName(leftNode);
                rightNode = CleanNodeName(rightNode);

                // Ensure left node shape exists
                if (!nodeShapeIds.ContainsKey(leftNode))
                {
                    long shapeId = CreateRectangleShape(page, leftNode, currentCol, currentRow);
                    nodeShapeIds[leftNode] = shapeId;
                    nodePositions[leftNode] = (currentCol, currentRow);
                    currentCol++;
                }

                // Ensure right node shape exists
                if (!nodeShapeIds.ContainsKey(rightNode))
                {
                    long shapeId = CreateRectangleShape(page, rightNode, currentCol, currentRow);
                    nodeShapeIds[rightNode] = shapeId;
                    nodePositions[rightNode] = (currentCol, currentRow);
                    currentCol++;
                }

                // Create a connector shape (Dynamic connector)
                long connectorId = page.AddShape(0, 0, 0, 0, "Dynamic connector");
                // Connect the two shapes using default connection points (Bottom -> Top)
                page.ConnectShapesViaConnector(
                    nodeShapeIds[leftNode],
                    ConnectionPointPlace.Bottom,
                    nodeShapeIds[rightNode],
                    ConnectionPointPlace.Top,
                    connectorId);
            }

            // Save the diagram as Visio VSDX
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");
        }

        // Helper method to clean node names (remove brackets, labels, etc.)
        private static string CleanNodeName(string raw)
        {
            // Remove surrounding brackets if present (e.g., A[Start] -> A)
            int bracketIndex = raw.IndexOf('[');
            if (bracketIndex > 0)
                raw = raw.Substring(0, bracketIndex).Trim();

            // Remove any surrounding quotes
            raw = raw.Trim('\"', '\'');

            return raw;
        }

        // Helper method to create a rectangle shape with text at a grid position
        private static long CreateRectangleShape(Page page, string text, int col, int row)
        {
            // Define size of the shape (in inches)
            double width = 1.5;
            double height = 0.8;

            // Simple grid layout: 2 inches apart horizontally and vertically
            double pinX = col * 2.0;
            double pinY = row * 2.0;

            // Add the rectangle shape using the built‑in "Rectangle" master
            long shapeId = page.AddShape(pinX, pinY, width, height, "Rectangle");

            // Retrieve the shape object to set its text
            Shape shape = page.Shapes.GetShape(shapeId);
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt(text));

            // Optional: center the text within the shape
            shape.TextXForm.TxtLocPinX.Value = 0.5; // center horizontally
            shape.TextXForm.TxtLocPinY.Value = 0.5; // center vertically

            return shapeId;
        }
    }