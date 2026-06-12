using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expecting four arguments: input file, output file, user cell name, new value
            if (args.Length != 4)
            {
                Console.WriteLine("Usage: DiagramUserCellUpdater <inputPath> <outputPath> <cellName> <newValue>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];
            string cellName = args[2];
            string newValue = args[3];

            // Validate input file existence
            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Input file not found: {inputPath}");
                return;
            }

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Gather all shapes from all pages
                List<Shape> allShapes = new List<Shape>();
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        allShapes.Add(shape);
                    }
                }

                // Update user-defined cells concurrently
                Parallel.ForEach(allShapes, shape =>
                {
                    // Lock per shape to avoid race conditions inside Aspose.Diagram
                    lock (shape)
                    {
                        UpdateOrCreateUserCell(shape, cellName, newValue);
                    }
                });

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to {outputPath}");
            }
        }

        /// <summary>
        /// Updates an existing user-defined cell or creates a new one if it does not exist.
        /// </summary>
        /// <param name="shape">The shape whose Users collection will be modified.</param>
        /// <param name="cellName">The name of the user-defined cell.</param>
        /// <param name="value">The new value to assign.</param>
        private static void UpdateOrCreateUserCell(Shape shape, string cellName, string value)
        {
            // Search for an existing user cell with the specified name
            User targetUser = null;
            foreach (User user in shape.Users)
            {
                if (user.Name == cellName)
                {
                    targetUser = user;
                    break;
                }
            }

            // If not found, create a new user cell and add it to the shape
            if (targetUser == null)
            {
                targetUser = new User();
                targetUser.Name = cellName;
                shape.Users.Add(targetUser);
            }

            // Assign the new value
            targetUser.Value.Val = value;
        }
    }