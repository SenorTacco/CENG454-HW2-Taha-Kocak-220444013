// SceneSetupTool.cs
// CENG 454 - HW2: Sky-High Prototype II
// Author: Taha Kocak | Student ID: 220444013
// Unity Editor tool - sahneye terrain, pist ve zone objelerini yerlestirmek icin

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class SceneSetupTool : EditorWindow
{
    [MenuItem("CENG454/Setup Threat Corridor Scene")]
    static void SetupScene()
    {
        // -------- TERRAIN --------
        // Terrain datasi olustur ve sahneye ekle
        TerrainData terrainData = new TerrainData();
        terrainData.heightmapResolution = 513;
        terrainData.size = new Vector3(500, 50, 500);

        GameObject terrainObj = Terrain.CreateTerrainGameObject(terrainData);
        terrainObj.name = "MainTerrain";
        terrainObj.transform.position = new Vector3(-250, 0, -250);

        // Terrain datasini Assets klasorune kaydet
        AssetDatabase.CreateAsset(terrainData, "Assets/MainTerrainData.asset");

        // -------- TAKEOFF RUNWAY --------
        // kalkis pisti - uzun duz bir kutu
        GameObject runway = GameObject.CreatePrimitive(PrimitiveType.Cube);
        runway.name = "TakeoffRunway";
        runway.tag = "TakeoffArea";
        runway.transform.position = new Vector3(0, 0.05f, -80);
        runway.transform.localScale = new Vector3(8, 0.1f, 60);

        // Pist uzerine trigger collider ekliyorum - kalkisi algilamak icin
        BoxCollider takeoffTrigger = runway.AddComponent<BoxCollider>();
        takeoffTrigger.isTrigger = true;
        takeoffTrigger.size = new Vector3(1, 20, 1);  // yukari dogru genis alan
        takeoffTrigger.center = new Vector3(0, 10, 0);

        // Pist materyali
        Renderer runwayRend = runway.GetComponent<Renderer>();
        Material runwayMat = new Material(Shader.Find("Standard"));
        runwayMat.color = new Color(0.3f, 0.3f, 0.3f); // koyu gri asfalt
        runwayRend.material = runwayMat;
        AssetDatabase.CreateAsset(runwayMat, "Assets/Materials/RunwayMaterial.mat");

        // -------- LANDING STRIP --------
        // inis pisti - kalkis pistinin karsi tarafinda
        GameObject landing = GameObject.CreatePrimitive(PrimitiveType.Cube);
        landing.name = "LandingStrip";
        landing.tag = "LandingArea";
        landing.transform.position = new Vector3(0, 0.05f, 180);
        landing.transform.localScale = new Vector3(10, 0.1f, 50);

        BoxCollider landingTrigger = landing.AddComponent<BoxCollider>();
        landingTrigger.isTrigger = true;
        landingTrigger.size = new Vector3(1, 15, 1);
        landingTrigger.center = new Vector3(0, 7.5f, 0);

        Renderer landingRend = landing.GetComponent<Renderer>();
        Material landingMat = new Material(Shader.Find("Standard"));
        landingMat.color = new Color(0.2f, 0.4f, 0.2f); // yesil - guvenli inis
        landingRend.material = landingMat;
        AssetDatabase.CreateAsset(landingMat, "Assets/Materials/LandingMaterial.mat");

        // -------- DANGER ZONE --------
        // tehlikeli bolge - kalkis ile inis arasinda buyuk bir trigger volume
        GameObject dangerZone = GameObject.CreatePrimitive(PrimitiveType.Cube);
        dangerZone.name = "DangerZone";
        dangerZone.tag = "DangerZone";
        dangerZone.transform.position = new Vector3(0, 30, 50);
        dangerZone.transform.localScale = new Vector3(80, 60, 80);

        BoxCollider zoneTrigger = dangerZone.GetComponent<BoxCollider>();
        zoneTrigger.isTrigger = true;

        // Yari saydam kirmizi materyal - bolgeyi gosterir
        Renderer zoneRend = dangerZone.GetComponent<Renderer>();
        Material zoneMat = new Material(Shader.Find("Standard"));
        zoneMat.color = new Color(1f, 0f, 0f, 0.15f);
        // Saydam render mode
        zoneMat.SetFloat("_Mode", 3); 
        zoneMat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        zoneMat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        zoneMat.SetInt("_ZWrite", 0);
        zoneMat.DisableKeyword("_ALPHATEST_ON");
        zoneMat.EnableKeyword("_ALPHABLEND_ON");
        zoneMat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        zoneMat.renderQueue = 3000;
        zoneRend.material = zoneMat;
        AssetDatabase.CreateAsset(zoneMat, "Assets/Materials/DangerZoneMaterial.mat");

        // -------- MISSILE LAUNCH POINT --------
        // Fuze firlatma noktasi - danger zone'un icinde yerden
        GameObject launchPoint = new GameObject("MissileLaunchPoint");
        launchPoint.transform.position = new Vector3(0, 0.5f, 50);

        // -------- RESPAWN POINT --------
        // Ucak vurulunca geri donecegi yer
        GameObject respawn = new GameObject("RespawnPoint");
        respawn.transform.position = new Vector3(0, 5, -80);

        Debug.Log("CENG454 HW2: Threat Corridor scene setup tamamlandi!");
    }
}
#endif
