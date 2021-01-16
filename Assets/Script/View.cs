using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// Classe singleton
/// </summary>
public class View : MonoBehaviour
{
    public Text feedback_msg;

    //[Header("Fields Cadastro Candidato")]
    public InputField field_nome, field_cpf, field_email,field_telefone, field_idade;
    public Dropdown field_cidade, field_estado, field_genero, field_raca, field_formacao;
    public Dropdown field_interesse1, field_interesse2, field_empr_foco1, field_empr_foco2;
    public InputField field_diferencial, field_motivação;


    [Header("Telas")]
    public RectTransform tela_cand_ou_empresa;
    public RectTransform tela_cad_cand1, tela_cad_cand2, tela_empresa_main, tela_ver_vagas, tela_login_empresa;
    
    public static View instance;
    public bool isFirstUse = true;

    private void Awake(){
        if(instance==null)instance = this;
    }

    public void ButtonProcurarMentoria() {
        if (isFirstUse) {
            isFirstUse = false;
            tela_cad_cand1.gameObject.SetActive(true);
        }        
    }

    public void ButtonAbrirMentorias() {
        tela_login_empresa.gameObject.SetActive(true);        
    }


















    #region feedbackmsg

    public static void ShowFeedbackMsg(string s) {
        instance.showToast(s,2);
    }
    
    public void showToast(string text,int duration)
    {
       StartCoroutine(showToastCOR(text, duration));
    }

    private IEnumerator showToastCOR(string text,
        int duration)
    {
        Color orginalColor = feedback_msg.color;

        feedback_msg.text = text;
        feedback_msg.enabled = true;

        //Fade in
        yield return fadeInAndOut(feedback_msg, true, 0.5f);

        //Wait for the duration
        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            yield return null;
        }

        //Fade out
        yield return fadeInAndOut(feedback_msg, false, 0.5f);

        feedback_msg.enabled = false;
        feedback_msg.color = orginalColor;
    }

    IEnumerator fadeInAndOut(Text targetText, bool fadeIn, float duration)
    {
        //Set Values depending on if fadeIn or fadeOut
        float a, b;
        if (fadeIn)
        {
            a = 0f;
            b = 1f;
        }
        else
        {
            a = 1f;
            b = 0f;
        }

        Color currentColor = Color.clear;
        float counter = 0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(a, b, counter / duration);

            targetText.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            yield return null;
        }
    }

    #endregion feedbackmsg
}
