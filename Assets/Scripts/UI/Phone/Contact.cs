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
                PoliceManager.instance.StartChasing();
                EndlessRunManager.instance.chasedCustomer = currentCustomer;
            } else {
                ScrollingText.instance.Show("Perhatikan lagi sketsa yang ku kasih");
            }
        } else {
             ScrollingText.instance.Show("Waspada sosok penjahat");
        } 
        phone.ClosePhone();      
    }
}
