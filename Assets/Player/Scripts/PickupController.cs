using static GunController;
using static CameraRecoilController;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    public float sphereSize;
    public float pickupDistance;
    public PickupInfo infoBox;
    public Material highlightMaterial;
    public LayerMask layerMask;
    private Gun lastHighlightedGun;
    private Material objectMat;

    private void Update()
    {
        CheckForGuns();

        if (lastHighlightedGun != null && Input.GetKeyDown(KeyCode.V))
        {
            gunController.currentGun.Drop();
            gunController.currentGun = lastHighlightedGun;
            lastHighlightedGun.PickUp();
            lastHighlightedGun = null;

            cameraRecoilController.SetRecoil(lastHighlightedGun);
        }
    }

    private void CheckForGuns()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, pickupDistance))
        {
            Collider[] col = Physics.OverlapSphere(hit.point, sphereSize, layerMask);
            
            if (col.Length == 0)
            {
                ClearData();
                return;
            }


            // Find the closest gun
            Gun tempGun = GetNearest(col, hit.point).GetComponent<Gun>();

            //Make sure its not already highlight and actually an object
            if (tempGun == lastHighlightedGun || tempGun == null)
                return;

            // After all the checks are passed swap the objects and highlight
            RemoveHighlight(lastHighlightedGun);
            lastHighlightedGun = tempGun;
            Highlight(tempGun);
        }
        else
        {
            // If nothing is hit clear the highlight data
            ClearData();
        }
    }

    private void ClearData()
    {
        RemoveHighlight(lastHighlightedGun);
        lastHighlightedGun = null;
        infoBox.gameObject.SetActive(false);
    }

    private void RemoveHighlight(Gun gun)
    {
        if (gun == null)
            return;

        MeshRenderer ren = gun.gameObject.GetComponent<MeshRenderer>();
        ren.sharedMaterial = objectMat;
    }

    private void Highlight(Gun gun)
    {
        MeshRenderer ren = gun.gameObject.GetComponent<MeshRenderer>();
        objectMat = ren.sharedMaterial;
        ren.sharedMaterial = highlightMaterial;

        // Update infoBox
        infoBox.SetInfo(gun);
        infoBox.gameObject.SetActive(true);
    }

    // Gets closest gameObject given colliders and a point
    private GameObject GetNearest(Collider[] cols, Vector3 point)
    {
        GameObject nearestGo = cols[0].gameObject;
        float nearestDistance = float.MaxValue;
        float distance;

        foreach (Collider collider in cols)
        {
            distance = (point - collider.gameObject.transform.position).sqrMagnitude;
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestGo = collider.gameObject;
            }
        }
        return nearestGo;
    }
}
