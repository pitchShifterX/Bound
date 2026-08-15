using GameEngine.Utilities;
using NativeFileDialogSharp;

namespace GameEngine.Platform
{
    public class FileService
    {
        public string? OpenDialog(string filter)
        {
            var openResult = Dialog.FileOpen(filter);

            if(openResult.IsOk)
                return openResult.Path;
            
            if(openResult.IsError)
            {
                Log.Error(openResult.ErrorMessage);
            }

            return null;
        }
    }
}