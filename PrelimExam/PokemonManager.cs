using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PokemonManager : MonoBehaviour
{
    //Review Note: Fields that will appear in the inspector
    public TMP_InputField nameInputField;
    public TMP_InputField levelInputField;
    public TMP_InputField flavorTextInputField;
    public TMP_Dropdown elementDropDown;
    public TMP_Dropdown genderDropDown;

    public Button addPokemonButton;
    public Button generateButtonsButton;
    public Transform buttonContainer;
    public GameObject buttonPrefab;
    public GameObject inputFieldPanel;
    public GameObject inventoryPanel;
    public GameObject infoPanel;

    public TextMeshProUGUI pokemonNameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI elementText;
    public TextMeshProUGUI genderText;
    public TextMeshProUGUI flavorText;
    public Button closeInventoryButton;
    public Button closeInfoButton;

    //Initialize the list
    private List<Pokemon> pokemonList = new List<Pokemon>();

    private void Start()
    {
        //add a listener for button functionalities
        addPokemonButton.onClick.AddListener(AddPokemon);
        generateButtonsButton.onClick.AddListener(GeneratePokemonButtons);
        closeInfoButton.onClick.AddListener(() => infoPanel.SetActive(false));
        closeInfoButton.onClick.AddListener(() => inventoryPanel.SetActive(true));
        closeInventoryButton.onClick.AddListener(() => inventoryPanel.SetActive(false));
        closeInventoryButton.onClick.AddListener(() => inputFieldPanel.SetActive(true));

        //add options in the dropbox
        elementDropDown.ClearOptions();
        elementDropDown.AddOptions(new List<string> { "Bug", "Dark", "Dragon", "Electric", "Fairy", "Fighting", "Fire", "Flying", "Grass", "Psychic", "Rock", "Steel", "Water", "Ghost", "Ground", "Ice", "Normal", "Poison", "Unknown" });

        genderDropDown.ClearOptions();
        genderDropDown.AddOptions(new List<string> { "Male", "Female", "Unknown"});


        infoPanel.SetActive(false);
    }

    public void AddPokemon()
    {
        string name = nameInputField.text;
        int level;

        //negative number checker
        if(!int.TryParse(levelInputField.text, out level) || level <= 0)
        {
            Debug.Log("Invalid level.Please enter a positive number.");
            return;
        }

        string element = elementDropDown.options[elementDropDown.value].text;
        string gender = genderDropDown.options[genderDropDown.value].text;
        string flavorText = flavorTextInputField.text;

        Pokemon newPokemon = new Pokemon( name, level, element, gender, flavorText);
        pokemonList.Add(newPokemon);

        Debug.Log($"Added Pokemon: {name}, Level: {level}, Element: {element}, Gender: {gender}, Flavor: {flavorText}");
    }

    public void GeneratePokemonButtons()
    {
        //clear previous buttons
        foreach(Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }

        //generate button for each pokemon
        foreach(Pokemon p in pokemonList)
        {
            GameObject buttonObject = Instantiate(buttonPrefab, buttonContainer);
            PokemonButtons buttonComponent = buttonObject.GetComponent<PokemonButtons>();
            buttonComponent.Initialize(p, this);
        }

        inputFieldPanel.SetActive(false);
        inventoryPanel.SetActive(true);
    }

    public void ShowPokemonInfo(Pokemon pokemon)
    {
        inventoryPanel.SetActive(false);
        infoPanel.SetActive(true);
        pokemonNameText.text = $"{pokemon.name}";
        levelText.text = $"{pokemon.level}";
        elementText.text = $"{pokemon.element}";
        genderText.text = $"{pokemon.gender}";
        flavorText.text = $"{pokemon.flavorText}";
    }   
}
