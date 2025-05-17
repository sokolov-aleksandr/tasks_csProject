using System;
using System.Text;
using System.Collections.Generic;

public enum SortType { QuickSort, TreeSort }

/// <summary>
/// Сортировщик символов в строке
/// </summary>
public static class StringSorter
{
    public static string SortByEnumType(string input, SortType sortType)
    {
        switch(sortType)
        {
            case SortType.QuickSort:
                return SortWithQuickSort(input);
            case SortType.TreeSort:
                return SortWithTreeSort(input);
            default:
                throw new ArgumentException($"{sortType.ToString()} не существует!");
        }
    }
    
    /// <summary>
    /// Сортировка строки быстрой сортировкой
    /// </summary>
    /// <param name="input">Строка для сортировки</param>
    /// <returns>Отсортированная строка</returns>
    public static string SortWithQuickSort(string input)
    {
        char[] chars = input.ToCharArray();
        QuickSort(chars, 0, chars.Length - 1);
        return new string(chars);
    }

    private static void QuickSort(char[] arr, int left, int right)
    {
        if (left >= right)
            return;

        int pivotIndex = Partition(arr, left, right);
        QuickSort(arr, left, pivotIndex - 1);
        QuickSort(arr, pivotIndex + 1, right);
    }

    private static int Partition(char[] arr, int left, int right)
    {
        char pivot = arr[right];
        int i = left - 1;

        for (int j = left; j < right; j++)
        {
            if (arr[j] <= pivot)
            {
                i++;
                (arr[i], arr[j]) = (arr[j], arr[i]);
            }
        }

        (arr[i + 1], arr[right]) = (arr[right], arr[i + 1]);
        return i + 1;
    }

    /// <summary>
    /// Сортировка строки с помощью двоичного дерева (TreeSort)
    /// </summary>
    /// <param name="input">Строка для сортировки</param>
    /// <returns>Отсортированная строка</returns>
    public static string SortWithTreeSort(string input)
    {
        TreeNode root = null;
        foreach (char c in input)
        {
            root = Insert(root, c);
        }

        var result = new StringBuilder();
        InOrderTraversal(root, result);
        return result.ToString();
    }

    private class TreeNode
    {
        public char Value;
        public TreeNode Left;
        public TreeNode Right;

        public TreeNode(char value)
        {
            Value = value;
        }
    }

    private static TreeNode Insert(TreeNode node, char value)
    {
        if (node == null)
            return new TreeNode(value);

        if (value <= node.Value)
            node.Left = Insert(node.Left, value);
        else
            node.Right = Insert(node.Right, value);

        return node;
    }

    private static void InOrderTraversal(TreeNode node, StringBuilder result)
    {
        if (node == null)
            return;

        InOrderTraversal(node.Left, result);
        result.Append(node.Value);
        InOrderTraversal(node.Right, result);
    }
}
