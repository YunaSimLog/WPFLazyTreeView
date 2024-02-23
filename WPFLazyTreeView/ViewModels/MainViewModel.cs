using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFLazyTreeView.Models;

namespace WPFLazyTreeView.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
            PathNodes = new();
            PathNodes.Add(new LazyTreeNode { Key = "1", Text = "홍길동" });
            PathNodes.Add(new LazyTreeNode { Key = "2", Text = "유관순" });
        }

        public ObservableCollection<LazyTreeNode> PathNodes { get; set; }
    }
}
