using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace VsIncludeEditor.Modules.TreeView
{
    /// <summary>
    /// Interaction logic for ListTreeControl.xaml
    /// </summary>
    public partial class ListTreeControl : UserControl
    {
        public ListTreeControl()
        {
            InitializeComponent();
            grdWrapper.DataContext = this;
        }

        public ICollection<TreeNode> Nodes
        {
            get { return (ICollection<TreeNode>)GetValue(NodesProperty); }
            set { SetValue(NodesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for list.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NodesProperty =
            DependencyProperty.Register("Nodes", typeof(ICollection<TreeNode>), typeof(ListTreeControl), new PropertyMetadata(null));
        
        public TreeNode SelectedNode
        {
            get { return (TreeNode)GetValue(SelectedNodeProperty); }
            set { SetValue(SelectedNodeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for treeNode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedNodeProperty =
            DependencyProperty.Register("SelectedNode", typeof(TreeNode), typeof(ListTreeControl), new PropertyMetadata(null));
        
        public ObservableCollection<TreeNode> SelectedNodes
        {
            get
            {
                return (ObservableCollection<TreeNode>)GetValue(SelectedNodesProperty);
            }
            set { SetValue(SelectedNodesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedNodes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedNodesProperty =
            DependencyProperty.Register("SelectedNodes", typeof(ObservableCollection<TreeNode>), typeof(ListTreeControl), new PropertyMetadata(new ObservableCollection<TreeNode>()));
        
        public int SelectedCount
        {
            get { return (int)GetValue(SelectedCountProperty); }
            set { SetValue(SelectedCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedCountProperty =
            DependencyProperty.Register("SelectedCount", typeof(int), typeof(ListTreeControl), new PropertyMetadata(0));
        
        private void lsbIncludes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var node = lsbIncludes.SelectedItem as TreeNode;
            if (node == null)
                return;

            SelectedNode = node;
        }

        private async void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var nodeCb = e.Source as CheckBox;
            var node = nodeCb?.DataContext as TreeNode;
            if (node == null)
                return;

            SelectedNode = node;
            SelectedNodes.Add(SelectedNode);

            if (node is FolderNode)
            {
                await Task.Run(() =>
                {
                    foreach (var item in node.Descendents.Where(p => !p.IsSelected))
                    {
                        item.IsSelected = true;
                        var del = new TreeNodeDelegate(AddSelectedNode);
                        App.Current.Dispatcher.Invoke(del, item);
                    }
                });
            }

            UpdateCount();
        }

        private async void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var nodeCb = e.Source as CheckBox;
            var node = nodeCb?.DataContext as TreeNode;
            if (node == null)
                return;

            SetSelectedNode(node);
            SelectedNodes.Remove(SelectedNode);

            if (node is FolderNode)
            {
                await Task.Run(() =>
                {
                    foreach (var item in node.Descendents.Where(p => p.IsSelected))
                    {
                        item.IsSelected = false;
                        var del = new TreeNodeDelegate(RemoveSelectedNode);
                        App.Current.Dispatcher.Invoke(del, item);
                    }
                });
            }

            UpdateCount();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in Nodes)
            {
                item.IsSelected = false;
                foreach (var child in item.Descendents)
                {
                    child.IsSelected = false;
                }
            }
            SelectedNodes.Clear();
        }

        // Delegate methods
        private delegate void CheckboxDelegate(object sender, RoutedEventArgs e);
        private delegate void TreeNodeDelegate(TreeNode node);

        private void UpdateCount()
        {
            SelectedCount = SelectedNodes.Count();
        }
        private void SetSelectedNode(TreeNode node)
        {
            SelectedNode = node;
        }
        private void AddSelectedNode(TreeNode node)
        {
            SelectedNodes.Add(node);
        }
        private void RemoveSelectedNode(TreeNode node)
        {
            SelectedNodes.Remove(node);
        }
    }
}
