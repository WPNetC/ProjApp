﻿using System;
using System.Collections.Generic;
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
    /// Interaction logic for TreeViewControl.xaml
    /// </summary>
    public partial class TreeViewControl : UserControl
    {
        public TreeViewControl()
        {
            InitializeComponent();
            trvIncludes.DataContext = this;
        }

        public ICollection<TreeNode> Nodes
        {
            get { return (ICollection<TreeNode>)GetValue(NodesProperty); }
            set { SetValue(NodesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for list.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NodesProperty =
            DependencyProperty.Register("Nodes", typeof(ICollection<TreeNode>), typeof(TreeViewControl), new PropertyMetadata(null));



        public TreeNode SelectedNode
        {
            get { return (TreeNode)GetValue(SelectedNodeProperty); }
            set { SetValue(SelectedNodeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for treeNode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedNodeProperty =
            DependencyProperty.Register("SelectedNode", typeof(TreeNode), typeof(TreeViewControl), new PropertyMetadata(null));

        public HashSet<TreeNode> SelectedNodes
        {
            get
            {
                return (HashSet<TreeNode>)GetValue(SelectedNodesProperty);
            }
            set { SetValue(SelectedNodesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedNodes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedNodesProperty =
            DependencyProperty.Register("SelectedNodes", typeof(HashSet<TreeNode>), typeof(TreeViewControl), new PropertyMetadata(new HashSet<TreeNode>()));



        public int SelectedCount
        {
            get { return (int)GetValue(SelectedCountProperty); }
            set { SetValue(SelectedCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedCountProperty =
            DependencyProperty.Register("SelectedCount", typeof(int), typeof(TreeViewControl), new PropertyMetadata(0));
        

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var nodeCb = e.Source as CheckBox;
            var node = nodeCb?.DataContext as TreeNode;
            if (node == null)
                return;

            SelectedNode = node;
            SelectedNodes.Add(SelectedNode);
            if (node is FolderNode)
            {
                foreach (var item in SelectedNode.Descendents)
                {
                    if (!item.IsSelected)
                    {
                        item.IsSelected = true;
                        SelectedNodes.Add(item);
                    }
                }
            }

            SelectedCount = SelectedNodes.Count();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var nodeCb = e.Source as CheckBox;
            var node = nodeCb?.DataContext as TreeNode;
            if (node == null)
                return;

            SelectedNode = node;
            SelectedNodes.Remove(SelectedNode);
            if (node is FolderNode)
            {
                foreach (var item in SelectedNode.Descendents)
                {
                    if (item.IsSelected)
                    {
                        item.IsSelected = false;
                        SelectedNodes.Remove(item);
                    }
                }
            }

            SelectedCount = SelectedNodes.Count();
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

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var node = trvIncludes.SelectedItem as TreeNode;
            if (node == null)
                return;

            SelectedNode = node;
        }
    }
}
