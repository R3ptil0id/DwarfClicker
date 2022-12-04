using System;
using UnityEngine;

namespace Controls.GameElements.CurrencyBar
{
    [Serializable]
    public class SmallBarControl
    {
        public Transform Bar;
        public int Id { get; private set; }

        public void Initialization(int id)
        {
            Id = id;
        }
    }
}