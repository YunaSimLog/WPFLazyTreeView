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
            PathNodes.Add(CreateNode("1", "홍길동"));
            PathNodes.Add(CreateNode("2", "유관순"));
        }

        public ObservableCollection<LazyTreeNode> PathNodes { get; set; }

        public LazyTreeNode CreateNode(string key, string text)
        {
            var node = new LazyTreeNode { Key = key, Text = text };
            node.OnExpand += Node_OnExpanded;
            return node;
        }

        private void Node_OnExpanded(LazyTreeNode node)
        {
            node.Children.Add(CreateNode("3","가나다"));
        }
    }
}
