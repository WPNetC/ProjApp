using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VsIncludeEditor.Modules.SettingsEditor
{
    /// <summary>
    /// Interaction logic for SettingsEditorView.xaml
    /// </summary>
    public partial class SettingsEditorView : UserControl
    {
        public SettingsEditorView()
        {
            InitializeComponent();
            Control.DataContext = this;

            CodeEditorPath = Properties.Settings.Default.CodeEditorPath;
            VSPath = Properties.Settings.Default.VSPath;
            GitGUIPath = Properties.Settings.Default.GitGUIPath;
        }



        public string VSPath
        {
            get
            {
                var val = (string)GetValue(VSPathProperty);
                if (string.IsNullOrEmpty(val))
                {
                    val = Properties.Settings.Default.VSPath;
                    if (!string.IsNullOrEmpty(val))
                    {
                        SetValue(VSPathProperty, val);
                    }
                }

                return val;
            }
            set
            {
                SetValue(VSPathProperty, value);
                if (File.Exists(value))
                {
                    Properties.Settings.Default.VSPath = value;
                    Properties.Settings.Default.Save();
                }
            }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VSPathProperty =
            DependencyProperty.Register("VSPath", typeof(string), typeof(SettingsEditorView), new PropertyMetadata(null));

        public string CodeEditorPath
        {
            get
            {
                var val = (string)GetValue(CodeEditorPathProperty);
                if (string.IsNullOrEmpty(val))
                {
                    val = Properties.Settings.Default.CodeEditorPath;
                    if (!string.IsNullOrEmpty(val))
                    {
                        SetValue(CodeEditorPathProperty, val);
                    }
                }

                return val;
            }
            set
            {
                SetValue(CodeEditorPathProperty, value);
                if (File.Exists(value))
                {
                    Properties.Settings.Default.CodeEditorPath = value;
                    Properties.Settings.Default.Save();
                }
            }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CodeEditorPathProperty =
            DependencyProperty.Register("CodeEditorPath", typeof(string), typeof(SettingsEditorView), new PropertyMetadata(null));
        
        public string GitGUIPath
        {
            get
            {
                var val = (string)GetValue(GitGUIPathProperty);
                if (string.IsNullOrEmpty(val?.Trim()))
                {
                    val = Properties.Settings.Default.GitGUIPath;
                    if (!string.IsNullOrEmpty(val))
                    {
                        SetValue(GitGUIPathProperty, val);
                    }
                }

                return val;
            }
            set
            {
                SetValue(GitGUIPathProperty, value);
                if (File.Exists(value))
                {
                    Properties.Settings.Default.GitGUIPath = value;
                    Properties.Settings.Default.Save();
                }
            }
        }

        // Using a DependencyProperty as the backing store for GitGUIPAth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GitGUIPathProperty =
            DependencyProperty.Register("GitGUIPath", typeof(string), typeof(SettingsEditorView), new PropertyMetadata(null));


        private void SelectFile(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn == null)
                return;

            string filter = "";

            switch (btn.Name)
            {
                case "btnCodeEditorPath":
                    filter = "Executeable|*.exe;*.bat";
                    break;
                case "btnVSPath":
                    filter = "Visual Studio Executeable|devenv.exe";
                    break;
                default:
                    filter = "Any|*.*";
                    break;
            }

            var path = "";
            using (var fbd = new System.Windows.Forms.OpenFileDialog())
            {
                fbd.Filter = filter;
                fbd.Multiselect = false;

                if (fbd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;

                if (!File.Exists(fbd.FileName))
                    return;

                path = fbd.FileName;
            }

            switch (btn.Name)
            {
                case "btnCodeEditorPath":
                    CodeEditorPath = path;
                    break;
                case "btnVSPath":
                    VSPath = path;
                    break;
                case "btnGitGUIPath":
                    GitGUIPath = path;
                    break;
                default:
                    break;
            }

        }
    }
}
