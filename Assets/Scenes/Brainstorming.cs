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
     *   Start as a baby ("Choose? Hatch your baby!)
     *   Start with one play node ("Babies need playtime, go play!")
     *   When hunger gets below ~80 or removes play node, spawn in a food node ("Life needs food as well as playtime")
     *   After a bit of time, spawn in a resource node ("Welcome to the world little sprout. Hard truth is you need to work for what you get...")
     *   When the player interacts with the resource node, show the UI ("Interact with your XXXX by providing it stimulus")
     *   After the first resource node is depleted, add the TRAIN node option ("Work is life though so train up to work better....")
     *   Then game on.
     * 
     * 
     * 
     *  To implement:
     * 
     * 
     * 
     * Sprite change on age and happiness
     * 
     * ART:
     *  Cursors
     *  Train, Food, Play, Work interaction points
     *  Character, smile, flat and sad, dummy, hair
     *  UI art
     *  
     *  Possible:
     *  Tooltips (nodes, me etc?)
     *  Scale work efficiency and other stats with age
     *  Move to point (clicking on terrain) - Maybe? More strategy if you cant move at all??
     * Spawning manager use Physics2D.OverlapCollider to find a CLEAN spawn point
     * 
     * BALANCING:
     * Life loss by hunger/sadness
     * Movement rate by life stages
     * Life stage commencement
     * Initial lifespan
     * 
     */
}
