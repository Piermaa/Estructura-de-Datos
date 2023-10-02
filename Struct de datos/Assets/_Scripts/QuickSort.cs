using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class QuickSort<T> where T : IElementoConPrioridad
{
    public int Partition(T[] arr, int left, int right)
    {
        int pivot;
        int aux = (left + right) / 2; //tomo el valor central del vector
        pivot = arr[aux].Priority;

        // en este ciclo debo dejar todos los valores menores al pivot
        // a la izquierda y los mayores a la derecha
        while (true)
        {
            while (arr[left].Priority < pivot)
            {
                left++;
            }

            while (arr[right].Priority > pivot)
            {
                right--;
            }

            if (left < right)
            {
                T temp = arr[right];
                arr[right] = arr[left];
                arr[left] = temp;
            }
            else
            {
                // este es el valor que devuelvo como proxima posicion de
                // la particion en el siguiente paso del algoritmo
                return right;
            }
        }
    }

    public void Sort(T[] arr, int left, int right)
    {
        int pivot;
        if (left < right)
        {
            pivot = Partition(arr, left, right);

            if (pivot > 1)
            {
                // mitad del lado izquierdo del vector
                Sort(arr, left, pivot - 1);
            }

            if (pivot + 1 < right)
            {
                // mitad del lado derecho del vector
                Sort(arr, pivot + 1, right);
            }
        }
    }
}
