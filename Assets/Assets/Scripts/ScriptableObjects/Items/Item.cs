using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects.Items {
    public enum type{
        Pickaxe,
        Armor,
        Hulka,
        Boty,
    }
    public enum rarita{
        Legendary,
        Epic,
        Rare,
        Common,
    }
    public abstract class Item : ScriptableObject {
        [SerializeField]private Sprite icona;
        [SerializeField]private int id;
        [SerializeField]private string nazev;
        [SerializeField][TextArea(15,20)]private string popis;
        [SerializeField]private type typ;
        [SerializeField]private rarita rarity;

        public Sprite Icona => icona;
        public int Id => id;

        public string Popis => popis;
        public string Nazev => nazev;

        public type Typ => typ;
        
        public rarita Rarity => rarity;
    }
}