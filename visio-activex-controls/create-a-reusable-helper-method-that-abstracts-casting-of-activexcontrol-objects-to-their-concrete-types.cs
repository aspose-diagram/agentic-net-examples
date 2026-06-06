using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

namespace DiagramActiveXHelper
{
    /// <summary>
    /// Provides utility methods for working with ActiveX controls in a Visio diagram.
    /// </summary>
    public static class ActiveXHelper
    {
        /// <summary>
        /// Retrieves the concrete ActiveX control of the expected type from the specified shape.
        /// </summary>
        /// <typeparam name="T">Concrete ActiveX control type (e.g., CommandButtonActiveXControl).</typeparam>
        /// <param name="shape">Shape that contains the ActiveX control.</param>
        /// <returns>Instance of the concrete ActiveX control.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the shape does not contain an ActiveX control or the control type does not match <typeparamref name="T"/>.</exception>
        public static T GetActiveXControl<T>(Shape shape) where T : ActiveXControl
        {
            if (shape == null)
                throw new ArgumentNullException(nameof(shape));

            // Ensure the shape actually has an ActiveX control.
            if (shape.ActiveXControl == null)
                throw new InvalidOperationException("The shape does not contain an ActiveX control.");

            // Determine the expected ControlType for the generic type T.
            ControlType expectedType = GetControlTypeFor<T>();

            // Verify that the control's type matches the expected type.
            if (shape.ActiveXControl.Type != expectedType)
                throw new InvalidOperationException(
                    $"ActiveX control type mismatch. Expected {expectedType}, but found {shape.ActiveXControl.Type}.");

            // Safe cast because we have verified the type.
            return (T)shape.ActiveXControl;
        }

        /// <summary>
        /// Maps concrete ActiveX control types to their corresponding <see cref="ControlType"/> enumeration values.
        /// Extend this method when new control types are needed.
        /// </summary>
        /// <typeparam name="T">Concrete ActiveX control type.</typeparam>
        /// <returns>Corresponding <see cref="ControlType"/> value.</returns>
        /// <exception cref="NotSupportedException">Thrown when the type T is not mapped.</exception>
        private static ControlType GetControlTypeFor<T>() where T : ActiveXControl
        {
            // Mapping based on Aspose.Diagram.ActiveXControls types.
            if (typeof(T) == typeof(CommandButtonActiveXControl))
                return ControlType.CommandButton;
            if (typeof(T) == typeof(ImageActiveXControl))
                return ControlType.Image;
            if (typeof(T) == typeof(CheckBoxActiveXControl))
                return ControlType.CheckBox;
            if (typeof(T) == typeof(ComboBoxActiveXControl))
                return ControlType.ComboBox;
            if (typeof(T) == typeof(LabelActiveXControl))
                return ControlType.Label;
            if (typeof(T) == typeof(ListBoxActiveXControl))
                return ControlType.ListBox;
            if (typeof(T) == typeof(SpinButtonActiveXControl))
                return ControlType.SpinButton;
            if (typeof(T) == typeof(TextBoxActiveXControl))
                return ControlType.TextBox;

            // If the type is not recognized, inform the caller.
            throw new NotSupportedException($"Mapping for ActiveX control type '{typeof(T).Name}' is not defined.");
        }
    }

    // Example usage of the helper within a console application.
    class Program
    {
        static void Main()
        {
            // Load a diagram (replace with an actual file path).
            string diagramPath = "sample.vsdx";
            if (!File.Exists(diagramPath))
            {
                Console.Error.WriteLine($"File not found: {diagramPath}");
                return;
            }

            Diagram diagram;
            try
            {
                diagram = new Diagram(diagramPath);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Assume we want to work with the first shape on the active page.
            Shape shape = diagram.ActivePage.Shapes[0];

            try
            {
                // Retrieve a CommandButtonActiveXControl from the shape using the helper.
                CommandButtonActiveXControl button = ActiveXHelper.GetActiveXControl<CommandButtonActiveXControl>(shape);

                // Manipulate the control (e.g., change its caption).
                button.Caption = "Updated Caption";

                // Save the diagram after modification.
                diagram.Save("updated.vsdx", SaveFileFormat.Vsdx);
                Console.WriteLine("Diagram saved successfully.");
            }
            catch (Exception ex)
            {
                // Simple error handling – in real scenarios, use proper logging.
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}