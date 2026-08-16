namespace GameEngine.UI.Event.Types
{
    public class FileOpenDialogEvent : UIEvent
    {
        public string FileFilter { get; }

        public FileOpenDialogEvent(string filter)
        {
            FileFilter = filter;
        }
    }
}