using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class Vaga : MonoBehaviour
{
    public int empresaId;
    public int vagaId;
    public string title;
    public string desc;
    public string adress;
    public string mentorName;

    public int formacaoMinima;
    public int state;
    public int city;

    public string date;//DATA de inicio
    public int cargaHrPorDia;//De 2 a 4 hrs

    void Start()
    {
        
    }

   
}
