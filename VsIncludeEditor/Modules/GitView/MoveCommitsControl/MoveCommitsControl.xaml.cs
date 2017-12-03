using System.Windows;
using System.Windows.Controls;
using VsIncludeEditor.Services;

namespace VsIncludeEditor.Modules.GitView.MoveCommitsControl
{
    /// <summary>
    /// Interaction logic for MoveCommitsControl.xaml
    /// </summary>
    public partial class MoveCommitsControl : UserControl
    {
        public MoveCommitsControl()
        {
            InitializeComponent();
            Wrapper.DataContext = this;
        }



        public string GitPath
        {
            get { return (string)GetValue(GitPathProperty); }
            set
            {
                SetValue(GitPathProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GitPathProperty =
            DependencyProperty.Register("GitPath", typeof(string), typeof(MoveCommitsControl), new PropertyMetadata(null));



        public string NewBranchName
        {
            get { return (string)GetValue(NewBranchNameProperty); }
            set { SetValue(NewBranchNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NewBranchName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NewBranchNameProperty =
            DependencyProperty.Register("NewBranchName", typeof(string), typeof(MoveCommitsControl), new PropertyMetadata(null));



        public bool RollBack
        {
            get { return (bool)GetValue(RollBackProperty); }
            set { SetValue(RollBackProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RollBack.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RollBackProperty =
            DependencyProperty.Register("RollBack", typeof(bool), typeof(MoveCommitsControl), new PropertyMetadata(false));



        public string RollbackAmount
        {
            get { return (string)GetValue(RollbackAmountProperty); }
            set { SetValue(RollbackAmountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RollbackAmount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RollbackAmountProperty =
            DependencyProperty.Register("RollbackAmount", typeof(string), typeof(MoveCommitsControl), new PropertyMetadata("0"));



        public bool RollbackAll
        {
            get { return (bool)GetValue(RollbackAllProperty); }
            set { SetValue(RollbackAllProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RollbackAll.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RollbackAllProperty =
            DependencyProperty.Register("RollbackAll", typeof(bool), typeof(MoveCommitsControl), new PropertyMetadata(false));



        public bool DeleteNewBranch
        {
            get { return (bool)GetValue(DeleteNewBranchProperty); }
            set { SetValue(DeleteNewBranchProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DeleteNewBranch.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeleteNewBranchProperty =
            DependencyProperty.Register("DeleteNewBranch", typeof(bool), typeof(MoveCommitsControl), new PropertyMetadata(false));

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NewBranchName))
                return;

            int.TryParse(RollbackAmount, out int rbAmnt);

            var success = GitService.MoveCommitsToNewBranch(GitPath, NewBranchName, RollbackAll, rbAmnt, DeleteNewBranch);
        }
    }
}
