using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Player player;
    private List<Ability> Abilities;
    void Start()
    {
        Abilities = Player.Abilities;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < Abilities.Count; i++){
            Abilities[i].currentCooldown -= Time.deltaTime;
            if(Abilities[i].currentCooldown < 0){
                Abilities[i].currentCooldown = 0;
            }
        }
    }

    public void putOnCooldown(Ability ability){
        if(!Abilities.Contains(ability)){
            return;
        }
        else {
            ability.currentCooldown = ability.getCooldown();
        }
    }
}
