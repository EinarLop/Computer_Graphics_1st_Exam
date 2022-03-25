// https://www.panebianco3d.com/en/20080318-isometric-3d-games-character-animation-test

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{   
    List<GameObject> bodyParts;
    List<Vector3[]> originalCoordinates;
    List<Vector3> places; 
    List<Vector3> sizes;
    

 
    

    float rotY;
    float dirY;
    float deltaY;

    Vector3[] ApplyTransform(Matrix4x4 m, Vector3[] vertices){
        int num = vertices.Length;
        Vector3[] result = new Vector3[num];
        for(int i=0; i<num; i++){
            Vector3 v = vertices[i];
            Vector4 temp = new Vector4(v.x,v.y,v.z,1);
            result[i] = m* temp;
        }
        return result;
    }

    // Orden jerarquico 
    // Todo depende de la cadera
    enum BODY{
        HIP, ABS, CHEST, NECK, HEAD,
        SHOULDERR, ELBOWR, FOREARMR, HANDR,
        SHOULDERL, ELBOWL, FOREARML, HANDL,
    }


    







    void Start()
    {
        bodyParts = new List<GameObject>();
        originalCoordinates = new List<Vector3[]>();
        places = new List<Vector3>();
        sizes = new List<Vector3>();

        rotY = 0;
        dirY = 1;
        deltaY =0.03f;

      
        

        bodyParts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
        bodyParts[(int)BODY.HIP].GetComponent<Renderer>().material.color= new Color(0.36f,0.36f,0.36f);
        originalCoordinates.Add(bodyParts[(int)BODY.HIP].GetComponent<MeshFilter>().mesh.vertices);
        places.Add(new Vector3(0, 0, 0));
        sizes.Add(new Vector3(1, 0.4f, 1));



        bodyParts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
        originalCoordinates.Add(bodyParts[(int)BODY.ABS].GetComponent<MeshFilter>().mesh.vertices);
        sizes.Add(new Vector3(0.8f,0.5f,0.6f));
        places.Add(new Vector3(
        0,
        //   prior/2             +      current/2
        sizes[(int)BODY.HIP].y/2 + sizes[(int)BODY.ABS].y/2, 
        0));

        bodyParts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
        bodyParts[(int)BODY.CHEST].GetComponent<Renderer>().material.color= new Color(1,0,0);
        originalCoordinates.Add(bodyParts[(int)BODY.CHEST].GetComponent<MeshFilter>().mesh.vertices);
        sizes.Add(new Vector3(1,0.5f,0.8f));
        places.Add(new Vector3(
        0,
        sizes[(int)BODY.ABS].y/2 + sizes[(int)BODY.CHEST].y/2,
        0));


        
        bodyParts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
        originalCoordinates.Add(bodyParts[(int)BODY.NECK].GetComponent<MeshFilter>().mesh.vertices);
        // En relación al padre
        sizes.Add(new Vector3(0.2f, 0.2f, 0.2f));
        places.Add(new Vector3(
        0,
        sizes[(int)BODY.CHEST].y/2 + sizes[(int)BODY.NECK].y/2,
        0)); 
        
        bodyParts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
        bodyParts[(int)BODY.HEAD].GetComponent<Renderer>().material.color= new Color(0,0,1);
        
        originalCoordinates.Add(bodyParts[(int)BODY.HEAD].GetComponent<MeshFilter>().mesh.vertices);
        // En relación al padre
        sizes.Add(new Vector3(0.5f, 0.5f, 0.5f));
        places.Add(new Vector3(
        0,
        sizes[(int)BODY.NECK].y/2 + sizes[(int)BODY.HEAD].y/2,
        0));
        

        //////////////////////////ARM RIGHT////////////////////////

        bodyParts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
        bodyParts[(int)BODY.SHOULDERR].GetComponent<Renderer>().material.color= new Color(255,0,0);
        
        originalCoordinates.Add(bodyParts[(int)BODY.SHOULDERR].GetComponent<MeshFilter>().mesh.vertices);
        sizes.Add(new Vector3(0.4f, 0.4f, 0.4f));
        places.Add(new Vector3(
        sizes[(int)BODY.CHEST].x/2 + sizes[(int)BODY.SHOULDERR].x/2,
        // Preguntar
        sizes[(int)BODY.HIP].y/2 + sizes[(int)BODY.ABS].y + sizes[(int)BODY.CHEST].y/2,

        0));

        bodyParts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
        originalCoordinates.Add(bodyParts[(int)BODY.ELBOWR].GetComponent<MeshFilter>().mesh.vertices);
        sizes.Add(new Vector3(0.3f, 0.3f, 0.3f));
        places.Add(new Vector3(
        0,
      
        (-1)*sizes[(int)BODY.SHOULDERR].y/2-sizes[(int)BODY.ELBOWR].y/2,

        0));

        bodyParts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
        bodyParts[(int)BODY.FOREARMR].GetComponent<Renderer>().material.color= new Color(255,0,0);
        originalCoordinates.Add(bodyParts[(int)BODY.FOREARMR].GetComponent<MeshFilter>().mesh.vertices);
        sizes.Add(new Vector3(0.4f, 0.5f, 0.4f));
        places.Add(new Vector3(
        0,
      
        (-1)*sizes[(int)BODY.ELBOWR].y/2-sizes[(int)BODY.FOREARMR].y/2,

        0));

        bodyParts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
        bodyParts[(int)BODY.HANDR].GetComponent<Renderer>().material.color= new Color(0,0,255);
        originalCoordinates.Add(bodyParts[(int)BODY.HANDR].GetComponent<MeshFilter>().mesh.vertices);
        sizes.Add(new Vector3(0.25f, 0.25f, 0.25f));
        places.Add(new Vector3(
        0,
      
        (-1)*sizes[(int)BODY.FOREARMR].y/2-sizes[(int)BODY.HANDR].y/2,

        0));

        //////////////////////////ARM RIGHT////////////////////////

        //////////////////////////ARM LEFT////////////////////////
        
        bodyParts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
        bodyParts[(int)BODY.SHOULDERL].GetComponent<Renderer>().material.color= new Color(255,0,0);
        originalCoordinates.Add(bodyParts[(int)BODY.SHOULDERL].GetComponent<MeshFilter>().mesh.vertices);
        sizes.Add(new Vector3(0.4f, 0.4f, 0.4f));
        places.Add(new Vector3(
        (-1)*sizes[(int)BODY.CHEST].x/2 - sizes[(int)BODY.SHOULDERL].x/2,
        // Preguntar
        sizes[(int)BODY.HIP].y/2 + sizes[(int)BODY.ABS].y + sizes[(int)BODY.CHEST].y/2,

        0));

        bodyParts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
        originalCoordinates.Add(bodyParts[(int)BODY.ELBOWL].GetComponent<MeshFilter>().mesh.vertices);
        sizes.Add(new Vector3(0.3f, 0.3f, 0.3f));
        places.Add(new Vector3(
        0,
      
        (-1)*sizes[(int)BODY.SHOULDERL].y/2-sizes[(int)BODY.ELBOWL].y/2,

        0));

        bodyParts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
        bodyParts[(int)BODY.FOREARML].GetComponent<Renderer>().material.color= new Color(255,0,0);
        
        originalCoordinates.Add(bodyParts[(int)BODY.FOREARML].GetComponent<MeshFilter>().mesh.vertices);
        sizes.Add(new Vector3(0.4f, 0.5f, 0.4f));
        places.Add(new Vector3(
        0,
      
        (-1)*sizes[(int)BODY.ELBOWL].y/2-sizes[(int)BODY.FOREARML].y/2,

        0));

        bodyParts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
        bodyParts[(int)BODY.HANDL].GetComponent<Renderer>().material.color= new Color(0,0,255);
        
        originalCoordinates.Add(bodyParts[(int)BODY.HANDL].GetComponent<MeshFilter>().mesh.vertices);
        sizes.Add(new Vector3(0.25f, 0.25f, 0.25f));
        places.Add(new Vector3(
        0,
      
        (-1)*sizes[(int)BODY.FOREARML].y/2-sizes[(int)BODY.HANDL].y/2,

        0));

        //////////////////////////ARM LEFT////////////////////////

    }


    void Update()
    {
          rotY += dirY*deltaY;
          if(rotY < -3 || rotY > 3) dirY = -dirY;

          List<Matrix4x4> matrixes = new List<Matrix4x4>();

          Matrix4x4 THips = Transformations.TranslateM(
          places[(int)BODY.HIP].x, 
          places[(int)BODY.HIP].y, 
          places[(int)BODY.HIP].z);

          Matrix4x4 SHips = Transformations.ScaleM(
          sizes[(int)BODY.HIP].x, 
          sizes[(int)BODY.HIP].y, 
          sizes[(int)BODY.HIP].z);

          matrixes.Add(THips*SHips);


          Matrix4x4 TAbs = Transformations.TranslateM(
          places[(int)BODY.ABS].x, 
          places[(int)BODY.ABS].y, 
          places[(int)BODY.ABS].z);

          Matrix4x4 SAbs = Transformations.ScaleM(
          sizes[(int)BODY.ABS].x, 
          sizes[(int)BODY.ABS].y, 
          sizes[(int)BODY.ABS].z);

          matrixes.Add(THips* TAbs* SAbs);

          Matrix4x4 RChest = Transformations.RotateM(rotY, Transformations.AXIS.AX_Y);
          Matrix4x4 TChest = Transformations.TranslateM(
          places[(int)BODY.CHEST].x, 
          places[(int)BODY.CHEST].y, 
          places[(int)BODY.CHEST].z);

          Matrix4x4 SChest = Transformations.ScaleM(
          sizes[(int)BODY.CHEST].x, 
          sizes[(int)BODY.CHEST].y, 
          sizes[(int)BODY.CHEST].z);

          matrixes.Add(THips* TAbs* RChest *TChest *SChest);


          Matrix4x4 TNeck = Transformations.TranslateM(
          places[(int)BODY.NECK].x, 
          places[(int)BODY.NECK].y, 
          places[(int)BODY.NECK].z);

          Matrix4x4 SNeck = Transformations.ScaleM(
          sizes[(int)BODY.NECK].x, 
          sizes[(int)BODY.NECK].y, 
          sizes[(int)BODY.NECK].z);

          matrixes.Add(THips* TAbs*RChest*TChest*TNeck*SNeck);

          Matrix4x4 THead = Transformations.TranslateM(
          places[(int)BODY.HEAD].x, 
          places[(int)BODY.HEAD].y, 
          places[(int)BODY.HEAD].z);

          Matrix4x4 SHead = Transformations.ScaleM(
          sizes[(int)BODY.HEAD].x, 
          sizes[(int)BODY.HEAD].y, 
          sizes[(int)BODY.HEAD].z);

          matrixes.Add(THips* TAbs*RChest*TChest*TNeck*THead*SHead);


          //////////////////////////ARM RIGHT////////////////////////

        
        

          Matrix4x4 TSHOULDERR = Transformations.TranslateM(
          places[(int)BODY.SHOULDERR].x, 
          places[(int)BODY.SHOULDERR].y, 
          places[(int)BODY.SHOULDERR].z);

          Matrix4x4 SSHOULDERR = Transformations.ScaleM(
          sizes[(int)BODY.SHOULDERR].x, 
          sizes[(int)BODY.SHOULDERR].y, 
          sizes[(int)BODY.SHOULDERR].z);

          Matrix4x4 RSHOULDERR = Transformations.RotateM(rotY, Transformations.AXIS.AX_X);

          matrixes.Add(RSHOULDERR* TSHOULDERR*SSHOULDERR);


          Matrix4x4 TELBOWR = Transformations.TranslateM(
          places[(int)BODY.ELBOWR].x, 
          places[(int)BODY.ELBOWR].y, 
          places[(int)BODY.ELBOWR].z);

          Matrix4x4 SELBOWR = Transformations.ScaleM(
          sizes[(int)BODY.ELBOWR].x, 
          sizes[(int)BODY.ELBOWR].y, 
          sizes[(int)BODY.ELBOWR].z);

          matrixes.Add(RSHOULDERR*TSHOULDERR*TELBOWR*SELBOWR);

          Matrix4x4 TFOREARMR = Transformations.TranslateM(
          places[(int)BODY.FOREARMR].x, 
          places[(int)BODY.FOREARMR].y, 
          places[(int)BODY.FOREARMR].z);

          Matrix4x4 SFOREARMR = Transformations.ScaleM(
          sizes[(int)BODY.FOREARMR].x, 
          sizes[(int)BODY.FOREARMR].y, 
          sizes[(int)BODY.FOREARMR].z);

          Matrix4x4 R2FOREARMR = Transformations.RotateM(15, Transformations.AXIS.AX_X);


          matrixes.Add(RSHOULDERR*TSHOULDERR*TELBOWR*R2FOREARMR*TFOREARMR*SFOREARMR);

          Matrix4x4 THANDR = Transformations.TranslateM(
          places[(int)BODY.HANDR].x, 
          places[(int)BODY.HANDR].y, 
          places[(int)BODY.HANDR].z);

          Matrix4x4 SHANDR = Transformations.ScaleM(
          sizes[(int)BODY.HANDR].x, 
          sizes[(int)BODY.HANDR].y, 
          sizes[(int)BODY.HANDR].z);

          matrixes.Add(RSHOULDERR*TSHOULDERR*TELBOWR*R2FOREARMR*TFOREARMR*THANDR*SHANDR);
          

        //////////////////////////ARM RIGHT////////////////////////

        //////////////////////////ARM LEFT////////////////////////


        Matrix4x4 TSHOULDERL = Transformations.TranslateM(
          places[(int)BODY.SHOULDERL].x, 
          places[(int)BODY.SHOULDERL].y, 
          places[(int)BODY.SHOULDERL].z);

          Matrix4x4 SSHOULDERL = Transformations.ScaleM(
          sizes[(int)BODY.SHOULDERL].x, 
          sizes[(int)BODY.SHOULDERL].y, 
          sizes[(int)BODY.SHOULDERL].z);

          Matrix4x4 RSHOULDERL = Transformations.RotateM((-1)*rotY, Transformations.AXIS.AX_X);

          matrixes.Add(RSHOULDERL* TSHOULDERL*SSHOULDERL);


          Matrix4x4 TELBOWL = Transformations.TranslateM(
          places[(int)BODY.ELBOWL].x, 
          places[(int)BODY.ELBOWL].y, 
          places[(int)BODY.ELBOWL].z);

          Matrix4x4 SELBOWL = Transformations.ScaleM(
          sizes[(int)BODY.ELBOWL].x, 
          sizes[(int)BODY.ELBOWL].y, 
          sizes[(int)BODY.ELBOWL].z);

          matrixes.Add(RSHOULDERL*TSHOULDERL*TELBOWL*SELBOWL);

          Matrix4x4 TFOREARML = Transformations.TranslateM(
          places[(int)BODY.FOREARML].x, 
          places[(int)BODY.FOREARML].y, 
          places[(int)BODY.FOREARML].z);

          Matrix4x4 SFOREARML = Transformations.ScaleM(
          sizes[(int)BODY.FOREARML].x, 
          sizes[(int)BODY.FOREARML].y, 
          sizes[(int)BODY.FOREARML].z);

          Matrix4x4 R2FOREARML = Transformations.RotateM(15, Transformations.AXIS.AX_X);

          matrixes.Add(RSHOULDERL*TSHOULDERL*TELBOWL*R2FOREARML*TFOREARML*SFOREARML);

          Matrix4x4 THANDL = Transformations.TranslateM(
          places[(int)BODY.HANDL].x, 
          places[(int)BODY.HANDL].y, 
          places[(int)BODY.HANDL].z);

          Matrix4x4 SHANDL = Transformations.ScaleM(
          sizes[(int)BODY.HANDL].x, 
          sizes[(int)BODY.HANDL].y, 
          sizes[(int)BODY.HANDL].z);

          matrixes.Add(RSHOULDERL*TSHOULDERL*TELBOWL*R2FOREARML*TFOREARML*THANDL*SHANDL);


        //////////////////////////ARM LEFT////////////////////////

          for(int i= 0; i<matrixes.Count; i++){
              bodyParts[i].GetComponent<MeshFilter>().mesh.vertices = ApplyTransform(matrixes[i], originalCoordinates[i]); 
          }     
     
    }
}


 
//     // Start is called before the first frame update
//     void Start()
//     {
//         parts = new List<GameObject>();
//         originals = new List<Vector3[]>();
//         places = new List<Vector3>();
//         sizes = new List<Vector3>();
 
