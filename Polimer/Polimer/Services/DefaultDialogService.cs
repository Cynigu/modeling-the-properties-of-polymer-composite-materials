using System.Collections.Generic;
using System.Windows;
using Microsoft.Win32;

namespace Polimer.App.Services;

public class DefaultDialogService : IDialogService
{
    public string FilePath { get; set; }

    public bool OpenFileDialog()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        if (openFileDialog.ShowDialog() == true)
        {
            FilePath = openFileDialog.FileName;
            return true;
        }

        return false;
    }

    public bool SaveFileDialog()
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.FileName = "Report"; // Default file name
        saveFileDialog.DefaultExt = ".docx"; // Default file extension
        saveFileDialog.Filter = "report (.docx)|*.docx"; // Filter files by extension

        if (saveFileDialog.ShowDialog() == true)
        {
            FilePath = saveFileDialog.FileName;
            return true;
        }

        return false;
    }

    public void ShowMessage(string message)
    {
        MessageBox.Show(message);
    }
}