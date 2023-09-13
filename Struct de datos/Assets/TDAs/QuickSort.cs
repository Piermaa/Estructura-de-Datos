using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PlayerQS
{
    public string name;
    public int score;

}

public class QuickSort<T> where T : IElementoConPrioridad, new()
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

    public void quickSort(T[] arr, int left, int right)
    {
        int pivot;
        if (left < right)
        {
            pivot = Partition(arr, left, right);

            if (pivot > 1)
            {
                // mitad del lado izquierdo del vector
                quickSort(arr, left, pivot - 1);
            }

            if (pivot + 1 < right)
            {
                // mitad del lado derecho del vector
                quickSort(arr, pivot + 1, right);
            }
        }
    }

    // static void imprimirVector(T[] vec)
    // {
    //     for (int i = 0; i < vec.Length; i++)
    //     {
    //         Console.WriteLine(vec[i].name + " " + vec[i].Priority);
    //     }
    // }

    void Main(string[] args)
    {
        // creo el vector de enteros para ordenar
        //int[] vectorEnteros = { 67, 12, 95, 56, 85, 1, 100, 23, 60, 9 };

        PlayerQS[] arrayPlayer = new PlayerQS[10];
        Random rnd = new Random();
        for (int i = 0; i < 10; i++)
        {
            arrayPlayer[i] = new PlayerQS();
            arrayPlayer[i].name = "Player_" + i.ToString();
            arrayPlayer[i].score = rnd.Next(1, 100);
        }

        Console.WriteLine("Inicio Programa: Quick Sort");

        // muestro vector desordenado
        Console.Write("\nLista Desordenada: ");
     //   imprimirVector(arrayPlayer);

        // algoritmo de ordenamiento
        // inicialmente los parametros left y right son los extremos del vector
  //      quickSort(arrayPlayer, 0, arrayPlayer.Length - 1);

        // muestro vector ordenado
        Console.Write("\nLista Ordenada: ");
     //   imprimirVector(arrayPlayer);

        Console.ReadKey();
    }
}