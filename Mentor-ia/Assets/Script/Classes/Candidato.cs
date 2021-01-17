using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Candidato {

    public int id;
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

    public int ratingMax;
    public int rankInteresse;
    public int rankConhecGerais;
    public int rankComunicacao;
    public int rankPensLogico;

    public string diferencial;
    public string motivacao;
    

    public Candidato() {
        this.cpf = "00011122233";
        this.name = "Nome Exemplo";
        this.email = "emailexemplo@hotmail.com";
        this.telNumber = "21999887766";
        this.age = 18;
        this.raceSkin = 1;
        this.state = 1;
        this.city = 1;
        this.gender = 1;
        this.formacao = 1;
        this.areaDeInteresse1 = 1;
        this.areaDeInteresse2 = 1;
        this.diferencial = "Luto kung fu nas horas vagas.";
        this.motivacao = "Ser um faixa preta.";

        this.ratingMax = 0;
        this.rankInteresse = 60;
        this.rankConhecGerais = 60;
        this.rankComunicacao = 60;
        this.rankPensLogico = 60;
}

    
}
