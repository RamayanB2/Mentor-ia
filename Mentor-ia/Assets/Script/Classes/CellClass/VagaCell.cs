using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VagaCell : MonoBehaviour
{

    public Image icon_vaga;
    public Text title_vaga;

    public Vaga vaga;

    void Start(){
    }

    public void LoadVagaOnCell(Vaga vaga) {
        this.vaga = vaga;
        this.title_vaga.text = vaga.title;
    }

    public void LoadVagaLogo(Sprite sp) {
        this.icon_vaga.sprite = sp;
    }

    public void ShowVagaInfoToCandidato() {
        FindObjectOfType<Controller>().ShowVagaInfoToCandidato(this.vaga);
    }

    public void ShowVagaInfoToEmpresa(){
        FindObjectOfType<Controller>().ShowVagaInfoToEmpresa(this.vaga);
    }




}
