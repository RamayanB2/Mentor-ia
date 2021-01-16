using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using System;

public class Controller : MonoBehaviour {
    
    [Header("PrefabCell")]
    public RectTransform pedidoCellsHolder;
    public RectTransform itemCellsHolder;
    public RectTransform lojaPriceItensHolder;
    public GameObject productsHolder;

    [Header("gps")]
    Gps gps;

    [Header("Lojas")]
    public bool hasLoadedLoja;

    [Header("InfoContas")]
    public bool isEmpresa;
    public bool isandidatp;

    [Header("Objects")]
    public Candidato perfil_usuario;
    public Empresa perfil_empresa;

    [Header("Debug")]
    public bool entrarComoLoja;
    public bool entrarComoCliente;

    void Start(){        
        DataBaseHandler.SetController(this);

    }


    


 

}
