﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using System;


public class Controller : MonoBehaviour {
    
    [Header("Holders")]
    public Transform vagaCellsHolder_LOADER;
    public Transform vagaCellsHolder_USER;
    public Transform empresaHolder;
    public Transform vagaCellsHolder_EMPVIEW;
    public Transform candsCellsHolder;
    public Transform candsCellsHolder_EMP;

    [Header("Cells")]
    public VagaCell prefabVagaCell;
    public VagaCell prefabVagaEmpViewCell;
    public CandCell prefabCandCell;

    [Header("gps")]
    Gps gps;
    Sprite emp_logo;

    [Header("Systems")]
    public RatingControl ratingControl;

    [Header("InfoContas")]
    public bool isEmpresa;
    public bool isCandidato;

    [Header("Objects")]
    public Candidato perfil_usuario;
    public Empresa perfil_empresa;
    public Vaga vaga_mentoria;

    private View view;

    [Header("Debug")]
    public bool deletePrefs;
    private BasicData basic_data;
    private SMSContacter contacter;
    private int r_old;


    void Start(){
#if UNITY_EDITOR
        if (deletePrefs) PlayerPrefs.DeleteAll();//NÃO ESQUECER ISSO AQUI SE NÃO NÃO VAI SALVAR
#endif
        view = FindObjectOfType<View>();
        DataBaseHandler.SetController(this);
        basic_data = FindObjectOfType<BasicData>();

        //StartCoroutine(PutExamplesInDatabaseVag());//REMOVER APÓS USO

        SetEmpresaListInDropDown();
        SetAreasListInDropDown();

        if (PlayerPrefs.GetInt("IsCandidato") == 1)
        {
            isCandidato = true;
            r_old = LoadCandidatoRank();
            DataBaseHandler.GetCandidatoXFromServer(LoadCandidatoCPF());           

            //LoadCandidatoProfile();//Device
            ButtonProcurarMentoria();

        }
        else if (PlayerPrefs.GetInt("IsEmpresa") == 1)
        {
            isEmpresa = true;
            view.tela_cand_ou_empresa.gameObject.SetActive(false);
            view.tela_login_empresa.gameObject.SetActive(true);
        }
    }

    public void ButtonProcurarMentoria(){
        if (!isCandidato){
            view.tela_cand_ou_empresa.gameObject.SetActive(false);
            view.tela_cad_cand1.gameObject.SetActive(true);
        }
        else {
            view.GoToMainPageCandidato();
            LoadVagasMentoriasToUser();
        }
    }

    public void BackFromCandidatoProfile(){
        view.BackFromCandidatoProfile(isCandidato);
    }

    public void LoginEmpresaCheck() {
        Empresa[] emps = empresaHolder.GetComponentsInChildren<Empresa>();
        string pass = view.field_empresa_pass.text;
        string id_emp = view.field_empresa_id.text;

        foreach (Empresa emp in emps){
            if ((emp.id.ToString()) == id_emp) {

                if ((emp.password.ToString()) == pass) {
                    Debug.Log("PASS CORRETO!");
                    view.GoToMainPageEmpresa();
                    this.emp_logo = GetLogoEmpresa(emp.id);
                    view.empresa_logo_main.sprite = this.emp_logo;
                    view.empresa_logo_mentcria.sprite = this.emp_logo;
                    view.bemvindotext.text = "Seja bem vinda, " + GetNameEmpresa(emp.id);
                    PlayerPrefs.SetInt("IsEmpresa", 1);
                    this.perfil_empresa = emp;
                    isEmpresa = true;
                    isCandidato = false;
                    View.ShowFeedbackMsg("Login bem sucedido!");
                }
            }
        }
        if(!isEmpresa) View.ShowFeedbackMsg("Login não existente!");
    }

    private void SetEmpresaListInDropDown()
    {
        List<String> ss = GetEmpresasNames();
        view.field_empr_foco1.ClearOptions();
        view.field_empr_foco1.AddOptions(ss);
        view.field_empr_foco1.captionText.text = "Empresa foco 1";

        view.field_empr_foco2.ClearOptions();
        view.field_empr_foco2.AddOptions(ss);
        view.field_empr_foco2.captionText.text = "Empresa foco 2";
    }

    private void SetAreasListInDropDown()
    {
        BasicData bd = FindObjectOfType<BasicData>();
        List<String> ss2 = new List<string>(bd.areaDeInteresse);

        view.field_interesse1.ClearOptions();
        view.field_interesse1.AddOptions(ss2);
        view.field_interesse1.captionText.text = "Interesse foco 1";

        view.field_interesse2.ClearOptions();
        view.field_interesse2.AddOptions(ss2);
        view.field_interesse2.captionText.text = "Interesse foco 2";
    }

