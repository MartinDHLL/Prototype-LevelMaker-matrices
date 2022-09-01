using System.Collections.Generic;
using UnityEngine;

public class LevelManagement : MonoBehaviour
{
    struct Level {
        string _name;
        string _author;
        int _size;

        public string name {
            get => _name;
            set => _name = value;
        }

        public string author {
            get => _author;
            set => _author = value;
        }

        public int size {
            get => _size;
            set => _size = value;
        }
    }

    Level thisLevel;
    List<GameObject> squares;
    public GameObject squareVariant;
    private bool isMaking, itemPlaced;

    private void Start() {
        thisLevel = new Level();
        thisLevel.name = "test";
        thisLevel.author = "Martin";
        thisLevel.size = 10;
        isMaking = true;
    }

    private void Update() {
        // configurer entrées texte pour indiquer le titre, l'auteur et la taille du niveau

        if(isMaking)
        {
            MakeLevel(new Vector3(0,0,0));
        }
        
        foreach (GameObject square in squares)
        {
            // pour un objet occupant 2 emplacements à la verticale
            if(square.GetComponent<squareController>().isClicked) {

                int calculation = square.GetComponent<squareController>().index - thisLevel.size;
                print("index = " + square.GetComponent<squareController>().index);
                print("calcul = " + calculation);

                if(calculation > 0 && !squares[calculation - 1].GetComponent<squareController>().isBusy && !square.GetComponent<squareController>().isBusy)
                {
                    PlaceItem(new GameObject(), square.GetComponent<squareController>().index, calculation - 1); // Mettre à jour avec un autre objet
                    square.GetComponent<squareController>().isClicked = false;
                }
                else
                {
                    
                    // error message : "the top place is busy"
                    square.GetComponent<squareController>().isClicked = false;
                }

            }
                
        }
    }
    
    /* 
    </summary> Instanciate the making area of the level <summary> 
    */
    private void MakeLevel(Vector3 squaresInitialPosition) {
        int incrementx = 0;
        int incrementy = 0;
        squares = new List<GameObject>();

        // vertical
        // z = incrementy
        for (int y = 0; y < thisLevel.size; y++)
        {
            // horizontal
            incrementx = 0;
            for (int x = 0; x < thisLevel.size; x++)
            {
                squares.Add(Instantiate(squareVariant, squaresInitialPosition + new Vector3(-incrementx,0,incrementy), Quaternion.Euler(90,0,0)));
                squares[
                    y == 0 ? x : 
                    int.Parse(y.ToString() + x.ToString())
                    ]
                    .GetComponent<squareController>()
                    .index = y == 0 ? x + 1 : 
                    int.Parse(y.ToString() + x.ToString()) + 1 ;
                incrementx += 2;
            }
            incrementy += 2;
        }
        foreach(GameObject square in squares)
        {
            square.GetComponent<squareController>().isBusyByDefault = square.GetComponent<squareController>().index <= thisLevel.size ? true 
            : false;
        }
        isMaking = false;
    }

    private void PlaceItem(GameObject item, int index, int indexCalculation) {
        if(!itemPlaced)
        {
            squares[index -1].GetComponent<squareController>().isBusy = true;
            squares[indexCalculation].GetComponent<squareController>().isBusy = true;
            print(index + " placed");
            // place item
        }
    }

}
