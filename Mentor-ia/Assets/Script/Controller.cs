using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using System;

public class Controller : MonoBehaviour {
    
    [Header("Holders")]
    public RectTransform vagaCellsHolder;
    public RectTransform empresaHolder;

    [Header("gps")]
    Gps gps;

    [Header("Lojas")]
    public bool hasLoadedLoja;

    [Header("InfoContas")]
    public bool isEmpresa;
    public bool isCandidato;

    [Header("Objects")]
    public Candidato perfil_usuario;
    public Empresa perfil_empresa;

    public bool isCandidatoRegistered = false;
    private View view;


    void Start(){
        view = FindObjectOfType<View>();
        DataBaseHandler.SetController(this);
    }

    public void ButtonProcurarMentoria(){
        if (!isCandidatoRegistered){
            view.tela_cad_cand1.gameObject.SetActive(true);
        }
    }

    public void CreateCandidato() {
        perfil_usuario = view.GetCandidatoFromFields();

        isCandidatoRegistered = true;
    }


    


 

}
