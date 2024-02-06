using UnityEngine;

public class RecipeActivator : MonoBehaviour
{
    [SerializeField] private GameObject RecipeUI;
    private RecipeOpener recipeOpener;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        recipeOpener = player.GetComponent<RecipeOpener>();
        recipeOpener.onEnabledRecipeUI += ActivateRecipeUI;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && RecipeUI.activeSelf)
        {
            RecipeUI.SetActive(false);
        }
    }
    private void OnDisable()
    {
        recipeOpener.onEnabledRecipeUI -= ActivateRecipeUI;
    }
    private void ActivateRecipeUI()
    {
        RecipeUI.SetActive(true);
    }
}
