using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatingControl : MonoBehaviour {

    public Color startcolor;
    public RectTransform telaPopup;
    public Button[] stars_interesse;
    public Button[] stars_conhgerais;
    public Button[] stars_comunicacao;
    public Button[] stars_penslog;
    private int rat_interesse, rat_conhgerais, rat_comunicacao, rat_penslog;

    public void SetRatingInteresse(int i) {
        this.rat_interesse = i;
        int j = i / 20;
        int h;
        for (h = 1; h <= 5; h++)
        {
            if (h <= j) this.stars_interesse[h - 1].image.color = startcolor;
            else this.stars_interesse[h - 1].image.color = Color.white;
        }
    }

    public void SetRatingConhG(int i){
        this.rat_conhgerais = i;
        int j = i / 20;
        int h;
        for (h = 1;h<= 5; h++)
        {
            if (h <= j) stars_conhgerais[h - 1].image.color = startcolor;
            else stars_conhgerais[h - 1].image.color = Color.white;
        }
    }

    public void SetRatingComunc(int i)
    {
        this.rat_comunicacao = i;
        int j = i / 20;
        int h;
        for (h = 1; h <= 5; h++)
        {
            if (h <= j) this.stars_comunicacao[h - 1].image.color = startcolor;
            else this.stars_comunicacao[h - 1].image.color = Color.white;
        }
    }

    public void SetRatingPensLog(int i)
    {
        this.rat_penslog = i;
        int j = i / 20;
        int h;
        for (h = 1; h <= 5; h++)
        {
            if (h <= j) this.stars_penslog[h - 1].image.color = startcolor;
            else this.stars_penslog[h - 1].image.color = Color.white;
        }
    }

    public Candidato ChangeRatings(Candidato c) {
        c.ratingMax += rat_interesse + rat_comunicacao + rat_conhgerais + rat_penslog;
        if(rat_interesse>20)c.rankInteresse = (int)((c.rankInteresse + (rat_interesse * 2)) / 3);
        if (rat_comunicacao > 20) c.rankComunicacao = (int)((c.rankComunicacao + (rat_comunicacao * 2)) / 3);
        if (rat_conhgerais > 20) c.rankConhecGerais = (int)((c.rankConhecGerais + (rat_conhgerais * 2)) / 3);
        if (rat_penslog > 20) c.rankPensLogico = (int)((c.rankComunicacao + (rat_penslog * 2))/3);

        return c;
    }

}
