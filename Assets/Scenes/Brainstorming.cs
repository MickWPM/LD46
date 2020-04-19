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
     * Spawning manager use Physics2D.OverlapCollider to find a CLEAN spawn point
     * Also use range of screen only in "game area"
     * 
     * SOUNDS!
     * 
     * Work credits - piggy bank image?
     * 
     *  *Build UI mechanics (middle cancel, raidal options) for right click
     *  Tooltips (nodes, me etc?) **
     *  Scale work efficiency and other stats with age**
     * 
     * Art pass 2 - Drop shadows, texture etc for character
     *  
     *  Possible:
     *  Egg cracked effect (disappear outwards)
     *  Fountain of youth?!?!
     *  Static bath/bed to wash dirt off/sleep intermittently
     * **** Enemy to dodge (requires click to move) - eg just moving over the screen
     *      Move to point (clicking on terrain) - Maybe? More strategy if you cant move at all??     * 
     * 
     *  UI Buttons 'box'
     * 
     * 
     * 
     * BALANCING:
     * Life loss by hunger/sadness
     * Movement rate by life stages
     * Life stage commencement
     * Initial lifespan
     * 
     * LIFESPAN - 5 minutes (change age to be lifespan LEFT over initial lifespan)
     * 1 minute as a baby (including tutorial stuff) - 0-20%
     * 2-2.5 minutes adult (possibly two phases, just graphically) 20%-65% (45% total)
     * Rest as aged 35%
     * 
     * Happiness is the fastest depleting resource from work/train
     * 
     * 40 seconds to full starvation
     * 1 minute to full sadness
     * 
     * Working will fully deplete food after 30s
     * Working will fully deplete happiness after 40s
     * 
     * Training will fully deplete food after 30s
     * Training will fully deplete happiness after 20s
     * 
     * Playing will fully recover happiness in 20 seconds
     * Eating will fully recover hunger in 10 seconds
     * 
     * Food has 20% per spawn
     * Play has 30% happiness per spawn and gives 
     * 
     * Training increases work rate by 1 every 30s
     * Training has 0.5 units per spawn
     * 
     * Spend ~5-10 seconds at a time working. 1 resource per second
     * Play = 5
     * Food = 7
     * Work = 2
     * Train = 10
     * 
     */
}
