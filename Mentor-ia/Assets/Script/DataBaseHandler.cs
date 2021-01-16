using System.Collections;
using System;
using System.Collections.Generic;
using Proyecto26;
using UnityEngine;

public static class DataBaseHandler
{
    [Header("DataBase indexes")]
    public static string TRACKERS_INDEXES = "INDEXES";
    public static string TABLE_ITENS = "TABLE_ITENS";
    public static string INDEX_PEDIDOS = "PEDIDOS";
    public static string INDEX_PEDIDOS_RESOLVIDOS = "PEDIDOS_RESOLVIDOS";
    public static string INDEX_LOJAS = "LOJAS";
    public static string PEDIDO_ID_TRACKER = "PEDIDO_ID_TRACKER";
    public static string CUSTOMER_ID_TRACKER = "CUSTOMER_ID_TRACKER";
    public static string STORE_ID_TRACKER = "STORE_ID_TRACKER";
    public static string DATABASE_URL = "https://quitanda-a5c94.firebaseio.com/";

    static Indexes indexes_server;
    public static Controller controller;
    static int id_ultimo_pedido = 0;
    static int id_ultima_loja = 0;


    public static void SetController(Controller c){
        controller = c;
    }

    /*
    /// <summary>
    /// Coloca o pedido feito pelo cliente no server (NOVO) (nao aceito pela loja)
    /// </summary>
    /// <param name="pedido"></param>
    public static void PutPedidoInServer()
    {
        RestClient.Get<Indexes>(DATABASE_URL + TRACKERS_INDEXES + ".json").Then(response => {
            indexes_server = response;

            Debug.Log("indexes_server.PEDIDO_ID_TRACKER:" + indexes_server.PEDIDO_ID_TRACKER);
            id_ultimo_pedido = indexes_server.PEDIDO_ID_TRACKER;
            id_ultimo_pedido++;
            //pedido.id = id_ultimo_pedido;
            indexes_server.PEDIDO_ID_TRACKER = id_ultimo_pedido;

            RestClient.Put<Indexes>($"{DATABASE_URL}{TRACKERS_INDEXES}.json", indexes_server);
            //RestClient.Put<Pedido>($"{DATABASE_URL}{INDEX_PEDIDOS}/{pedido.id}.json", pedido);
        });
    }
    */
    /// <summary>
    /// Coloca pedido de volta no server/edita ele lá (Após a loja clicar em aceito)
    /// </summary>
    /// <param name="pedido"></param>
    /*
    public static void PutPedidoAceitoInServer(Pedido pedido){
            pedido.resolvido = true;            
            //RestClient.Put<Pedido>($"{DATABASE_URL}{INDEX_PEDIDOS}/{pedido.id}.json", pedido);
    }
    */
    /*
    /// <summary>
    /// Pega pedidos do server (não aceitos ainda e na distancia que a loja entrega)
    /// </summary>
    public static void GetPedidosFromServer()
    {
        RestClient.Get<Indexes>(DATABASE_URL + TRACKERS_INDEXES + ".json").Then(response => {
            indexes_server = response;

            id_ultimo_pedido = indexes_server.PEDIDO_ID_TRACKER;
            int i;
            for (i=0;i<=id_ultimo_pedido;i++) {
                
                RestClient.Get<Pedido>($"{DATABASE_URL}{INDEX_PEDIDOS}/{i}.json").Then(response2 => {
                    //if (response2 != null) Debug.Log("============================> EXISTE PEDIDO");
                    //PedidosPendentes pp = response;
                    //controller.pedidos_pendentes = pp.pedidos;
                    Pedido p = response2;
                    if (p.resolvido == false) controller.LoadPedidoCell(p);
                    //controller.ReLoadPedidos();
                    return;

                });
                
            }
            //controller.ShowMsgNoPedido();

        });

        
    }
*/
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
    /*
    /// <summary>
    /// Salva a loja nova no server
    /// </summary>
    /// <param name="l"></param>
    public static void PutNewLojaInServer(Loja l){
        RestClient.Get<Indexes>(DATABASE_URL + TRACKERS_INDEXES + ".json").Then(response => {
            indexes_server = response;

            int id_loja = indexes_server.STORE_ID_TRACKER;
            id_loja++;
            indexes_server.STORE_ID_TRACKER = id_loja;
            l.id = id_loja;
            l.SaveLojaIdOnDevice();

            RestClient.Put<Indexes>($"{DATABASE_URL}{TRACKERS_INDEXES}.json", indexes_server);
            RestClient.Put<Loja>($"{DATABASE_URL}{INDEX_LOJAS}/{id_loja}.json", l);
        });
       
    }
    */
    /*
    /// <summary>
    /// Edita a loja e salva no server
    /// </summary>
    /// <param name="l"></param>
    public static void EditLojaInServer(Loja l){
        RestClient.Put<Loja>($"{DATABASE_URL}{INDEX_LOJAS}/{l.id}.json", l);  
    }
    */
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
