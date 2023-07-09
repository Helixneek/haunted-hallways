using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathSetter : MonoBehaviour
{
    private BoxCollider2D box;
    private PlayerMovement movement;
    private PlayerBlueprintViewer blueprint;
    private PlayerTeleporter teleporter;
    private PlayerHealthManagement health;
    private PlayerObjectiveHandler objective;
    private PlayerSFXHandler sfx;
    private PlayerFlickeringFlashlight flashlight;

    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        movement = GetComponent<PlayerMovement>();
        blueprint = GetComponent<PlayerBlueprintViewer>();
        teleporter = GetComponent<PlayerTeleporter>();
        health = GetComponent<PlayerHealthManagement>();
        objective = GetComponent<PlayerObjectiveHandler>();
        sfx = GetComponent<PlayerSFXHandler>();
        flashlight = GetComponent<PlayerFlickeringFlashlight>();
    }

    public void DisableComponents()
    {
        box.enabled = false;
        movement.enabled = false;
        blueprint.enabled = false;
        teleporter.enabled = false;
        health.enabled = false;
        objective.enabled = false;
        sfx.enabled = false;
        flashlight.enabled = false;
    }

}
