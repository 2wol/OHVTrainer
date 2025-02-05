using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OHVTrainer
{
    internal class Events
    {
        public void JoinDziupla()
        {
            new Player().AddMoney(10);
            var dziupla = GameObject.FindObjectOfType<OHVDziupla>();
            dziupla.HandleJoinAndPay();
        }
    }
}
