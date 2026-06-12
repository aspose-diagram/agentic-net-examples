using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Example usage:
                // Validate that the shape with name "OldShapeName" keeps its ID after being renamed to "NewShapeName"
                ValidateShapeIdAfterRename("sample.vsdx", "OldShapeName", "NewShapeName");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Loads a Visio diagram, renames a shape, and verifies that the shape's ID remains unchanged.
        /// </summary>
        /// <param name="filePath">Path to the Visio file.</param>
        /// <param name="oldName">Current name of the shape to be renamed.</param>
        /// <param name="newName">New name to assign to the shape.</param>
        static void ValidateShapeIdAfterRename(string filePath, string oldName, string newName)
        {
            // Load the diagram (using the provided load rule)
            Diagram diagram = new Diagram(filePath);

            // Assume the shape is on the first page; adjust if needed
            Page page = diagram.Pages[0];

            // Retrieve the shape by its original name
            Shape shapeBeforeRename = page.Shapes.GetShape(oldName);
            if (shapeBeforeRename == null)
            {
                Console.WriteLine($"Shape with name \"{oldName}\" not found.");
                return;
            }

            // Store the original ID
            long originalId = shapeBeforeRename.ID;

            // Rename the shape
            shapeBeforeRename.Name = newName;

            // Refresh shape data to ensure internal references are updated
            shapeBeforeRename.RefreshData();

            // Retrieve the shape by its new name
            Shape shapeAfterRename = page.Shapes.GetShape(newName);
            if (shapeAfterRename == null)
            {
                Console.WriteLine($"Shape with new name \"{newName}\" not found after rename.");
                return;
            }

            // Compare IDs
            long newId = shapeAfterRename.ID;
            if (originalId == newId)
            {
                Console.WriteLine($"Success: Shape ID remained consistent after rename. ID = {originalId}");
            }
            else
            {
                Console.WriteLine($"Failure: Shape ID changed after rename. Original ID = {originalId}, New ID = {newId}");
            }

            // (Optional) Save the diagram if you need to persist the rename
            // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
    }