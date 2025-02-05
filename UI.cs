using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OHVTrainer
{
    internal class UI
    {
        public TMPro.TMP_Text GetPlayerPositionText(GameObject MainWindow)
        {
            return MainWindow.transform.Find("MainWindow").Find("PlayerPos").GetComponent<TMP_Text>();
        }

        public TMPro.TMP_Text GetNysaDetailsText(GameObject MainWindow)
        {
            return MainWindow.transform.Find("MainWindow").Find("NysaDetails").GetComponent<TMP_Text>();
        }

        public TMPro.TMP_InputField GetMoneyInputField(GameObject MainWindow)
        {
            return GetWindow(MainWindow).Find("Player").Find("Money").Find("MoneyTextInput").GetComponent<TMPro.TMP_InputField>();
        }

        public Button GetMoneyButton(GameObject MainWindow)
        {
            return GetWindow(MainWindow).Find("Player").Find("Money").Find("SetMoneyButton").GetComponent<Button>();
        }

        public Toggle GetInfiniteHealthToggle(GameObject MainWindow)
        {
            return GetWindow(MainWindow).Find("Player").Find("Stats").Find("InfiniteHealthCheckbox").GetComponent<Toggle>();
        }

        public Toggle GetInfiniteStaminaToggle(GameObject MainWindow)
        {
            return GetWindow(MainWindow).Find("Player").Find("Stats").Find("InfiniteStaminaCheckbox").GetComponent<Toggle>();
        }

        private Transform GetWindow(GameObject MainWindow)
        {
            return MainWindow.transform.Find("MainWindow").Find("Wrapper").Find("Scroll View").Find("Viewport").Find("Content");
        }
    }
}
