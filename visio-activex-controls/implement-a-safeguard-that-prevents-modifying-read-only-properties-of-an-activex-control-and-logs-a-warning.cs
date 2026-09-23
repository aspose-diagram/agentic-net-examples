using System;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape contains an ActiveX control
                        if (shape.ActiveXControl != null)
                        {
                            // Determine the specific control type
                            ControlType ctrlType = shape.ActiveXControl.Type;

                            // Example: handle CommandButtonActiveXControl
                            if (ctrlType == ControlType.CommandButton)
                            {
                                var button = (CommandButtonActiveXControl)shape.ActiveXControl;

                                // Attempt to set a writable property safely
                                SetPropertySafely(
                                    () => button.Caption = "Clicked!",
                                    $"Shape ID {shape.ID} - CommandButton Caption");

                                // Attempt to set a read‑only property (for demonstration)
                                // This will throw because the property does not exist or is read‑only.
                                // We wrap it in the safeguard to log a warning instead of crashing.
                                SetPropertySafely(
                                    () => { /* No writable property to set; placeholder for read‑only attempt */ },
                                    $"Shape ID {shape.ID} - Attempted read‑only property");
                            }
                            // Example: handle CheckBoxActiveXControl
                            else if (ctrlType == ControlType.CheckBox)
                            {
                                var checkBox = (CheckBoxActiveXControl)shape.ActiveXControl;

                                // Safely set the checked state
                                SetPropertySafely(
                                    () => checkBox.Value = CheckValueType.Checked,
                                    $"Shape ID {shape.ID} - CheckBox Value");
                            }
                            // Example: handle TextBoxActiveXControl
                            else if (ctrlType == ControlType.TextBox)
                            {
                                var textBox = (TextBoxActiveXControl)shape.ActiveXControl;

                                // Safely set the text content
                                SetPropertySafely(
                                    () => textBox.Text = "Sample text",
                                    $"Shape ID {shape.ID} - TextBox Text");
                            }
                            // Add handling for other control types as needed
                        }
                    }
                }

                // Save the diagram after modifications
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Executes a property‑setting action and logs a warning if the operation fails,
        /// which typically indicates an attempt to modify a read‑only property.
        /// </summary>
        /// <param name="setAction">The action that sets the property.</param>
        /// <param name="propertyDescription">A description of the property being set.</param>
        private static void SetPropertySafely(Action setAction, string propertyDescription)
        {
            try
            {
                setAction();
                Console.WriteLine($"[INFO] Successfully set {propertyDescription}.");
            }
            catch (Exception ex)
            {
                // Log a warning without throwing, preventing the application from crashing
                Console.WriteLine($"[WARNING] Could not modify {propertyDescription}. Reason: {ex.Message}");
            }
        }
    }