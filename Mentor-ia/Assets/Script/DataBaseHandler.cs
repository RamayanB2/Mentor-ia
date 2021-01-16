using System.Collections;
using System;
using System.Collections.Generic;
using Proyecto26;
using UnityEngine;

public static class DataBaseHandler
{
    [Header("DataBase indexes")]
    public static string TRACKERS_INDEXES = "INDEXES";
    public static string VAGA_ID_TRACKER = "VAGA_ID_TRACKER";
    public static string CANDIDATO_ID_TRACKER = "CANDIDATO_ID_TRACKER";

    public static string INDEX_CANDIDATOS = "CANDIDATOS";
    public static string INDEX_VAGAS = "VAGAS";
    public static string DATABASE_URL = "https://mentor-c2cac-default-rtdb.firebaseio.com/";

    static Indexes indexes_server;
    public static Controller controller;
    static int id_ultima_vaga = 0;
    static int id_ultimo_cand = 0;


    public static void SetController(Controller c){
        controller = c;
        //Comandos de primeiro uso abaixo
        //indexes_server = new Indexes();
        //RestClient.Put<Indexes>($"{DATABASE_URL}{TRACKERS_INDEXES}.json", indexes_server);
    }

    
    public static void PutVagaInServer()
    {
        RestClient.Get<Indexes>(DATABASE_URL + TRACKERS_INDEXES + ".json").Then(response => {
            indexes_server = response;

            Debug.Log("indexes_server.PEDIDO_ID_TRACKER:" + indexes_server.VAGA_ID_TRACKER);
            id_ultima_vaga = indexes_server.VAGA_ID_TRACKER;
            id_ultima_vaga++;
            //pedido.id = id_ultimo_pedido;
            indexes_server.VAGA_ID_TRACKER = id_ultima_vaga;

            RestClient.Put<Indexes>($"{DATABASE_URL}{TRACKERS_INDEXES}.json", indexes_server);
           // RestClient.Put<Vaga>($"{DATABASE_URL}{INDEX_VAGAS}/{vaga.id}.json", pedido);
        });
    }
    
    /// <summary>
    /// Coloca pedido de volta no server/edita ele lá (Após a loja clicar em aceito)
    /// </summary>
    /// <param name="pedido"></param>
    
    public static void PutPedidoAceitoInServer(Vaga vaga){
            RestClient.Put<Vaga>($"{DATABASE_URL}{INDEX_VAGAS}/{vaga.vagaId}.json", vaga);
    }
    
    
    public static void GetVagasFromServer()
    {
        RestClient.Get<Indexes>(DATABASE_URL + TRACKERS_INDEXES + ".json").Then(response => {
            indexes_server = response;

            id_ultima_vaga = indexes_server.VAGA_ID_TRACKER;
            int i;
            for (i=0;i<= id_ultima_vaga; i++) {
                
                RestClient.Get<Vaga>($"{DATABASE_URL}{INDEX_VAGAS}/{i}.json").Then(response2 => {
                    //if (response2 != null) Debug.Log("============================> EXISTE PEDIDO");
                    //PedidosPendentes pp = response;
                    //controller.pedidos_pendentes = pp.pedidos;
                    Vaga v = response2;
                    //if (p.resolvido == false) controller.LoadPedidoCell(p);
                    //controller.ReLoadPedidos();
                    return;

                });
                
            }
            //controller.ShowMsgNoPedido();

        });

        
    }

    public static void UpdateQuitandaItemTable() {
        //RestClient.Put<QuitandaItemTable>($"{DATABASE_URL}{TABLE_ITENS}.json", qit);
    }

    public static void GetQuitandaItemTable()
    {
        /*
        RestClient.Get<QuitandaItemTable>($"{DATABASE_URL}{TABLE_ITENS}.json").Then(response =>
        {
            controller.qit=response;
            controller.PresetTheProductListQIT();
        });
        */
    }

    public static void PutNewCandidatoInServer(Candidato cand){
        RestClient.Get<Indexes>(DATABASE_URL + TRACKERS_INDEXES + ".json").Then(response => {
            indexes_server = response;

            int id_cand= indexes_server.CANDIDATO_ID_TRACKER;
            id_cand++;
            indexes_server.CANDIDATO_ID_TRACKER = id_cand;
            cand.id = id_cand;
            //l.SaveLojaIdOnDevice();

            RestClient.Put<Indexes>($"{DATABASE_URL}{TRACKERS_INDEXES}.json", indexes_server);
            RestClient.Put<Candidato>($"{DATABASE_URL}{INDEX_CANDIDATOS}/{id_cand}.json", cand);
        });       
    }

    public static void PutNewVagaInServer(Vaga vag)
    {
        RestClient.Get<Indexes>(DATABASE_URL + TRACKERS_INDEXES + ".json").Then(response => {
            indexes_server = response;

            int id_vag = indexes_server.VAGA_ID_TRACKER;
            id_vag++;
            indexes_server.VAGA_ID_TRACKER = id_vag;
            vag.vagaId = id_vag;
            //l.SaveLojaIdOnDevice();

            RestClient.Put<Indexes>($"{DATABASE_URL}{TRACKERS_INDEXES}.json", indexes_server);
            RestClient.Put<Vaga>($"{DATABASE_URL}{INDEX_VAGAS}/{id_vag}.json", vag);
        });

    }


    public static void EditCandidatoInServer(Candidato cand){
        RestClient.Put<Candidato>($"{DATABASE_URL}{INDEX_CANDIDATOS}/{cand.id}.json", cand);  
    }
    

    /*
    public static void GetLojasFromServer() {
        Loja loja;
        int i;
        RestClient.Get<Indexes>(DATABASE_URL + TRACKERS_INDEXES + ".json").Then(response =>
        {
            indexes_server = response;
            bool hasLojaInReach = false;
            id_ultima_loja = indexes_server.STORE_ID_TRACKER;
            //Ve as lojas
            for (i = 0; i <= id_ultima_loja; i++)
            {
                RestClient.Get<Loja>($"{DATABASE_URL}{INDEX_LOJAS}/{i}.json").Then(response2 =>
                {
                    //if (response != null) Debug.Log("============================> PEGOU AS LOJAS!");
                    loja = response2;
                    //Debug.Log("============== " + loja.name);
                    
                    if (!hasLojaInReach && controller.CanLojaDevilerToCustomer(loja))
                    {
                        controller.PresetTheProductListQIT();
                        hasLojaInReach = true;
                        Debug.Log("============================> Há loja no alcance! " + loja.name);
                        return;
                    }
                    
                });

                if (hasLojaInReach) break;
                //Retorna ao achar primeira loja
            }
            if (!hasLojaInReach) View.ShowFeedbackMsg("Não há loja no alcance");
        });
    
}
*/
}
