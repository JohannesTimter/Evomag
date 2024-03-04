using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDrop : MonoBehaviour, IDropHandler
{
    public GameObject Player;

    public GameObject Flosse;
    public GameObject Flagella;
    public GameObject Spike;
    public GameObject Körperteil;
    public GameObject Kiefer;
    public GameObject Filtermund;


    private GameObject connectionPoint;
    private GameObject newObject;

    public AudioClip clickSound;

    public bool hasBodypart = false;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log($"OnDrop {eventData.pointerDrag.gameObject.name} with id {getId()}");
        PlayerController playerController = Player.GetComponent<PlayerController>();

        
        switch (eventData.pointerDrag.gameObject.name)
        {
            case "KieferImage":
                if (playerController.getCurrentEvoPoints() >= 50)
                {
                    GameObject kiefer = instantiateBodyPart(Kiefer);
                    kiefer.GetComponent<KieferController>().player = Player;
                    playerController.ChangeEvolutionPoints(-50);
                }

                break;
            case "FiltermundImage":
                if (playerController.getCurrentEvoPoints() >= 45)
                {
                    GameObject filtermund = instantiateBodyPart(Filtermund);
                    filtermund.GetComponent<FiltermundController>().player = Player;
                    playerController.ChangeEvolutionPoints(-45);
                }

                break;
            case "FlagellaImage":
                if(playerController.getCurrentEvoPoints() >= 20)
                {
                    instantiateBodyPart(Flagella);
                    playerController.ChangeEvolutionPoints(-20);
                    playerController.changeSpeed(1);
                    playerController.changeEnergyDrainRate(1);
                }
                break;

            case "FlosseImage":
                if (playerController.getCurrentEvoPoints() >= 20)
                {
                    instantiateBodyPart(Flosse);
                    playerController.ChangeEvolutionPoints(-20);
                    playerController.changeRotationSpeed(100);
                    playerController.changeEnergyDrainRate(1);
                }
                break;

            case "SpikeImage":
                if (playerController.getCurrentEvoPoints() >= 20)
                {
                    instantiateBodyPart(Spike);
                    playerController.ChangeEvolutionPoints(-20);
                    playerController.changeEnergyDrainRate(1);
                }

                break;
            case "KörperteilImage":
                if (playerController.getCurrentEvoPoints() >= 40)
                {
                    instantiateBodyPart(Körperteil);
                    playerController.ChangeEvolutionPoints(-40);
                    playerController.changeEnergyDrainRate(1);
                    playerController.changeMaxHealt(3);
                    playerController.ChangeHealth(3);
                }

                break;
            default:
                Console.WriteLine("Invalid Object Dropped");
                break;
        }

        //Falls genug EvoPunkte vorhanden: Instantiate Körperteil als Child des ConnectionPoints mit entsprechender Position und Rotation
        //Ziehe die entsprechenden EvoPunkte vom PlayerController ab
        //Füge dem Playercontroller die entsprechenden Stats hinzu (Geschwindigkeit, Kalorienverbrauch, Wendigkeit, HP)
        
    }

    private GameObject instantiateBodyPart(GameObject bodyPart)
    {
        newObject = Instantiate(bodyPart, connectionPoint.transform.position, connectionPoint.transform.rotation);
        newObject.transform.parent = connectionPoint.transform;
        newObject.transform.Rotate(0, 0, getId() * -45);
        hasBodypart = true;

        GetComponent<Image>().gameObject.SetActive(false);

        Player.GetComponent<AudioSource>().PlayOneShot(clickSound);

        return newObject;
    }

    public void setData(GameObject connectionPoint, GameObject player)
    {
        this.Player = player;
        this.connectionPoint = connectionPoint;
    }

    private int getId()
    {
        char lastChar = connectionPoint.gameObject.name[connectionPoint.gameObject.name.Length - 1];
        return lastChar - '0';
    }
}


