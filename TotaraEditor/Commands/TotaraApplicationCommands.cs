using System.Windows.Input;

namespace TotaraEditor
{
    class TotaraApplicationCommands
    {
        private static RoutedUICommand quit;

        static TotaraApplicationCommands()
        {
            InputGestureCollection inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.Q, ModifierKeys.Control, "Ctrl+Q"));
            quit = new RoutedUICommand("Quit", "Quit", typeof(TotaraApplicationCommands), inputs);
        }

        public static RoutedUICommand Quit
        {
            get
            {
                return quit;
            }
        }
    }
}
