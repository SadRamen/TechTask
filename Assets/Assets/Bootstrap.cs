using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine.Rendering.PostProcessing;

public class Bootstrap : MonoBehaviour
{
    public Image urlImage;
    public TMP_InputField imageURL;
    public Slider urlImageLoadProgress;
    public TMP_Text loadingURLPercentTxt;

    public Image resourceImage;
    public Slider resourceImageLoadProgress;
    public TMP_Text loadingResourceImgPercentTxt;

    private void Start()
    {
        urlImageLoadProgress.value = 0;
        loadingURLPercentTxt.text = "0%";

        resourceImageLoadProgress.value = 0;
        loadingResourceImgPercentTxt.text = "0%";
    }

    public void LoadButton()
    {
        string url = imageURL.text;
        if (!string.IsNullOrEmpty(url))
        {
            LoadAllImagesAsync(url).Forget();
            urlImage.gameObject.SetActive(true);
            imageURL.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Введите URL картинки");
        }
    }

    private async UniTaskVoid LoadAllImagesAsync(string url)
    {
        await LoadImageAsync(url);
        urlImage.gameObject.SetActive(true);

        await LoadResourcesImageAsync("Factory");
        resourceImage.gameObject.SetActive(true);
    }

    private async UniTask LoadImageAsync(string url)
    {
        using (UnityWebRequest webR = UnityWebRequestTexture.GetTexture(url))
        {
            await webR.SendWebRequest();

            while (!webR.isDone)
            {
                urlImageLoadProgress.value = webR.downloadProgress;
                loadingResourceImgPercentTxt.text = $"{(int)webR.downloadProgress * 100}";
                await UniTask.Yield();
            }

            urlImageLoadProgress.value = 1;
            loadingURLPercentTxt.text = "100%";


            if (webR.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Ошибка загрузки изображения: {webR.error}");
                return;
            }

            Texture2D texture = DownloadHandlerTexture.GetContent(webR);

            if (texture != null)
            {
                Sprite sprite = Sprite.Create(texture,new Rect(0, 0, texture.width, texture.height),new Vector2());
                urlImage.sprite = sprite;
            }
        }
    }

    private async UniTask LoadResourcesImageAsync(string resPath)
    {
        ResourceRequest rr = Resources.LoadAsync<Sprite>(resPath);
        while (!rr.isDone)
        {
            resourceImageLoadProgress.value = rr.progress;
            loadingResourceImgPercentTxt.text = $"{(int)rr.progress * 100}";
            await UniTask.Yield();
        }

        resourceImageLoadProgress.value = 1;
        loadingResourceImgPercentTxt.text = "100%";

        Sprite sprite = rr.asset as Sprite;
        if (sprite != null)
        {
            resourceImage.sprite = sprite;
        }
        else
        {
            Debug.LogError($"Ошибка загрузки изображения: {resPath}");
        }
    }
}