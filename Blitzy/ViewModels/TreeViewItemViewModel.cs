using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;

namespace Blitzy.ViewModels
{
	internal interface ITreeViewItemViewModel
	{
		event EventHandler<TreeViewSelectionEventArgs> SelectionChanged;

		ICollection<ITreeViewItemViewModel> Children { get; }
		bool IsExpanded { get; set; }
		bool IsSelected { get; set; }
		ITreeViewItemViewModel Parent { get; }
		string Text { get; }
	}

	internal class TreeViewItemViewModel : ObservableObject, ITreeViewItemViewModel
	{
		protected TreeViewItemViewModel( ITreeViewItemViewModel parent, string title, bool lazyLoadChildren = false )
		{
			Text = title;
			Parent = parent;

			ObservableChildren = new ObservableCollection<ITreeViewItemViewModel>();

			if( lazyLoadChildren )
			{
				Children.Add( DummyChild );
			}
		}

		// This is used to create the DummyChild instance.
		private TreeViewItemViewModel()
		{
		}

		public event EventHandler<TreeViewSelectionEventArgs> SelectionChanged;

		protected virtual void LoadChildren()
		{
		}

		private void RaiseSelectionChanged( ITreeViewItemViewModel selectedNode )
		{
			SelectionChanged?.Invoke( this, new TreeViewSelectionEventArgs( selectedNode ) );
		}

		public ICollection<ITreeViewItemViewModel> Children => ObservableChildren;
		public bool HasDummyChild => Children.FirstOrDefault() == DummyChild;

		public bool IsExpanded
		{
			get { return _IsExpanded; }
			set
			{
				if( value != _IsExpanded )
				{
					_IsExpanded = value;
					RaisePropertyChanged();
				}

				// Expand all the way up to the root.
				if( _IsExpanded && Parent != null )
				{
					Parent.IsExpanded = true;
				}

				// Lazy load the child items, if necessary.
				if( HasDummyChild )
				{
					Children.Remove( DummyChild );
					LoadChildren();
				}
			}
		}

		public bool IsSelected
		{
			get { return _IsSelected; }
			set
			{
				if( value != _IsSelected )
				{
					_IsSelected = value;
					RaisePropertyChanged();
				}

				if( IsSelected )
				{
					if( Parent == null )
					{
						RaiseSelectionChanged( this );
					}
					else
					{
						var p = Parent as TreeViewItemViewModel;
						while( p?.Parent != null )
						{
							p = p.Parent as TreeViewItemViewModel;
						}

						p?.RaiseSelectionChanged( this );
					}
				}
			}
		}

		public ITreeViewItemViewModel Parent { get; }
		public string Text { get; }
		protected ObservableCollection<ITreeViewItemViewModel> ObservableChildren { get; }
		private static readonly TreeViewItemViewModel DummyChild = new TreeViewItemViewModel();

		private bool _IsExpanded;
		private bool _IsSelected;
	}

	internal class TreeViewSelectionEventArgs : EventArgs
	{
		public TreeViewSelectionEventArgs( ITreeViewItemViewModel selectedNode )
		{
			SelectedNode = selectedNode;
		}

		public ITreeViewItemViewModel SelectedNode { get; }
	}
}