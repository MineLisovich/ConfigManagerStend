using ConfigManagerStend.Models;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.IO;
using ConfigManagerStend.Logic;

namespace ConfigManagerStend
{
    public partial class MainWindow : Window
    {
        private ParserModel parser = new();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BrowseJsonFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;

            if (openFileDialog.ShowDialog() == true)
            {              
                parser.JsonFilePath = openFileDialog.FileName;

                string[] pth = openFileDialog.FileName.Split("\\");
                parser.JsonFileName = pth[pth.Length-1];

                jsonFilePathTextBox.Text = parser.JsonFileName;

                string? directoryPath = new FileInfo(parser.JsonFilePath).DirectoryName;

                if (!string.IsNullOrEmpty(directoryPath))
                {
                    directoryPath += "\\";
                    parser.JsonPathSave = directoryPath;
                }
            }
        }

        private void BrowseDirectory_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new();
            dialog.IsFolderPicker = true;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                parser.DebugPath = dialog.FileName;
                directoryPathTextBox.Text = parser.DebugPath;
            }
        }

        private void SubstitutionBtn_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(parser.DebugPath) ||
               string.IsNullOrEmpty(parser.JsonFilePath) ||
               string.IsNullOrEmpty(parser.JsonFileName) ||
               string.IsNullOrEmpty(parser.JsonPathSave))
            {
                MessageBox.Show("Невыбран исходный файл или путь до папки Debug");
            }

            ParserLogic logic = new();
            bool result = logic.ParserFile(parser);
            string message = (result) ? "Успешно" : "Ошибка";
            MessageBox.Show(message);
        }
    }
}