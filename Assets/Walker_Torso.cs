// https://www.panebianco3d.com/en/20080318-isometric-3d-games-character-animation-test

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker_Torso : MonoBehaviour
{   
  

    float rotY;
    float dirY;
    float deltaY;
 
   public List<GameObject> bodyParts;
   public List<Vector3[]> originalCoordinates;
   public List<Vector3> places; 
   public List<Vector3> sizes;
   public List<Matrix4x4> matrixes;
   public Matrix4x4 chestHierarchy;

  

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


    enum BODY{
        HIP, ABS, CHEST, NECK, HEAD,
        SHOULDERR, ELBOWR, FOREARMR, HANDR,
        SHOULDERL, ELBOWL, FOREARML, HANDL,
    }


    







   public  void bodySetup()
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


        
    }


    public void bodyMovement()
    {
        List<Matrix4x4> matrixes = new List<Matrix4x4>();
          rotY += dirY*deltaY;
          if(rotY < -3 || rotY > 3) dirY = -dirY;

          

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

          chestHierarchy = THips* TAbs* RChest *TChest;


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


          for(int i= 0; i<matrixes.Count; i++){
              bodyParts[i].GetComponent<MeshFilter>().mesh.vertices = ApplyTransform(matrixes[i], originalCoordinates[i]); 
          }   

    

    }
}