//         parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
//         originals.Add(parts[(int)PARTES.HIPS].GetComponent<MeshFilter>().mesh.vertices);
//         places.Add(new Vector3(0, 0, 0));
//         sizes.Add(new Vector3(1, 0.2f, 0.6f));
 
//         parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
//         originals.Add(parts[(int)PARTES.ABDOMEN].GetComponent<MeshFilter>().mesh.vertices);
//         places.Add(new Vector3(0, 0.1f + 0.25f, 0));
//         sizes.Add(new Vector3(0.8f, 0.5f, 0.6f));
 
//         parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
//         originals.Add(parts[(int)PARTES.CHEST].GetComponent<MeshFilter>().mesh.vertices);
//         places.Add(new Vector3(0, 0.25f + 0.25f, 0));
//         sizes.Add(new Vector3(1, 0.5f, 0.8f));
 
//         parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
//         originals.Add(parts[(int)PARTES.NECK].GetComponent<MeshFilter>().mesh.vertices);
//         places.Add(new Vector3(0, 0.25f + 0.1f, 0));
//         sizes.Add(new Vector3(0.2f, 0.2f, 0.2f));
 
//         parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
//         originals.Add(parts[(int)PARTES.HEAD].GetComponent<MeshFilter>().mesh.vertices);
//         places.Add(new Vector3(0, 0.1f + 0.25f, 0));
//         sizes.Add(new Vector3(0.5f, 0.5f, 0.5f));
 
