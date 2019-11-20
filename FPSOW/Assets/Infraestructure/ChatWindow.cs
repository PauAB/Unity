using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Message
{
    public string text;
    public Text textObject;
}
public class ChatWindow : MonoBehaviour
{
    public int maxMessages = 25;
    public GameObject chatPanel, textObject;
    public InputField chatBox;
    List<Message> messageList = new List<Message>();
    Color[] colors = { Color.white,Color.yellow, Color.red, Color.blue };
    public enum MESSAGETYPE
    {
        REGULAR,
        DANGER,
        ERROR,
        HELP
    }
    public void SendMessageToChat(string text, MESSAGETYPE type)
    {

        if(messageList.Count >= maxMessages)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
            
        }
        Message newMessage = new Message();
        newMessage.text = text;
    

        GameObject newText = Instantiate(textObject, chatPanel.transform);
        newMessage.textObject = newText.GetComponent<Text>();
        newMessage.textObject.color = colors[(int)type];
        newMessage.textObject.text = newMessage.text;
        messageList.Add(newMessage);

        
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if (chatBox.text != "")
            {
            #if _CONSOLE
                GameManager.instance.console.DispatchMessage(chatBox.text);
            #endif
                chatBox.text = "";
            }
        }
    }
}