    public List<String> GetEmpresasNames() {
        Empresa[] emps = empresaHolder.GetComponentsInChildren<Empresa>();
        List<String> names_emps = new List<string>();
        foreach (Empresa emp in emps){
            names_emps.Add(emp.name);
        }
        return names_emps;
    }

    public Sprite GetLogoEmpresa(int id_emp) {
        Empresa[] emps = empresaHolder.GetComponentsInChildren<Empresa>();
        foreach (Empresa emp in emps){
            if (emp.id == id_emp) return emp.icon_empresa;
        }
        return null;
    }

    public String GetNameEmpresa(int id_emp)
    {
        Empresa[] emps = empresaHolder.GetComponentsInChildren<Empresa>();
        foreach (Empresa emp in emps)
        {
            if (emp.id == id_emp) return emp.name;
        }
        return null;
    }

    public Sprite GetPhotoCandPadrao(string cpf){
        Sprite s = FindObjectOfType<BasicData>().GetPhotoCand(cpf);
        return s;
    }
    

    /// <summary>
    /// Cadastro de Candidato
    /// </summary>
    public void CreateCandidato() {
        perfil_usuario = view.GetCandidatoFromFields();
        SaveCandidatoProfile();
        View.ShowFeedbackMsg("Seu perfil foi criado!");
        if (!isCandidato) DataBaseHandler.PutNewCandidatoInServer(perfil_usuario);//<---
        isCandidato = true;
        ShowLoadedVagasMentoriaToUser();
    }

    public void CreateVagaMentoria() {
        vaga_mentoria = view.GetVagaMentoriaFromFields();
        vaga_mentoria.SetEmpresa(perfil_empresa);

        VagaCell vc = Instantiate(prefabVagaCell, vagaCellsHolder_LOADER.transform);
        VagaCell vc2 = Instantiate(prefabVagaEmpViewCell, vagaCellsHolder_EMPVIEW.transform);
        vc.LoadVagaOnCell(vaga_mentoria);
        vc.LoadVagaLogo(emp_logo);
        vc2.LoadVagaOnCell(vaga_mentoria);
        vc2.LoadVagaLogo(emp_logo);
        vaga_mentoria.RandomCandToVaga();
        DataBaseHandler.PutNewVagaInServer(vaga_mentoria);//<---
        View.ShowFeedbackMsg("Mentoria Criada!");
    }

    public void LoadVagasMentoriasToUser() {
        StartCoroutine(ShowLoadedVagasMentoriaToUser());
    }

    public IEnumerator ShowLoadedVagasMentoriaToUser() {
        yield return new WaitForSeconds(1f);
        view.CleanHolder(vagaCellsHolder_USER,false,true);
        VagaCell[] vcs = vagaCellsHolder_LOADER.GetComponentsInChildren<VagaCell>();
        VagaCell vctemp;
        foreach (VagaCell vc in vcs) {
            //if (vc.vaga.city == perfil_usuario.city){
                vctemp = Instantiate(prefabVagaCell, vagaCellsHolder_USER.transform);
                vctemp.LoadVagaOnCell(vc.vaga);
                vctemp.LoadVagaLogo(GetLogoEmpresa(vc.vaga.empresaId));
           // }
        }
        yield return new WaitForSeconds(0.1f);
    }

    public void ShowMyVagasMentoriaToEmp()
    {
        view.CleanHolder(vagaCellsHolder_EMPVIEW, false, true);
        VagaCell[] vcs = vagaCellsHolder_LOADER.GetComponentsInChildren<VagaCell>();
        VagaCell vctemp;
        foreach (VagaCell vc in vcs)
        {
            if (vc.vaga.empresaId == perfil_empresa.id){
                vctemp = Instantiate(prefabVagaEmpViewCell, vagaCellsHolder_EMPVIEW.transform);
                vctemp.LoadVagaOnCell(vc.vaga);
                vctemp.LoadVagaLogo(GetLogoEmpresa(vc.vaga.empresaId));
             }
        }
    }

    public void ShowLoadedCandsFromVaga() {
        view.CleanHolder(candsCellsHolder_EMP, true,false);
        CandCell[] ccs = candsCellsHolder.GetComponentsInChildren<CandCell>();
        CandCell cctemp;
        foreach (CandCell cc in ccs){
            if (this.vaga_mentoria.HasCpf(cc.candidato.cpf)) {
                cctemp = Instantiate(prefabCandCell, candsCellsHolder_EMP.transform);
                cctemp.LoadCandidatoOnCell(cc.candidato);
                cctemp.LoadCandLogo(GetPhotoCandPadrao(cc.candidato.cpf));
            }
        }
    }

    public void ShowVagaInfoToCandidato(Vaga v){
        this.vaga_mentoria = v;
        view.ShowVagaInfo(v, GetLogoEmpresa(v.empresaId), GetNameEmpresa(v.empresaId),false);
    }


