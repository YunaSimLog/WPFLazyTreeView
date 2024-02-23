using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFLazyTreeView.Models
{
    public class LazyTreeNode
    {
        // 자식노드의 부모가 누구인지 할당
        private void Children_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (LazyTreeNode node in e.NewItems!)
                {
                    node.Parent = this;
                }
            }
        }

        // 노드 트리가 펼쳐졌을 때 이벤트
        public event Action<LazyTreeNode>? OnExpand;

        private bool _isExpanded;
        private ObservableCollection<LazyTreeNode>? _children;
        public string Key { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public object? Tag { get; set; }

        public LazyTreeNode? Parent { get; set; }

        public ObservableCollection<LazyTreeNode> Children
        {
            get
            {
                if (_children == null)
                {
                    _children = new ObservableCollection<LazyTreeNode>();
                    _children.CollectionChanged += Children_CollectionChanged;
                }
                    
                return _children;
            }
            set => _children = value;
        }

        public bool IsExpanded
        {
            get =>_isExpanded;
            set
            {
                // 펼침 상태 속성이 True로 될 경우, 아래 이벤트 동작되게 처리
                _isExpanded=value; 
                if (_isExpanded)
                {
                    OnExpand?.Invoke(this);
                }
            }
        }
    }
}
