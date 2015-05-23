using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace FolderDeleter
{
    public partial class MainWindow : Window
    {
        FolderBrowserDialog fb = new FolderBrowserDialog();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ClickOnSelectButton(object sender, RoutedEventArgs e)
        {
            fb.ShowDialog();
        }

        private void ClickOnDeleteButton(object sender, RoutedEventArgs e)
        {
            try
            {
                DeleteFoldersRecursive(fb.SelectedPath);
                Environment.Exit(1);
            }
            catch (UnauthorizedAccessException)
            {
                System.Windows.Forms.MessageBox.Show("You search in forbidden directory");
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("There is some mistake. Try again or select different directory!");
            }
        }

        private static void DeleteFoldersRecursive(string directory)
        {
            string[] selectedTaskDirectories = Directory.GetDirectories(directory);

            foreach (var folder in selectedTaskDirectories)
            {
                if (folder.ToLower() == "bin" || folder.ToLower() == "obj")
                {
                    DirectoryInfo d = new DirectoryInfo(folder);
                    d.Delete(true);
                }
                else if (folder.Contains("System Volume Information"))
                {
                    continue;
                }
                else
                {
                    DeleteFoldersRecursive(folder);
                }
            }
        }
    }
}