    public void ShowVagaInfoToEmpresa(Vaga v){
        this.vaga_mentoria = v;
        view.ShowVagaInfoToEmp(v, emp_logo);
        view.ShowVagaInfoToEmp(v, emp_logo);
        ShowLoadedCandsFromVaga();
    }

    public void ShowDetailVagaToEp() {
        view.ShowVagaInfo(this.vaga_mentoria, GetLogoEmpresa(this.vaga_mentoria.empresaId), GetNameEmpresa(this.vaga_mentoria.empresaId), true);
    }

    public void ShowCandidatoInfoToEmpresa(Candidato cand) {
        this.perfil_usuario = cand;
        view.ShowCandInfoToEmpresa(cand, GetPhotoCandPadrao(cand.cpf));
    }

    public void ShowCandidatoProfileToCand() {
        view.ShowCandInfo(perfil_usuario, GetPhotoCandPadrao(perfil_usuario.cpf));
    }

    public void AskCandidatoConfirmCall(){
        view.AskConfirmSelectCandidato(vaga_mentoria.title);
    }

    public void InviteCandidatoEmailSMS() {
        contacter = new SMSContacter();
        contacter.SetTargetCand(perfil_usuario, 
            "Olá " + perfil_usuario.name + "!!!\n\nVocê foi selecionado para participar da mentoria '" + this.vaga_mentoria.title
            + "' por meio do app 'Mentor-ia'.\n\nA(O) " + perfil_empresa.name+ " fez questão de selecioná-lo para essa oportunidade de aprendizado, na qual" +
            " você poderá estar mais próximo de futuras vagas de emprego, ao mesmo tempo que pode ser recompensado com experiência e conhecimento na mentoria prestada no dia!" +
            "\n\nLembre-se que ao ser atencioso na mentoria prestada no dia, você irá acumular pontos EXP no aplicativo e futuramente ganhará recompensas ao subir de nível! Bolsas de estudo ou cursos completas, material, equipamento ou até mesmo 'apoio financeiro' são tipos de recompensas que pode ganhar, então fique atento(a)!" +
            "\n\nSegue a data da mentoria "+ this.vaga_mentoria.title+ ":"+"\n"+ this.vaga_mentoria.date_hr + "\n"+ basic_data.GetStateName(this.vaga_mentoria.state)+", "+
            basic_data.GetCityName(this.vaga_mentoria.city)+", "+this.vaga_mentoria.adress+"\nO mentor(a) '"+this.vaga_mentoria.mentorName+"' estará te aguardando no horário e local descritos." +
            "\n\nAproveite o aprendizado. Esperamos que algum dia a(o) " + perfil_empresa.name + " possa contar com você!");    
        contacter.SendEmail();
        contacter.SendSMS2();
      //contacter.SendText();
    }


    public void ConfirmCandidatarAVaga() {
        this.vaga_mentoria.CandidatarAvaga(this.perfil_usuario.cpf);
        DataBaseHandler.EditVagaInServer(this.vaga_mentoria);//<-------
        View.ShowFeedbackMsg("Candidatura realizada!");
    }

    #region edit_from_database

    public void ChangeCadRating() {
        this.perfil_usuario = ratingControl.ChangeRatings(perfil_usuario);
        DataBaseHandler.EditCandidatoInServer(this.perfil_usuario);
        View.ShowFeedbackMsg("Candidato avaliado com sucesso!");
    }

    #endregion

    #region load_from_database

    public void LoadVagaCell(Vaga v)
    {
        //Aqui poderia filtrar a vaga por ser da mesma cidade do jovem por exemplo
        if (v.isVagaOpen)
        {
            VagaCell vc = Instantiate(prefabVagaCell, vagaCellsHolder_LOADER.transform);
            vc.LoadVagaOnCell(v);
            vc.LoadVagaLogo(GetLogoEmpresa(v.empresaId));
        }
    }

    public void LoadCandidatoCell(Candidato cand)
    {
        //Aqui poderia filtrar o candidato por ser da mesma cidade por exemplo

        CandCell cc = Instantiate(prefabCandCell, candsCellsHolder.transform);
        cc.LoadCandidatoOnCell(cand);
        cc.LoadCandLogo(basic_data.photoCands[0]);
        
    }

    public void LoadCandidatoProfile(Candidato cand){
        //Aqui poderia filtrar o candidato por ser da mesma cidade por exemplo
        this.perfil_usuario = cand;
        int r_new = this.perfil_usuario.ratingMax;
        //View.ShowFeedbackMsg("Old rank = "+r_old);
        //View.ShowFeedbackMsg("New rank = " + r_new);
        if (r_new != r_old && r_new > r_old){
            view.ShowRewardExp((int)(r_new - r_old));
            SaveCandidatoProfile();
        }

    }

    #endregion




    #region save_load

