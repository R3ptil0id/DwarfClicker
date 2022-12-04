using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Controls.GameElements.CurrencyBar
{
    [Serializable]
    public class SmallBarControl
    {
        public Transform Bar;
        
        public int Id { get; private set; }
        public Vector3 Position{ get; private set; }

        public void Initialization(int id)
        {
            Id = id;
            Position = Bar.position;
        }

        // private Dictionary<string, Vector3> _positions = new Dictionary<string, Vector3>(5);
    }
}