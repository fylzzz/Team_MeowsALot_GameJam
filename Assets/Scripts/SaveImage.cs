using UnityEngine;

public class SaveImage : MonoBehaviour
{
    public static class ImageConversion
    {
        public static void SaveTextureToFile(Texture2D texture, string filePath)
        {
            byte[] bytes = texture.EncodeToPNG();
            System.IO.File.WriteAllBytes(filePath, bytes);
            Debug.Log($"Saved image to {filePath}");
        }
    }
}
