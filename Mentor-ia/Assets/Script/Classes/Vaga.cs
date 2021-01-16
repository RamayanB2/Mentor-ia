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

    public string date_hr;//Data e HORA
    public MENTOR_TYPE mentor_type;

    public List<String> cpfs_candidatos;

    //MODO 1 - MENTORIA GRATUITA EM 1 DIA APENAS (CERCA DE 1 HRS), TOUR PELA EMPRESA E AS FUNÇÕES DA VAGA DIVULGADA NA MENTORIA DELA E BANCO DE VAGAS
    //MODO 2 - MENTORIA PAGA DE 50 REAIS EM 1 DIAS APENAS, PODE PEDIR PARA O CANDIDATO REALIZAR UMA PROVA

    public Vaga() {
        cpfs_candidatos = new List<string>();
        this.title = "Exemplo de Mentoria";
        this.desc = "Exemplo de descrição de  mentoria - Dizendo o que a empresa apresentará aos candidatos selecionados, podendo ser 1-Rotina de trabalho do cargo da mentoria, 2-Acompanhar o mentor do cargo da mentoria durante o dia dele, 3- Mostrar toda a empresa e todos os setores e suas atividades e importâncias, 4-Fazer uma explicação sobre a empresa e as vagas que abrirão em breve e como elas são importantes para o funcionamento da empresa.";
        this.adress = "Rua xxx do bairro xx, numero xx predio xx, anda xxx";
        this.mentorName = "Heitor, o mentor";
        this.formacaoMinima = 1;
        this.state = 16;
        this.city = 1;
        this.date_hr = "Dia 25 de jan as 15hrs";
    }

    public void SetEmpresa(Empresa emp) {
        this.empresaId = emp.id;
        //this.vagaId = ??
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
        int r1 = rnd.Next(1,10);
        int r2 = rnd.Next(1, 10);
        int r3 = rnd.Next(1, 10);
        if ((r1 == r2 || r1 == r3 )&& (r1!=10)) r1++;
        if ((r2 == r3) && (r2 != 10)) r2++;

        CandidatarAvaga(r1 + "");
        CandidatarAvaga(r2 + "");
        CandidatarAvaga(r3 + "");
    }

   
}
