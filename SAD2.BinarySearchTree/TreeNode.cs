namespace SAD2.BinarySearchTree
{
	public class TreeNode
	{
		public TreeNode Left, Right;
		public string Name;
		public double Value;

		public TreeNode(string name, double d)
		{
			Name = name;
			Value = d;
			Left = null;
			Right = null;
		}
	}
}