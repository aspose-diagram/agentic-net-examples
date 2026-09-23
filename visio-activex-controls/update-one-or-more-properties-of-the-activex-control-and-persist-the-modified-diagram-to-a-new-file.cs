using System;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string sourcePath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(sourcePath);

                // Access the first page (ensure at least one page exists)
                Page page = diagram.Pages[0];

                // -------------------------------------------------
                // Add a CommandButton ActiveX control to the page
                // Parameters: ControlType, pinX, pinY, width, height
                // -------------------------------------------------
                long cmdButtonShapeId = page.AddActiveXControl(ControlType.CommandButton, 2.0, 2.0, 1.5, 0.5);
                Shape cmdButtonShape = page.Shapes.GetShape(cmdButtonShapeId);

                // Cast the generic ActiveXControl to the specific type
                CommandButtonActiveXControl cmdButton = (CommandButtonActiveXControl)cmdButtonShape.ActiveXControl;

                // Update properties of the command button
                cmdButton.Caption = "Submit";
                cmdButton.Width = 1.5;   // width in inches
                cmdButton.Height = 0.5;  // height in inches

                // -------------------------------------------------
                // Add a TextBox ActiveX control to the page
                // -------------------------------------------------
                long textBoxShapeId = page.AddActiveXControl(ControlType.TextBox, 4.0, 2.0, 2.0, 0.5);
                Shape textBoxShape = page.Shapes.GetShape(textBoxShapeId);

                // Cast to TextBoxActiveXControl
                TextBoxActiveXControl textBox = (TextBoxActiveXControl)textBoxShape.ActiveXControl;

                // Update properties of the text box
                textBox.Text = "Enter name here";

                // -------------------------------------------------
                // Save the modified diagram to a new file
                // -------------------------------------------------
                string outputPath = "output_modified.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Diagram saved successfully to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }