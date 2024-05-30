using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlMr_Inventory
{
    public class NewBehaviourScript : MonoBehaviour
    {
        public void change_button()
        {
            SceneManager.LoadScene("Boss");
        }
        private void Update()
        {
            enabled = false;
        }
    }
}