//     }
 
//     // Update is called once per frame
//     void Update()
//     {
//         List<Matrix4x4> matrices = new List<Matrix4x4>();
 
//         Matrix4x4 tHips = Transformations.TranslateM(places[(int)PARTES.HIPS].x, places[(int)PARTES.HIPS].y, places[(int)PARTES.HIPS].z);
//         Matrix4x4 sHips = Transformations.ScaleM(sizes[(int)PARTES.HIPS].x, sizes[(int)PARTES.HIPS].y, sizes[(int)PARTES.HIPS].z);
//         matrices.Add(tHips * sHips);
 
//         Matrix4x4 tAbs = Transformations.TranslateM(places[(int)PARTES.ABDOMEN].x, places[(int)PARTES.ABDOMEN].y, places[(int)PARTES.ABDOMEN].z);
//         Matrix4x4 sAbs = Transformations.ScaleM(sizes[(int)PARTES.ABDOMEN].x, sizes[(int)PARTES.ABDOMEN].y, sizes[(int)PARTES.ABDOMEN].z);
//         matrices.Add(tHips * tAbs * sAbs);
 
//         Matrix4x4 tChest = Transformations.TranslateM(places[(int)PARTES.CHEST].x, places[(int)PARTES.CHEST].y, places[(int)PARTES.CHEST].z);
//         Matrix4x4 sChest = Transformations.ScaleM(sizes[(int)PARTES.CHEST].x, sizes[(int)PARTES.CHEST].y, sizes[(int)PARTES.CHEST].z);
//         matrices.Add(tHips * tAbs * tChest * sChest);
 
//         Matrix4x4 tNeck = Transformations.TranslateM(places[(int)PARTES.NECK].x, places[(int)PARTES.NECK].y, places[(int)PARTES.NECK].z);
//         Matrix4x4 sNeck = Transformations.ScaleM(sizes[(int)PARTES.NECK].x, sizes[(int)PARTES.NECK].y, sizes[(int)PARTES.NECK].z);
//         matrices.Add(tHips * tAbs * tChest * tNeck * sNeck);
 
//         Matrix4x4 tHead = Transformations.TranslateM(places[(int)PARTES.HEAD].x, places[(int)PARTES.HEAD].y, places[(int)PARTES.HEAD].z);
//         Matrix4x4 sHead = Transformations.ScaleM(sizes[(int)PARTES.HEAD].x, sizes[(int)PARTES.HEAD].y, sizes[(int)PARTES.HEAD].z);
//         matrices.Add(tHips * tAbs * tChest * tNeck * tHead * sHead);
 
//         for(int i = 0; i < matrices.Count; i++)
//         {
//             parts[i].GetComponent<MeshFilter>().mesh.vertices = ApplyTransform(matrices[i], originals[i]);
//         }
//     }
// }