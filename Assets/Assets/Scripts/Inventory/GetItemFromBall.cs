using UnityEngine;

namespace Assets.Scripts.Inventory {
    public class GetItemFromBall : MonoBehaviour {
        private GameObject item;
        private HandleStackable handleStackable;
        private void Start() {
            item = GetComponent<Item>().item;
            handleStackable = GetComponent<HandleStackable>();
        }

        public void SpawnItem() {
            Debug.Log(handleStackable.nuberOfItemsInside);
            if (handleStackable.nuberOfItemsInside > 1) {
                Instantiate(item, transform.position, Quaternion.identity);
                handleStackable.ChangeNumberOfItems(-1);
            }
            else {
                Instantiate(item, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            
        }
    }
}
