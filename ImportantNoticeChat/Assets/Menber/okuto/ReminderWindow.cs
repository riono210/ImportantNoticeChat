using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReminderWindow : MonoBehaviour
{
    public GameObject messageManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void OKButtonClicked(){        
        this.messageManager.GetComponent<MessageManager>().updatePriority();
        this.transform.parent.parent.gameObject.SetActive(false);
    }

    public void CancelButtonClicked(){
        this.transform.parent.parent.gameObject.SetActive(false);
    }


}
