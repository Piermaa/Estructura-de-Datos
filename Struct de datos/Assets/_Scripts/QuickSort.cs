using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class QuickSort<T> where T : IElementoConPrioridad, new()
{
    public static void QuickSortMethod(T[] arr, int left, int right)
    {
        if (left < right)
        {
            int pivot = Partition(arr, left, right);
            if (pivot > 1)
            {
                QuickSortMethod(arr, left, pivot - 1);
            }

            if (pivot + 1 < right)
            {
                QuickSortMethod(arr, pivot + 1, right);
            }
        }
    }

    private static int Partition(T[] arr, int left, int right)
    {
        int pivot = arr[right].Priority;
        int i = left - 1;

        for (int j = left; j < right; j++)
        {
            if (arr[j].Priority <= pivot)
            {
                i++;
                Swap(arr, i, j);
            }
        }

        Swap(arr, i + 1, right);
        return i + 1;
    }

    private static void Swap(T[] arr, int i, int j)
    {
        T temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
    }
}
