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

    [Header("Fields Cadastro Candidato")]
    public InputField field_nome;
    public InputField field_cpf, field_email, field_telefone, field_idade;
    public Dropdown field_cidade, field_estado, field_genero, field_raca, field_formacao;
    public Dropdown field_interesse1, field_interesse2, field_empr_foco1, field_empr_foco2;
    public InputField field_diferencial, field_motivacao;

    [Header("Fields Empresa")]
    public InputField field_empresa_id;
    public InputField field_empresa_pass;
    public Image empresa_logo_main, empresa_logo_mentcria;
    public Text bemvindotext;

    [Header("Fields Vaga")]
    public InputField field_titleVaga;
    public InputField field_descVaga, field_endVaga, field_nomeMentor, field_dataHr, field_vaganb;
    public Dropdown field_cidadeVaga, field_estadoVaga, field_formacaoVaga;

    [Header("Fields Vaga Show To Cand")]
    public Text titleVagaShow;
    public Text topdescVagaShow;
    public Text descVagaShow;
    public Image iconVagaShow;
    public Image botbarAccept;

    [Header("Fields Vaga Show To Emp")]
    public Text topbarVagaTitle;
    public Image topbarVagaIcon;
    public Image botbarDelete;

    [Header("Fields Candidato Show")]
    public Text tnameCandShow;
    public Image photoCandShow;
    public Text descCandShow;
    public Text diferencialCandShow;
    public Text interessesCandShow;
    public Slider interesse_slider, conhec_slider, comunic_slider, pensLog_slider;
    public Button sendInviteToMentoria, ratingCand;
    public Text vagaNameInConfirm;
    public Text exp_rankmax;
    public Text lvl_rankmax;

    [Header("Telas")]
    public RectTransform tela_cand_ou_empresa;
    public RectTransform tela_cad_cand1, tela_cad_cand2, tela_empresa_main, tela_ver_vagas, tela_login_empresa;
    public RectTransform tela_vaga_show_to_cand;
    public RectTransform tela_vaga_show_to_emp;
    public RectTransform tela_cand_showemp;
    public RectTransform tela_confirm_invite;

    public static View instance;

    [Header("Feedbacks")]
    public Image telaIntro;
    public Text feedback_msg;
    public Text feedBackReward;
    public Image feedBackRewardPanel;
    public Image panel_info;
    public Text title_info;
    public Text body_info;

    private void Awake() {
        if (instance == null) instance = this;
    }

    private void Start(){
        if (PlayerPrefs.GetInt("IsFirstUse", 1) == 1) telaIntro.gameObject.SetActive(true);
        //ShowRewardExp(350);//Teste

        if (PlayerPrefs.GetInt("IsCandidato") == 1) {
            GoToMainPageCandidato();

        } else if (PlayerPrefs.GetInt("IsEmpresa") == 1) {

        }
    }

    public void DoneFirstUse() {
        PlayerPrefs.SetInt("IsFirstUse", 0);
    }

    public void ButtonAbrirMentorias() {
        tela_login_empresa.gameObject.SetActive(true);
    }

    public void GoToMainPageEmpresa() {
        tela_login_empresa.gameObject.SetActive(false);
        tela_empresa_main.gameObject.SetActive(true);
    }

    public void GoToMainPageCandidato() {
        tela_cand_ou_empresa.gameObject.SetActive(false);
        tela_login_empresa.gameObject.SetActive(false);
        tela_ver_vagas.gameObject.SetActive(true);
    }

    public void BackFromCandidatoProfile(bool isCand) {
        tela_cand_showemp.gameObject.SetActive(false);
        if (isCand) {
            tela_ver_vagas.gameObject.SetActive(true);
        } else
            tela_vaga_show_to_emp.gameObject.SetActive(true);
    }

    public Candidato GetCandidatoFromFields() {
        Candidato c = new Candidato();
        c.name = field_nome.text;
        c.cpf = field_cpf.text;
        if (field_email.text != "") c.email = field_email.text;
        if (field_email.text != "") c.email = field_email.text;
        if (field_telefone.text != "")c.telNumber = field_telefone.text;
        if(field_idade.text!="")c.age = Int32.Parse(field_idade.text);
        c.city = field_cidade.value;
        c.state = field_estado.value;
        c.gender = field_genero.value;
        c.raceSkin = field_raca.value;
        c.formacao = field_formacao.value;
        c.areaDeInteresse1 = field_interesse1.value;
        c.areaDeInteresse2 = field_interesse2.value;
        c.diferencial = field_diferencial.text;
        c.motivacao = field_motivacao.text;
        return c;
    }

    public Vaga GetVagaMentoriaFromFields() {
        Vaga v = new Vaga();
        v.title = field_titleVaga.text;
        v.desc = field_descVaga.text;
        v.adress = field_endVaga.text;
        v.mentorName = field_nomeMentor.text;
        v.date_hr = field_dataHr.text;
        v.city = field_cidadeVaga.value;
        v.state = field_estadoVaga.value;
        v.formacaoMinima = field_formacaoVaga.value;
        v.maxVagas = Int32.Parse(field_vaganb.text);
        return v;
    }

    public void ShowVagaInfo(Vaga v, Sprite sp, string name_emp, bool isEmp) {
        if (isEmp)
        {
            botbarDelete.gameObject.SetActive(true);
            botbarAccept.gameObject.SetActive(false);
        }
        else {
            botbarDelete.gameObject.SetActive(false);
            botbarAccept.gameObject.SetActive(true);
        }
        BasicData bd = FindObjectOfType<BasicData>();
        tela_vaga_show_to_cand.gameObject.SetActive(true);
        titleVagaShow.text = v.title;
        topdescVagaShow.text = name_emp.ToUpper() + ", <color=#ffffff>"+ bd.GetCityName(v.city)+", "+bd.GetStateName(v.state)+ "</color>";
        descVagaShow.text = v.desc+ "\n\nFormação mínima: " + bd.GetFormacaoName(v.formacaoMinima);
        iconVagaShow.sprite = sp;
    }

    public void ShowVagaInfoToEmp(Vaga v, Sprite sp) {
        tela_vaga_show_to_emp.gameObject.SetActive(true);
        topbarVagaTitle.text = v.title;
        topbarVagaIcon.sprite = sp;
    }


    public void ShowCandInfo(Candidato c, Sprite sp)
    {
        BasicData bd = FindObjectOfType<BasicData>();
        tela_cand_showemp.gameObject.SetActive(true);
        tnameCandShow.text = c.name;
        descCandShow.text = c.age + " anos, " + bd.GetCityName(c.city) + ", " + bd.GetStateName(c.state)+" - "+bd.GetRaca(c.raceSkin)+", "+bd.GetGenero(c.gender) + "\n" + bd.GetFormacaoName(c.formacao) + "" + "\nContato: " + c.email + "\nTel: " + c.telNumber;
        diferencialCandShow.text = "Diferencial: " + c.diferencial;
        interessesCandShow.text = "Áreas de interesse: " + bd.GetAreaInteresse(c.areaDeInteresse1) + ", " + bd.GetAreaInteresse(c.areaDeInteresse2);

        exp_rankmax.text = ""+ c.ratingMax;
        if(c.ratingMax<100)lvl_rankmax.text = "1";
        else if(c.ratingMax>=100 && c.ratingMax<=300)lvl_rankmax.text = "2";
        else if (c.ratingMax > 300) lvl_rankmax.text = "3";

        interesse_slider.value = c.rankInteresse;
        conhec_slider.value = c.rankConhecGerais;
        comunic_slider.value = c.rankComunicacao;
        pensLog_slider.value = c.rankPensLogico;

        photoCandShow.sprite = sp;
        sendInviteToMentoria.gameObject.SetActive(false);
        ratingCand.gameObject.SetActive(false);
    }

    public void ShowCandInfoToEmpresa(Candidato c, Sprite sp){
        ShowCandInfo(c,sp);
        sendInviteToMentoria.gameObject.SetActive(true);
        ratingCand.gameObject.SetActive(true);
    }

    public void AskConfirmSelectCandidato(string vaganame) {
        tela_confirm_invite.gameObject.SetActive(true);
        vagaNameInConfirm.text = "Confirma o contato com este candidato via SMS e email para comparecer na mentoria <color=#272727>'" +  vaganame+ "'</COLOR> ?";
}

    public void CleanHolder(Transform holder, bool isCandCell, bool isVagaCell)
    {
        if (isCandCell)
        {
            CandCell[] ccs = holder.GetComponentsInChildren<CandCell>();
            if (ccs != null)
            {
                foreach (CandCell cc in ccs)
                {
                    GameObject.Destroy(cc.gameObject);
                }
            }
        }
        else if (isVagaCell)
        {
            VagaCell[] vcs = holder.GetComponentsInChildren<VagaCell>();
            if (vcs != null)
            {
                foreach (VagaCell vc in vcs)
                {
                    GameObject.Destroy(vc.gameObject);
                }
            }

        }
    }





















    #region feedbackmsg

    public void InfoEnviarContato(){
        ShowInfoPanel("Convite para mentoria", "Ao confirmar essa etapa, você enviará um email e SMS para o candidato com os dados cadastrados na sua mentoria e estará assumindo que o candidato estará presente na hora e local marcados.");
    }

    public void InfoRating(){
        ShowInfoPanel("Rating da Mentoria", "Você pode dar pontos de experiência por meio de estrelas ao candidato a mentoria, de acordo com o que pôde avaliar dele na atividade diária da mentoria. A avaliação não deve ser uma prova, mas uma observação de comportamento e interesse do candidato.\n\nCaso não tenha tido possibilidade de avaliar algum quesito, deixe o campo de estrelas em branco, pois assim o candidato não perderá pontos no rank.\n\n Seja justo nas avaliações.");
    }

    public void InfoTelaCriarMentoria()
    {
        ShowInfoPanel("Criação de Mentoria", "Titulo - Deve conter o nome do cargo da empresa/organização a ser acompanhado/mentorado" +
            "\n\n" + "Descrição - Explique as atividades que serão feitas na mentoria durante o dia, se é um acompanhamento do mentor e suas atividades ou uma demostração de como o cargo funciona na empresa por exemplo." +
           "\n\nNome do mentor - Quem ficará responsável direto pelas explicações e acompanhamento dos mentorados?" +
           "\n\nData e hr - Exemplo: 19/02/2021 as 12 hrs" +
           "\n\nNo Vagas - Receberá 1 pessoa ou mais nesse dia?(ao mesmo tempo)");
        
    }

    public void InfoTelaLoginEmpresa() {
        ShowInfoPanel("Empresas e Mentorias", "Esta é a área de login de empresas. Se você é responsável pelo RH ou por ações de impacto social de uma empresa ou centro cultural e deseja criar mentorias para jovens terem oportunidades de aprendizado no app, entre em contato conosco para cadastrarmos suas credenciais.\n\n" +
            "Após isso será possível criar mentorias na sua organização.");
    }

    public void ShowInfoPanel(string title, string body) {
        panel_info.gameObject.SetActive(true);
        title_info.text = title;
        body_info.text = body;
}

    public void ShowRewardExp(int i) {
        StartCoroutine(RewardedMsg(i));
    }

    public IEnumerator RewardedMsg(int exp) { 
        feedBackRewardPanel.gameObject.SetActive(true);
        feedBackReward.text = "Parabéns!\nUma mentoria que fez recentemente lhe qualificou com + <b><color=#809EC3>"+exp +"exp</color></b>!!";
        yield return new WaitForSeconds(7f);
        feedBackRewardPanel.gameObject.SetActive(false);
    }

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
