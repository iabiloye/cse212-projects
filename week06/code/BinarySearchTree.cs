using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;


public class BinarySearchTree : IEnumerable<int>
{
    private Node? _root;

    /// <summary>
    /// Insert a new node in the BST.
    public class Node
    {
        public int Data { get; set; }
        public Node? Left { get; set; }
        public Node? Right { get; set; }

        public Node(int data)
        {
            Data = data;
            Left = null;
            Right = null;
        }
        /// <summary>
        /// inserting a unique values
        /// ensuring duplictae cannot be inserted
        /// </summary>
        /// <param name="value"></param>
        public void Insert(int value)
        {
            if (value == Data)
            {
                return; /// insert no duplicates
            }
            if (value < Data)
            {
                if (Left is null)
                {
                    Left = new Node(value);
                }
                else
                {
                    Left.Insert(value);
                }
            }
            else if (value > Data)
            {
                if (Right is null)
                {
                    Right = new Node(value);
                }
                else
                {
                    Right.Insert(value);
                }
            }
        }
        /// Contain
        /// Check to see if the tree contains a certain value

        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(int value)
        {
            if (value == Data)
            {
                return true;
            }
            else if (value < Data)
            {
                return Left != null && Left.Contains(value);
            }
            else
            {
                return Right != null && Right.Contains(value);
            }
        }

        public int GetHeight()
        {
            int leftHeight = Left?.GetHeight() ?? 0;
            int rightHeight = Right?.GetHeight() ?? 0;
            return Math.Max(leftHeight, rightHeight) + 1;
        }
    }
    
    /// </summary>
    public void Insert(int value)
    {
        // Create new node
        Node newNode = new(value);
        // If the list is empty, then point both head and tail to the new node.
        if (_root is null)
        {
            _root = new Node(value);
        }
        // If the list is not empty, then only head will be affected.
        else
        {
            _root.Insert(value);
        }
    }

    /// <summary>
    /// Check to see if the tree contains a certain value
    /// </summary>
    /// <param name="value">The value to look for</param>
    /// <returns>true if found, otherwise false</returns>
    public bool Contains(int value)
    {
        return _root != null && _root.Contains(value);
    }

    /// <summary>
    /// Yields all values in the tree
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        // call the generic version of the method
        return GetEnumerator();
    }

    /// <summary>
    /// Iterate forward through the BST
    /// </summary>
    public IEnumerator<int> GetEnumerator()
    {
        var numbers = new List<int>();
        TraverseForward(_root, numbers);
        foreach (var number in numbers)
        {
            yield return number;
        }
    }

    private void TraverseForward(Node? node, List<int> values)
    {
        if (node is not null)
        {
            TraverseForward(node.Left, values);
            values.Add(node.Data);
            TraverseForward(node.Right, values);
        }
    }

    /// <summary>
    /// Iterate backward through the BST.
    /// </summary>
    public IEnumerable Reverse()
    {
        var numbers = new List<int>();
        TraverseBackward(_root, numbers);
        foreach (var number in numbers)
        {
            yield return number;
        }
    }

    private void TraverseBackward(Node? node, List<int> values)
    {
        // TODO Problem 3
        /// Traversing the right subtree first, then the node itself, and finally the left subtree
        if (node is not null)
        {
            TraverseBackward(node.Right, values);
            values.Add(node.Data);
            TraverseBackward(node.Left, values);
        }
    }

    /// <summary>
    /// Get the height of the tree
    /// </summary>
    public int GetHeight()
    {
        if (_root is null)
            return 0;
        return _root.GetHeight();
    }

    public override string ToString()
    {
        return "<Bst>{" + string.Join(", ", this) + "}";
    }
}

public static class IntArrayExtensionMethods
{
    public static string AsString(this IEnumerable array)
    {
        return "<IEnumerable>{" + string.Join(", ", array.Cast<int>()) + "}";
    }
}
