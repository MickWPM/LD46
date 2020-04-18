using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brainstorming : MonoBehaviour
{
    /*
     * KEEP IT ALIVE
     * Principles:
     * Minimal art requirement - Simple art only
     * Full gameplay experience in ~5 minutes
     * Fun in the first minute
     * 
     * 
     * Tamagochi style game
     *   Top down 2D
     *   No animations (for now)
     *   
     *   Feed (hunger++)
     *   Work (hunger--, happiness--, money++)
     *   Train (Work efficiency+, hunger--, Happiness-)
     *   Recretaion, play with etc (Happiness++)
     *   
     *   
     *   Game loop:
     *   Start as a baby
     *   Start with one play node
     *   When hunger gets below ~80 or removes play node, spawn in a food node
     *   After a bit of time, spawn in a resource node
     *   When the player interacts with the resource node, show the UI
     *   After the first resource node is depleted, add the TRAIN node option
     *   Then game on.
     * 
     * 
     * 
     *  To implement:
     *  Add node effects (hunger/happiness changes - store as a per second value in the node, call the event)
     *  Change movement speeds based on age (other stats?)
     *  Pat (Major happiness increase as baby, happiness decrease as an adult, minor happiness increase in old age)
     *  
     *  Possible:
     *  Scale work efficiency with age
     *  Move to point (clicking on terrain) - Maybe? More strategy if you cant move at all??
     * 
     */
}
