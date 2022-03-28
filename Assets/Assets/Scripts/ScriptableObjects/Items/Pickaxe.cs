using System;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects.Items {
    [CreateAssetMenu(fileName = "New Pickaxe", menuName = "Items/Pickaxe")]
    public class Pickaxe : Item {
        [SerializeField]private int addMana;

        public int AddMana => addMana;
    }
}
