using System;
using static System.String;

namespace SAD2.BinarySearchTree
{
	public class BinarySearchTree
	{
		private int _count;
		private TreeNode _root;

		public BinarySearchTree()
		{
			_root = null;
			_count = 0;
		}

		private void killTree(ref TreeNode p)
		{
			if (p != null)
			{
				killTree(ref p.Left);
				killTree(ref p.Right);
				p = null;
			}
		}

		public void Clear()
		{
			killTree(ref _root);
			_count = 0;
		}

		public int Count()
		{
			return _count;
		}

		public TreeNode FindSymbol(string name)
		{
			var np = _root;
			int cmp;
			while (np != null)
			{
				cmp = CompareOrdinal(name, np.Name);
				if (cmp == 0)
					return np;

				if (cmp < 0)
					np = np.Left;
				else
					np = np.Right;
			}
			return null;
		}

		private void add(TreeNode node, ref TreeNode tree)
		{
			if (tree == null)
				tree = node;
			else
			{
				var comparison = CompareOrdinal(node.Name, tree.Name);
				if (comparison == 0)
					throw new Exception();

				if (comparison < 0)
				{
					add(node, ref tree.Left);
				}
				else
				{
					add(node, ref tree.Right);
				}
			}
		}

		public TreeNode Insert(string name, double d)
		{
			var node = new TreeNode(name, d);
			try
			{
				if (_root == null)
					_root = node;
				else
					add(node, ref _root);
				_count++;
				return node;
			}
			catch (Exception)
			{
				return null;
			}
		}

		private TreeNode FindParent(string name, ref TreeNode parent)
		{
			var np = _root;
			parent = null;
			int cmp;
			while (np != null)
			{
				cmp = Compare(name, np.Name);
				if (cmp == 0)
					return np;

				if (cmp < 0)
				{
					parent = np;
					np = np.Left;
				}
				else
				{
					parent = np;
					np = np.Right;
				}
			}
			return null;
		}

		public TreeNode FindSuccessor(TreeNode startNode, ref TreeNode parent)
		{
			parent = startNode;

			startNode = startNode.Right;
			while (startNode.Left != null)
			{
				parent = startNode;
				startNode = startNode.Left;
			}
			return startNode;
		}

		public void Delete(string key)
		{
			TreeNode parent = null;

			var nodeToDelete = FindParent(key, ref parent);
			if (nodeToDelete == null)
				throw new Exception("Unable to Delete node: " + key);

			if ((nodeToDelete.Left == null) && (nodeToDelete.Right == null))
			{
				if (parent == null)
				{
					_root = null;
					return;
				}

				if (parent.Left == nodeToDelete)
					parent.Left = null;
				else
					parent.Right = null;
				_count--;
				return;
			}

			if (nodeToDelete.Left == null)
			{

				if (parent == null)
				{
					_root = nodeToDelete.Right;
					return;
				}


				if (parent.Left == nodeToDelete)
					parent.Right = nodeToDelete.Right;
				else
					parent.Left = nodeToDelete.Right;
				_count--;
				return;
			}

			if (nodeToDelete.Right == null)
			{

				if (parent == null)
				{
					_root = nodeToDelete.Left;
					return;
				}


				if (parent.Left == nodeToDelete)
					parent.Left = nodeToDelete.Left;
				else
					parent.Right = nodeToDelete.Left;
				_count--;
				return;
			}

			var successor = FindSuccessor(nodeToDelete, ref parent);
			var tmp = new TreeNode(successor.Name, successor.Value);
			if (parent.Left == successor)
				parent.Left = null;
			else
				parent.Right = null;
			nodeToDelete.Name = tmp.Name;
			nodeToDelete.Value = tmp.Value;
			_count--;
		}

		private string drawNode(TreeNode node)
		{
			if (node == null)
				return "empty";

			if ((node.Left == null) && (node.Right == null))
				return node.Name;
			if ((node.Left != null) && (node.Right == null))
				return node.Name + "(" + drawNode(node.Left) + ", _)";

			if ((node.Right != null) && (node.Left == null))
				return node.Name + "(_, " + drawNode(node.Right) + ")";

			return node.Name + "(" + drawNode(node.Left) + ", " + drawNode(node.Right) + ")";
		}
		public string DrawTree()
		{
			return drawNode(_root);
		}
	}
}