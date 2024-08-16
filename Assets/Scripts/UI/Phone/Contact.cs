using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contact : MonoBehaviour
{
    [SerializeField] private Phone phone;
    public void Call() {
        if (CustomerManager.instance.currentCustomer != null)
        {
            CustomerAI currentCustomer = CustomerManager.instance.currentCustomer;
            if (currentCustomer.isEvil)
            {
                PoliceManager.instance.StartChasing(currentCustomer);
            } else {
                ScrollingText.instance.Show("Call Police 1");
            }
        } else {
             ScrollingText.instance.Show("Call Police 2");
        } 
        phone.ClosePhone();      
    }
}
