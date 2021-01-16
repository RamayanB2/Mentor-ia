using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CandCell : MonoBehaviour
{

    public Image photo_cand;
    public Text cand_name;

    public Candidato candidato;


    public void LoadCandidatoOnCell(Candidato cand) {
        this.candidato = cand;
        this.cand_name.text = cand.name;
    }

    public void LoadCandLogo(Sprite sp) {
        this.photo_cand.sprite = sp;
    }

    public void ShowCandidatoInfoToEmpresa() {
        FindObjectOfType<Controller>().ShowCandidatoInfoToEmpresa(this.candidato);
    }



    
}