    /// <summary>
    /// Usado apenas para preencher atabase com exemplo, nao usar em outros casos
    /// </summary>
    /// <returns></returns>
    public IEnumerator PutExamplesInDatabaseCand() {
        CandCell[] ccs = candsCellsHolder.GetComponentsInChildren<CandCell>();
        //StartCoroutine(DataBaseHandler.PutNewCandidatoInServer(ccs[9].candidato));
        yield return new WaitForSeconds(0.1f);
        
    }

    public IEnumerator PutExamplesInDatabaseVag()
    {
        VagaCell[] vcs = vagaCellsHolder_LOADER.GetComponentsInChildren<VagaCell>();
        DataBaseHandler.PutNewVagaInServer(vcs[5].vaga);
        yield return new WaitForSeconds(0.1f);

    }

    public void RemoveVaga() {
        this.vaga_mentoria.isVagaOpen = false;
        DataBaseHandler.EditVagaInServer(this.vaga_mentoria);
        View.ShowFeedbackMsg("Vaga Removida!");
    }

    private void SaveCandidatoProfile() {
        PlayerPrefs.SetInt("IsCandidato", 1);
        string s1 = perfil_usuario.cpf + "," + perfil_usuario.name + "," + perfil_usuario.email + "," + perfil_usuario.telNumber + "," + perfil_usuario.diferencial + "," + perfil_usuario.motivacao;
        PlayerPrefs.SetString("StringsCandidato",s1);
        string s2 = perfil_usuario.age + "," + perfil_usuario.raceSkin + "," + perfil_usuario.state + "," + perfil_usuario.city + "," + perfil_usuario.gender + "," + perfil_usuario.formacao
            + "," + perfil_usuario.areaDeInteresse1 + "," + perfil_usuario.areaDeInteresse2;
        PlayerPrefs.SetString("IntsCandidato", s2);
        string s3 = perfil_usuario.ratingMax +","+ perfil_usuario.rankInteresse + "," + perfil_usuario.rankConhecGerais + "," + perfil_usuario.rankComunicacao + "," + perfil_usuario.rankPensLogico;
        PlayerPrefs.SetString("RanksCandidato", s3);
        PlayerPrefs.SetInt("RankMax",perfil_usuario.ratingMax);
    }

    private void LoadCandidatoProfile(){
        string[] stringsCandidato = (PlayerPrefs.GetString("StringsCandidato").Split(','));
        string[] intsCandidato = (PlayerPrefs.GetString("IntsCandidato").Split(','));
        string[] ranksCandidato = (PlayerPrefs.GetString("RanksCandidato").Split(','));

        perfil_usuario = new Candidato();
        perfil_usuario.cpf = stringsCandidato[0];
        perfil_usuario.name = stringsCandidato[1];
        perfil_usuario.email = stringsCandidato[2];
        perfil_usuario.telNumber = stringsCandidato[3];
        perfil_usuario.diferencial = stringsCandidato[4];
        perfil_usuario.motivacao = stringsCandidato[5];

        perfil_usuario.age = Int32.Parse(intsCandidato[0]);
        perfil_usuario.raceSkin = Int32.Parse(intsCandidato[1]);
        perfil_usuario.state = Int32.Parse(intsCandidato[2]);
        perfil_usuario.city = Int32.Parse(intsCandidato[3]);
        perfil_usuario.gender = Int32.Parse(intsCandidato[4]);
        perfil_usuario.formacao = Int32.Parse(intsCandidato[5]);
        perfil_usuario.areaDeInteresse1 = Int32.Parse(intsCandidato[6]);
        perfil_usuario.areaDeInteresse2 = Int32.Parse(intsCandidato[7]);

        perfil_usuario.ratingMax = Int32.Parse(ranksCandidato[0]);
        perfil_usuario.rankInteresse = Int32.Parse(ranksCandidato[1]);
        perfil_usuario.rankConhecGerais = Int32.Parse(ranksCandidato[2]);
        perfil_usuario.rankComunicacao = Int32.Parse(ranksCandidato[3]);
        perfil_usuario.rankPensLogico = Int32.Parse(ranksCandidato[4]);
    }

    private string LoadCandidatoCPF()
    {
        string[] stringsCandidato = (PlayerPrefs.GetString("StringsCandidato").Split(','));

        perfil_usuario = new Candidato();
        perfil_usuario.cpf = stringsCandidato[0];
        perfil_usuario.name = stringsCandidato[1];
        perfil_usuario.email = stringsCandidato[2];
        perfil_usuario.telNumber = stringsCandidato[3];
        perfil_usuario.diferencial = stringsCandidato[4];
        perfil_usuario.motivacao = stringsCandidato[5];

        return stringsCandidato[0];
    }

    private int LoadCandidatoRank() {
        int r = PlayerPrefs.GetInt("RankMax",0);
        return r; 
    }


    #endregion

}
