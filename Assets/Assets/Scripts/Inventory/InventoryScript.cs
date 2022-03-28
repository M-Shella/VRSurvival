using System;
using UnityEngine;

namespace Assets.Scripts.Inventory {
    public class InventoryScript : MonoBehaviour {

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Item") || other.CompareTag("StackableItem")) {
                String text = other.name;
                int index = text.IndexOf("(");
                if (index >= 0) text = text.Substring(0, index);
                if (other.CompareTag("StackableItem")) {
                    bool changed = false;
                    for (var i = 0; i < transform.GetChild(1).childCount; i++) {
                        if (!transform.GetChild(1).GetChild(i).name.Contains(text)) continue;
                        transform.GetChild(1).GetChild(i).GetComponent<HandleStackable>().ChangeNumberOfItems(1);
                        Destroy(other.gameObject);
                        changed = true;
                        break;
                    }
                    if (changed) return;
                }
                var itemBall = Instantiate(Resources.Load<GameObject>("Prefabs/ItemBall"), transform.position,
                    Quaternion.identity);
                itemBall.GetComponent<Item>().item = Resources.Load<GameObject>("Prefabs/" + text);
                itemBall.transform.SetParent(transform.GetChild(1));
                itemBall.name = "ItemBall(" + text + ")";
                Destroy(other.gameObject);
            }else if (other.CompareTag("ItemBall")) {
                    other.transform.SetParent(transform.GetChild(1));
            }
        }
    }
}
