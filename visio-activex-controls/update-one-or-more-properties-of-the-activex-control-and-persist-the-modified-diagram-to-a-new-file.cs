using System;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths for the source and the modified diagram
                string inputPath = "input.vsdx";
                string outputPath = "output_modified.vsdx";

                try
                {
                    // Load the existing Visio diagram
                    Diagram diagram = new Diagram(inputPath);

                    // Access the first page (index 0)
                    Page page = diagram.Pages[0];

                    // -------------------------------------------------
                    // 1. Add a CommandButton ActiveX control to the page
                    // -------------------------------------------------
                    // Parameters: ControlType, PinX, PinY, Width, Height (all in inches)
                    long commandButtonId = page.AddActiveXControl(ControlType.CommandButton, 2.0, 2.0, 1.5, 0.5);

                    // Retrieve the shape that hosts the control
                    Shape commandButtonShape = page.Shapes.GetShape(commandButtonId);

                    // Cast the generic ActiveXControl to the specific type
                    CommandButtonActiveXControl commandButton = (CommandButtonActiveXControl)commandButtonShape.ActiveXControl;

                    // Update visual properties of the command button
                    commandButton.Caption = "Submit";

                    // -------------------------------------------------
                    // 2. Add a TextBox ActiveX control to the page
                    // -------------------------------------------------
                    long textBoxId = page.AddActiveXControl(ControlType.TextBox, 4.0, 2.0, 2.0, 0.5);
                    Shape textBoxShape = page.Shapes.GetShape(textBoxId);
                    TextBoxActiveXControl textBox = (TextBoxActiveXControl)textBoxShape.ActiveXControl;

                    // Set the displayed text inside the TextBox
                    textBox.Text = "Hello, World!";

                    // -------------------------------------------------
                    // 3. Persist the modified diagram to a new file
                    // -------------------------------------------------
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);

                    Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
                }
                catch (Exception ex)
                {
                    // Report any errors that occur during processing
                    Console.WriteLine("An error occurred: " + ex.Message);
                    throw;
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }