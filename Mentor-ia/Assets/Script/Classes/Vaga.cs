using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum MENTOR_TYPE{
    BASIC_FREE,
    TEST_AND_PAID
}

[Serializable]
public class Vaga
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

    public string date_hr;//DATA de inicio
    public MENTOR_TYPE mentor_type;

    public List<String> cpfs_candidatos;

    //MODO 1 - MENTORIA GRATUITA EM 1 DIA APENAS (CERCA DE 1 HRS), TOUR PELA EMPRESA E AS FUNÇÕES DA VAGA DIVULGADA NA MENTORIA DELA E BANCO DE VAGAS
    //MODO 2 - MENTORIA PAGA DE 50 REAIS EM 1 DIAS APENAS, PODE PEDIR PARA O CANDIDATO REALIZAR UMA PROVA

    public Vaga() {
        cpfs_candidatos = new List<string>();
    }

    public void CandidatarAvaga(string cpf_cand) {
        this.cpfs_candidatos.Add(cpf_cand);
    }

    public bool HasCpf(string s) {
        foreach (String cpf in cpfs_candidatos) {
            if (cpf == s) return true;
        }
        return false;
    }

    public void RandomCandToVaga() {
        System.Random rnd = new System.Random();
        int r1 = rnd.Next(0,10);
        int r2 = rnd.Next(0, 10);
        int r3 = rnd.Next(0, 10);
        CandidatarAvaga(r1 + "");
        CandidatarAvaga(r2 + "");
        CandidatarAvaga(r3 + "");
    }

   
}
