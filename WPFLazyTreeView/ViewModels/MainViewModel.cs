using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFLazyTreeView.Models;
using WPFLazyTreeView.Utils;

namespace WPFLazyTreeView.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
            PathNodes = new();
            AddDriveNodes();
        }

        public ObservableCollection<LazyTreeNode> PathNodes { get; set; }

        public LazyTreeNode CreateNode(string key, string text)
        {
            var node = new LazyTreeNode { Key = key, Text = text };
            node.OnExpand += Node_OnExpanded;

            if (DirectoryUtils.IsDirectoryOrFileExists(key))
            {
                node.AddDummyNode();
            }

            return node;
        }

        private void Node_OnExpanded(LazyTreeNode node)
        {
            foreach (var di in DirectoryUtils.GetDirectories(node.Key))
            {
                node.Children.Add(CreateNode(di.FullName, di.Name));
            }

            foreach (var fi in DirectoryUtils.GetFiles(node.Key))
            {
                node.Children.Add(CreateNode(fi.FullName, fi.Name));
            }
        }

        private void AddDriveNodes()
        {
            foreach (var driveInfo in DriveInfo.GetDrives())
            {
                PathNodes.Add(CreateNode(driveInfo.Name, driveInfo.Name));
            }
        }
    }
}
