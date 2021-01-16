using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Candidato {

    public string cpf;
    public string name;
    public string email;
    public string telNumber;

    public int age;
    public int raceSkin;
    public int state;
    public int city;
    public int gender;
    public int formacao;

    public int areaDeInteresse1;
    public int areaDeInteresse2;

    public float rankInteresse;
    public float rankConhecGerais;
    public float rankComunicacao;
    public float rankPensLogico;

    public int empresaFocoId;
    public int empresaFocoId2;

    public string diferencial;
    public string motivacao;

    void Start()
    {
        
    }

    
}
