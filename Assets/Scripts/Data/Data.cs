using UnityEngine;
using TMPro;
using System.Collections;

public class Data : MonoBehaviour
{
    [Header("UI Elements")]
        public Canvas canvas;
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI dynamicPointsText;

    public static Data instance;

    public int score;

    //Awake es llamado cuando la instancia del script se está cargando
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }




    private void DataUpdate()
    {
        if(scoreText != null)
            scoreText.text = "Score: " + score.ToString();
    }


    public void AddScore(int points)
    {
        score += points;
        DataUpdate();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (dynamicPointsText != null)
            dynamicPointsText.gameObject.SetActive(false);

        DataUpdate();

    }

    // Mostrar puntos dinámicos en la posición del PowerUp
    public void ShowDynamicPoints(int points, Vector3 positionPU)
    {
        if (dynamicPointsText != null)
        {
            dynamicPointsText.text = "+" + points.ToString();

            // Convertir la posición del PowerUp en el mundo a la posición en pantalla
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(positionPU + Vector3.up * 0.8f); // Ajustar un poco hacia arriba
            dynamicPointsText.transform.position = screenPosition;

            //Si el objeto esta detras camara, no mostrar el texto
            if (screenPosition.z < 0)
            {
                dynamicPointsText.gameObject.SetActive(false);
            }

            // Ajustar la posición del texto dinámico en el canvas
            RectTransform rectTransform = dynamicPointsText.rectTransform;
            if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
            {
                rectTransform.position = screenPosition;
            }
            else
            {
                // Convertir la posición de pantalla a posición local del canvas
                RectTransform canvasRT = canvas.transform as RectTransform;
                Vector2 localPoint;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRT, screenPosition, canvas.worldCamera, out localPoint);
                rectTransform.anchoredPosition = localPoint;

            }

            dynamicPointsText.gameObject.SetActive(true);

            StartCoroutine(PointsAnimation(rectTransform));
        }
    }
    

    private IEnumerator PointsAnimation(RectTransform rectTransform)
    {
        float duration = 1.0f; // Duración de la animación
        float t = 0f;
        Vector2 startPos = rectTransform.anchoredPosition;
        //Vector3 endPos = startPos + new Vector3(0, 50, 0); // Mover hacia arriba 50 unidades

        while (t < duration)
        {
            t += Time.deltaTime;
            rectTransform.anchoredPosition = startPos + new Vector2(0, t*40f); // Mover hacia arriba con el tiempo
            yield return null;
        }

        dynamicPointsText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
