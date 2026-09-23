using System;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Input diagram file path (existing Visio file)
            Console.Write("Enter the path to the source Visio file: ");
            string sourcePath = Console.ReadLine();

            // Validate input
            if (string.IsNullOrWhiteSpace(sourcePath))
            {
                Console.WriteLine("Source path is required.");
                return;
            }

            // Load the diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(sourcePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Use the first page (or create one if none exist)
            Page page;
            if (diagram.Pages.Count > 0)
            {
                page = diagram.Pages[0];
            }
            else
            {
                page = new Page(1);
                diagram.Pages.Add(page);
            }

            // Prompt user for ActiveX CommandButton properties
            Console.Write("Enter button caption: ");
            string caption = Console.ReadLine();

            Console.Write("Enter button X position (in inches): ");
            double posX = ReadDoubleFromConsole();

            Console.Write("Enter button Y position (in inches): ");
            double posY = ReadDoubleFromConsole();

            Console.Write("Enter button width (in inches): ");
            double width = ReadDoubleFromConsole();

            Console.Write("Enter button height (in inches): ");
            double height = ReadDoubleFromConsole();

            // Add a CommandButton ActiveX control to the page
            long controlId = page.AddActiveXControl(ControlType.CommandButton, posX, posY, width, height);

            // Retrieve the shape that hosts the ActiveX control
            Shape controlShape = page.Shapes.GetShape(controlId);

            // Cast the ActiveXControl to the specific type
            CommandButtonActiveXControl button = (CommandButtonActiveXControl)controlShape.ActiveXControl;

            // Set visual properties
            button.Caption = caption;
            button.Width = width;
            button.Height = height;

            // Optionally, update shape text to reflect the caption
            controlShape.Text.Value.Clear();
            controlShape.Text.Value.Add(new Txt(caption));

            // Save the updated diagram
            Console.Write("Enter the output path for the updated diagram (e.g., output.vsdx): ");
            string outputPath = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(outputPath))
            {
                Console.WriteLine("Output path is required.");
                return;
            }

            try
            {
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save diagram: {ex.Message}");
            }
        }

        // Helper method to read a double value from console with validation
        private static double ReadDoubleFromConsole()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (double.TryParse(input, out double result))
                {
                    return result;
                }
                Console.Write("Invalid number. Please enter a valid numeric value: ");
            }
        }
    }