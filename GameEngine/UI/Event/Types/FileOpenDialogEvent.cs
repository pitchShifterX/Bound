namespace GameEngine.UI.Event
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