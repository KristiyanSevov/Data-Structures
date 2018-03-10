using System;

public class BinaryTree<T>
{
    private T value;
    private BinaryTree<T> leftChild;
    private BinaryTree<T> rightChild;

    public BinaryTree(T value, BinaryTree<T> leftChild = null, BinaryTree<T> rightChild = null)
    {
        this.value = value;
        this.leftChild = leftChild;
        this.rightChild = rightChild;
    }

    public void PrintIndentedPreOrder(int indent = 0)
    {
        BinaryTree<T> root = this;
        this.PrintPreOrder(root, indent);
    }

    private void PrintPreOrder(BinaryTree<T> node, int indent)
    {
        if (node != null)
        {
            Console.WriteLine(new string(' ', indent) + node.value);
            PrintPreOrder(node.leftChild, indent + 2);
            PrintPreOrder(node.rightChild, indent + 2);
        }
    }

    public void EachInOrder(Action<T> action)
    {
        if (this.leftChild != null)
        {
            this.leftChild.EachInOrder(action);
        }
        action(this.value);
        if (this.rightChild != null)
        {
            this.rightChild.EachInOrder(action);
        }
    }

    public void EachPostOrder(Action<T> action)
    {
        BinaryTree<T> root = this;
        this.EachPostOrder(action, root);
    }

    private void EachPostOrder(Action<T> action, BinaryTree<T> node)
    {
        if (node != null)
        {
            EachPostOrder(action, node.leftChild);
            EachPostOrder(action, node.rightChild);
            action(node.value);
        }
    }
}
