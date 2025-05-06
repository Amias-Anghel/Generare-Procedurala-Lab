using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

/* 
    Used to generate a procedural plant.
    EDITOR ONLY
 */
public class ProceduralPlant : MonoBehaviour
{
    public string X_rule;
    public string F_rule;
    public int itterations;
    public float rotateAngle;
    public float forwardLen;
    [SerializeField] private GameObject part_prefab;

    private string shape;

    [ContextMenu("Procedural Generate Plant")]
    public void Generate()
    {
        shape = GeneratePlant();
        PlaceParts(shape);
        
        MergeMesh();

        var mf = GetComponent<MeshFilter>();
        Mesh mesh = mf.sharedMesh;
        #if UNITY_EDITOR
                string path = "Assets/Tema/GeneratedPlants/" + gameObject.name + "_plant.asset";
                Directory.CreateDirectory("Assets/Tema/GeneratedPlants");
                AssetDatabase.CreateAsset(mesh, path);
                AssetDatabase.SaveAssets();
                Debug.Log("Saved combined mesh to " + path);
        #endif

        DestroyImmediate(this);
    }

    private string GeneratePlant() {
        string axion = "X";
        string result = axion;

        for (int i = 0; i < itterations; i++) {
            result = ApplyRules(result);
        }

        return result;
    }

    private string ApplyRules(string initialString) {
        StringBuilder stringBuilder = new StringBuilder();

        foreach(char c in initialString) {
            switch (c) {
                case 'X':
                    stringBuilder.Append(X_rule);
                    break;
                case 'F':
                    stringBuilder.Append(F_rule);
                    break;
                default:
                    stringBuilder.Append(c);
                    break;
            }
        }

        return stringBuilder.ToString();
    }

    private void PlaceParts(string design) {
        float nextRotation = 0;
        Vector3 rootPosition = transform.position;
        Stack<float> rotation_stack = new Stack<float>();
        Stack<Vector3> position_stack = new Stack<Vector3>();

        foreach(char c in design) {
            switch (c) {
                case 'F':
                    PlantPart part = Instantiate(part_prefab, rootPosition, Quaternion.identity).GetComponent<PlantPart>();
                    rootPosition = part.ApplyTransform(forwardLen, nextRotation, transform);
                    DestroyImmediate(part.gameObject);
                    break;
                case '+':
                    nextRotation += rotateAngle;
                    break;
                case '-':
                    nextRotation -= rotateAngle;
                    break;
                case '[':
                    rotation_stack.Push(nextRotation);
                    position_stack.Push(rootPosition);
                    break;
                case ']':
                    rootPosition = position_stack.Pop();
                    nextRotation = rotation_stack.Pop();
                    break;
            }
        }
    }

    private void MergeMesh() {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        var combineList = new List<CombineInstance>();

        foreach (var mf in meshFilters)
        {
            if (mf.transform == transform) 
                continue;

            if (mf.sharedMesh == null) 
                continue;

            CombineInstance ci = new CombineInstance
            {
                mesh      = mf.sharedMesh,
                transform = mf.transform.localToWorldMatrix
            };
            combineList.Add(ci);
            DestroyImmediate(mf.gameObject);
        }

        // Create new combined mesh
        Mesh combinedMesh = new Mesh
        {
            indexFormat = UnityEngine.Rendering.IndexFormat.UInt32
        };
        combinedMesh.CombineMeshes(combineList.ToArray(), true, true);

        var mfSelf = GetComponent<MeshFilter>();
        mfSelf.sharedMesh = combinedMesh;

        var mrSelf = GetComponent<MeshRenderer>();
        var firstChildMR = meshFilters[0].GetComponent<MeshRenderer>();
        if (firstChildMR != null)
            mrSelf.sharedMaterial = firstChildMR.sharedMaterial;

        Debug.Log($"Combined {combineList.Count} meshes into one ({combinedMesh.vertexCount} verts).");
    }

}
