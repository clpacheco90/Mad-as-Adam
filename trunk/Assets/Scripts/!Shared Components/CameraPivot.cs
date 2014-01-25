using UnityEngine;
using System.Collections;

[System.Serializable]
public static class CameraPivot {

    public static Vector3 Left() {
        return Camera.main.ScreenToWorldPoint(Vector3.zero);
    }

    public static Vector3 Right() {
        return Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.transform.position.x + Screen.width, Camera.main.transform.position.y + Screen.height, 0));
    }

    public static Vector3 Center() {
        return Camera.main.ScreenToWorldPoint(new Vector3((Camera.main.transform.position.x + Screen.width) * .5f, (Camera.main.transform.position.y + Screen.height) * .5f, 0));
    }

}
