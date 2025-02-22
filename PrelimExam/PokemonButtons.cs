using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PokemonButtons : MonoBehaviour
{

    private Pokemon pokemon;
    private PokemonManager pokemonManager;

    public void Initialize(Pokemon pokemonData, PokemonManager manager)
    {
        pokemon = pokemonData;
        pokemonManager = manager;

        GetComponentInChildren<TextMeshProUGUI>().text = $"{pokemon.name}";
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        pokemonManager.ShowPokemonInfo(pokemon);    
    }
}
