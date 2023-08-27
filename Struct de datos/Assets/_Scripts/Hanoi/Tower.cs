using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Stack<Disc> towerStack;
    [SerializeField] private bool isStarterTower;

    private void Start()
    {
        towerStack = new Stack<Disc>();

        //Inicializar el stack de la torre donde los discos comienzan el juego y decirle a cada disco que torre es su dueño
        if (isStarterTower)
        {
            for (int i = HanoiManager.Instance.GameDiscs.Length - 1; i >= 0; i--)
            {
                HanoiManager.Instance.GameDiscs[i].TowerOwner = this;
                towerStack.Push(HanoiManager.Instance.GameDiscs[i]);
            }
        }
    }

    public void TryPlaceDiscInNewTower(Tower previousTower, Disc incomingDisc)
    {
        if (previousTower.towerStack.Count > 0)
        {
            Disc previousTowerUpperDisc = previousTower.towerStack.Peek();
            print("disco arriba de todo es " + previousTowerUpperDisc.name);

            if (incomingDisc.DiscSize > previousTowerUpperDisc.DiscSize)
            {
                print("No se puede sacar, hay discos por encima de este en esta torre");
            }
            else
            {
                //Si la nueva tower a donde se esta moviendo el disco tiene discos fijarse cual es el que esta arriba de todo
                if (towerStack.Count > 0)
                {
                    Disc upperDisc = towerStack.Peek();

                    //Si el disco entra (porque el que esta ultimo es mas grande que el disco que entra),
                    //sacarlo del stack de la torre anterior y ponerlo en el nuevo
                    if (upperDisc.DiscSize > incomingDisc.DiscSize)
                    {
                        SwapDiscs(previousTower, incomingDisc);
                        print("colocnado disco" + incomingDisc.name + " en nueva torre " + this.name);
                    }
                    //Si el disco que entra es mas grande que el ultimo disco de la torre, no se puede
                    else
                    {
                        print("no se puede, el disco es mas grande que el que esta primero");
                    }
                }
                //Si tower esta vacio se pone el disco que viene
                else
                {
                    SwapDiscs(previousTower, incomingDisc);
                    print("no hay discos en la nueva torre");
                }
            }
        }
        else print("No hay discos disponibles en la torre de la que se intenta mover discos");
    }

    private void SwapDiscs(Tower previousTower, Disc incomingDisc)
    {
        towerStack.Push(incomingDisc);
        previousTower.towerStack.Pop();

        //Mover el disco a la pos de la nueva torre e informale que tiene nuevo dueño porque asi es la vida
        incomingDisc.transform.position = this.transform.position;
        incomingDisc.TowerOwner = this;
    }
}
