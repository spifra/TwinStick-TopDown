using UnityEngine;

public class ExplosibleDeath : MonoBehaviour
{
    public float cubeSize = 0.2f;
    public int cubesInRow = 5;
    public float explosionForce = 50f;
    public float explosionRadius = 4f;
    public float explosionUpward = 0.4f;

    private Vector3 cubesPivot = new Vector3();
    private Material myMaterial;

    private GameObject vfxParent;

    private void Start()
    {
        myMaterial = GetComponentInChildren<MeshRenderer>().material;
        vfxParent = GameObject.Find("VFX");
    }

    public void Explosion()
    {
        gameObject.SetActive(false);

        //loop 3 times to create 5x5x5 pieces in x,y,z coordinates
        for (int x = 0; x < cubesInRow; x++)
        {
            for (int y = 0; y < cubesInRow; y++)
            {
                for (int z = 0; z < cubesInRow; z++)
                {
                    createPiece(x, y, z);
                }
            }
        }

        //get explosion position
        Vector3 explosionPos = transform.position;
        //get colliders in that position and radius
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        //add explosion force to all colliders in that overlap sphere
        foreach (Collider hit in colliders)
        {
            if (hit.CompareTag("CubeExplosion"))
            {
                //get rigidbody from collider object
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    //add explosion force to this body with given parameters
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);
                }
                Destroy(hit.gameObject, 2f);
            }
        }
    }

    void createPiece(int x, int y, int z)
    {
        //create piece
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
        piece.transform.parent = vfxParent.transform;
        piece.tag = "CubeExplosion";

        //set piece position and scale
        piece.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

        //add rigidbody, set mass and material
        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = cubeSize;

        piece.GetComponent<MeshRenderer>().material = myMaterial;

    }

}
