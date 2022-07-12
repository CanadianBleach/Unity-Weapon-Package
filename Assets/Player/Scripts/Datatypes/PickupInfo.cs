using TMPro;
using UnityEngine;

public class PickupInfo : MonoBehaviour
{
    [HideInInspector]
    public Gun gun;

    public TextMeshProUGUI gunNameText;
    public TextMeshProUGUI ammoText;

    public void SetInfo(Gun gun)
    {
        Vector3 gunPos = Camera.main.WorldToScreenPoint(gun.gameObject.transform.position + new Vector3(0, .75f, 0));
        gameObject.transform.position = new Vector3(gunPos.x, gunPos.y, 0);

        this.gun = gun;
        gunNameText.text = this.gun.name;
        ammoText.text = this.gun.leftInClip + "/" + this.gun.clipSize;
    }

    private void Update()
    {
        Vector3 gunPos = Camera.main.WorldToScreenPoint(gun.gameObject.transform.position + new Vector3(0, .75f, 0));
        gameObject.transform.position = new Vector3(gunPos.x, gunPos.y, 0);
    }
}
