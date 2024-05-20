using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;
using Microsoft.MixedReality.GraphicsTools;



public class WarningSystemScript : MonoBehaviour
{
    public TMP_Text noTasksWarningsText;
    public TMP_Text warningText;
    public TMP_Text warningDetailsText;
    public TMP_Text messageText;
    public TMP_Text messageDetailsText;
    public GameObject warningVignette;

    public ConnectionHandler connection;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // show or hide background
        if (messageText.gameObject.activeSelf || warningText.gameObject.activeSelf) noTasksWarningsText.gameObject.SetActive(false);
        else noTasksWarningsText.gameObject.SetActive(true);


        GatewayConnection conn = connection.GetConnection();

        /// CHECK/UPDATE TODOLIST 
        if (conn != null && conn.isTODOITEMSUpdated())
        {
            string[][] todoItems = JObject.Parse(conn.GetTODOITEMSJsonString())["todoItems"].ToObject<string[][]>();
            string newTodoList = "<indent=5%>";
            foreach (var todoItem in todoItems)
            {
                if (todoItem[1] != "True")
                {
                    newTodoList += $"-  {todoItem[0]} \n";
                }
                else
                {
                    newTodoList += $"- <s> {todoItem[0]} </s>\n";
                }
            }
            messageDetailsText.text = newTodoList;

            if (newTodoList == "<indent=5%>")
            {
                messageText.gameObject.SetActive(false);
                messageDetailsText.gameObject.SetActive(false);
            }
            else
            {
                messageText.gameObject.SetActive(true);
                messageDetailsText.gameObject.SetActive(true);
            }
        }

        /// CHECK/UPDATE WARNING
        if (conn != null && conn.isWARNINGUpdated())
        {
            string infoWarning = JObject.Parse(conn.GetWARNINGJsonString())["infoWarning"].ToObject<string>();
            if (infoWarning != "")
            {
                //warningText.text = "Warning:";
                warningDetailsText.text = infoWarning;
                OpenWarning();
            }
            else CloseWarning();
        }
    }

    public void OpenWarning()
    {
        warningText.gameObject.SetActive(true);
        warningDetailsText.gameObject.SetActive(true);
        warningVignette.SetActive(true);
    }

    public void CloseWarning()
    {
        warningText.gameObject.SetActive(false);
        warningDetailsText.gameObject.SetActive(false);
        warningVignette.SetActive(false);
    }

    public void OpenToDo()
    {
        messageText.gameObject.SetActive(true);
        messageDetailsText.gameObject.SetActive(true);
    }
    public void CloseToDo()
    {
        messageText.gameObject.SetActive(false);
        messageDetailsText.gameObject.SetActive(false);
    }
}